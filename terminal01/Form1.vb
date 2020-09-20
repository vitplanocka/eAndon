Imports System.IO

'TODO:  
' - 

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
	Public alarmfile As String                        ' alarm sound filename
	Public iconLbl(alarmTypes) As String              ' labels for alarm types
	Public iconImgFile(alarmTypes) As String          ' image filenames for alarm types   

	Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load  ' Things to do when app starts

		'Check Directory is available or not.
		If (Directory.Exists(Application.StartupPath & "/Data") = False) Then
			Directory.CreateDirectory(Application.StartupPath & "/Data")
		End If

		' Read Settings from Text File
		Dim rootpath As String = Application.StartupPath
		Using MyReader As New FileIO.TextFieldParser("Assets/settings.txt")
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
			lineReader = MyReader.ReadLine().Split(":")
			TextBoxIns.Text = lineReader(1).ToString().Trim().TrimStart()
			' Alarm type descriptions and icons
			ReDim iconLbl(alarmTypes)
			ReDim iconImgFile(alarmTypes)
			For i = 0 To alarmTypes
				lineReader = MyReader.ReadLine().Split(":")
				iconLbl(i) = lineReader(1).ToString().Trim().TrimStart()
				lineReader = MyReader.ReadLine().Split(":")
				iconImgFile(i) = "Assets/" & lineReader(1).ToString().Trim().TrimStart()
			Next
		End Using

		' Read line labels from text file
		Using MyReader As New FileIO.TextFieldParser("Assets/production_lines.txt")

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

		' Set positioning of dynamic labels and fields
		Dim originHor As Int16 = 50    ' origin of coordinates horizontal
		Dim originVer As Int16 = 10    ' origin of coordinates vertical
		Dim rectWidth As Int16 = 80    ' width of 1 alarm field
		Dim rectHeight As Int16 = 40   ' height of 1 alarm field
		Dim spacingHor As Int16 = rectWidth + 13   ' spacing between same corners of adjacent alarm fields
		Dim spacingVer As Int16 = rectHeight + 5   ' spacing between same corners of adjacent alarm fields

		' Create dynamic Label alarm descriptions
		Dim newbox3 As Label
		For i As Integer = 0 To alarmTypes - 1 'Create labels and set properties
			newbox3 = New Label With {
				.Size = New Drawing.Size(rectWidth, rectHeight + 20),
				.Location = New Point(originHor + 150 + i * spacingHor, originVer + 45),
				.TextAlign = ContentAlignment.MiddleCenter,
				.Font = New Font("Arial", 8, FontStyle.Bold),
				.Text = iconLbl(i)
			}
			' Draw alarm labels
			newbox3.Name = "alarmDescription" & i
			newbox3.BorderStyle = BorderStyle.None
			Controls.Add(newbox3)
		Next
		PictureBoxLogo.Select()  ' Set focus on logo to prevent selecting text of TextBoxes

		' Create dynamic PictureBox alarm labels
		Dim newbox As PictureBox
		For i As Integer = 0 To alarmTypes - 1 'Create labels and set properties
			newbox = New PictureBox With {
				.Size = New Drawing.Size(rectWidth, rectHeight + 10),
				.Location = New Point(originHor + 150 + i * spacingHor, originVer + 100),
				.ImageLocation = iconImgFile(i),
				.SizeMode = PictureBoxSizeMode.StretchImage
			}
			' Draw alarm labels
			newbox.Name = "alarmLabel" & i
			newbox.BorderStyle = BorderStyle.None
			Controls.Add(newbox)
		Next

		' Create dynamic alarm labels, add handlers
		Dim newbox2 As Label
		Dim y As Int32 = 0
		Dim lineNo As Int32 = 1
		For i As Integer = 0 To lineCount * alarmTypes - 1 'Create labels and set properties
			'set vertical offset
			If (i / lineNo) = alarmTypes Then
				y += spacingVer
				lineNo += 1
			End If

			newbox2 = New Label With {
				.Size = New Drawing.Size(rectWidth, rectHeight),
				.Location = New Point(originHor + 150 + (i Mod alarmTypes) * spacingHor, originVer + 155 + y),
				.Font = New Font("Arial", 11, FontStyle.Bold),
				.TextAlign = ContentAlignment.MiddleCenter
			}
			' Draw alarm labels
			newbox2.Name = "lineLabel" & i
			newbox2.Text = ""
			newbox2.BorderStyle = BorderStyle.FixedSingle
			newbox2.BackColor = Color.White
			AddHandler newbox2.MouseEnter, AddressOf Label_MouseEnter
			AddHandler newbox2.MouseLeave, AddressOf Label_MouseLeave
			AddHandler newbox2.Click, AddressOf Label_Click
			Controls.Add(newbox2)
		Next

		' Create dynamic line labels
		Dim newbox4, newbox5 As Label
		y = 0
		lineNo = 1
		For i As Integer = 0 To lineCount - 1 'Create labels and set properties
			newbox4 = New Label With {
				.Size = New Drawing.Size(rectWidth, rectHeight),
				.Location = New Point(originHor - 30, originVer + 155 + i * spacingVer),
				.Font = New Font("Arial", 12, FontStyle.Bold),
				.TextAlign = ContentAlignment.MiddleLeft
			}
			' Draw line labels
			newbox4.Name = "lineLabel" & i & "1"
			newbox4.Text = lineLabels(displayedLines(i), 1)
			newbox4.BorderStyle = BorderStyle.None
			newbox4.BackColor = Color.White
			Controls.Add(newbox4)

			newbox5 = New Label With {
				.Size = New Drawing.Size(rectWidth + 50, rectHeight),
				.Location = New Point(originHor + 20, originVer + 155 + i * spacingVer),
				.Font = New Font("Arial", 12, FontStyle.Bold),
				.TextAlign = ContentAlignment.MiddleCenter
			}
			' Draw line labels
			newbox5.Name = "lineLabel" & i & "2"
			newbox5.Text = lineLabels(displayedLines(i), 2)
			newbox5.BorderStyle = BorderStyle.None
			newbox5.BackColor = Color.White
			Controls.Add(newbox5)
		Next

		' Set window text
		Me.Text = terminalName

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
							PictureBoxLogo.Select()
						End If
						myBox.ForeColor = Color.White
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), 0)

					ElseIf lineStatusStr(i, j) = "Yellow" Then
						alarmStartDateTime(i, j) = DateTime.Now
						myBox.Text = "0 min"
						myBox.ForeColor = Color.Black
						PictureBoxLogo.Select()
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), 0)

					ElseIf lineStatusStr(i, j) = "Green" Then
						alarmEndDateTime(i, j) = DateTime.Now
						myBox.Text = ""
						PictureBoxLogo.Select()
						LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), DateDiff(DateInterval.Minute, alarmStartDateTime(i, j), alarmEndDateTime(i, j)))
					Else
						myBox.Text = ""
						PictureBoxLogo.Select()
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
