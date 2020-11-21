Public Class Alarm_type

    Public Property YellowOrRed As String

    Private Sub Alarm_Type_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        YellowOrRed = ""
        RadioButton1.Checked = False
        RadioButton1.Checked = False
        Me.Text = Form1.alarmTypesString(0)
        Label1.Text = Form1.alarmTypesString(0)
        RadioButton1.Text = Form1.alarmTypesString(1)
        RadioButton2.Text = Form1.alarmTypesString(2)
        Button12.Text = Form1.alarmTypesString(3)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        RadioButton1.Checked = True
        YellowOrRed = Form1.yellowName
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        YellowOrRed = ""
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        RadioButton2.Checked = True
        YellowOrRed = Form1.redName
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.Click
        YellowOrRed = Form1.yellowName
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click
        YellowOrRed = Form1.redName
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Label_MouseEnter(sender As Object, e As System.EventArgs) Handles PictureBox1.MouseEnter, PictureBox2.MouseEnter, RadioButton1.MouseEnter, RadioButton2.MouseEnter
        sender.Cursor = Cursors.Hand
    End Sub

    Private Sub Label_MouseLeave(sender As Object, e As System.EventArgs) Handles PictureBox1.MouseLeave, PictureBox2.MouseLeave, RadioButton1.MouseLeave, RadioButton2.MouseLeave
        sender.Cursor = Cursors.Default
    End Sub

End Class