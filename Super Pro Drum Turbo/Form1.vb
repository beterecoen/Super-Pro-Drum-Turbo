Imports Microsoft.DirectX
Imports Microsoft.DirectX.DirectDraw
Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading

Public Class Form1
    Dim TrackSample(10) As Audio
    Dim TrackNotes(10, 64) As Boolean
    Dim BPM As Integer
    Dim TrackSpacing As Double
    Dim TrackPlayThread As New Thread(AddressOf AudioPlay)
    Dim Playing As Boolean
    Dim CurrentNoteIndex As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        'Voor test
        BPM = 80

        TrackNotes(1, 0) = True
        TrackNotes(1, 4) = True
        TrackNotes(1, 8) = True
        TrackNotes(1, 12) = True
        TrackNotes(1, 16) = True
        TrackNotes(1, 20) = True
        'Tot hier

        If Playing = True Then
            Playing = False
            Play.Text = "Play"
        Else
            CurrentNoteIndex = 0
            TrackSpacing = (1 / (BPM * 4 / 60)) * 1000
            Playing = True
            Play.Text = "Stop"
        End If

    End Sub

    Public Sub AudioPlay()
        Do While True
            If Playing Then
                If TrackNotes(1, CurrentNoteIndex) = True Then
                    Dim playSound As Audio
                    playSound = New Audio("h:\Desktop\kick_06.wav", True)
                End If

                If CurrentNoteIndex < 20 Then
                    CurrentNoteIndex += 1
                Else
                    CurrentNoteIndex = 0
                End If

                Thread.Sleep(TrackSpacing)
            End If
        Loop
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TrackPlayThread.Start()
    End Sub
End Class
