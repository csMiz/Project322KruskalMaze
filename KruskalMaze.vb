' -----------------------------------------
' Copyright (c) 2018 All Rights Reserved.
' 
' Filename: KruskalMaze
' Author: Miz
' Date: 2018/12/29 20:50:09
' -----------------------------------------

Public Class KruskalMaze

    Public Map(,) As KruskalMazeCell

    Public BoundX As Integer, BoundY As Integer

    Public Groups As New List(Of KruskalMazeGroup)

    Public BindingGraphics As Graphics = Nothing

    Public Sub InitializeMap(x As Integer, y As Integer)
        ReDim Me.Map(x - 1, y - 1)
        Me.BoundX = x
        Me.BoundY = y
        Groups.Clear()
        For i = 0 To x - 1
            For j = 0 To y - 1
                Dim tmpCell As New KruskalMazeCell With {.GroupIndex = j + i * y}
                If i = 0 Then
                    tmpCell.Wall -= 1
                ElseIf i = x - 1 Then
                    tmpCell.Wall -= 1 << 2
                End If
                If y = 0 Then
                    tmpCell.Wall -= 1 << 3
                ElseIf y = BoundY - 1 Then
                    tmpCell.Wall -= 1 << 1
                End If
                Me.Map(i, j) = tmpCell
                Dim tmpGroup As New KruskalMazeGroup With {.Index = j + i * y}
                tmpGroup.Content.Add(New KeyValuePair(Of Integer, Integer)(i, j))
                Groups.Add(tmpGroup)
            Next
        Next
    End Sub

    Private Function GetCell(x As Integer, y As Integer, dir As Byte) As KruskalMazeCell
        Select Case dir
            Case 0
                If y Then
                    Return Me.Map(x, y - 1)
                Else
                    Return Nothing
                End If
            Case 1
                If x = BoundX - 1 Then
                    Return Nothing
                Else
                    Return Me.Map(x + 1, y)
                End If
            Case 2
                If y = BoundY - 1 Then
                    Return Nothing
                Else
                    Return Me.Map(x, y + 1)
                End If
            Case Else
                If x Then
                    Return Me.Map(x - 1, y)
                Else
                    Return Nothing
                End If
        End Select
        Return Nothing
    End Function

    Private Function GetGroup(index As Integer) As KruskalMazeGroup
        For Each g As KruskalMazeGroup In Me.Groups
            If g.Index = index Then Return g
        Next
        Return Nothing
    End Function

    Public Async Sub Process()
        Dim rnd As New Random
lb2:
        Dim originalGroup As New List(Of KruskalMazeGroup)(Me.Groups)
        'Dim nextGroup As New List(Of KruskalMazeGroup)
        While originalGroup.Count
            Dim targetGroup As KruskalMazeGroup = originalGroup.First
            Dim targetCell As KruskalMazeCell = Nothing
            Dim tmpPosition As KeyValuePair(Of Integer, Integer)
lb1:
            Do
                tmpPosition = targetGroup.Content(Math.Floor(targetGroup.Content.Count * rnd.NextDouble))
                targetCell = Me.Map(tmpPosition.Key, tmpPosition.Value)
            Loop Until targetCell.Wall
            Dim dirRepeatCount As Short = 0
            Dim dirValid As Boolean = True
            Dim findDir As Byte = Math.Floor(4 * rnd.NextDouble)    'Top Right Bottom Left
            Dim nextCell As KruskalMazeCell
            Do
                dirValid = True
                nextCell = GetCell(tmpPosition.Key, tmpPosition.Value, findDir)
                If nextCell Is Nothing Then
                    dirValid = False
                    dirRepeatCount += 1
                    findDir += 1
                    If findDir > 3 Then findDir -= 4
                Else
                    If nextCell.GroupIndex = targetCell.GroupIndex Then
                        dirValid = False
                        dirRepeatCount += 1
                        findDir += 1
                        If findDir > 3 Then findDir -= 4
                    End If
                End If
                If dirRepeatCount >= 4 Then GoTo lb1
            Loop While Not dirValid
            targetCell.Wall -= 1 << (3 - findDir)
            Select Case findDir
                Case 0
                    nextCell.Wall -= 1 << 1
                Case 1
                    nextCell.Wall -= 1
                Case 2
                    nextCell.Wall -= 1 << 3
                Case Else
                    nextCell.Wall -= 1 << 2
            End Select
            Dim mergeGroup As KruskalMazeGroup = GetGroup(nextCell.GroupIndex)
            For Each c As KeyValuePair(Of Integer, Integer) In mergeGroup.Content
                Me.Map(c.Key, c.Value).GroupIndex = targetCell.GroupIndex
                targetGroup.Content.Add(c)
            Next
            originalGroup.Remove(mergeGroup)
            originalGroup.Remove(targetGroup)
            mergeGroup.Index = -1

            Me.DrawMap(BindingGraphics, 800, True)
            Await Task.Delay(25)

        End While
        If Me.Groups.Count Then
            For i = Me.Groups.Count - 1 To 0 Step -1
                If Me.Groups(i).Index = -1 Then
                    Me.Groups.RemoveAt(i)
                End If
            Next
        End If
        If Me.Groups.Count > 1 Then GoTo lb2

        'MsgBox("Finished")
        Me.DrawMap(BindingGraphics, 800, False)
    End Sub

    Public Sub DrawMap(g As Graphics, size As Single, showGroup As Boolean)
        g.Clear(Color.White)
        Dim tmpPen As New Pen(Color.Black, 2.0F)
        Dim tmpFont As Font = New Font("Arial", 10)
        Dim cellSize As Single = size / BoundX
        For j = 0 To BoundY - 1
            For i = 0 To BoundX - 1
                Dim tmpCell As KruskalMazeCell = Me.Map(i, j)
                If tmpCell.Wall >> 3 And 1 Then
                    g.DrawLine(tmpPen, New PointF(i * cellSize, j * cellSize), New PointF((i + 1) * cellSize, j * cellSize))
                End If
                If tmpCell.Wall >> 2 And 1 Then
                    g.DrawLine(tmpPen, New PointF((i + 1) * cellSize, j * cellSize), New PointF((i + 1) * cellSize, (j + 1) * cellSize))
                End If
                If tmpCell.Wall >> 1 And 1 Then
                    g.DrawLine(tmpPen, New PointF(i * cellSize, (j + 1) * cellSize), New PointF((i + 1) * cellSize, (j + 1) * cellSize))
                End If
                If tmpCell.Wall And 1 Then
                    g.DrawLine(tmpPen, New PointF(i * cellSize, j * cellSize), New PointF(i * cellSize, (j + 1) * cellSize))
                End If
                If showGroup Then
                    g.DrawString(tmpCell.GroupIndex, tmpFont, Brushes.Black, New PointF(i * cellSize + 5, j * cellSize + 5))
                End If
            Next
        Next
        Form1.P.Image = Form1.bitmap
        Form1.P.Refresh()
    End Sub

End Class

Public Class KruskalMazeCell
    ''' <summary>
    ''' 0000 Top Right Bottom Left
    ''' </summary>
    Public Wall As Byte = 15

    Public GroupIndex As Integer = -1

End Class

Public Class KruskalMazeGroup

    Public Content As New List(Of KeyValuePair(Of Integer, Integer))

    Public Index As Integer = -1

End Class
