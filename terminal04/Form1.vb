Imports System.IO

Public Class Form1
    Public terminalName As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name    ' Get name of current file
    Public lineCount As Integer = 0                   ' Number of lines displayed on this form
    Public displayedLines(10) As Integer              ' Numbers or lines from linky.txt displayed on this form
    Public alarmStartDateTime(10, 4) As DateTime      ' When alarm was pressed last time
    Public alarmEndDateTime(10, 4) As DateTime        ' When alarm was turned off
    Public noOfColors As Integer = 2                  ' default is 2 but we will read it from settings file
    Public lineLabels(100, 3) As String               ' #, line number, line name, output file name 
    Public lineStatusStr(10, 4) As String             ' Green, Yellow, Red
    Public logofile As String
    Public alarmfile As String
    Public instructions As String
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


    Private Sub Andon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load  ' Things to do when app starts


        ' Read Settings from Text File
        Dim rootpath As String = Application.StartupPath
        Using MyReader2 As New Microsoft.VisualBasic.FileIO.TextFieldParser("Assets/settings.txt")
            logofile = MyReader2.ReadLine()
            alarmfile = MyReader2.ReadLine()
            instructions = MyReader2.ReadLine()
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
            Try
                noOfColors = CInt(MyReader2.ReadLine()) 'read the next line for no. of color schemes to be used
            Catch ex As Exception
                'if there is an error reading no of colors then we will keep it default (2)
            End Try
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
                        lineCount = lineCount + 1
                    End If
                    i += 1
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

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
            For j = 0 To lineStatusStr.GetLength(1) - 1
                lineStatusStr(i, j) = "Green"
            Next
        Next
        For i = 0 To lineCount * 5 - 1
            Dim myBox As TextBox = CType(Me.Controls("TBox" & i + 1), TextBox)
            If myBox Is Nothing Then

            Else
                myBox.BackColor = Color.FromArgb(0, 255, 0)
                myBox.BorderStyle = BorderStyle.FixedSingle
            End If

        Next

        ' Create initial text file
        Button1_Click(sender, e)
    End Sub
    Private Sub PopulateAutoInfo()
        PictureBox11.ImageLocation = logofile
        TextBox8.Text = instructions
        TextBox1.Text = icon1lbl
        TextBox2.Text = icon2lbl
        TextBox3.Text = icon3lbl
        TextBox4.Text = icon4lbl
        TextBox5.Text = icon5lbl
        PictureBox1.ImageLocation = icon1imgfile
        PictureBox2.ImageLocation = icon2imgfile
        PictureBox3.ImageLocation = icon3imgfile
        PictureBox4.ImageLocation = icon4imgfile
        PictureBox5.ImageLocation = icon5imgfile
    End Sub

    Private Sub Tbox1_Click(sender As Object, e As EventArgs) Handles TBox1.Click, TBox2.Click, TBox3.Click, TBox4.Click, TBox5.Click, TBox6.Click, TBox7.Click, TBox8.Click, TBox9.Click, TBox10.Click _
                                                                    , TBox11.Click, TBox12.Click, TBox13.Click, TBox14.Click, TBox15.Click, TBox16.Click, TBox17.Click, TBox18.Click, TBox19.Click, TBox20.Click _
                                                                    , TBox21.Click, TBox22.Click, TBox23.Click, TBox24.Click, TBox25.Click, TBox26.Click, TBox27.Click, TBox28.Click, TBox29.Click, TBox30.Click _
                                                                    , TBox31.Click, TBox32.Click, TBox33.Click, TBox34.Click, TBox35.Click, TBox36.Click, TBox37.Click, TBox38.Click, TBox39.Click, TBox40.Click _
                                                                    , TBox41.Click, TBox42.Click, TBox43.Click, TBox44.Click, TBox45.Click, TBox46.Click, TBox47.Click, TBox48.Click, TBox49.Click, TBox50.Click
        ' If button is clicked, update the fields and change colour
        Button1_Click(sender, e)
    End Sub

    Private Sub Tbox1_MouseEnter(sender As Object, e As System.EventArgs) Handles TBox1.MouseEnter, TBox2.MouseEnter, TBox3.MouseEnter, TBox4.MouseEnter, TBox5.MouseEnter, TBox6.MouseEnter, TBox7.MouseEnter, TBox8.MouseEnter, TBox9.MouseEnter, TBox10.MouseEnter _
                                                                    , TBox11.MouseEnter, TBox12.MouseEnter, TBox13.MouseEnter, TBox14.MouseEnter, TBox15.MouseEnter, TBox16.MouseEnter, TBox17.MouseEnter, TBox18.MouseEnter, TBox19.MouseEnter, TBox20.MouseEnter _
                                                                    , TBox21.MouseEnter, TBox22.MouseEnter, TBox23.MouseEnter, TBox24.MouseEnter, TBox25.MouseEnter, TBox26.MouseEnter, TBox27.MouseEnter, TBox28.MouseEnter, TBox29.MouseEnter, TBox30.MouseEnter _
                                                                    , TBox31.MouseEnter, TBox32.MouseEnter, TBox33.MouseEnter, TBox34.MouseEnter, TBox35.MouseEnter, TBox36.MouseEnter, TBox37.MouseEnter, TBox38.MouseEnter, TBox39.MouseEnter, TBox40.MouseEnter _
                                                                    , TBox41.MouseEnter, TBox42.MouseEnter, TBox43.MouseEnter, TBox44.MouseEnter, TBox45.MouseEnter, TBox46.MouseEnter, TBox47.MouseEnter, TBox48.MouseEnter, TBox49.MouseEnter, TBox50.MouseEnter

        For i = 0 To lineCount * 5 - 1
            Dim myBox As TextBox = CType(Me.Controls("TBox" & i + 1), TextBox)
            If myBox Is Nothing Then

            Else
                myBox.Cursor = Cursors.Hand
            End If

        Next
    End Sub
    Private Sub Tbox1_MouseLeave(sender As Object, e As System.EventArgs) Handles TBox1.MouseLeave, TBox2.MouseLeave, TBox3.MouseLeave, TBox4.MouseLeave, TBox5.MouseLeave, TBox6.MouseLeave, TBox7.MouseLeave, TBox8.MouseLeave, TBox9.MouseLeave, TBox10.MouseLeave _
                                                                    , TBox11.MouseLeave, TBox12.MouseLeave, TBox13.MouseLeave, TBox14.MouseLeave, TBox15.MouseLeave, TBox16.MouseLeave, TBox17.MouseLeave, TBox18.MouseLeave, TBox19.MouseLeave, TBox20.MouseLeave _
                                                                    , TBox21.MouseLeave, TBox22.MouseLeave, TBox23.MouseLeave, TBox24.MouseLeave, TBox25.MouseLeave, TBox26.MouseLeave, TBox27.MouseLeave, TBox28.MouseLeave, TBox29.MouseLeave, TBox30.MouseLeave _
                                                                    , TBox31.MouseLeave, TBox32.MouseLeave, TBox33.MouseLeave, TBox34.MouseLeave, TBox35.MouseLeave, TBox36.MouseLeave, TBox37.MouseLeave, TBox38.MouseLeave, TBox39.MouseLeave, TBox40.MouseLeave _
                                                                    , TBox41.MouseLeave, TBox42.MouseLeave, TBox43.MouseLeave, TBox44.MouseLeave, TBox45.MouseLeave, TBox46.MouseLeave, TBox47.MouseLeave, TBox48.MouseLeave, TBox49.MouseLeave, TBox50.MouseLeave
        For i = 0 To lineCount * 5 - 1
            Dim myBox As TextBox = CType(Me.Controls("TBox" & i + 1), TextBox)
            If myBox Is Nothing Then

            Else
                myBox.Cursor = Cursors.Default
            End If

        Next
    End Sub

    Private Sub LogAlarmInfo(line As String, controlObj As String, color As String, alarmLength As Long)
        Try
            Dim alarmType As Integer
            Using outputFile As New StreamWriter("Data/alarmlog_" & terminalName & ".txt", True)
                Dim sb As New System.Text.StringBuilder
                If (CInt(controlObj.Remove(0, 4)) Mod 5) = 0 Then
                    alarmType = 5
                Else alarmType = (CInt(controlObj.Remove(0, 4)) Mod 5)
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
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Alarm was clicked, update the fields and export text file

        ' If alarm is switched on, set starting time in alarmStartTime and write initial label in text box
        For i As Integer = 0 To lineCount - 1
            For j As Integer = 0 To 4
                Dim myBox As TextBox = CType(Me.Controls("TBox" & i * 5 + j + 1), TextBox)
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
                            myBox.Text = "                       0 min"
                            PictureBox1.Select()
                        End If
                        myBox.ForeColor = Color.White
                        LogAlarmInfo(lineLabels(displayedLines(i), 1), myBox.Name, lineStatusStr(i, j), 0)

                    ElseIf lineStatusStr(i, j) = "Yellow" Then
                        alarmStartDateTime(i, j) = DateTime.Now
                        myBox.Text = "                       0 min"
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
            For j = 0 To 4
                Dim myBox As TextBox = CType(Me.Controls("TBox" & i * 5 + j + 1), TextBox)
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
                For j = 0 To lineCount - 1
                    outputFile.WriteLine(lineLabels(displayedLines(j), 0))
                    outputFile.WriteLine(lineLabels(displayedLines(j), 1))
                    Try
                        outputFile.WriteLine(lineStatusStr(j, 0))
                        outputFile.WriteLine(lineStatusStr(j, 1))
                        outputFile.WriteLine(lineStatusStr(j, 2))
                        outputFile.WriteLine(lineStatusStr(j, 3))
                        outputFile.WriteLine(lineStatusStr(j, 4))
                    Catch ex As Exception

                    End Try
                Next
            End Using
            Exit Try
        Catch ex As Exception
            System.Threading.Thread.Sleep(100)
            Button1_Click(sender, e)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Periodically update the text file and the labels for time since alarm was activated
        Button1_Click(sender, e)
        Dim i, j As Integer
        For i = 0 To lineCount - 1
            For j = 0 To 4
                Dim myBox As TextBox = CType(Me.Controls("TBox" & i * 5 + j + 1), TextBox)
                Try
                    If lineStatusStr(i, j) = "Yellow" Or lineStatusStr(i, j) = "Red" Then myBox.Text = "                       " & DateDiff(DateInterval.Minute, alarmStartDateTime(i, j), DateTime.Now) & " min"
                Catch ex As Exception

                End Try
            Next
        Next
    End Sub

End Class
