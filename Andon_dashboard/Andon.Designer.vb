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
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.PictureBox70 = New System.Windows.Forms.PictureBox()
        Me.PictureBox100 = New System.Windows.Forms.PictureBox()
        Me.PictureBox110 = New System.Windows.Forms.PictureBox()
        Me.PictureBox120 = New System.Windows.Forms.PictureBox()
        Me.PictureBox90 = New System.Windows.Forms.PictureBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.PictureBox80 = New System.Windows.Forms.PictureBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LabelSound = New System.Windows.Forms.Label()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.watcher2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxSound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox70, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox100, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox110, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox120, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox90, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox80, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
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
        Me.PictureBoxSound.Location = New System.Drawing.Point(24, 35)
        Me.PictureBoxSound.Name = "PictureBoxSound"
        Me.PictureBoxSound.Size = New System.Drawing.Size(60, 48)
        Me.PictureBoxSound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxSound.TabIndex = 19
        Me.PictureBoxSound.TabStop = False
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(420, 19)
        Me.Label64.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(195, 28)
        Me.Label64.TabIndex = 2
        Me.Label64.Text = "Standard workstation"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(429, 108)
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
        Me.Label68.Location = New System.Drawing.Point(428, 135)
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
        Me.Label69.Location = New System.Drawing.Point(429, 162)
        Me.Label69.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(191, 28)
        Me.Label69.TabIndex = 2
        Me.Label69.Text = "Workstation stopped"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(420, 61)
        Me.Label65.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(174, 28)
        Me.Label65.TabIndex = 2
        Me.Label65.Text = "Priority workstation"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox70
        '
        Me.PictureBox70.Image = CType(resources.GetObject("PictureBox70.Image"), System.Drawing.Image)
        Me.PictureBox70.Location = New System.Drawing.Point(366, 16)
        Me.PictureBox70.Name = "PictureBox70"
        Me.PictureBox70.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox70.TabIndex = 19
        Me.PictureBox70.TabStop = False
        '
        'PictureBox100
        '
        Me.PictureBox100.Image = CType(resources.GetObject("PictureBox100.Image"), System.Drawing.Image)
        Me.PictureBox100.Location = New System.Drawing.Point(368, 104)
        Me.PictureBox100.Name = "PictureBox100"
        Me.PictureBox100.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox100.TabIndex = 19
        Me.PictureBox100.TabStop = False
        '
        'PictureBox110
        '
        Me.PictureBox110.Image = CType(resources.GetObject("PictureBox110.Image"), System.Drawing.Image)
        Me.PictureBox110.Location = New System.Drawing.Point(368, 132)
        Me.PictureBox110.Name = "PictureBox110"
        Me.PictureBox110.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox110.TabIndex = 19
        Me.PictureBox110.TabStop = False
        '
        'PictureBox120
        '
        Me.PictureBox120.Image = CType(resources.GetObject("PictureBox120.Image"), System.Drawing.Image)
        Me.PictureBox120.Location = New System.Drawing.Point(368, 160)
        Me.PictureBox120.Name = "PictureBox120"
        Me.PictureBox120.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox120.TabIndex = 19
        Me.PictureBox120.TabStop = False
        '
        'PictureBox90
        '
        Me.PictureBox90.Image = CType(resources.GetObject("PictureBox90.Image"), System.Drawing.Image)
        Me.PictureBox90.Location = New System.Drawing.Point(114, 63)
        Me.PictureBox90.Name = "PictureBox90"
        Me.PictureBox90.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox90.TabIndex = 19
        Me.PictureBox90.TabStop = False
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label66.Location = New System.Drawing.Point(6, 64)
        Me.Label66.MinimumSize = New System.Drawing.Size(0, 28)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(102, 28)
        Me.Label66.TabIndex = 2
        Me.Label66.Text = "Last alarm"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox80
        '
        Me.PictureBox80.Image = CType(resources.GetObject("PictureBox80.Image"), System.Drawing.Image)
        Me.PictureBox80.Location = New System.Drawing.Point(366, 58)
        Me.PictureBox80.Name = "PictureBox80"
        Me.PictureBox80.Size = New System.Drawing.Size(55, 29)
        Me.PictureBox80.TabIndex = 19
        Me.PictureBox80.TabStop = False
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.Label63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label63.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.Red
        Me.Label63.Location = New System.Drawing.Point(175, 59)
        Me.Label63.MinimumSize = New System.Drawing.Size(150, 28)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(185, 32)
        Me.Label63.TabIndex = 12
        Me.Label63.Text = "  02-nnn"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.White
        Me.Label62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label62.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(175, 17)
        Me.Label62.MinimumSize = New System.Drawing.Size(150, 28)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(185, 32)
        Me.Label62.TabIndex = 12
        Me.Label62.Text = "  02-nnn"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox10
        '
        Me.TextBox10.BackColor = System.Drawing.Color.Red
        Me.TextBox10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox10.ForeColor = System.Drawing.SystemColors.Window
        Me.TextBox10.Location = New System.Drawing.Point(303, 63)
        Me.TextBox10.Margin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.TextBox10.Multiline = True
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(49, 24)
        Me.TextBox10.TabIndex = 17
        Me.TextBox10.Text = "2 min"
        Me.TextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.Color.Lime
        Me.TextBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox7.Location = New System.Drawing.Point(252, 63)
        Me.TextBox7.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox7.Multiline = True
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(49, 24)
        Me.TextBox7.TabIndex = 14
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.Lime
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox4.Location = New System.Drawing.Point(252, 21)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(49, 24)
        Me.TextBox4.TabIndex = 14
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox11
        '
        Me.TextBox11.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox11.BackColor = System.Drawing.Color.Lime
        Me.TextBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox11.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox11.Location = New System.Drawing.Point(305, 109)
        Me.TextBox11.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox11.Multiline = True
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(49, 24)
        Me.TextBox11.TabIndex = 13
        Me.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox12
        '
        Me.TextBox12.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox12.BackColor = System.Drawing.Color.DarkOrange
        Me.TextBox12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox12.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox12.Location = New System.Drawing.Point(305, 137)
        Me.TextBox12.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox12.Multiline = True
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(49, 24)
        Me.TextBox12.TabIndex = 13
        Me.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox13
        '
        Me.TextBox13.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBox13.BackColor = System.Drawing.Color.Red
        Me.TextBox13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox13.Location = New System.Drawing.Point(305, 166)
        Me.TextBox13.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox13.Multiline = True
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(49, 24)
        Me.TextBox13.TabIndex = 13
        Me.TextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.GroupBox1.Controls.Add(Me.TextBox13)
        Me.GroupBox1.Controls.Add(Me.TextBox12)
        Me.GroupBox1.Controls.Add(Me.TextBox11)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.TextBox7)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox10)
        Me.GroupBox1.Controls.Add(Me.Label62)
        Me.GroupBox1.Controls.Add(Me.Label63)
        Me.GroupBox1.Controls.Add(Me.PictureBox80)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label66)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.PictureBox90)
        Me.GroupBox1.Controls.Add(Me.PictureBox120)
        Me.GroupBox1.Controls.Add(Me.PictureBox110)
        Me.GroupBox1.Controls.Add(Me.PictureBox100)
        Me.GroupBox1.Controls.Add(Me.PictureBox70)
        Me.GroupBox1.Controls.Add(Me.Label65)
        Me.GroupBox1.Controls.Add(Me.Label69)
        Me.GroupBox1.Controls.Add(Me.Label68)
        Me.GroupBox1.Controls.Add(Me.Label67)
        Me.GroupBox1.Controls.Add(Me.Label64)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(569, 121)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(600, 204)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Legend"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Lime
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox1.Location = New System.Drawing.Point(305, 21)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(49, 24)
        Me.TextBox1.TabIndex = 14
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.DarkOrange
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox2.Location = New System.Drawing.Point(175, 137)
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
        Me.Label1.Location = New System.Drawing.Point(9, 132)
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
        Me.PictureBox1.Location = New System.Drawing.Point(117, 133)
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
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox2.Controls.Add(Me.PictureBoxSound)
        Me.GroupBox2.Controls.Add(Me.LabelSound)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(569, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(413, 105)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Controls"
        '
        'LabelSound
        '
        Me.LabelSound.AutoSize = True
        Me.LabelSound.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSound.Location = New System.Drawing.Point(90, 45)
        Me.LabelSound.MinimumSize = New System.Drawing.Size(50, 28)
        Me.LabelSound.Name = "LabelSound"
        Me.LabelSound.Size = New System.Drawing.Size(83, 29)
        Me.LabelSound.TabIndex = 2
        Me.LabelSound.Text = "Sound"
        Me.LabelSound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Andon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1215, 686)
        Me.Controls.Add(Me.GroupBox2)
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
        CType(Me.PictureBox70, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox100, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox110, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox120, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox90, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox80, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents watcher2 As IO.FileSystemWatcher
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBoxSound As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox13 As TextBox
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents Label62 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents PictureBox80 As PictureBox
    Friend WithEvents Label66 As Label
    Friend WithEvents PictureBox90 As PictureBox
    Friend WithEvents PictureBox120 As PictureBox
    Friend WithEvents PictureBox110 As PictureBox
    Friend WithEvents PictureBox100 As PictureBox
    Friend WithEvents PictureBox70 As PictureBox
    Friend WithEvents Label65 As Label
    Friend WithEvents Label69 As Label
    Friend WithEvents Label68 As Label
    Friend WithEvents Label67 As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents LabelSound As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
