﻿'Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading
Imports System.Collections.ObjectModel

Partial Public Class MainPage
    Inherits UserControl

    'Related to the data grid
    Dim NotesPerBeat As Integer = 5
    Dim NumberOfBeats As Integer = 10
    Dim NumberOfTacks As Integer = 10
    Dim PlaySamplesPerTrack As Integer = 4

    'Playback related
    Dim BPM As Integer = 200
    Dim TrackSpacing As Double
    Dim Playing As Boolean

    'Indexes for playback
    Dim CurrentNoteIndex As Integer = 1
    Dim CurrentBeatIndex As Integer = 1
    Dim CurrentPlayIndex As Integer = 1

    'Create Tracks Collection
    Dim TrackCollection As New ObservableCollection(Of Track)
    Dim SampleCollection As New Microsoft.VisualBasic.Collection()

    Dim TrackPlayThread As New Thread(AddressOf AudioPlay)

    Public Sub New()
        InitializeComponent()
        TrackPlayThread.Start()
        InitializeSamples()
        InitializeTracks()
    End Sub

    'Function to initialize the Tracks
    Sub InitializeTracks()
        For trackIndex As Integer = 0 To NumberOfTacks - 1
            Dim track As New Track
            track.volume = 0.3
            track.name = "kick_" & Format(trackIndex + 1, "00")
            track.numberOfPlaySamples = PlaySamplesPerTrack
            track.sampleOptions = SampleCollection
            track.sampleIndex = trackIndex + 1

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
        TrackControlsPanel.DataContext = TrackCollection
    End Sub

    Sub InitializeSamples()
        SampleCollection.Add(New Sample(New Uri("Samples/kick_01.wma", UriKind.Relative), "Kick", 1))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_02.wma", UriKind.Relative), "Slip", 2))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_03.wma", UriKind.Relative), "Plip", 3))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_04.wma", UriKind.Relative), "Hip", 4))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_05.wma", UriKind.Relative), "Knip", 5))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_06.wma", UriKind.Relative), "Lip", 6))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_07.wma", UriKind.Relative), "Pip", 7))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_08.wma", UriKind.Relative), "Sip", 8))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_09.wma", UriKind.Relative), "Rip", 9))
        SampleCollection.Add(New Sample(New Uri("Samples/kick_10.wma", UriKind.Relative), "Rap", 10))
    End Sub

    'The click callback on the Play/Stop button
    Private Sub Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        If Playing = False Then
            CurrentNoteIndex = 1
            CurrentBeatIndex = 1
            Playing = True
            Play.Content = "Stop"
        End If
    End Sub

    Private Sub Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles [Stop].Click
        If Playing = True Then
            Playing = False
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

    Public Delegate Sub _PlaySample(ByRef trackIndex As Integer)
    Sub PlaySample(ByRef trackIndex As Integer)
        Dim track As Track = TrackCollection.Item(trackIndex)
        Dim sample As MediaElement = track.playSamples.Item(CurrentPlayIndex)
        sample.Stop()
        sample.Volume = track.volume
        sample.Play()
    End Sub


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
                For trackIndex As Integer = 0 To NumberOfTacks - 1
                    If TrackCollection.Item(trackIndex).beats.Item(CurrentBeatIndex).notes.Item(CurrentNoteIndex).checked Then
                        Dispatcher.BeginInvoke(New _PlaySample(AddressOf PlaySample), trackIndex)
                    End If
                Next

                'Loop trough MediaElements
                If CurrentPlayIndex < PlaySamplesPerTrack Then
                    CurrentPlayIndex += 1
                Else
                    CurrentPlayIndex = 1
                End If

                'Loop trough Notes and Beats
                If CurrentNoteIndex < NotesPerBeat Then
                    CurrentNoteIndex += 1
                Else
                    CurrentNoteIndex = 1
                    If CurrentBeatIndex < NumberOfBeats Then
                        CurrentBeatIndex += 1
                    Else
                        CurrentBeatIndex = 1
                    End If
                End If

                'Used for the timing
                'Elapsed = GetTickCount - Elapsed
                'If Elapsed < TrackSpacing Then
                '    Thread.Sleep(TrackSpacing - Elapsed)
                'Else
                '    'MsgBox("loopt achter")
                'End If
                Thread.Sleep(TrackSpacing)
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

    Private Sub Close_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Close.Click
        If Application.Current.IsRunningOutOfBrowser AndAlso Application.Current.HasElevatedPermissions Then
            Application.Current.MainWindow.Close()
        End If
    End Sub

End Class