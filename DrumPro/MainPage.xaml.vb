'Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading
Imports System.Collections.ObjectModel

Partial Public Class MainPage
    Inherits UserControl

    'Setup
    Dim NotesPerBeat As Integer = 5
    Dim NumberOfBeats As Integer = 10
    Dim NumberOfTacks As Integer = 10
    Dim BPM As Integer = 80
    Dim TrackSpacing As Double
    Dim TrackPlayThread As New Thread(AddressOf AudioPlay)
    Dim Playing As Boolean
    Dim CurrentNoteIndex As Integer
    Dim playIndex As Integer = 0

    Dim TrackCollection As New ObservableCollection(Of Track)

    Public Sub New()
        InitializeComponent()

        TrackPlayThread.Start()

        'Set some defaults
        'TrackNotes(1, 0) = True
        'TrackNotes(1, 4) = True
        'TrackNotes(1, 8) = True
        'TrackNotes(1, 12) = True
        'TrackNotes(1, 16) = True
        'TrackNotes(1, 20) = True
        BPM = 20

        'Put the samples in the TrackPlayStream makes multiple instances
        'For row As Integer = 0 To TrackSample.GetUpperBound(0)
        '    'For playIndex As Integer = 0 To TrackPlayStream.GetUpperBound(1)
        '    '    TrackPlayStream(row, playIndex) = New Audio(Application.StartupPath & "\samples\kick_" & Format(row + 1, "00") & ".wav", False)
        '    'Next
        'Next

        InitializeTracks()
    End Sub

    'Function to initialize the Tracks
    Sub InitializeTracks()
        For trackIndex As Integer = 0 To NumberOfTacks - 1
            Dim track As New Track
            track.volume = 5
            track.name = "kick_" & Format(trackIndex + 1, "00")
            'track.smapleFile = New Uri("\samples\kick_" & Format(trackIndex + 1, "00") & ".wav")
            For beatIndex As Integer = 0 To NumberOfBeats - 1
                Dim beat As New Beat
                For noteIndex As Integer = 0 To NotesPerBeat - 1
                    Dim note As New Note
                    beat.notes.Add(note, CStr(noteIndex))
                Next
                track.beats.Add(beat, CStr(beatIndex))
            Next

            TrackCollection.Add(track)
        Next

        TrackTilesPanel.DataContext = TrackCollection
    End Sub

    'Funcation triggerd when a Tile is clicked
    Sub trackTileClicked(ByVal trackTile As System.Object, ByVal e As System.EventArgs)
        Dim row, column As Integer

        'Get the clicked row and column from the tag
        row = Split(trackTile.Tag, ",")(0)
        column = Split(trackTile.Tag, ",")(1)

        'If TrackNotes(row, column) = True Then
        '    TrackNotes(row, column) = False
        '    trackTile.BackgroundImage = New Uri("Resources/button_selected.png")
        'Else
        '    TrackNotes(row, column) = True
        '    trackTile.BackgroundImage = New Uri("Resources/button_unselected.png")
        'End If

    End Sub

    'Function for drawing the tiles in the TrackTilesPanel
    'Private Sub drawTiles()
    '    For column As Integer = 0 To TrackNotes.GetUpperBound(1)
    '        Dim playColumn As New DockPanel
    '        playColumn. = New System.Drawing.Point(20 * column, 0)
    '        playColumn.Size = New System.Drawing.Size(20, 20 * TrackNotes.GetUpperBound(0))

    '        For row As Integer = 0 To TrackNotes.GetUpperBound(0)
    '            Dim trackTile As New PictureBox

    '            If TrackNotes(row, column) = True Then
    '                trackTile.BackgroundImage = My.Resources.button_selected
    '            Else
    '                trackTile.BackgroundImage = My.Resources.button_unselected
    '            End If
    '            trackTile.Size = New System.Drawing.Size(20, 20)
    '            trackTile.Location = New System.Drawing.Point(0, 20 * row)
    '            playColumn.Controls.Add(trackTile)
    '            trackTile.Tag = row & "," & column

    '            AddHandler trackTile.Click, AddressOf trackTileClicked
    '        Next
    '        Location()
    '        TrackTilesPanel.Controls.Add(playColumn)
    '    Next
    'End Sub

    'The click callback on the Play/Stop button
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        If Playing = True Then
            Playing = False
            Play.Content = "Play"
        Else
            CurrentNoteIndex = 0
            Playing = True
            Play.Content = "Stop"
        End If

    End Sub

    'Delegate Function needed to be able to call ToggelPlayingColumn from within the AudioPlay Thread
    Private Delegate Sub _TogglePlayingColumn(ByRef column As Integer)

    'Function to visualise the current playing column
    Sub TogglePlayingColumn(ByRef column As Integer)

        ''Make the container scroll if needed
        'If TrackTilesPanel.HorizontalScroll.Enabled Then
        '    If (TrackTilesPanel.Width / 20 / 2) > column Then
        '        TrackTilesPanel.HorizontalScroll.Value = 0
        '    ElseIf column > (TrackNotes.GetUpperBound(1) - (TrackTilesPanel.Width / 20 / 2)) Then
        '        TrackTilesPanel.HorizontalScroll.Value = TrackTilesPanel.HorizontalScroll.Maximum
        '    Else
        '        TrackTilesPanel.HorizontalScroll.Value = (column - ((TrackTilesPanel.Width / 20 / 2) - 1)) * 20
        '    End If
        'End If

        ''Toggeling the column borders
        'Dim index As Integer = 0
        'For Each ctrl In TrackTilesPanel.Controls
        '    If index = column Then
        '        ctrl.BorderStyle = BorderStyle.FixedSingle
        '    Else
        '        ctrl.BorderStyle = BorderStyle.None
        '    End If
        '    index += 1
        'Next
    End Sub

    'Function used for calibartion of the audioplayer
    Private Declare Function GetTickCount Lib "kernel32" () As Integer
    Dim Elapsed As Integer

    'Thread which plays the audio
    Public Sub AudioPlay()

        Do While True
            If Playing Then
                'Set some variables
                'Elapsed = GetTickCount

                'BPM = bmpField.Text


                If BPM = 0 Then
                    TrackSpacing = 1 / ((BPM + 5) * 4 / 60) * 1000
                Else
                    TrackSpacing = 1 / (BPM * 4 / 60) * 1000
                End If

                'Call to visualise the current playing column
                'Me.Invoke(New _TogglePlayingColumn(AddressOf TogglePlayingColumn), CurrentNoteIndex)

                'Go through all the tracks
                'For row As Integer = 0 To TrackSample.GetUpperBound(0)
                '    If TrackNotes(row, CurrentNoteIndex) = True Then
                '        'Find a none playing track play stream
                '        'TrackPlayStream(row, playIndex).Stop()
                '        'TrackPlayStream(row, playIndex).Volume = 0
                '        'TrackPlayStream(row, playIndex).Play()
                '    End If
                'Next

                'If playIndex < TrackPlayStream.GetUpperBound(1) Then
                '    playIndex += 1
                'Else
                '    playIndex = 0
                'End If

                'Make the CurrentNoteIndex loop
                'If CurrentNoteIndex < TrackNotes.GetUpperBound(1) Then
                '    CurrentNoteIndex += 1
                'Else
                '    CurrentNoteIndex = 0
                'End If

                'Used for the timing
                'Elapsed = GetTickCount - Elapsed
                'If Elapsed < TrackSpacing Then
                '    Thread.Sleep(TrackSpacing - Elapsed)
                'Else
                '    'MsgBox("loopt achter")
                'End If
            End If
        Loop
    End Sub

    'Runs when program is closed
    'Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Unloaded
    '    'Kill the audio Thread
    '    TrackPlayThread.Abort()
    'End Sub
    'Private Sub BPMup_down(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPMup.MouseDown
    '    'initialize timer
    '    timerMenupalying = True
    '    Dim timer As New System.Windows.Forms.Timer
    '    timer.Enabled = True
    '    timer.Interval = 350
    '    AddHandler timer.Tick, AddressOf Timer_Tick_up
    'End Sub
    'Private Sub BPMup_up(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPMup.MouseUp
    '    'commence kill timer
    '    timerMenupalying = False
    'End Sub
    'Dim timerMenupalying As Boolean = True

    'Private Sub BPMdown_release(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPMdown.MouseUp
    '    'commence kill timer
    '    timerMenupalying = False
    'End Sub

    'Private Sub BPMdown_PressAndHold(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPMdown.MouseDown
    '    'initialize timer
    '    timerMenupalying = True
    '    Dim timer As New System.Windows.Forms.Timer
    '    timer.Enabled = True
    '    timer.Interval = 350
    '    AddHandler timer.Tick, AddressOf Timer_Tick_down
    'End Sub
    'Private Sub Timer_Tick_up(ByVal Timer As System.Object, ByVal e As System.EventArgs)
    '    'kills timer
    '    If timerMenupalying = False Then
    '        Timer.Dispose()
    '    End If
    '    'limits maximum bpm and increments bpm
    '    If bpmField.Text < 250 Then
    '        bpmField.Text += 1
    '        Timer.Interval = 55
    '    End If

    'End Sub
    'Private Sub Timer_Tick_down(ByVal Timer As System.Object, ByVal e As System.EventArgs)
    '    'kills timer
    '    If timerMenupalying = False Then
    '        Timer.Dispose()
    '    End If
    '    'limits minimum bpm and increments bpm
    '    If bpmField.Text > 0 Then
    '        bpmField.Text += -1
    '        Timer.Interval = 55
    '    End If
    'End Sub
    ''limit bpm value
    'Private Sub bpmField_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bpmField.TextChanged
    '    If bpmField.Text = "" Then
    '    Else
    '        If bpmField.Text > 250 Then
    '            bpmField.Text = 250
    '        End If
    '    End If
    'End Sub

End Class