<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.P = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.P, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'P
        '
        Me.P.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.P.Location = New System.Drawing.Point(61, 51)
        Me.P.Name = "P"
        Me.P.Size = New System.Drawing.Size(800, 800)
        Me.P.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.P.TabIndex = 0
        Me.P.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(925, 57)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(282, 126)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1269, 914)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.P)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.P, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents P As PictureBox
    Friend WithEvents Button1 As Button
End Class
