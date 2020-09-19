Imports System.IO

'TODO:  
' - change icon1lbl and icon1imgfile to arrays
' - fix Terminal01
' - fix 308, 309, 324
' - copy Terminal 01 to 02-04
' - remove reading nOfLines from first line of production_lines.txt 
' - create variables for positioning of nameLabels and lineLabels
' - make creation of Terminals dynamic


Public Class Andon

	Public nOfLines As Integer = 0 ' number of displayed lines
	Public alarmTypes As Integer = 0  ' number of displayed alarm types
	Public lineLabels(100, 3) As String
	Public nOfPreviousAlarms As Integer
	Public nOfAlarms As Integer
	Public lineStatusStr(nOfLines - 1, alarmTypes - 1) As String
	Public previousLineStatusStr(nOfLines - 1, alarmTypes - 1) As String
	Public alarmStartTime(100, alarmTypes - 1) As Date
	Public oldFile As Boolean
	Public maxDelay As Integer = 1   ' text files older than x minutes are ignored
	Public soundOn As Boolean = False
	Public priorityLines(nOfLines) As Integer

	Public logofile As String     ' resources from Asset folder
	Public alarmfile As String
	Public icon1lbl As String
	Public icon2lbl As String
	Public icon3lbl As String
	Public icon4lbl As String
	Public icon5lbl As String
	Public icon6lbl As String
	Public icon7lbl As String
	Public icon8lbl As String
	Public icon9lbl As String
	Public icon10lbl As String
	Public icon1imgfile As String
	Public icon2imgfile As String
	Public icon3imgfile As String
	Public icon4imgfile As String
	Public icon5imgfile As String
	Public icon6imgfile As String
	Public icon7imgfile As String
	Public icon8imgfile As String
	Public icon9imgfile As String
	Public icon10imgfile As String

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
		Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/settings.txt")
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
			' Alarm type descriptions
			icon1lbl = MyReader.ReadLine()
			icon2lbl = MyReader.ReadLine()
			icon3lbl = MyReader.ReadLine()
			icon4lbl = MyReader.ReadLine()
			icon5lbl = MyReader.ReadLine()
			icon6lbl = MyReader.ReadLine()
			icon7lbl = MyReader.ReadLine()
			icon8lbl = MyReader.ReadLine()
			icon9lbl = MyReader.ReadLine()
			icon10lbl = MyReader.ReadLine()
			' Alarm type icons
			PictureBox1.ImageLocation = MyReader.ReadLine()
			PictureBox2.ImageLocation = MyReader.ReadLine()
			PictureBox3.ImageLocation = MyReader.ReadLine()
			PictureBox4.ImageLocation = MyReader.ReadLine()
			PictureBox5.ImageLocation = MyReader.ReadLine()
			PictureBox6.ImageLocation = MyReader.ReadLine()
			PictureBox7.ImageLocation = MyReader.ReadLine()
			PictureBox8.ImageLocation = MyReader.ReadLine()
			PictureBox9.ImageLocation = MyReader.ReadLine()
			PictureBox10.ImageLocation = MyReader.ReadLine()
		End Using

		' Load the line labels
		Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/production_lines.txt")
			MyReader.TextFieldType = FileIO.FieldType.Delimited
			MyReader.SetDelimiters(";")

			nOfLines = MyReader.ReadLine

			lineStatusStr = New String(nOfLines - 1, alarmTypes - 1) {}
			previousLineStatusStr = New String(nOfLines - 1, alarmTypes - 1) {}
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

		' Alarm labels
		Dim newbox As Label
		Dim y As Int32 = 0
		Dim lineNo As Int32 = 1
		For i As Integer = 0 To (nOfLines * alarmTypes) - 1 'Create labels and set properties
			'set Y
			If (i / lineNo) = alarmTypes Then
				y += 25
				lineNo += 1
			End If

			newbox = New Label With {
				.Size = New Drawing.Size(40, 20),
				.Location = New Point(100 + (i Mod alarmTypes) * 53, 55 + y),
				.Font = New Font("Arial", 8),
				.TextAlign = ContentAlignment.MiddleCenter
			}
			' Draw alarm labels
			newbox.Name = "lineLabel" & i
			newbox.Text = ""
			newbox.BorderStyle = BorderStyle.FixedSingle
			newbox.BackColor = Color.White
			Controls.Add(newbox)
		Next

		'Show PictureBox
		For i = 1 To alarmTypes
			Dim pbox As PictureBox = CType(Controls("PictureBox" & i), PictureBox)
			pbox.Visible = True
		Next

		'Line Labels
		For i As Integer = 0 To nOfLines - 1 'Create labels and set properties
			newbox = New Label With {
				.Size = New Drawing.Size(64 + (alarmTypes * 54), 29),
				.Location = New Point(20, 50 + (i * 25)),
				.Font = New Font("Arial", 14),
				.TextAlign = ContentAlignment.MiddleLeft
			}
			' Draw alarm labels
			newbox.Name = "nameLabel" & i
			newbox.Text = lineLabels(i, 1)
			newbox.BorderStyle = BorderStyle.None
			newbox.BackColor = Color.White
			newbox.SendToBack()
			Controls.Add(newbox)
		Next


		' Set tooltips for line nubmers
		Dim toolTip1 As New ToolTip()
		For i = 0 To nOfLines - 1
			Dim myLabel As Label = CType(Controls("nameLabel" & i), Label)
			toolTip1.SetToolTip(myLabel, lineLabels(i, 2))
		Next

		nOfAlarms = 0

		For i = 0 To nOfLines - 1
			For j = 0 To alarmTypes - 1
				If lineStatusStr(i, j) = "Yellow" Then nOfAlarms += 1
				If lineStatusStr(i, j) = "Red" Then nOfAlarms += 2
			Next
		Next

		' Set everything to green
		For i = 0 To nOfLines - 1
			For j = 0 To alarmTypes - 1
				lineStatusStr(i, j) = "Green"
				previousLineStatusStr(i, j) = "Green"
			Next
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

	Private Sub UpdateFields(sender As Object, e As FileSystemEventArgs)
		' Update alarm fields according to the current text files

		' Copy initial lineStatus state to be able to find out later which lines were updated
		Dim i As Integer
		For i = 0 To nOfLines - 1
			For j = 0 To alarmTypes - 1
				previousLineStatusStr(i, j) = lineStatusStr(i, j)
			Next
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
					For i = 0 To alarmTypes - 1
						Try
							lineStatusStr(CInt(lineNumber), i) = inputFile.ReadLine()
						Catch ex As Exception

						End Try
					Next
					Try
						For i = 1 To alarmTypes
							Dim myLabel As Label = CType(Me.Controls("LineLabel" & lineNumber * alarmTypes + i - 1), Label)
							If lineStatusStr(CInt(lineNumber), i - 1) = "Green" Then
								myLabel.BackColor = Color.FromArgb(0, 255, 0)

							ElseIf lineStatusStr(CInt(lineNumber), i - 1) = "Yellow" Then
								myLabel.BackColor = Color.FromArgb(255, 192, 0)
							ElseIf lineStatusStr(CInt(lineNumber), i - 1) = "Red" Then
								myLabel.BackColor = Color.FromArgb(255, 0, 0)
							End If


							'Display time since last alarm
							If (lineStatusStr(CInt(lineNumber), i - 1) = "Yellow") And (previousLineStatusStr(CInt(lineNumber), i - 1) = "Green") Then   ' it's a new alarm
								alarmStartTime(CInt(lineNumber), i - 1) = Date.Now
								myLabel.Text = "0 min"
								myLabel.ForeColor = Color.Black
								PictureBox1.Select() ' Remove the cursor from updated field
							ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Red") And (previousLineStatusStr(CInt(lineNumber), i - 1) = "Yellow") Then   ' it's a new alarm
								alarmStartTime(CInt(lineNumber), i - 1) = Date.Now
								myLabel.Text = "0 min"
								myLabel.ForeColor = Color.White
								PictureBox1.Select() ' Remove the cursor from updated field
							ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Green" And previousLineStatusStr(CInt(lineNumber), i - 1) = "Red") Then  ' we're going from red to green
								myLabel.Text = ""
								PictureBox1.Select() ' Remove the cursor from updated field
							ElseIf (lineStatusStr(CInt(lineNumber), i - 1) = "Green" And previousLineStatusStr(CInt(lineNumber), i - 1) = "Green") Then  ' in case of initialization on startup, we musts remove all labels
								myLabel.Text = ""
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
		Dim alarmWorsened As Boolean = False
		nOfPreviousAlarms = nOfAlarms
		nOfAlarms = 0
		For i = 0 To nOfLines - 1
			For j = 0 To alarmTypes - 1
				If lineStatusStr(i, j) = "Yellow" Then nOfAlarms += 1
				If lineStatusStr(i, j) = "Red" Then nOfAlarms += 2
			Next
		Next
		If nOfAlarms > nOfPreviousAlarms Then
			Dim fpath As String = Application.StartupPath + "/Assets/alarm.wav"
			If soundOn Then My.Computer.Audio.Play(fpath, AudioPlayMode.Background)

			For i = 0 To nOfLines - 1
				' Identify the last alarm
				For j = 0 To alarmTypes - 1
					If (previousLineStatusStr(i, j) = "Green" And lineStatusStr(i, j) = "Yellow") Then alarmWorsened = True
					If (previousLineStatusStr(i, j) = "Yellow" And lineStatusStr(i, j) = "Red") Then alarmWorsened = True
					If (previousLineStatusStr(i, j) = "Green" And lineStatusStr(i, j) = "Red") Then alarmWorsened = True
				Next
				If alarmWorsened Then lastAlarmLineNr = i
			Next
			For i = 0 To nOfLines - 1
				' Remove previous marking of last alarm 
				Dim myLabel As Label = CType(Controls("nameLabel" & i), Label)
				myLabel.ForeColor = Color.FromArgb(0, 0, 0)
			Next
			' Mark last alarm
			Dim myLabel2 As Label = CType(Controls("nameLabel" & lastAlarmLineNr), Label)
			myLabel2.ForeColor = Color.FromArgb(255, 0, 0)
		End If

		' Remove last alarm label if all alarms have been solved
		'	For i = 0 To nOfLines - 1
		'Dim myLabel As Label = CType(Controls("nameLabel" & i), Label)
		'	If (lineStatusStr(i, 0) = "Green") And (lineStatusStr(i, 1) = "Green") And (lineStatusStr(i, 2) = "Green") And (lineStatusStr(i, 3) = "Green") And (lineStatusStr(i, 4) = "Green") Then myLabel.ForeColor = Color.FromArgb(0, 0, 0)
		'	Next

	End Sub


	Private Sub Watcher2_Changed(sender As Object, e As FileSystemEventArgs) Handles watcher2.Changed
		' If text file in the watched folder is created or rewritten, call update function
		If e.Name.Substring(0, 8) = "terminal" Then
			Try
				UpdateFields(sender, e)
			Catch ex As Exception
				System.Diagnostics.Debug.WriteLine("Exception : " + ex.StackTrace)
			End Try
		End If
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

		' Periodically update time since alarm was started
		Dim i, j As Integer
		For i = 0 To nOfLines - 1
			For j = 0 To alarmTypes - 1
				Dim myLabel As Label = CType(Controls("LineLabel" & i * alarmTypes + j), Label)
				If lineStatusStr(i, j) = "Yellow" Or lineStatusStr(i, j) = "Red" Then
					If DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) > 9 Then myLabel.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) Else myLabel.Text = DateDiff(DateInterval.Minute, alarmStartTime(i, j), DateTime.Now) & " min"
				End If
			Next
		Next

		' Mark priority lines
		For i = 0 To nOfLines - 1
			Dim myLbl As Label = CType(Controls("nameLabel" & i), Label)
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
					Dim prioLine As String = MyReader.ReadLine()
					For i = 0 To nOfLines - 1
						If lineLabels(i, 1) = prioLine Then
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
				Dim myLbl2 As Label = CType(Controls("nameLabel" & priorityLines(i)), Label)
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