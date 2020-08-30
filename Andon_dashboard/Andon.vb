﻿Imports System.IO

Public Class Andon

    Public nOfLines As Integer = 0 ' number of displayed lines
    Public lineLabels(100, 3) As String
    Public nOfPreviousAlarms As Integer
    Public nOfAlarms As Integer
    Public lineStatusStr(nOfLines - 1, 4) As String
    Public previousLineStatusStr(nOfLines - 1, 4) As String
    Public alarmStartTime(100, 4) As Date
    Public oldFile As Boolean
    Public maxDelay As Integer = 10   'text files older than x minutes are ignored
    Public soundOn As Boolean = False
    Public priorityLines(nOfLines) As Integer

    Public logofile As String     ' resources from Asset folder
    Public alarmfile As String
    Public icon1lbl As String
    Public icon2lbl As String
    Public icon3lbl As String
    Public icon4lbl As String
    Public icon5lbl As String
    Public icon1imgfile As String
    Public icon2imgfile As String
    Public icon3imgfile As String
    Public icon4imgfile As String
    Public icon5imgfile As String

    Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Things to do when app starts

        ' Maximize the window
        Me.WindowState = FormWindowState.Maximized

        ' Set watcher path to current folder
        watcher2.Path = Application.StartupPath & "/Data"

        ' Read Settings from Text File
        Dim rootpath As String = Application.StartupPath
        Using MyReader2 As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/settings.txt")
            logofile = MyReader2.ReadLine()
            alarmfile = MyReader2.ReadLine()
            MyReader2.ReadLine()
            icon1lbl = MyReader2.ReadLine()
            icon2lbl = MyReader2.ReadLine()
            icon3lbl = MyReader2.ReadLine()
            icon4lbl = MyReader2.ReadLine()
            icon5lbl = MyReader2.ReadLine()
            icon1imgfile = MyReader2.ReadLine()
            icon2imgfile = MyReader2.ReadLine()
            icon3imgfile = MyReader2.ReadLine()
            icon4imgfile = MyReader2.ReadLine()
            icon5imgfile = MyReader2.ReadLine()
            PopulateAutoInfo()
        End Using


        ' Load the line labels
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/production_lines.txt")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")

            nOfLines = MyReader.ReadLine

            lineStatusStr = New String(nOfLines - 1, 4) {}
            previousLineStatusStr = New String(nOfLines - 1, 4) {}
            Dim i As Integer = 0

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    ' Read row as an array of strings '
                    currentRow = MyReader.ReadFields()
                    ' Parse line into strings '
                    lineLabels(i, 0) = currentRow(0)
                    lineLabels(i, 1) = currentRow(1)
                    lineLabels(i, 2) = currentRow(2)
                    lineLabels(i, 3) = currentRow(3)
                    i += 1
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        ' Set tooltips for line nubmers
        Dim toolTip1 As New ToolTip()
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Me.Controls("Label" & i + 1), Label)
            toolTip1.SetToolTip(myLabel, lineLabels(i, 2))
        Next

        ' Set line number labels
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Me.Controls("Label" & i + 1), Label)
            myLabel.Text = lineLabels(i, 1)
        Next

        nOfAlarms = 0

        For i = 0 To nOfLines - 1
            For j = 0 To 4
                If lineStatusStr(i, j) = "Yellow" Then nOfAlarms = nOfAlarms + 1
                If lineStatusStr(i, j) = "Red" Then nOfAlarms = nOfAlarms + 2
            Next
        Next

        For i = 0 To nOfLines - 1
            lineStatusStr(i, 0) = "Green"
            previousLineStatusStr(i, 0) = "Green"
            lineStatusStr(i, 1) = "Green"
            previousLineStatusStr(i, 1) = "Green"
            lineStatusStr(i, 2) = "Green"
            previousLineStatusStr(i, 2) = "Green"
            lineStatusStr(i, 3) = "Green"
            previousLineStatusStr(i, 3) = "Green"
            lineStatusStr(i, 4) = "Green"
            previousLineStatusStr(i, 4) = "Green"

        Next

        ' Ignore old files. If it's a current file, trigger update of alert fields
        Dim di As New DirectoryInfo("Data/")
        ' Get a reference to each file in that directory.
        Dim fiArr As FileInfo() = di.GetFiles()
        ' Display the names of the files.
        Dim fri As FileInfo
        For Each fri In fiArr
            oldFile = DateDiff(DateInterval.Minute, Convert.ToDateTime(System.IO.File.GetLastWriteTime("Data/" & fri.ToString)), DateTime.Now) > maxDelay
            If Not oldFile Then System.IO.File.SetLastWriteTime("Data/" & fri.ToString, DateTime.Now)
        Next fri

        ' Update line visualization (active lines and priority lines)
        Timer1_Tick(sender, e)

    End Sub

    Private Sub PopulateAutoInfo()    ' Load labels and descriptions from the settings file
        PictureBoxLogo.ImageLocation = logofile
        PictureBox1.ImageLocation = icon1imgfile
        PictureBox6.ImageLocation = icon1imgfile
        PictureBox11.ImageLocation = icon1imgfile
        PictureBox2.ImageLocation = icon2imgfile
        PictureBox7.ImageLocation = icon2imgfile
        PictureBox12.ImageLocation = icon2imgfile
        PictureBox3.ImageLocation = icon3imgfile
        PictureBox8.ImageLocation = icon3imgfile
        PictureBox13.ImageLocation = icon3imgfile
        PictureBox4.ImageLocation = icon4imgfile
        PictureBox9.ImageLocation = icon4imgfile
        PictureBox14.ImageLocation = icon4imgfile
        PictureBox5.ImageLocation = icon5imgfile
        PictureBox10.ImageLocation = icon5imgfile
        PictureBox15.ImageLocation = icon5imgfile
    End Sub

    Private Sub UpdateFields(sender As Object, e As FileSystemEventArgs)
        ' Update alarm fields according to the current text files

        ' Copy initial lineStatus state to be able to find out later which lines were updated
        Dim i As Integer
        For i = 0 To nOfLines - 1
            previousLineStatusStr(i, 0) = lineStatusStr(i, 0)
            previousLineStatusStr(i, 1) = lineStatusStr(i, 1)
            previousLineStatusStr(i, 2) = lineStatusStr(i, 2)
            previousLineStatusStr(i, 3) = lineStatusStr(i, 3)
            previousLineStatusStr(i, 4) = lineStatusStr(i, 4)
        Next

        '-----------------------------------------------------------------------
        ' Read updated text file and update relevant fields
        Dim lineNumber As String
        Try
            Using inputFile As New StreamReader(e.FullPath)
                oldFile = DateDiff(DateInterval.Minute, Convert.ToDateTime(System.IO.File.GetLastWriteTime(e.FullPath)), DateTime.Now) > maxDelay
                While Not (inputFile.EndOfStream Or oldFile)
                    lineNumber = inputFile.ReadLine()
                    inputFile.ReadLine()
                    For i = 0 To 4
                        Try
                            lineStatusStr(CInt(lineNumber), i) = inputFile.ReadLine()
                        Catch ex As Exception

                        End Try
                    Next
                    Try
                        For i = 1 To 5
                            Dim myBox As TextBox = CType(Me.Controls("TBox" & lineNumber + 1 & i), TextBox)
                            If lineStatusStr(CInt(lineNumber), i - 1) = "Green" Then
                                myBox.BackColor = Color.FromArgb(0, 255, 0)

                            ElseIf lineStatusStr(CInt(lineNumber), i - 1) = "Yellow" Then
                                myBox.BackColor = Color.FromArgb(255, 192, 0)
                            ElseIf lineStatusStr(CInt(lineNumber), i - 1) = "Red" Then
                                myBox.BackColor = Color.FromArgb(255, 0, 0)

                            End If
                            'Display time since last alarm
                            If (lineStatusStr(CInt(lineNumber), i - 1) = "Yellow") And (previousLineStatusStr(CInt(lineNumber), i - 1) = "Green") Then   ' it's a new alarm
                                alarmStartTime(CInt(lineNumber), i - 1) = Date.Now
                                myBox.Text = "0 min"
                                myBox.ForeColor = Color.Black
                                PictureBox1.Select() ' Remove the cursor from updated field
                            ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Red") And (previousLineStatusStr(CInt(lineNumber), i - 1) = "Yellow") Then   ' it's a new alarm
                                alarmStartTime(CInt(lineNumber), i - 1) = Date.Now
                                myBox.Text = "0 min"
                                myBox.ForeColor = Color.White
                                PictureBox1.Select() ' Remove the cursor from updated field
                            ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Green" And previousLineStatusStr(CInt(lineNumber), i - 1) = "Red") Then  ' we're going from red to green
                                myBox.Text = ""
                                PictureBox1.Select() ' Remove the cursor from updated field
                            ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Green" And previousLineStatusStr(CInt(lineNumber), i - 1) = "Green") Then  ' in case of initialization on startup, we musts remove all labels
                                myBox.Text = ""
                                PictureBox1.Select() ' Remove the cursor from updated field
                            End If

                        Next
                    Catch exz As Exception
                        System.Diagnostics.Debug.WriteLine("Exception : " + exz.StackTrace)
                    End Try
                End While
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Exception : " + ex.Message + ". Trace : " + ex.StackTrace)
        End Try
        '-----------------------------------------------------------------------

        ' Highlight the line with last alarm
        Dim lastAlarmLineNr As Integer = -1
        nOfPreviousAlarms = nOfAlarms
        nOfAlarms = 0
        For i = 0 To nOfLines - 1
            For j = 0 To 4
                If lineStatusStr(i, j) = "Yellow" Then nOfAlarms = nOfAlarms + 1
                If lineStatusStr(i, j) = "Red" Then nOfAlarms = nOfAlarms + 2
            Next
        Next
            If nOfAlarms > nOfPreviousAlarms Then
            Dim fpath As String = Application.StartupPath + "/Assets/alarm.wav"
            If soundOn Then My.Computer.Audio.Play(fpath, AudioPlayMode.Background)

            For i = 0 To nOfLines - 1
                ' Identify the last alarm
                If (previousLineStatusStr(i, 0) = "Green" And lineStatusStr(i, 0) = "Yellow") Or (previousLineStatusStr(i, 1) = "Green" And lineStatusStr(i, 1) = "Yellow") Or (previousLineStatusStr(i, 2) = "Green" And lineStatusStr(i, 2) = "Yellow") Or (previousLineStatusStr(i, 3) = "Green" And lineStatusStr(i, 3) = "Yellow") Or (previousLineStatusStr(i, 4) = "Green" And lineStatusStr(i, 4) = "Yellow") Then lastAlarmLineNr = i
                If (previousLineStatusStr(i, 0) = "Yellow" And lineStatusStr(i, 0) = "Red") Or (previousLineStatusStr(i, 1) = "Yellow" And lineStatusStr(i, 1) = "Red") Or (previousLineStatusStr(i, 2) = "Yellow" And lineStatusStr(i, 2) = "Red") Or (previousLineStatusStr(i, 3) = "Yellow" And lineStatusStr(i, 3) = "Red") Or (previousLineStatusStr(i, 4) = "Yellow" And lineStatusStr(i, 4) = "Red") Then lastAlarmLineNr = i
            Next
            For i = 0 To nOfLines - 1
                ' Remove previous marking of last alarm 
                Dim myLabel As Label = CType(Me.Controls("Label" & i + 1), Label)
                myLabel.ForeColor = Color.FromArgb(0, 0, 0)
            Next
            ' Mark last alarm
            Dim myLabel2 As Label = CType(Me.Controls("Label" & lastAlarmLineNr + 1), Label)
            myLabel2.ForeColor = Color.FromArgb(255, 0, 0)
        End If

        ' Remove last alarm label if all alarms have been solved
        For i = 0 To nOfLines - 1
            Dim myLabel As Label = CType(Me.Controls("Label" & i + 1), Label)
            If (lineStatusStr(i, 0) = "Green") And (lineStatusStr(i, 1) = "Green") And (lineStatusStr(i, 2) = "Green") And (lineStatusStr(i, 3) = "Green") And (lineStatusStr(i, 4) = "Green") Then myLabel.ForeColor = Color.FromArgb(0, 0, 0)
        Next

    End Sub


    Private Sub watcher2_Changed(sender As Object, e As FileSystemEventArgs) Handles watcher2.Changed
        ' If text file in the watched folder is created or rewritten, call update function
        If e.Name.Substring(0, 8) = "terminal" Then
            Try
                Me.UpdateFields(sender, e)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Exception : " + ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ' Periodically update time since alarm was started
        Dim i, j As Integer
        For i = 0 To nOfLines - 1
            For j = 0 To 4
                Dim myBox As TextBox = CType(Me.Controls("TBox" & i + 1 & j + 1), TextBox)
                If lineStatusStr(i, j) = "Yellow" Or lineStatusStr(i, j) = "Red" Then
                    If DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) > 9 Then myBox.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) Else myBox.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) & " min"
                End If
            Next
        Next

        ' Mark priority lines
        For i = 0 To nOfLines - 1
            Dim myLbl As Label = CType(Me.Controls("Label" & i + 1), Label)
            myLbl.BackColor = Color.FromArgb(255, 255, 255)
        Next

        ReDim priorityLines(nOfLines)
        For i = 0 To nOfLines - 1
            priorityLines(i) = -1
        Next

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("prioritni_linky.txt")
            j = 0
            While Not MyReader.EndOfData
                Try
                    Dim escLine As String = MyReader.ReadLine()
                    For i = 0 To nOfLines - 1
                        If lineLabels(i, 1) = escLine Then
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
            If priorityLines(i) >= 0 Then
                Dim myLbl2 As Label = CType(Me.Controls("Label" & priorityLines(i) + 1), Label)
                myLbl2.BackColor = Color.FromArgb(252, 228, 214)
            End If
        Next
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBoxSound.Click
        ' Toggle sound on/off
        If soundOn Then PictureBoxSound.Image = Image.FromFile("Assets/soundoff.png") Else PictureBoxSound.Image = Image.FromFile("Assets/soundon.png")
        soundOn = Not soundOn
    End Sub

    Private Sub Tbox1_MouseEnter(sender As Object, e As System.EventArgs) Handles PictureBoxSound.MouseEnter
        PictureBoxSound.Cursor = Cursors.Hand
    End Sub
    Private Sub Tbox1_MouseLeave(sender As Object, e As System.EventArgs) Handles PictureBoxSound.MouseLeave
        PictureBoxSound.Cursor = Cursors.Default
    End Sub

End Class