'TODO:  
' 

'PROCESS MAP:
'
' - form startup:                                       - form closing:
'    AndonLoad                                             AndonClose
'    --> UpdateFields                                     --> LogAlarmInfo
'        --> LogAlarmInfo
'
' - alarm field clicked:                                - line label clicked
'    UpdateFields                                         LineLabelClicked
'    --> LogAlarmInfo
'
' - periodical automatical updates:
'    TimerLabelsTick     --> alarm durations
'    InfoBoxTimerTick    --> InfoBox text
'    --> UpdateFields
'        --> LogAlarmInfo

Imports System.IO

Public Class Form1
	Public terminalName As String = Reflection.Assembly.GetExecutingAssembly().GetName().Name    ' Get name of current file
	Public workstationCount As Integer = 0                   ' Number of lines displayed on this form
	Public alarmTypes As Integer = 0                         ' Number of displayed alarm types
	Public displayedLines(10) As Integer                     ' Numbers or lines from production_lines.txt displayed on this form
	Public alarmStartDateTime(10, 20) As DateTime            ' When alarm was pressed last time
	Public alarmEndDateTime(10, 20) As DateTime              ' When alarm was turned off
	Public workstationLabels(100, 3) As String               ' #, line number, line name, output file name 
	Public workstationStatus(20, 20) As String               ' Green, Yellow, Red
	Public alarmfile As String                               ' Alarm sound filename
	Public iconLbl(alarmTypes) As String                     ' Labels for alarm types
	Public iconImgFile(alarmTypes) As String                 ' Image filenames for alarm types
	Public alarmTypesString(4) As String                     ' Labels for the Alarm_type form
	Public greenName, yellowName, redName As String          ' Names for the green, yellow and red status

	ReadOnly AlarmTypeForm As New Alarm_type                 ' Initialize the Alarm_Type form
	ReadOnly AlOverview As New AlarmOverview                 ' Initialize the AlarmOverview form

	Private Sub AndonLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load  ' Things to do when app starts

		' Set form location on the screen to upper left corner
		Me.Location = New Point(100, 100)

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
			lineReader = MyReader.ReadLine().Split(":")
			Dim strArr(), instrText As String
			instrText = ""
			strArr = lineReader(1).ToString().Split("|")
			For i = 0 To strArr.Length - 1
				instrText = instrText & strArr(i) & vbCrLf
			Next
			TextBoxIns.Text = instrText
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

		' Read line labels from text file
		Using MyReader As New FileIO.TextFieldParser("Assets/Workstations_terminals.txt")

			MyReader.TextFieldType = FileIO.FieldType.Delimited
			MyReader.SetDelimiters(";")

			MyReader.ReadLine()          ' number of lines from configuration file
			MyReader.ReadLine()          ' Skip format legend
			Dim i As Integer = 0
			Dim currentRow As String()
			While Not MyReader.EndOfData
				Try
					' Read row as an array of strings 
					currentRow = MyReader.ReadFields()
					If currentRow(3) = terminalName & ".txt" Then
						' Parse line into strings '
						workstationLabels(i, 0) = currentRow(0)
						workstationLabels(i, 1) = currentRow(1)
						workstationLabels(i, 2) = currentRow(2)
						workstationLabels(i, 3) = currentRow(3)
						displayedLines(workstationCount) = CInt(currentRow(0))
						workstationCount += 1
					End If
					i += 1
				Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
					MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
				End Try
			End While
		End Using

		' Set positioning of dynamic labels and fields
		Dim originHor As Int16 = 65    ' origin of coordinates horizontal
		Dim originVer As Int16 = 35    ' origin of coordinates vertical
		Dim rectWidth As Int16 = 80    ' width of 1 alarm field
		Dim rectHeight As Int16 = 40   ' height of 1 alarm field
		Dim spacingHor As Int16 = rectWidth + 13   ' spacing between same corners of adjacent alarm fields
		Dim spacingVer As Int16 = rectHeight + 5   ' spacing between same corners of adjacent alarm fields

		' Adjust width and height of form
		Me.Size = New Size(300 + alarmTypes * spacingHor, 385 + workstationCount * spacingVer)
		TextBoxIns.Width = 200 + alarmTypes * spacingHor
		RichTextBox1.Width = 200 + alarmTypes * spacingHor

		' Create dynamic Label alarm descriptions
		Dim newbox3 As Label
		For i As Integer = 0 To alarmTypes - 1 'Create labels and set properties
			newbox3 = New Label With {
				.Size = New Drawing.Size(rectWidth, rectHeight),
				.Location = New Point(originHor + 150 + i * spacingHor, originVer + 110),
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
			Dim img = Image.FromFile(iconImgFile(i))
			newbox = New PictureBox With {
				.Size = New Drawing.Size(img.Size.Width, img.Size.Height),
				.Location = New Point(originHor + 160 + i * spacingHor, originVer + 50),
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
		For i As Integer = 0 To workstationCount * alarmTypes - 1 'Create labels and set properties
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
			AddHandler newbox2.MouseEnter, AddressOf LabelMouseEnter
			AddHandler newbox2.MouseLeave, AddressOf LabelMouseLeave
			AddHandler newbox2.Click, AddressOf UpdateFields
			Controls.Add(newbox2)
		Next

		' Create dynamic line labels
		Dim newbox4, newbox5 As Label
		y = 0
		lineNo = 1
		For i As Integer = 0 To workstationCount - 1 'Create labels and set properties
			newbox4 = New Label With {
				.Size = New Drawing.Size(rectWidth / 2 + 10, rectHeight),
				.Location = New Point(originHor - 30, originVer + 155 + i * spacingVer),
				.Font = New Font("Arial", 12, FontStyle.Bold),
				.TextAlign = ContentAlignment.MiddleLeft
			}
			newbox4.Name = "lineLabel" & i & "1"
			newbox4.Text = workstationLabels(displayedLines(i), 1)
			newbox4.BorderStyle = BorderStyle.FixedSingle
			newbox4.BackColor = Color.FromArgb(240, 240, 240)
			AddHandler newbox4.MouseEnter, AddressOf LabelMouseEnter
			AddHandler newbox4.MouseLeave, AddressOf LabelMouseLeave
			AddHandler newbox4.Click, AddressOf LineLabelClicked
			Controls.Add(newbox4)

			newbox5 = New Label With {
				.Size = New Drawing.Size(rectWidth + 35, rectHeight),
				.Location = New Point(originHor + 25, originVer + 155 + i * spacingVer),
				.Font = New Font("Arial", 12, FontStyle.Bold),
				.TextAlign = ContentAlignment.MiddleLeft
			}
			newbox5.Name = "lineLabel" & i & "2"
			newbox5.Text = workstationLabels(displayedLines(i), 2)
			newbox5.BorderStyle = BorderStyle.FixedSingle
			newbox5.BackColor = Color.FromArgb(240, 240, 240)
			Controls.Add(newbox5)
		Next

		' Set window text
		Me.Text = terminalName

		' Initialize everything to green
		For i = 0 To workstationCount - 1
			For j = 0 To alarmTypes - 1
				workstationStatus(i, j) = "Green"
			Next
		Next
		For i = 0 To workstationCount * alarmTypes - 1
			Dim myBox As Label = CType(Me.Controls("lineLabel" & i), Label)
			If myBox Is Nothing Then
			Else
				myBox.BackColor = Color.FromArgb(0, 255, 0)
				myBox.BorderStyle = BorderStyle.FixedSingle
			End If
		Next

		' Update text in InfoBox
		Try
			RichTextBox1.LoadFile("InfoTextForOperators.rtf", RichTextBoxStreamType.RichText)
		Catch ex As Exception
			System.Diagnostics.Debug.WriteLine("Exception : " + ex.StackTrace)
		End Try

		' Create initial text file
		UpdateFields(sender, e)
	End Sub

	Private Sub AndonClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closing
		Dim remainingAlarm As Boolean = False
		For i As Integer = 0 To workstationCount - 1
			For j As Integer = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				If workstationStatus(i, j) <> greenName Then    ' We are cancelling a yellow or red alarm
					workstationStatus(i, j) = greenName
					LogAlarmInfo(workstationLabels(displayedLines(i), 1), myBox.Name, workstationStatus(i, j), DateDiff(DateInterval.Second, alarmStartDateTime(i, j), Date.Now) / 60)
					remainingAlarm = True
				End If
			Next
		Next

		If remainingAlarm = True Then           ' If there was a remaining alarm during closing write alarm status to text file
			Try
				Using outputFile As New StreamWriter("Data/" & terminalName & ".txt")
					For i = 0 To workstationCount - 1
						outputFile.WriteLine(workstationLabels(displayedLines(i), 0))
						outputFile.WriteLine(workstationLabels(displayedLines(i), 1))
						Try
							For j = 0 To alarmTypes - 1
								outputFile.WriteLine(workstationStatus(i, j))
								outputFile.WriteLine(alarmStartDateTime(i, j).ToString("s"))  ' Save the alarm date and time in ISO format
							Next
						Catch ex As Exception
							Debug.WriteLine("Exception : " + ex.StackTrace)
						End Try
					Next
				End Using
				Exit Try
			Catch ex As Exception
				Debug.WriteLine("Exception : " + ex.StackTrace)
			End Try
		End If
	End Sub

	Private Sub LabelMouseEnter(sender As Object, e As System.EventArgs)
		sender.Cursor = Cursors.Hand
	End Sub

	Private Sub LabelMouseLeave(sender As Object, e As System.EventArgs)
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
		For i As Integer = 0 To workstationCount - 1
			Dim myBox As Label = CType(Me.Controls("lineLabel" & i & "1"), Label)
			If sender.Name = myBox.Name Then clickedWorkstationLine = i
		Next
		clickedWorkstationName = workstationLabels(displayedLines(clickedWorkstationLine), 1)
		AlOverview.Label1.Text = clickedWorkstationName
		AlOverview.Button1.Text = alarmTypesString(3)

		If File.Exists("Logs/alarmlog_" & terminalName & ".txt") Then
			' Read the alarm log and build up the history of alarms
			Dim s As String() = File.ReadAllLines("Logs/alarmlog_" & terminalName & ".txt")
			Dim alarmLogs(100000, 4) As String              ' Fill this array with alarms from the text file
			Dim workstationAlarmLogs(100000, 5) As String   ' Filter only alarms relevant for the workstation, calculate alarm durations
			Dim lineReader() As String
			Dim linesOfAlarms As Integer = s.GetUpperBound(0)
			Dim j As Integer = 0
			For i = 0 To linesOfAlarms
				lineReader = s(i).Split(";")
				For k = 0 To 4
					alarmLogs(i, k) = lineReader(k).Split("|").Last().Trim()
				Next
			Next

			Dim lineOfWorkstationAlarmlogs As Integer = 0
			Dim lineOfAlarmTypes As Integer() = {-1, -1, -1, -1, -1}
			For i = 0 To linesOfAlarms
				If alarmLogs(i, 1) = clickedWorkstationName Then  ' Does the line in alarmlog belong to the workstation that was clicked?
					If alarmLogs(i, 3) <> greenName Then            ' It's a beginning of an alarm
						For k = 0 To 3
							workstationAlarmLogs(lineOfWorkstationAlarmlogs, k) = alarmLogs(i, k)
						Next
						workstationAlarmLogs(lineOfWorkstationAlarmlogs, 4) = DateDiff(DateInterval.Second, DateTime.ParseExact(workstationAlarmLogs(lineOfWorkstationAlarmlogs, 0), "s", Nothing), Date.Now)
						workstationAlarmLogs(lineOfWorkstationAlarmlogs, 5) = "Open alarm"
						lineOfAlarmTypes(alarmLogs(i, 2)) = lineOfWorkstationAlarmlogs    ' Increase the appropriate alarm type to the latest line
						lineOfWorkstationAlarmlogs += 1
					Else                                           ' It's a finish of an existing alarm
						If lineOfAlarmTypes(alarmLogs(i, 2)) > -1 Then
							workstationAlarmLogs(lineOfAlarmTypes(alarmLogs(i, 2)), 4) = Math.Round(CDbl(alarmLogs(i, 4)), 1)
							workstationAlarmLogs(lineOfAlarmTypes(alarmLogs(i, 2)), 5) = "Closed alarm"
							lineOfAlarmTypes(alarmLogs(i, 2)) += 1
						End If
					End If
				End If
			Next

			' Write the text to RichTextBox and format it
			For i = 0 To lineOfWorkstationAlarmlogs - 1
				If workstationAlarmLogs(i, 5) = "Closed alarm" Then       ' Write start and end time, total duration
					FormatInRich(AlOverview.RichTextBox1, "bold", (DateTime.ParseExact(workstationAlarmLogs(i, 0), "s", Nothing).ToString("dd.MM.yyyy HH:mm - ") & DateTime.ParseExact(workstationAlarmLogs(i, 0), "s", Nothing).AddSeconds(workstationAlarmLogs(i, 4)).ToString("HH:mm") & " (" & CInt(workstationAlarmLogs(i, 4) / 60) & " min)").PadRight(40))
				Else                                                      ' Write only the start time and current duration
					FormatInRich(AlOverview.RichTextBox1, "bold", (DateTime.ParseExact(workstationAlarmLogs(i, 0), "s", Nothing).ToString("dd.MM.yyyy HH:mm - ") & " (" & CInt(workstationAlarmLogs(i, 4) / 60) & " min)").PadRight(40))
				End If
				FormatInRich(AlOverview.RichTextBox1, "regular", iconLbl(workstationAlarmLogs(i, 2)).PadRight(35))
				If workstationAlarmLogs(i, 3) = redName Then
					FormatInRich(AlOverview.RichTextBox1, "red", workstationAlarmLogs(i, 3))
				ElseIf workstationAlarmLogs(i, 3) = yellowName Then
					FormatInRich(AlOverview.RichTextBox1, "yellow", workstationAlarmLogs(i, 3))
				End If
				AlOverview.RichTextBox1.AppendText(vbNewLine)
			Next
		Else  ' The alarm log doesn't exist
			AlOverview.RichTextBox1.AppendText("===  no records found ===")
		End If

		AlOverview.ShowDialog()
	End Sub

	Private Sub LogAlarmInfo(line As String, controlObj As String, color As String, alarmLength As Double)
		Try
			Using outputFile As New StreamWriter("Logs/alarmlog_" & terminalName & ".txt", True)
				Dim sb As New System.Text.StringBuilder
				sb.Append("Event DateTime | " & DateTime.Now.ToString("s") & ";")  ' Save the alarm date and time in ISO format
				sb.Append(" Workstation | " & line & ";")
				sb.Append(" Alarm type | " & CInt(controlObj.Remove(0, 9)) Mod alarmTypes & ";")
				sb.Append(" New Color | " & color & ";")
				sb.Append(" Length of alarm (seconds) | " & CInt(alarmLength * 60))
				outputFile.WriteLine(sb.ToString())
			End Using
			Exit Try
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try

	End Sub

	Private Sub UpdateFields(sender As Object, e As EventArgs)
		' Alarm was clicked, update the fields and export text file

		' If alarm is switched on, set starting time in alarmStartTime and write initial label in text box
		For i As Integer = 0 To workstationCount - 1
			For j As Integer = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				If sender Is myBox Then

					If workstationStatus(i, j) <> "Green" Then    ' We are cancelling a yellow or red alarm
						workstationStatus(i, j) = "Green"
						alarmEndDateTime(i, j) = DateTime.Now
						myBox.Text = ""
						PictureBoxLogo.Select()
						LogAlarmInfo(workstationLabels(displayedLines(i), 1), myBox.Name, workstationStatus(i, j), DateDiff(DateInterval.Second, alarmStartDateTime(i, j), alarmEndDateTime(i, j)) / 60)
					Else                                          ' We are switching from green to yellow or red alarm
						AlarmTypeForm.StartPosition = FormStartPosition.CenterParent
						AlarmTypeForm.ShowDialog()
						If AlarmTypeForm.DialogResult = DialogResult.OK Then
							If AlarmTypeForm.YellowOrRed = "Yellow" Then
								workstationStatus(i, j) = "Yellow"
								alarmStartDateTime(i, j) = DateTime.Now
								myBox.Text = "0 min"
								myBox.ForeColor = Color.Black
								PictureBoxLogo.Select()
								LogAlarmInfo(workstationLabels(displayedLines(i), 1), myBox.Name, workstationStatus(i, j), 0)
							End If
							If AlarmTypeForm.YellowOrRed = "Red" Then
								workstationStatus(i, j) = "Red"
								alarmStartDateTime(i, j) = DateTime.Now
								myBox.Text = "0 min"
								PictureBoxLogo.Select()
								myBox.ForeColor = Color.White
								LogAlarmInfo(workstationLabels(displayedLines(i), 1), myBox.Name, workstationStatus(i, j), 0)
							End If
						End If
					End If
				End If
			Next
		Next

		' Update background colours of TextBoxes
		For i = 0 To workstationCount - 1
			For j = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				If myBox Is Nothing Then
				Else
					If workstationStatus(i, j) = "Green" Then
						myBox.BackColor = Color.FromArgb(0, 255, 0)
					ElseIf workstationStatus(i, j) = "Yellow" Then
						myBox.BackColor = Color.FromArgb(255, 192, 0)
					ElseIf workstationStatus(i, j) = "Red" Then
						myBox.BackColor = Color.FromArgb(255, 0, 0)
					End If
				End If
			Next
		Next

		' Write alarm status to text file
		Try
			Using outputFile As New StreamWriter("Data/" & terminalName & ".txt")
				For i = 0 To workstationCount - 1
					outputFile.WriteLine(workstationLabels(displayedLines(i), 0))
					outputFile.WriteLine(workstationLabels(displayedLines(i), 1))
					Try
						For j = 0 To alarmTypes - 1
							outputFile.WriteLine(workstationStatus(i, j))
							outputFile.WriteLine(alarmStartDateTime(i, j).ToString("s"))  ' Save the alarm date and time in ISO format
						Next
					Catch ex As Exception

					End Try
				Next
			End Using
			Exit Try
		Catch ex As Exception
			Threading.Thread.Sleep(100)
			UpdateFields(sender, e)
		End Try
	End Sub

	Private Sub TimerLabelsTick(sender As Object, e As EventArgs) Handles Timer1.Tick
		' Periodically update the labels for time since alarm was activated
		Dim i, j As Integer
		For i = 0 To workstationCount - 1
			For j = 0 To alarmTypes - 1
				Dim myBox As Label = CType(Me.Controls("lineLabel" & i * alarmTypes + j), Label)
				Try
					If workstationStatus(i, j) = "Yellow" Or workstationStatus(i, j) = "Red" Then myBox.Text = "" & DateDiff(DateInterval.Minute, alarmStartDateTime(i, j), DateTime.Now) & " min"
				Catch ex As Exception
				End Try
			Next
		Next
	End Sub

	Private Sub InfoBoxTimerTick(sender As Object, e As EventArgs) Handles Timer2.Tick
		' Periodically refresh the text files so that the status remains visible to the dashboard
		UpdateFields(sender, e)
		' Update text in InfoBox
		Try
			RichTextBox1.LoadFile("InfoTextForOperators.rtf", RichTextBoxStreamType.RichText)
		Catch ex As Exception
			System.Diagnostics.Debug.WriteLine("Exception : " + ex.StackTrace)
		End Try
	End Sub

End Class
