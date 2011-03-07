Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading

Public Class Form1
    Dim TrackSample(9) As String
    Dim TrackNotes(9, 31) As Boolean
    Dim BPM As Integer
    Dim TrackSpacing As Double
    Dim TrackPlayThread As New Thread(AddressOf AudioPlay)
    Dim Playing As Boolean
    Dim CurrentNoteIndex As Integer

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TrackPlayThread.Start()

        'Set some defaults
        TrackNotes(1, 0) = True
        TrackNotes(1, 4) = True
        TrackNotes(1, 8) = True
        TrackNotes(1, 12) = True
        TrackNotes(1, 16) = True
        TrackNotes(1, 20) = True
        BPM = 80

        For row As Integer = 0 To TrackSample.GetUpperBound(0)
            TrackSample(row) = Application.StartupPath & "\samples\kick_" & Format(row + 1, "00") & ".wav"
        Next

        drawTiles()
    End Sub

    Sub trackTileClicked(ByVal trackTile As System.Object, ByVal e As System.EventArgs)
        Dim row, column As Integer

        row = Split(trackTile.Tag, ",")(0)
        column = Split(trackTile.Tag, ",")(1)

        If TrackNotes(row, column) = True Then
            TrackNotes(row, column) = False
            trackTile.BackgroundImage = My.Resources.button_unselected
        Else
            TrackNotes(row, column) = True
            trackTile.BackgroundImage = My.Resources.button_selected
        End If

    End Sub

    Private Sub drawTiles()
        For column As Integer = 0 To TrackNotes.GetUpperBound(1)
            Dim playColumn As New Panel
            playColumn.Location = New System.Drawing.Point(20 * column, 0)
            playColumn.Size = New System.Drawing.Size(20, 20 * TrackNotes.GetUpperBound(0))

            For row As Integer = 0 To TrackNotes.GetUpperBound(0)
                Dim trackTile As New PictureBox

                If TrackNotes(row, column) = True Then
                    trackTile.BackgroundImage = My.Resources.button_selected
                Else
                    trackTile.BackgroundImage = My.Resources.button_unselected
                End If
                trackTile.Size = New System.Drawing.Size(20, 20)
                trackTile.Location = New System.Drawing.Point(0, 20 * row)
                playColumn.Controls.Add(trackTile)
                trackTile.Tag = row & "," & column

                AddHandler trackTile.Click, AddressOf trackTileClicked
            Next

            TrackTilesPanel.Controls.Add(playColumn)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        If Playing = True Then
            Playing = False
            Play.Text = "Play"
        Else
            CurrentNoteIndex = 0
            Playing = True
            Play.Text = "Stop"
        End If

    End Sub

    Public Sub DisposeAudio(ByVal playSound As System.Object, ByVal e As System.EventArgs)
        playSound.Dispose()
    End Sub

    Private Delegate Sub _TogglePlayingColumn(ByRef column As Integer)

    Sub TogglePlayingColumn(ByRef column As Integer)
        Dim current, previous As Object
        current = TrackTilesPanel.GetChildAtPoint(New System.Drawing.Point(column * 20, 0))
        current.BorderStyle = BorderStyle.FixedSingle
        Dim x As Integer
        If column < 1 Then
            x = TrackNotes.GetUpperBound(1)
        Else
            x = column - 1
        End If
        previous = TrackTilesPanel.GetChildAtPoint(New System.Drawing.Point(x * 20, 0))
        previous.BorderStyle = BorderStyle.None

    End Sub

    Public Sub AudioPlay()
        Do While True
            If Playing Then
                BPM = bpmField.Text
                TrackSpacing = (1 / (BPM * 4 / 60)) * 1000

                Me.Invoke(New _TogglePlayingColumn(AddressOf TogglePlayingColumn), CurrentNoteIndex)

                For row As Integer = 0 To TrackSample.GetUpperBound(0)
                    If TrackNotes(row, CurrentNoteIndex) = True Then
                        Dim playSound As Audio = New Audio(TrackSample(row), True)
                        playSound.Volume = -2000
                        playSound.Play()
                        AddHandler playSound.Pausing, AddressOf DisposeAudio
                    End If
                Next

                If CurrentNoteIndex < TrackNotes.GetUpperBound(1) Then
                    CurrentNoteIndex += 1
                Else
                    CurrentNoteIndex = 0
                End If

                Thread.Sleep(TrackSpacing)
            End If
        Loop
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        TrackPlayThread.Abort()
    End Sub

End Class
