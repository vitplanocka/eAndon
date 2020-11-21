Imports System.IO

' TODO:  
' - create variables for positioning of nameLabels and workstationLabels
' - add grid view / lay-out view option
' - make creation of Terminals dynamic
' - instruction box - add colours for Green, Yellow, Red
' - add info field?
' - add translations to UI (Legend)
' - Adjust height of form dynamically - remove constants


Public Class Andon

    Public nOfLines As Integer = 0                                            ' Number of displayed lines
    Public alarmTypes As Integer = 0                                          ' Number of displayed alarm types (columns)
    Public workstationLabels(nOfLines - 1, 3) As String                       ' #, line number, line name for displayed lines
    Public nOfPreviousAlarms As Integer                                       ' Number of previous displayed alarms 
    Public nOfAlarms As Integer                                               ' Number of displayed alarms
    Public workstationStatus(nOfLines - 1, alarmTypes - 1) As String          ' Current status of alarms for all lines
    Public previousworkstationStatus(nOfLines - 1, alarmTypes - 1) As String  ' Previous status of alarms for all lines
    Public alarmStartTime(nOfLines - 1, alarmTypes - 1) As Date               ' Date of last start of yellow or red alarm
    Public oldFile As Boolean                                                 ' Is the file change date too old to display in dashboard?
    Public maxDelay As Integer = 60                                           ' Text files terminal0*.txt older than x minutes are ignored
    Public soundOn As Boolean = False                                         ' Do alarms play a sound?
    Public priorityLines(nOfLines) As Integer                                 ' Array of lines that are highlighted as priority
    Public alarmfile As String                                                ' Alarm sound filename
    Public iconLbl(alarmTypes) As String                                      ' Labels for alarm types
    Public iconImgFile(alarmTypes) As String                                  ' Image filenames for alarm types 
    Public greenName, yellowName, redName As String                           ' Names for the green, yellow and red status
    Public displayedLines(100) As Integer                                     ' Numbers or lines from production_lines.txt displayed on this form
    Public alarmTypesString(4) As String                                      ' Labels for the Alarm_type form

    ReadOnly AlOverview As New AlarmOverview                                  ' Initialize the AlarmOverview form

    Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Things to do when app starts

        ' Maximize the window
        ' Me.WindowState = FormWindowState.Maximized

        'Check Directory is available or not.
        If (Directory.Exists(Application.StartupPath & "/Data") = False) Then
            Directory.CreateDirectory(Application.StartupPath & "/Data")
        End If
        ' Set watcher path to current folder
        watcher2.Path = Application.StartupPath & "/Data"

        ' Read Settings from Text File
        Dim rootpath As String = Application.StartupPath
        Using MyReader As New FileIO.TextFieldParser("Assets/settings.txt")
            ' Number of alarm types to display
            Dim lineReader() As String = MyReader.ReadLine().Split(":")
            alarmTypes = CInt(lineReader(1).ToString().Trim().TrimStart())
            ' Names for the green, yellow and red status
            lineReader = MyReader.ReadLine().Split(":")
            greenName = lineReader(1).ToString().Trim().TrimStart()
            lineReader = MyReader.ReadLine().Split(":")
            yellowName = lineReader(1).ToString().Trim().TrimStart()
            lineReader = MyReader.ReadLine().Split(":")
            redName = lineReader(1).ToString().Trim().TrimStart()
            ' Image file for company logo
            lineReader = MyReader.ReadLine().Split(":")
            PictureBoxLogo.ImageLocation = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
            ' Alarm sound file
            lineReader = MyReader.ReadLine().Split(":")
            alarmfile = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
            ' Terminal usage instruction
            MyReader.ReadLine()
            ' Labels for the Alarm_type form
            For i = 0 To 3
                lineReader = MyReader.ReadLine().Split(":")
                alarmTypesString(i) = lineReader(1).ToString().Trim().TrimStart()
            Next
            ' Alarm type descriptions and icons
            ReDim iconLbl(alarmTypes)
            ReDim iconImgFile(alarmTypes)
            For i = 0 To alarmTypes - 1
                lineReader = MyReader.ReadLine().Split(":")
                iconLbl(i) = lineReader(1).ToString().Trim().TrimStart()
                lineReader = MyReader.ReadLine().Split(":")
                iconImgFile(i) = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
            Next
        End Using

        ' Load the line labels
        Using MyReader As New FileIO.TextFieldParser("Assets/Workstations_terminals.txt")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")
            nOfLines = MyReader.ReadLine
            ReDim workstationLabels(nOfLines - 1, 3)
            MyReader.ReadLine()  ' Skip format legend
            workstationStatus = New String(nOfLines - 1, alarmTypes - 1) {}
            previousworkstationStatus = New String(nOfLines - 1, alarmTypes - 1) {}
            Dim i As Integer = 0

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    ' Read row as an array of strings '
                    currentRow = MyReader.ReadFields()
                    ' Parse line into strings '
                    workstationLabels(i, 0) = currentRow(0)
                    workstationLabels(i, 1) = currentRow(1)
                    workstationLabels(i, 2) = currentRow(2)
                    workstationLabels(i, 3) = currentRow(3)
                    displayedLines(i) = CInt(currentRow(0))
                    i += 1
                Catch ex As FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        'ReDim all arrays that depend on nOfLines, alarmTypes
        ReDim alarmStartTime(nOfLines - 1, alarmTypes - 1)
        ReDim priorityLines(nOfLines - 1)

        ' Create dynamic alarm labels
        Dim newbox As Label
        Dim y As Int32 = 0
        Dim lineNo As Int32 = 1
        For i As Integer = 0 To nOfLines * alarmTypes - 1 'Create labels and set properties
            'set Y
            If (i / lineNo) = alarmTypes Then
                y += 25
                lineNo += 1
            End If

            newbox = New Label With {
                .Size = New Size(40, 20),
                .Location = New Point(270 + (i Mod alarmTypes) * 53, 55 + y),
                .Font = New Font("Arial", 8, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleCenter
            }
            ' Draw alarm labels
            newbox.Name = "lineLabel" & i
            newbox.Text = ""
            newbox.BorderStyle = BorderStyle.FixedSingle
            newbox.BackColor = Color.White
            Controls.Add(newbox)
        Next

        ' Create dynamic alarm column labels
        Dim newbox2 As PictureBox
        For i As Integer = 0 To alarmTypes - 1 'Create labels and set properties
            Dim img = Image.FromFile(iconImgFile(i))
            newbox2 = New PictureBox With {
                .Size = New Drawing.Size(img.Size.Width * 0.7, img.Size.Height * 0.7),
                .Location = New Point(270 + i * 53, 0),
                .ImageLocation = iconImgFile(i),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            ' Draw alarm labels
            newbox2.Name = "alarmLabel" & i
            newbox2.BorderStyle = BorderStyle.None
            Controls.Add(newbox2)
        Next

        ' Adjust height of form dynamically
        Me.Size = New Size(Me.Width, 25 + nOfLines * 30)

        ' Create Priority Line Labels
        For i As Integer = 0 To nOfLines - 1 'Create labels and set properties
            newbox = New Label With {
                .Size = New Size(240 + (alarmTypes * 54), 29),
                .Location = New Point(20, 50 + (i * 25) - 2 * Math.Ceiling(i / 1000)),
                .Font = New Font("Arial", 14),
                .TextAlign = ContentAlignment.MiddleLeft
            }
            newbox.Name = "prioLabel" & i
            newbox.Text = workstationLabels(i, 1) & "          " & workstationLabels(i, 2)
            newbox.BorderStyle = BorderStyle.None
            newbox.BackColor = Color.White
            newbox.SendToBack()
            AddHandler newbox.MouseEnter, AddressOf Label_MouseEnter
            AddHandler newbox.MouseLeave, AddressOf Label_MouseLeave
            AddHandler newbox.Click, AddressOf LineLabelClicked
            Controls.Add(newbox)
        Next

        ' Set tooltips for line nubmers
        Dim toolTip1 As New ToolTip()
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Controls("prioLabel" & i), Label)
            toolTip1.SetToolTip(myLabel, workstationLabels(i, 2))
        Next

        ' Set everything to green
        For i = 0 To nOfLines - 1
            For j = 0 To alarmTypes - 1
                workstationStatus(i, j) = greenName
                previousworkstationStatus(i, j) = greenName
            Next
        Next

        ' Ignore old files. If it's a current file, trigger update of alert fields
        Dim di As New DirectoryInfo("Data/")
        ' Get a reference to each file in that directory.
        Dim fiArr As FileInfo() = di.GetFiles()
        ' Display the names of the files
        Dim fri As FileInfo
        For Each fri In fiArr
            oldFile = DateDiff(DateInterval.Minute, Convert.ToDateTime(File.GetLastWriteTime("Data/" & fri.ToString)), DateTime.Now) > maxDelay
            If Not oldFile Then
                ' Raise a new FileSystemEventArgs event with the changed file
                Dim fileSEA As New FileSystemEventArgs(WatcherChangeTypes.Changed, fri.DirectoryName, fri.Name)
                UpdateFields(sender, fileSEA)
            End If
        Next fri

        If soundOn = True Then LabelSound.Text = "Sound is on" Else LabelSound.Text = "Sound is off"
        PictureBoxLogo.Select()  ' Set focus on logo to prevent selecting text of TextBoxes

        ' Update line visualization (active lines and priority lines)
        Timer1_Tick(sender, e)

        ' Update text in InfoBox
        Try
            RichTextBox1.LoadFile("InfoTextForOperators.rtf", RichTextBoxStreamType.RichText)
        Catch ex As Exception
            Debug.WriteLine("Exception : " + ex.StackTrace)
        End Try

    End Sub

    Private Sub Label_MouseEnter(sender As Object, e As System.EventArgs)
        sender.Cursor = Cursors.Hand
    End Sub

    Private Sub Label_MouseLeave(sender As Object, e As System.EventArgs)
        sender.Cursor = Cursors.Default
    End Sub

    Private Sub FormatInRich(ByVal rtb As RichTextBox, formatPar As String, ByVal textr As String)
        rtb.AppendText(textr)
        rtb.Select(InStrRev(rtb.Text, textr) - 1, Len(rtb.Text))
        Dim currentFont As System.Drawing.Font = rtb.SelectionFont
        If formatPar = "regular" Then
            rtb.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, FontStyle.Regular)
        Else rtb.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold)
        End If
        If formatPar = "regular" Then rtb.SelectionColor = Color.Black
        If formatPar = "yellow" Then rtb.SelectionColor = Color.Orange
        If formatPar = "red" Then rtb.SelectionColor = Color.Red
        rtb.Select(0, 1)
    End Sub

    Private Sub LineLabelClicked(sender As Object, e As System.EventArgs)
        ' Line label was clicked, show alarm history
        Dim clickedWorkstationLine As Integer
        Dim clickedWorkstationName As String

        AlOverview.RichTextBox1.Text = ""
        For i As Integer = 0 To nOfLines - 1
            Dim myBox As Label = CType(Me.Controls("prioLabel" & i), Label)
            If sender.Name = myBox.Name Then clickedWorkstationLine = i
        Next
        clickedWorkstationName = workstationLabels(displayedLines(clickedWorkstationLine), 1)
        AlOverview.Label1.Text = clickedWorkstationName

        ' Read the alarm log and build up the history of alarms
        Dim s2 As String = ""
        For i As Integer = 0 To nOfLines - 1             ' Find terminal name that we should use
            If workstationLabels(i, 1) = workstationLabels(displayedLines(clickedWorkstationLine), 1) Then s2 = workstationLabels(i, 3)
        Next

        If File.Exists("Logs/alarmlog_" & s2) Then

            Dim s As String() = File.ReadAllLines("Logs/alarmlog_" & s2)
            Dim alarmLogs(100000, 4) As String              ' Fill this array with alarms from the text file
            Dim workstationAlarmLogs(100000, 4) As String   ' Filter only alarms relevant for the workstation, calculate alarm durations
            Dim lineReader() As String
            Dim linesOfAlarms As Integer = s.GetUpperBound(0)
            For i = 0 To linesOfAlarms
                lineReader = s(i).Split(";")
                For k = 0 To 4
                    alarmLogs(i, k) = lineReader(k).Split("|").Last().Trim()
                Next
            Next
            Dim lineOfWorkstationAlarmlogs As Integer = 0
            Dim lineOfAlarmTypes As Integer()
            ReDim lineOfAlarmTypes(alarmTypes)
            For i = 0 To alarmTypes - 1
                lineOfAlarmTypes(i) = -1
            Next
            For i = 0 To linesOfAlarms
                If alarmLogs(i, 1) = clickedWorkstationName Then  ' Does the line in alarmlog belong to the workstation that was clicked?
                    If alarmLogs(i, 3) <> greenName Then            ' It's a beginning of an alarm
                        For k = 0 To 4
                            workstationAlarmLogs(lineOfWorkstationAlarmlogs, k) = alarmLogs(i, k)
                        Next
                        lineOfAlarmTypes(alarmLogs(i, 2)) = lineOfWorkstationAlarmlogs    ' Increase the appropriate alarm type to the latest line
                        lineOfWorkstationAlarmlogs += 1
                    Else                                           ' It's a finish of an existing alarm
                        If lineOfAlarmTypes(alarmLogs(i, 2)) > -1 Then
                            workstationAlarmLogs(lineOfAlarmTypes(alarmLogs(i, 2)), 4) = alarmLogs(i, 4)
                            lineOfAlarmTypes(alarmLogs(i, 2)) += 1
                        End If
                    End If
                End If
            Next

            ' Write the text to RichTextBox and format it
            For i = 0 To lineOfWorkstationAlarmlogs - 1
                FormatInRich(AlOverview.RichTextBox1, "bold", (DateTime.ParseExact(workstationAlarmLogs(i, 0), "s", Nothing).ToString("dd.MM.yyyy HH:mm - ") & DateTime.ParseExact(workstationAlarmLogs(i, 0), "s", Nothing).AddSeconds(workstationAlarmLogs(i, 4)).ToString("HH:mm") & " (" & CInt(workstationAlarmLogs(i, 4) / 60) & " min)").PadRight(40))
                FormatInRich(AlOverview.RichTextBox1, "regular", iconLbl(workstationAlarmLogs(i, 2)).PadRight(35))
                If workstationAlarmLogs(i, 3) = redName Then
                    FormatInRich(AlOverview.RichTextBox1, "red", workstationAlarmLogs(i, 3))
                ElseIf workstationAlarmLogs(i, 3) = yellowName Then
                    FormatInRich(AlOverview.RichTextBox1, "yellow", workstationAlarmLogs(i, 3))
                End If
                AlOverview.RichTextBox1.AppendText(vbNewLine)
            Next

            AlOverview.Button1.Text = alarmTypesString(3)

        Else  ' The alarm log doesn't exist
            AlOverview.RichTextBox1.AppendText("=== No record found ===")
        End If

        AlOverview.ShowDialog()
    End Sub

    Private Sub UpdateFields(sender As Object, e As FileSystemEventArgs)
        ' Update alarm fields according to the current text files

        ' Copy initial lineStatus state to be able to find out later which lines were updated
        Dim i As Integer
        For i = 0 To nOfLines - 1
            For j = 0 To alarmTypes - 1
                previousworkstationStatus(i, j) = workstationStatus(i, j)
            Next
        Next

        '-----------------------------------------------------------------------
        ' Read updated text file and update relevant fields
        Dim lineNumber As String
        Try
            Using inputFile As New StreamReader(e.FullPath)
                oldFile = DateDiff(DateInterval.Minute, Convert.ToDateTime(File.GetLastWriteTime(e.FullPath)), DateTime.Now) > maxDelay
                While Not (inputFile.EndOfStream Or oldFile)
                    lineNumber = inputFile.ReadLine()
                    inputFile.ReadLine()
                    For i = 0 To alarmTypes - 1
                        Try
                            workstationStatus(CInt(lineNumber), i) = inputFile.ReadLine()
                            alarmStartTime(CInt(lineNumber), i) = Date.ParseExact(inputFile.ReadLine(), "s", Nothing)  ' Read date time in ISO format
                        Catch ex As Exception
                        End Try
                    Next

                    Try
                        For i = 0 To alarmTypes - 1
                            Dim myLabel As Label = CType(Me.Controls("lineLabel" & lineNumber * alarmTypes + i), Label)
                            If workstationStatus(CInt(lineNumber), i) = greenName Then
                                myLabel.BackColor = Color.FromArgb(0, 255, 0)
                            ElseIf workstationStatus(CInt(lineNumber), i) = yellowName Then
                                myLabel.BackColor = Color.FromArgb(255, 192, 0)
                            ElseIf workstationStatus(CInt(lineNumber), i) = redName Then
                                myLabel.BackColor = Color.FromArgb(255, 0, 0)
                            End If

                            ' Display time since last alarm in the alarm field
                            If (workstationStatus(CInt(lineNumber), i) = yellowName) And (previousworkstationStatus(CInt(lineNumber), i) = greenName) Then   ' it's a new alarm
                                myLabel.Text = "0 min"
                                myLabel.ForeColor = Color.Black
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            ElseIf (workstationStatus(CInt(lineNumber), i) = redName) And (previousworkstationStatus(CInt(lineNumber), i) = greenName) Then   ' it's a new alarm
                                myLabel.Text = "0 min"
                                myLabel.ForeColor = Color.White
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            ElseIf workstationStatus(CInt(lineNumber), i) = greenName Then  ' remove all labels
                                myLabel.Text = ""
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            End If
                        Next
                    Catch exz As Exception
                        Debug.WriteLine("Exception : " + exz.StackTrace)
                    End Try
                End While
            End Using
        Catch ex As Exception
            Debug.WriteLine("Exception : " + ex.Message + ". Trace : " + ex.StackTrace)
        End Try
        '-----------------------------------------------------------------------

        ' Highlight the line with last alarm
        Dim lastAlarmLineNr As Integer = -1
        Dim alarmWorsened As Boolean = False
        nOfPreviousAlarms = nOfAlarms
        nOfAlarms = 0
        For i = 0 To nOfLines - 1
            For j = 0 To alarmTypes - 1
                If workstationStatus(i, j) = yellowName Then nOfAlarms += 1
                If workstationStatus(i, j) = redName Then nOfAlarms += 2
            Next
        Next
        If nOfAlarms > nOfPreviousAlarms Then
            Dim fpath As String = Application.StartupPath + "/Assets/alarm.wav"
            If soundOn Then My.Computer.Audio.Play(fpath, AudioPlayMode.Background)

            For i = 0 To nOfLines - 1
                ' Identify the last alarm
                For j = 0 To alarmTypes - 1
                    If (previousworkstationStatus(i, j) = greenName And workstationStatus(i, j) = yellowName) Then alarmWorsened = True
                    If (previousworkstationStatus(i, j) = greenName And workstationStatus(i, j) = redName) Then alarmWorsened = True
                Next
                If alarmWorsened Then
                    lastAlarmLineNr = i
                    alarmWorsened = False
                End If
            Next

            For i = 0 To nOfLines - 1
                ' Remove previous marking of last alarm 
                Dim myLabel As Label = CType(Controls("prioLabel" & i), Label)
                myLabel.ForeColor = Color.FromArgb(0, 0, 0)
            Next
            ' Mark last alarm
            If lastAlarmLineNr > -1 Then
                Dim myLabel2 As Label = CType(Controls("prioLabel" & lastAlarmLineNr), Label)
                myLabel2.ForeColor = Color.FromArgb(255, 0, 0)
            End If
        End If

        ' Remove last alarm label if all alarms have been solved
        Dim someAlarmsExist As Boolean
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Controls("prioLabel" & i), Label)
            someAlarmsExist = False
            For j = 0 To alarmTypes - 1
                If workstationStatus(i, j) <> greenName Then someAlarmsExist = True
            Next
            If Not someAlarmsExist Then myLabel.ForeColor = Color.FromArgb(0, 0, 0)
        Next

    End Sub


    Private Sub Watcher2_Changed(sender As Object, e As FileSystemEventArgs) Handles watcher2.Changed
        ' If text file in the watched folder is created or rewritten, call update function
        Try
            UpdateFields(sender, e)
        Catch ex As Exception
            Debug.WriteLine("Exception : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Periodically update time since alarm was started
        Dim i, j As Integer
        For i = 0 To nOfLines - 1
            For j = 0 To alarmTypes - 1
                Dim myLabel As Label = CType(Controls("lineLabel" & i * alarmTypes + j), Label)
                If workstationStatus(i, j) = yellowName Or workstationStatus(i, j) = redName Then
                    If DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) > 9 Then myLabel.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) Else myLabel.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) & " min"
                End If
            Next
        Next

        ' Mark priority lines
        For i = 0 To nOfLines - 1
            Dim myLbl As Label = CType(Controls("prioLabel" & i), Label)
            myLbl.BackColor = Color.FromArgb(255, 255, 255)
        Next

        For i = 0 To nOfLines - 1
            priorityLines(i) = -1
        Next

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("priority_workstations.txt")
            j = 0
            While Not MyReader.EndOfData
                Try
                    Dim prioLine As String = MyReader.ReadLine()
                    For i = 0 To nOfLines - 1
                        If workstationLabels(i, 1) = prioLine Then
                            priorityLines(j) = i
                            j += 1
                        End If
                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox(ex.Message)
                End Try
            End While
        End Using

        For i = 0 To nOfLines - 1
            If priorityLines(i) > -1 Then
                Dim myLbl As Label = CType(Controls("prioLabel" & priorityLines(i)), Label)
                myLbl.BackColor = Color.FromArgb(252, 228, 214)
            End If
        Next
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBoxSound.Click
        ' Toggle sound on/off
        If soundOn Then
            PictureBoxSound.Image = Image.FromFile("Assets/Soundoff.jpg")
            LabelSound.Text = "Sound is off"
        Else
            PictureBoxSound.Image = Image.FromFile("Assets/Soundon.jpg")
            LabelSound.Text = "Sound is on"
        End If
        soundOn = Not soundOn
    End Sub

    Private Sub Tbox1_MouseEnter(sender As Object, e As System.EventArgs) Handles PictureBoxSound.MouseEnter
        PictureBoxSound.Cursor = Cursors.Hand
    End Sub
    Private Sub Tbox1_MouseLeave(sender As Object, e As System.EventArgs) Handles PictureBoxSound.MouseLeave
        PictureBoxSound.Cursor = Cursors.Default
    End Sub

    Private Sub InfoBoxTimer_Tick(sender As Object, e As EventArgs) Handles InfoBoxTimer.Tick
        ' Update text in InfoBox
        Try
            RichTextBox1.LoadFile("InfoTextForOperators.rtf", RichTextBoxStreamType.RichText)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Exception : " + ex.StackTrace)
        End Try
    End Sub
End Class