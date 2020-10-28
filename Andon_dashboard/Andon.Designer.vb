<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Andon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Andon))
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.watcher2 = New System.IO.FileSystemWatcher()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBoxSound = New System.Windows.Forms.PictureBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.PictureBox100 = New System.Windows.Forms.PictureBox()
        Me.PictureBox110 = New System.Windows.Forms.PictureBox()
        Me.PictureBox120 = New System.Windows.Forms.PictureBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.LabelSound = New System.Windows.Forms.Label()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.watcher2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxSound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox100, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox110, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox120, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBoxLogo.Location = New System.Drawing.Point(994, 20)
        Me.PictureBoxLogo.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(175, 96)
        Me.PictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxLogo.TabIndex = 9
        Me.PictureBoxLogo.TabStop = False
        '
        'watcher2
        '
        Me.watcher2.EnableRaisingEvents = True
        Me.watcher2.Filter = "*.txt"
        Me.watcher2.NotifyFilter = System.IO.NotifyFilters.LastWrite
        Me.watcher2.SynchronizingObject = Me
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 15000
        '
        'PictureBoxSound
        '
        Me.PictureBoxSound.BackgroundImage = CType(resources.GetObject("PictureBoxSound.BackgroundImage"), System.Drawing.Image)
        Me.PictureBoxSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBoxSound.Location = New System.Drawing.Point(32, 41)
        Me.PictureBoxSound.Name = "PictureBoxSound"
        Me.PictureBoxSound.Size = New System.Drawing.Size(60, 48)
        Me.PictureBoxSound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxSound.TabIndex = 19
        Me.PictureBoxSound.TabStop = False
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(395, 27)
        Me.Label67.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(157, 28)
        Me.Label67.TabIndex = 2
        Me.Label67.Text = "Normal condition"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(394, 54)
        Me.Label68.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(180, 28)
        Me.Label68.TabIndex = 2
        Me.Label68.Text = "Workstation trouble"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(395, 81)
        Me.Label69.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(191, 28)
        Me.Label69.TabIndex = 2
        Me.Label69.Text = "Workstation stopped"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox100
        '
        Me.PictureBox100.Image = CType(resources.GetObject("PictureBox100.Image"), System.Drawing.Image)
        Me.PictureBox100.Location = New System.Drawing.Point(334, 23)
        Me.PictureBox100.Name = "PictureBox100"
        Me.PictureBox100.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox100.TabIndex = 19
        Me.PictureBox100.TabStop = False
        '
        'PictureBox110
        '
        Me.PictureBox110.Image = CType(resources.GetObject("PictureBox110.Image"), System.Drawing.Image)
        Me.PictureBox110.Location = New System.Drawing.Point(334, 51)
        Me.PictureBox110.Name = "PictureBox110"
        Me.PictureBox110.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox110.TabIndex = 19
        Me.PictureBox110.TabStop = False
        '
        'PictureBox120
        '
        Me.PictureBox120.Image = CType(resources.GetObject("PictureBox120.Image"), System.Drawing.Image)
        Me.PictureBox120.Location = New System.Drawing.Point(334, 79)
        Me.PictureBox120.Name = "PictureBox120"
        Me.PictureBox120.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox120.TabIndex = 19
        Me.PictureBox120.TabStop = False
        '
        'TextBox11
        '
        Me.TextBox11.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox11.BackColor = System.Drawing.Color.Lime
        Me.TextBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox11.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox11.Location = New System.Drawing.Point(271, 28)
        Me.TextBox11.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox11.Multiline = True
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(49, 24)
        Me.TextBox11.TabIndex = 13
        Me.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox12
        '
        Me.TextBox12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox12.BackColor = System.Drawing.Color.DarkOrange
        Me.TextBox12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox12.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox12.Location = New System.Drawing.Point(271, 56)
        Me.TextBox12.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox12.Multiline = True
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(49, 24)
        Me.TextBox12.TabIndex = 13
        Me.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.GroupBox1.Controls.Add(Me.PictureBoxSound)
        Me.GroupBox1.Controls.Add(Me.TextBox13)
        Me.GroupBox1.Controls.Add(Me.LabelSound)
        Me.GroupBox1.Controls.Add(Me.TextBox12)
        Me.GroupBox1.Controls.Add(Me.TextBox11)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.Label63)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.PictureBox120)
        Me.GroupBox1.Controls.Add(Me.PictureBox110)
        Me.GroupBox1.Controls.Add(Me.PictureBox100)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label69)
        Me.GroupBox1.Controls.Add(Me.Label68)
        Me.GroupBox1.Controls.Add(Me.Label67)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(569, 121)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(600, 156)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Legend"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.DarkOrange
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox2.Location = New System.Drawing.Point(184, 116)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(49, 24)
        Me.TextBox2.TabIndex = 17
        Me.TextBox2.Text = "2 min"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Location = New System.Drawing.Point(18, 111)
        Me.Label1.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 28)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Alarm length"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(126, 112)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox1.TabIndex = 19
        Me.PictureBox1.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(18, 16)
        Me.Label16.MinimumSize = New System.Drawing.Size(50, 28)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(310, 29)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Number   Workstation name"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelSound
        '
        Me.LabelSound.AutoSize = True
        Me.LabelSound.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSound.Location = New System.Drawing.Point(98, 51)
        Me.LabelSound.MinimumSize = New System.Drawing.Size(50, 28)
        Me.LabelSound.Name = "LabelSound"
        Me.LabelSound.Size = New System.Drawing.Size(83, 29)
        Me.LabelSound.TabIndex = 2
        Me.LabelSound.Text = "Sound"
        Me.LabelSound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox13
        '
        Me.TextBox13.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox13.BackColor = System.Drawing.Color.Red
        Me.TextBox13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox13.Location = New System.Drawing.Point(271, 85)
        Me.TextBox13.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox13.Multiline = True
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(49, 24)
        Me.TextBox13.TabIndex = 13
        Me.TextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.Label63.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.Red
        Me.Label63.Location = New System.Drawing.Point(266, 112)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(61, 28)
        Me.Label63.TabIndex = 12
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(395, 111)
        Me.Label2.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(174, 28)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Priority workstation"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(334, 109)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox2.TabIndex = 19
        Me.PictureBox2.TabStop = False
        '
        'Andon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1215, 686)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Andon"
        Me.Text = "eAndon dashboard"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.watcher2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxSound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox100, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox110, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox120, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents watcher2 As IO.FileSystemWatcher
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBoxSound As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents PictureBox120 As PictureBox
    Friend WithEvents PictureBox110 As PictureBox
    Friend WithEvents PictureBox100 As PictureBox
    Friend WithEvents Label69 As Label
    Friend WithEvents Label68 As Label
    Friend WithEvents Label67 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents LabelSound As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBox13 As TextBox
    Friend WithEvents Label63 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
End Class
