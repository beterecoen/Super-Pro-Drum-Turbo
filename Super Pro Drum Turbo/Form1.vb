Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading

Public Class Form1
    Dim TrackSample(9) As String
    Dim TrackNotes(9, 63) As Boolean
    Dim TrackPlayStream(9, 20) As Audio
    Dim BPM As Integer
    Dim TrackSpacing As Double
    Dim TrackPlayThread As New Thread(AddressOf AudioPlay)
    Dim Playing As Boolean
    Dim CurrentNoteIndex As Integer
    Dim playIndex As Integer = 0

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TrackPlayThread.Start()

        'Set some defaults
        TrackNotes(1, 0) = True
        TrackNotes(1, 4) = True
        TrackNotes(1, 8) = True
        TrackNotes(1, 12) = True
        TrackNotes(1, 16) = True
        TrackNotes(1, 20) = True
        BPM = 20

        'Put the samples in the TrackPlayStream makes multiple instances
        For row As Integer = 0 To TrackSample.GetUpperBound(0)
            For playIndex As Integer = 0 To TrackPlayStream.GetUpperBound(1)
                TrackPlayStream(row, playIndex) = New Audio(Application.StartupPath & "\samples\kick_" & Format(row + 1, "00") & ".wav", False)
            Next
        Next

        drawTiles()
    End Sub

    'Funcation triggerd when a Tile is clicked
    Sub trackTileClicked(ByVal trackTile As System.Object, ByVal e As System.EventArgs)
        Dim row, column As Integer

        'Get the clicked row and column from the tag
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

    'Function for drawing the tiles in the TrackTilesPanel
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

    'The click callback on the Play/Stop button
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

    'Delegate Function needed to be able to call ToggelPlayingColumn from within the AudioPlay Thread
    Private Delegate Sub _TogglePlayingColumn(ByRef column As Integer)

    'Function to visualise the current playing column
    Sub TogglePlayingColumn(ByRef column As Integer)
        Dim index As Integer = 0
        For Each ctrl In TrackTilesPanel.Controls
            If index = column Then
                ctrl.BorderStyle = BorderStyle.FixedSingle
            Else
                ctrl.BorderStyle = BorderStyle.None
            End If
            index += 1
        Next
    End Sub

    'Function used for calibartion of the audioplayer
    Private Declare Function GetTickCount Lib "kernel32" () As Integer
    Dim Elapsed As Integer

    'Thread which plays the audio
    Public Sub AudioPlay()
        Do While True
            If Playing Then
                'Set some variables

                Elapsed = GetTickCount

                BPM = bpmField.Text
                TrackSpacing = (1 / (BPM * 4 / 60)) * 1000

                'Call to visualise the current playing column
                Me.Invoke(New _TogglePlayingColumn(AddressOf TogglePlayingColumn), CurrentNoteIndex)

                'Go through all the tracks
                For row As Integer = 0 To TrackSample.GetUpperBound(0)
                    If TrackNotes(row, CurrentNoteIndex) = True Then
                        'Find a none playing track play stream
                        TrackPlayStream(row, playIndex).Stop()
                        TrackPlayStream(row, playIndex).Volume = -3000
                        TrackPlayStream(row, playIndex).Play()
                    End If
                Next

                If playIndex < TrackPlayStream.GetUpperBound(1) Then
                    playIndex += 1
                Else
                    playIndex = 0
                End If

                'Make the CurrentNoteIndex loop
                If CurrentNoteIndex < TrackNotes.GetUpperBound(1) Then
                    CurrentNoteIndex += 1
                Else
                    CurrentNoteIndex = 0
                End If

                'Used for the timing
                Elapsed = GetTickCount - Elapsed
                If Elapsed < TrackSpacing Then
                    Thread.Sleep(TrackSpacing - Elapsed)
                Else
                    'MsgBox("loopt achter")
                End If
            End If
        Loop
    End Sub

    'Runs when program is closed
    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Kill the audio Thread
        TrackPlayThread.Abort()
    End Sub

End Class
