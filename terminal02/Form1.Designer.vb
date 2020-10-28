<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Formulář přepisuje metodu Dispose, aby vyčistil seznam součástí.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Vyžadováno Návrhářem Windows Form
    Private components As System.ComponentModel.IContainer

    'POZNÁMKA: Následující procedura je vyžadována Návrhářem Windows Form
    'Může být upraveno pomocí Návrháře Windows Form.  
    'Neupravovat pomocí editoru kódu
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxIns = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TBox46 = New System.Windows.Forms.TextBox()
        Me.TBox47 = New System.Windows.Forms.TextBox()
        Me.TBox48 = New System.Windows.Forms.TextBox()
        Me.TBox49 = New System.Windows.Forms.TextBox()
        Me.TBox50 = New System.Windows.Forms.TextBox()
        Me.TextLbl101 = New System.Windows.Forms.TextBox()
        Me.TextLbl102 = New System.Windows.Forms.TextBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Location = New System.Drawing.Point(43, 135)
        Me.PictureBoxLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(221, 63)
        Me.PictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxLogo.TabIndex = 15
        Me.PictureBoxLogo.TabStop = False
        '
        'TextBoxIns
        '
        Me.TextBoxIns.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TextBoxIns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxIns.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxIns.Location = New System.Drawing.Point(43, 11)
        Me.TextBoxIns.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxIns.Multiline = True
        Me.TextBoxIns.Name = "TextBoxIns"
        Me.TextBoxIns.Size = New System.Drawing.Size(1190, 92)
        Me.TextBoxIns.TabIndex = 13
        Me.TextBoxIns.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 15000
        '
        'TBox46
        '
        Me.TBox46.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBox46.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBox46.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TBox46.Location = New System.Drawing.Point(429, 750)
        Me.TBox46.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBox46.Multiline = True
        Me.TBox46.Name = "TBox46"
        Me.TBox46.Size = New System.Drawing.Size(117, 50)
        Me.TBox46.TabIndex = 13
        Me.TBox46.TabStop = False
        Me.TBox46.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TBox47
        '
        Me.TBox47.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBox47.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBox47.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TBox47.Location = New System.Drawing.Point(579, 750)
        Me.TBox47.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBox47.Multiline = True
        Me.TBox47.Name = "TBox47"
        Me.TBox47.Size = New System.Drawing.Size(117, 50)
        Me.TBox47.TabIndex = 13
        Me.TBox47.TabStop = False
        Me.TBox47.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TBox48
        '
        Me.TBox48.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBox48.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBox48.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TBox48.Location = New System.Drawing.Point(725, 750)
        Me.TBox48.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBox48.Multiline = True
        Me.TBox48.Name = "TBox48"
        Me.TBox48.Size = New System.Drawing.Size(117, 50)
        Me.TBox48.TabIndex = 13
        Me.TBox48.TabStop = False
        Me.TBox48.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TBox49
        '
        Me.TBox49.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBox49.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBox49.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TBox49.Location = New System.Drawing.Point(871, 750)
        Me.TBox49.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBox49.Multiline = True
        Me.TBox49.Name = "TBox49"
        Me.TBox49.Size = New System.Drawing.Size(117, 50)
        Me.TBox49.TabIndex = 13
        Me.TBox49.TabStop = False
        Me.TBox49.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TBox50
        '
        Me.TBox50.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBox50.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBox50.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TBox50.Location = New System.Drawing.Point(1017, 750)
        Me.TBox50.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBox50.Multiline = True
        Me.TBox50.Name = "TBox50"
        Me.TBox50.Size = New System.Drawing.Size(117, 50)
        Me.TBox50.TabIndex = 13
        Me.TBox50.TabStop = False
        Me.TBox50.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextLbl101
        '
        Me.TextLbl101.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextLbl101.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextLbl101.Location = New System.Drawing.Point(28, 750)
        Me.TextLbl101.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextLbl101.Multiline = True
        Me.TextLbl101.Name = "TextLbl101"
        Me.TextLbl101.Size = New System.Drawing.Size(97, 50)
        Me.TextLbl101.TabIndex = 13
        Me.TextLbl101.TabStop = False
        Me.TextLbl101.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextLbl102
        '
        Me.TextLbl102.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextLbl102.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextLbl102.Location = New System.Drawing.Point(145, 750)
        Me.TextLbl102.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextLbl102.Multiline = True
        Me.TextLbl102.Name = "TextLbl102"
        Me.TextLbl102.Size = New System.Drawing.Size(257, 50)
        Me.TextLbl102.TabIndex = 13
        Me.TextLbl102.TabStop = False
        Me.TextLbl102.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 900000
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Menu
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox1.Location = New System.Drawing.Point(43, 323)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(1190, 150)
        Me.RichTextBox1.TabIndex = 20
        Me.RichTextBox1.Text = ""
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ClientSize = New System.Drawing.Size(1261, 494)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.TextLbl102)
        Me.Controls.Add(Me.TextBoxIns)
        Me.Controls.Add(Me.TextLbl101)
        Me.Controls.Add(Me.TBox50)
        Me.Controls.Add(Me.TBox49)
        Me.Controls.Add(Me.TBox48)
        Me.Controls.Add(Me.TBox47)
        Me.Controls.Add(Me.TBox46)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Andon"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents TextBoxIns As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TBox46 As TextBox
    Friend WithEvents TBox47 As TextBox
    Friend WithEvents TBox48 As TextBox
    Friend WithEvents TBox49 As TextBox
    Friend WithEvents TBox50 As TextBox
    Friend WithEvents TextLbl101 As TextBox
    Friend WithEvents TextLbl102 As TextBox
    Friend WithEvents Timer2 As Timer
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
