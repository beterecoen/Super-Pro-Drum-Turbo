<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Play = New System.Windows.Forms.Button
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.TrackTilesPanel = New System.Windows.Forms.Panel
        Me.bpmField = New System.Windows.Forms.MaskedTextBox
        Me.BPMup = New System.Windows.Forms.PictureBox
        Me.BPMdown = New System.Windows.Forms.PictureBox
        CType(Me.BPMup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BPMdown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Play
        '
        Me.Play.Location = New System.Drawing.Point(26, 25)
        Me.Play.Name = "Play"
        Me.Play.Size = New System.Drawing.Size(75, 23)
        Me.Play.TabIndex = 0
        Me.Play.Text = "Play"
        Me.Play.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(472, 280)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "TabPage1"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(192, 74)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "TabPage2"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TrackTilesPanel
        '
        Me.TrackTilesPanel.AutoScroll = True
        Me.TrackTilesPanel.Location = New System.Drawing.Point(177, 146)
        Me.TrackTilesPanel.Name = "TrackTilesPanel"
        Me.TrackTilesPanel.Size = New System.Drawing.Size(600, 367)
        Me.TrackTilesPanel.TabIndex = 1
        '
        'bpmField
        '
        Me.bpmField.Location = New System.Drawing.Point(122, 27)
        Me.bpmField.Mask = "000"
        Me.bpmField.Name = "bpmField"
        Me.bpmField.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.bpmField.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.bpmField.Size = New System.Drawing.Size(27, 20)
        Me.bpmField.TabIndex = 4
        Me.bpmField.Text = "80"
        Me.bpmField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BPMup
        '
        Me.BPMup.Image = Global.WindowsApplication1.My.Resources.Resources.button_selected
        Me.BPMup.Location = New System.Drawing.Point(165, 12)
        Me.BPMup.Name = "BPMup"
        Me.BPMup.Size = New System.Drawing.Size(20, 21)
        Me.BPMup.TabIndex = 5
        Me.BPMup.TabStop = False
        '
        'BPMdown
        '
        Me.BPMdown.Image = Global.WindowsApplication1.My.Resources.Resources.button_unselected
        Me.BPMdown.Location = New System.Drawing.Point(165, 39)
        Me.BPMdown.Name = "BPMdown"
        Me.BPMdown.Size = New System.Drawing.Size(20, 21)
        Me.BPMdown.TabIndex = 6
        Me.BPMdown.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 525)
        Me.Controls.Add(Me.BPMdown)
        Me.Controls.Add(Me.BPMup)
        Me.Controls.Add(Me.bpmField)
        Me.Controls.Add(Me.TrackTilesPanel)
        Me.Controls.Add(Me.Play)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.BPMup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BPMdown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Play As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TrackTilesPanel As System.Windows.Forms.Panel
    Friend WithEvents bpmField As System.Windows.Forms.MaskedTextBox
    Friend WithEvents BPMup As System.Windows.Forms.PictureBox
    Friend WithEvents BPMdown As System.Windows.Forms.PictureBox

End Class
