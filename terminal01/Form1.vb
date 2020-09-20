Imports System.IO

'TODO:  
' - logo doesn't show up
' - fix  timer tick exception
' - make alarm fields dynamic

Public Class Form1
	Public terminalName As String = Reflection.Assembly.GetExecutingAssembly().GetName().Name    ' Get name of current file
	Public lineCount As Integer = 0                   ' Number of lines displayed on this form
	Public alarmTypes As Integer = 0                  ' Number of displayed alarm types
	Public displayedLines(10) As Integer              ' Numbers or lines from production_lines.txt displayed on this form
	Public alarmStartDateTime(10, 20) As DateTime     ' When alarm was pressed last time
	Public alarmEndDateTime(10, 20) As DateTime       ' When alarm was turned off
	Public noOfColors As Integer = 2                  ' default is 2 but we will read it from settings file
	Public lineLabels(100, 3) As String               ' #, line number, line name, output file name 
	Public lineStatusStr(20, 20) As String            ' Green, Yellow, Red
	Public logofile As String
	Public alarmfile As String
	Public instructions As String
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

	Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load  ' Things to do when app starts

		'Check Directory is available or not.
		If (Directory.Exists(Application.StartupPath & "/Data") = False) Then
			Directory.CreateDirectory(Application.StartupPath & "/Data")
		End If

		' Read Settings from Text File
		Dim rootpath As String = Application.StartupPath
		Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/settings.txt")
			' Number of alarm types to display
			Dim lineReader() As String = MyReader.ReadLine().Split(":")
			alarmTypes = CInt(lineReader(1).ToString().Trim().TrimStart())
			' Number of status colours
			lineReader = MyReader.ReadLine().Split(":")
			noOfColors = CInt(lineReader(1).ToString().Trim().TrimStart())
			' Image file for company logo
			lineReader = MyReader.ReadLine().Split(":")
			PictureBoxLogo.ImageLocation = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
			' Alarm sound file
			lineReader = MyReader.ReadLine().Split(":")
			alarmfile = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
			' Terminal usage instruction
			instructions = MyReader.ReadLine()
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
			icon1imgfile = MyReader.ReadLine()
			icon2imgfile = MyReader.ReadLine()
			icon3imgfile = MyReader.ReadLine()
			icon4imgfile = MyReader.ReadLine()
			icon5imgfile = MyReader.ReadLine()
			icon6imgfile = MyReader.ReadLine()
			icon7imgfile = MyReader.ReadLine()
			icon8imgfile = MyReader.ReadLine()
			icon9imgfile = MyReader.ReadLine()
			icon10imgfile = MyReader.ReadLine()

			PopulateAutoInfo()
		End Using

		' Read line labels from text file
		Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/production_lines.txt")

			MyReader.TextFieldType = FileIO.FieldType.Delimited
			MyReader.SetDelimiters(";")

			MyReader.ReadLine()          ' number of lines from configuration file
			Dim i As Integer = 0

			Dim currentRow As String()
			While Not MyReader.EndOfData
				Try
					' Read row as an array of strings 
					currentRow = MyReader.ReadFields()
					If currentRow(3) = terminalName & ".txt" Then
						' Parse line into strings '
						lineLabels(i, 0) = currentRow(0)
						lineLabels(i, 1) = currentRow(1)
						lineLabels(i, 2) = currentRow(2)
						lineLabels(i, 3) = currentRow(3)
						displayedLines(lineCount) = CInt(currentRow(0))
						lineCount += 1
					End If
					i += 1
				Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
					MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
				End Try
			End While
		End Using

		' Create dynamic alarm labels, add handlers
		Dim newbox As Label
		Dim y As Int32 = 0
		Dim lineNo As Int32 = 1
		For i As Integer = 0 To (lineCount * alarmTypes) - 1 'Create labels and set properties
			'set Y
			If (i / lineNo) = alarmTypes Then
				y += 25
				lineNo += 1
			End If

			newbox = New Label With {
				.Size = New Drawing.Size(40, 20),
				.Location = New Point(100 + (i Mod alarmTypes) * 53, 355 + y),
				.Font = New Font("Arial", 8),
				.TextAlign = ContentAlignment.MiddleCenter
			}
			' Draw alarm labels
			newbox.Name = "lineLabel" & i
			newbox.Text = ""
			newbox.BorderStyle = BorderStyle.FixedSingle
			newbox.BackColor = Color.White
			AddHandler newbox.MouseEnter, AddressOf Label_MouseEnter
			AddHandler newbox.MouseLeave, AddressOf Label_MouseLeave
			AddHandler newbox.Click, AddressOf Label_Click
			Controls.Add(newbox)
		Next


		' Set window text
		Me.Text = terminalName

		' Update line labels in form
		For i = 0 To lineCount - 1
			Dim myBox As TextBox = CType(Me.Controls("TextLbl" & i + 1 & "1"), TextBox)
			If myBox Is Nothing Then
			Else
				myBox.Text = lineLabels(displayedLines(i), 1)
				myBox = CType(Me.Controls("TextLbl" & i + 1 & "2"), TextBox)
				myBox.Text = lineLabels(displayedLines(i), 2)
			End If
		Next

		' Initialize everything to green
		For i = 0 To lineCount - 1
			For j = 0 To alarmTypes - 1
				lineStatusStr(i, j) = "Green"
			Next
		Next
		For i = 0 To lineCount * alarmTypes - 1
			Dim myBox As Label = CType(Me.Controls("lineLabel" & i), Label)
			If myBox Is Nothing Then
			Else
				myBox.BackColor = Color.FromArgb(0, 255, 0)
				myBox.BorderStyle = BorderStyle.FixedSingle
			End If
		Next

		' Create initial text file
		Update_Fields(sender, e)
	End Sub
	Private Sub PopulateAutoInfo()
		PictureBoxLogo.ImageLocation = logofile
		TextBoxIns.Text = instructions
		TextBox1.Text = IIf(icon1lbl = "NA", "", icon1lbl)
		TextBox2.Text = IIf(icon2lbl = "NA", "", icon2lbl)
		TextBox3.Text = IIf(icon3lbl = "NA", "", icon3lbl)
		TextBox4.Text = IIf(icon4lbl = "NA", "", icon4lbl)
		TextBox5.Text = IIf(icon5lbl = "NA", "", icon5lbl)
		TextBox6.Text = IIf(icon6lbl = "NA", "", icon6lbl)
		TextBox7.Text = IIf(icon7lbl = "NA", "", icon7lbl)
		TextBox8.Text = IIf(icon8lbl = "NA", "", icon8lbl)
		TextBox9.Text = IIf(icon9lbl = "NA", "", icon9lbl)
		TextBox10.Text = IIf(icon10lbl = "NA", "", icon10lbl)

		PictureBox1.ImageLocation = icon1imgfile
		PictureBox2.ImageLocation = icon2imgfile
		PictureBox3.ImageLocation = icon3imgfile
		PictureBox4.ImageLocation = icon4imgfile
		PictureBox5.ImageLocation = icon5imgfile
		PictureBox6.ImageLocation = icon6imgfile
		PictureBox7.ImageLocation = icon7imgfile
		PictureBox8.ImageLocation = icon8imgfile
		PictureBox9.ImageLocation = icon9imgfile
		PictureBox10.ImageLocation = icon10imgfile

		'Show Textbox
		For i = 1 To alarmTypes
			Dim txtbox As TextBox = CType(Controls("TextBox" & i), TextBox)
			txtbox.Visible = True
		Next

		'Show TBox
		For i = 1 To alarmTypes
			Dim txtbox As TextBox = CType(Controls("TBox" & i), TextBox)
			txtbox.Visible = True
		Next

		'Show TBox
		For i = 11 To (alarmTypes + 10)
			Dim txtbox As TextBox = CType(Controls("TBox" & i), TextBox)
			txtbox.Visible = True
		Next

		'Show PictureBox
		For i = 1 To alarmTypes
			Dim picbox As PictureBox = CType(Controls("PictureBox" & i), PictureBox)
			picbox.Visible = True
		Next
	End Sub

	Private Sub Label_Click(sender As Object, e As System.EventArgs)
		sender.Cursor = Cursors.Hand
		' If button is clicked, update the fields and change colour
		Update_Fields(sender, e)
	End Sub

	Private Sub Label_MouseEnter(sender As Object, e As System.EventArgs)
		sender.Cursor = Cursors.Hand
	End Sub

	Private Sub Label_MouseLeave(sender As Object, e As System.EventArgs)
		sender.Cursor = Cursors.Default
	End Sub


	Private Sub LogAlarmInfo(line As String, controlObj As String, color As String, alarmLength As Long)
		Try
			Dim alarmType As Integer
			Using outputFile As New StreamWriter("Data/alarmlog_" & terminalName & ".txt", True)
				Dim sb As New System.Text.StringBuilder
				If (CInt(controlObj.Remove(0, 9)) Mod alarmTypes) = 0 Then
					alarmType = alarmTypes
				Else alarmType = (CInt(controlObj.Remove(0, 9)) Mod alarmTypes)
				End If
				sb.Append("Event DateTime : " & DateTime.Now & ";")
				sb.Append(" Line : " & line & ";")
				sb.Append(" Alarm type : " & alarmType & ";")
				sb.Append(" New Color : " & color & ";")
				sb.Append(" Length of alarm (minutes) : " & alarmLength)
				outputFile.WriteLine(sb.ToString())
			End Using
			Exit Try
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try

	End Sub
	Private Sub Update_Fields(sender As Object, e As EventArgs)
		' Alarm was clicked, update the fields and export text file

		' If alarm is switched on, set starting time in alarmStartTime and write initial label in text box
		For i As Integer = 0 To lineCount - 1
			For j As Integer = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				If sender Is myBox Then
					If noOfColors = 3 Then
						If lineStatusStr(i, j) = "Red" Then
							lineStatusStr(i, j) = "Green"
						ElseIf lineStatusStr(i, j) = "Green" Then
							lineStatusStr(i, j) = "Yellow"
						ElseIf lineStatusStr(i, j) = "Yellow" Then
							lineStatusStr(i, j) = "Red"
						End If
					ElseIf noOfColors = 2 Then
						If lineStatusStr(i, j) = "Red" Then
							lineStatusStr(i, j) = "Green"
						ElseIf lineStatusStr(i, j) = "Green" Then
							lineStatusStr(i, j) = "Red"
						End If
					End If

					If lineStatusStr(i, j) = "Red" Then
						If noOfColors = 2 Then                     ' in case we switch from yellow to red, we continue the yellow alarm length 
							alarmStartDateTime(i, j) = DateTime.Now
							myBox.Text = "0 min"
							PictureBox1.Select()
						End If
						myBox.ForeColor = Color.White
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), 0)

					ElseIf lineStatusStr(i, j) = "Yellow" Then
						alarmStartDateTime(i, j) = DateTime.Now
						myBox.Text = "0 min"
						myBox.ForeColor = Color.Black
						PictureBox1.Select()
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), 0)

					ElseIf lineStatusStr(i, j) = "Green" Then
						alarmEndDateTime(i, j) = DateTime.Now
						myBox.Text = ""
						PictureBox1.Select()
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), DateDiff(DateInterval.Minute, alarmStartDateTime(i, j), alarmEndDateTime(i, j)))
					Else
						myBox.Text = ""
						PictureBox1.Select()
					End If
				End If
			Next
		Next

		' Update background colours of TextBoxes
		For i = 0 To lineCount - 1
			For j = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				If myBox Is Nothing Then
				Else
					If lineStatusStr(i, j) = "Green" Then
						myBox.BackColor = Color.FromArgb(0, 255, 0)
					ElseIf lineStatusStr(i, j) = "Yellow" Then
						myBox.BackColor = Color.FromArgb(255, 192, 0)
					ElseIf lineStatusStr(i, j) = "Red" Then
						myBox.BackColor = Color.FromArgb(255, 0, 0)
					End If
				End If
			Next
		Next

		' Write alarm status to text file
		Try

			Using outputFile As New StreamWriter("Data/" & terminalName & ".txt")
				For i = 0 To lineCount - 1
					outputFile.WriteLine(lineLabels(displayedLines(i), 0))
					outputFile.WriteLine(lineLabels(displayedLines(i), 1))
					Try
						For j = 0 To alarmTypes - 1
							outputFile.WriteLine(lineStatusStr(i, j))
						Next
					Catch ex As Exception

					End Try
				Next
			End Using
			Exit Try
		Catch ex As Exception
			Threading.Thread.Sleep(100)
			Update_Fields(sender, e)
		End Try

	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		' Periodically update the text file and the labels for time since alarm was activated
		Update_Fields(sender, e)
		Dim i, j As Integer
		For i = 0 To lineCount - 1
			For j = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				Try
					If lineStatusStr(i, j) = "Yellow" Or lineStatusStr(i, j) = "Red" Then myBox.Text = "" & DateDiff(DateInterval.Minute, alarmStartDateTime(i, j), DateTime.Now) & " min"
				Catch ex As Exception
				End Try
			Next
		Next
	End Sub

End Class
