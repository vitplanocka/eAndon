Imports System.IO

'TODO:  
' - create variables for positioning of nameLabels and workstationLabels
' - add grid view / lay-out view option
' - make creation of Terminals dynamic


Public Class Andon

    Public nOfLines As Integer = 0                                            ' number of displayed lines
    Public alarmTypes As Integer = 0                                          ' number of displayed alarm types (columns)
    Public workstationLabels(nOfLines - 1, 3) As String                       ' #, line number, line name for displayed lines
    Public nOfPreviousAlarms As Integer                                       ' number of previous displayed alarms 
    Public nOfAlarms As Integer                                               ' number of displayed alarms
    Public workstationStatus(nOfLines - 1, alarmTypes - 1) As String          ' current status of alarms for all lines
    Public previousworkstationStatus(nOfLines - 1, alarmTypes - 1) As String  ' previous status of alarms for all lines
    Public alarmStartTime(nOfLines - 1, alarmTypes - 1) As Date               ' date of last start of yellow or red alarm
    Public oldFile As Boolean                                                 ' is the file change date too old to display in dashboard?
    Public maxDelay As Integer = 1                                            ' text files older than x minutes are ignored
    Public soundOn As Boolean = False                                         ' do alarms play a sound?
    Public priorityLines(nOfLines) As Integer                                 ' array of lines that are highlighted as priority
    Public alarmfile As String                                                ' alarm sound filename
    Public iconLbl(alarmTypes) As String                                      ' labels for alarm types
    Public iconImgFile(alarmTypes) As String                                  ' image filenames for alarm types  

    Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Things to do when app starts

        ' Maximize the window
        Me.WindowState = FormWindowState.Maximized

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
            ' Number of status colours
            MyReader.ReadLine()
            ' Image file for company logo
            lineReader = MyReader.ReadLine().Split(":")
            PictureBoxLogo.ImageLocation = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
            ' Alarm sound file
            lineReader = MyReader.ReadLine().Split(":")
            alarmfile = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
            ' Terminal usage instruction
            MyReader.ReadLine()
            ' Alarm type descriptions and icons
            ReDim iconLbl(alarmTypes)
            ReDim iconImgFile(alarmTypes)
            For i = 0 To alarmTypes
                MyReader.ReadLine()
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
            newbox2 = New PictureBox With {
                .Size = New Drawing.Size(40, 30),
                .Location = New Point(270 + i * 53, 10),
                .ImageLocation = iconImgFile(i),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            ' Draw alarm labels
            newbox2.Name = "alarmLabel" & i
            newbox2.BorderStyle = BorderStyle.None
            Controls.Add(newbox2)
        Next

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
                workstationStatus(i, j) = "Green"
                previousworkstationStatus(i, j) = "Green"
            Next
        Next

        ' Ignore old files. If it's a current file, trigger update of alert fields
        Dim di As New DirectoryInfo("Data/")
        ' Get a reference to each file in that directory.
        Dim fiArr As FileInfo() = di.GetFiles()
        ' Display the names of the files.
        Dim fri As FileInfo
        For Each fri In fiArr
            oldFile = DateDiff(DateInterval.Minute, Convert.ToDateTime(File.GetLastWriteTime("Data/" & fri.ToString)), DateTime.Now) > maxDelay
            If Not oldFile Then File.SetLastWriteTime("Data/" & fri.ToString, DateTime.Now)
        Next fri

        If soundOn = True Then LabelSound.Text = "Sound is on" Else LabelSound.Text = "Sound is off"
        PictureBoxLogo.Select()  ' Set focus on logo to prevent selecting text of TextBoxes

        ' Update line visualization (active lines and priority lines)
        Timer1_Tick(sender, e)

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
                        Catch ex As Exception
                        End Try
                    Next

                    Try
                        For i = 0 To alarmTypes - 1
                            Dim myLabel As Label = CType(Me.Controls("lineLabel" & lineNumber * alarmTypes + i), Label)
                            If workstationStatus(CInt(lineNumber), i) = "Green" Then
                                myLabel.BackColor = Color.FromArgb(0, 255, 0)
                            ElseIf workstationStatus(CInt(lineNumber), i) = "Yellow" Then
                                myLabel.BackColor = Color.FromArgb(255, 192, 0)
                            ElseIf workstationStatus(CInt(lineNumber), i) = "Red" Then
                                myLabel.BackColor = Color.FromArgb(255, 0, 0)
                            End If


                            'Display time since last alarm
                            If (workstationStatus(CInt(lineNumber), i) = "Yellow") And (previousworkstationStatus(CInt(lineNumber), i) = "Green") Then   ' it's a new alarm
                                Try
                                    alarmStartTime(CInt(lineNumber), i) = Date.Now
                                Catch ex As Exception
                                    Debug.WriteLine("Exception : " + ex.StackTrace)
                                End Try

                                myLabel.Text = "0 min"
                                myLabel.ForeColor = Color.Black
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            ElseIf (workstationStatus(CInt(lineNumber), i) = "Red") And (previousworkstationStatus(CInt(lineNumber), i) = "Yellow") Then   ' it's a new alarm
                                Try
                                    alarmStartTime(CInt(lineNumber), i) = Date.Now
                                Catch ex As Exception
                                    Debug.WriteLine("Exception : " + ex.StackTrace)
                                End Try

                                myLabel.Text = "0 min"
                                myLabel.ForeColor = Color.White
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            ElseIf (workstationStatus(CInt(lineNumber), i) = "Green" And previousworkstationStatus(CInt(lineNumber), i) = "Red") Then  ' we're going from red to green
                                myLabel.Text = ""
                                PictureBoxLogo.Select() ' Remove the cursor from updated field
                            ElseIf (workstationStatus(CInt(lineNumber), i) = "Green" And previousworkstationStatus(CInt(lineNumber), i) = "Green") Then  ' in case of initialization on startup, we must remove all labels
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
                If workstationStatus(i, j) = "Yellow" Then nOfAlarms += 1
                If workstationStatus(i, j) = "Red" Then nOfAlarms += 2
            Next
        Next
        If nOfAlarms > nOfPreviousAlarms Then
            Dim fpath As String = Application.StartupPath + "/Assets/alarm.wav"
            If soundOn Then My.Computer.Audio.Play(fpath, AudioPlayMode.Background)

            For i = 0 To nOfLines - 1
                ' Identify the last alarm
                For j = 0 To alarmTypes - 1
                    If (previousworkstationStatus(i, j) = "Green" And workstationStatus(i, j) = "Yellow") Then alarmWorsened = True
                    If (previousworkstationStatus(i, j) = "Yellow" And workstationStatus(i, j) = "Red") Then alarmWorsened = True
                    If (previousworkstationStatus(i, j) = "Green" And workstationStatus(i, j) = "Red") Then alarmWorsened = True
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

        Dim someAlarmsExist As Boolean
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Controls("prioLabel" & i), Label)
            someAlarmsExist = False
            For j = 0 To alarmTypes - 1
                If workstationStatus(i, j) <> "Green" Then someAlarmsExist = True
            Next
            If Not someAlarmsExist Then myLabel.ForeColor = Color.FromArgb(0, 0, 0)
        Next

    End Sub


    Private Sub Watcher2_Changed(sender As Object, e As FileSystemEventArgs) Handles watcher2.Changed
        ' If text file in the watched folder is created or rewritten, call update function
        If e.Name.Substring(0, 8) = "terminal" Then
            Try
                UpdateFields(sender, e)
            Catch ex As Exception
                Debug.WriteLine("Exception : " + ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ' Periodically update time since alarm was started
        Dim i, j As Integer
        For i = 0 To nOfLines - 1
            For j = 0 To alarmTypes - 1
                Dim myLabel As Label = CType(Controls("lineLabel" & i * alarmTypes + j), Label)
                If workstationStatus(i, j) = "Yellow" Or workstationStatus(i, j) = "Red" Then
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

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("priority_lines.txt")
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
            PictureBoxSound.Image = Image.FromFile("Assets/soundoff.png")
            LabelSound.Text = "Sound is off"
        Else
            PictureBoxSound.Image = Image.FromFile("Assets/soundon.png")
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

End Class