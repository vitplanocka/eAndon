Imports System.IO

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


			'	PopulateAutoInfo()
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

		' Alarm labels
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

		For i = 0 To lineCount - 1
			For j = 0 To alarmTypes - 1
				lineStatusStr(i, j) = "Green"
			Next
		Next
		For i = 0 To lineCount * alarmTypes - 1
			Dim myBox As TextBox = CType(Me.Controls("TBox" & i + 1), TextBox)
			If myBox Is Nothing Then

			Else
				myBox.BackColor = Color.FromArgb(0, 255, 0)
				myBox.BorderStyle = BorderStyle.FixedSingle
			End If
		Next

		' Create initial text file
		'	Update_Fields(sender, e)
	End Sub

End Class
