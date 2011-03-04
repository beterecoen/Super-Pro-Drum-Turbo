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

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TrackPlayThread.Start()

        TrackNotes(1, 0) = True
        TrackNotes(1, 4) = True
        TrackNotes(1, 8) = True
        TrackNotes(1, 12) = True
        TrackNotes(1, 16) = True
        TrackNotes(1, 20) = True

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        For row As Integer = 0 To TrackNotes.GetUpperBound(0)
            'Dim trackContainer As New Panel
            'trackContainer.Dock = DockStyle.Top
            'trackContainer.Height = 20
            'trackContainer.Width = 20 * TrackNotes.GetUpperBound(1)
            'trackContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            For column As Integer = 0 To TrackNotes.GetUpperBound(1)
                Dim clsButton1 As New PictureBox
                If TrackNotes(row, column) = True Then
                    clsButton1.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
                Else
                    clsButton1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
                End If
                'clsButton1.Dock = DockStyle.Left
                clsButton1.Size = New System.Drawing.Size(20, 20)
                clsButton1.Location = New System.Drawing.Point(20 * column, 20 * row)
                TrackTilesPanel.Controls.Add(clsButton1)
            Next
            'TrackTilesPanel.Controls.Add(trackContainer)
            'TrackTilesPanel.ScrollState()
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        'Voor test
        BPM = 80


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

    Private Sub TrackTilesPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TrackTilesPanel.Paint

    End Sub

End Class
