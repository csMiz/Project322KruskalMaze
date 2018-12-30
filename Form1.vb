Public Class Form1

    Public maze As New KruskalMaze

    Public bitmap As New Bitmap(800, 800)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        P.Image = Nothing
        Dim G As Graphics = Graphics.FromImage(bitmap)
        maze.BindingGraphics = G
        maze.InitializeMap(20, 20)

        maze.Process()

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        P.Height = Me.Height - 100
        P.Width = P.Height
        Button1.Left = P.Left + P.Width + 10
    End Sub
End Class
