'Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading
Imports System.Collections.ObjectModel

Partial Public Class MainPage
    Inherits UserControl

    'Related to the data grid
    Dim NotesPerBeat As Integer = 4
    Dim NumberOfBeats As Integer = 4
    Dim NumberOfTacks As Integer = 10

    'Indexes for playback
    Dim CurrentNoteIndex As Integer = 1
    Dim CurrentBeatIndex As Integer = 1
    Dim CurrentPlayIndex As Integer = 1
    Dim CleaningThreshold As Integer = 25

    'Create Tracks Collection
    Dim TrackCollection As New ObservableCollection(Of Track)
    Dim SampleCollection As New Microsoft.VisualBasic.Collection()
    Dim mediaElementContainer As New Canvas
    Dim ControlPropertiesObject As New ControlProperties()
    Dim AudioTimer As New System.Windows.Threading.DispatcherTimer()

    Public Sub New()
        InitializeComponent()
        InitializeSamples()
        InitializeTracks()
        UpdateAudioSpacing()
        LayoutRoot.Children.Add(mediaElementContainer)
        MasterVolumeSilder.DataContext = ControlPropertiesObject
        BMPControl.DataContext = ControlPropertiesObject
        AddHandler ControlPropertiesObject.onBMPChanged, AddressOf UpdateAudioSpacing
        AddHandler AudioTimer.Tick, AddressOf PlayColumnSamples
    End Sub

    Sub UpdateAudioSpacing()
        AudioTimer.Interval = New TimeSpan(0, 0, 0, 0, (1 / (ControlPropertiesObject.BPM * 4 / 60) * 1000))
    End Sub


    'Function to initialize the Tracks
    Sub InitializeTracks()
        For trackIndex As Integer = 0 To NumberOfTacks - 1
            Dim track As New Track
            track.volume = 0.3
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

    Sub InitializeSamples2()
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_HiHat.wMA", UriKind.Relative), "Vinyl HiHat 1", 1))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_HiHat_2.wma", UriKind.Relative), "Vinyl HiHat 2", 2))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_open_hihat.wma", UriKind.Relative), "Vinyl open hihat", 3))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Kick.wma", UriKind.Relative), "Vinyl Kick 1", 4))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Kick_2.wma", UriKind.Relative), "Vinyl Kick 2", 5))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Ride.wma", UriKind.Relative), "Vinyl Ride 1", 6))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Ride_2.wma", UriKind.Relative), "Vinyl Ride 2", 7))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Snare.wma", UriKind.Relative), "Vinyl Snare", 8))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Tom_2.wma", UriKind.Relative), "Vinyl Tom 1", 9))
        SampleCollection.Add(New Sample(New Uri("Samples/Vinyl_Tom_3.wma", UriKind.Relative), "Vinyl Tom 2", 10))
    End Sub
    Sub InitializeSamples()
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Clap.wma", UriKind.Relative), "FreakyKit Clap", 1))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Crash 1.wma", UriKind.Relative), "FreakyKit Crash 1", 2))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Crash 2.wma", UriKind.Relative), "FreakyKit Crash 2", 3))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Kick 1.wma", UriKind.Relative), "FreakyKit Kick 1", 4))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Kick 2.wma", UriKind.Relative), "FreakyKit Kick 2", 5))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Perc 1.wma", UriKind.Relative), "FreakyKit Perc 1", 6))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Perc 2.wma", UriKind.Relative), "FreakyKit Perc 2", 7))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Perc 3.wma", UriKind.Relative), "FreakyKit Perc 3", 8))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Perc 4.wma", UriKind.Relative), "FreakyKit Perc 4", 9))
        SampleCollection.Add(New Sample(New Uri("Samples/FreakyKit Perc 5.wma", UriKind.Relative), "FreakyKit Perc 5", 10))
    End Sub

    'The click callback on the Play/Stop button
    Private Sub Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        CurrentNoteIndex = 1
        CurrentBeatIndex = 1
        AudioTimer.Start()
    End Sub

    Private Sub Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles [Stop].Click
        AudioTimer.Stop()
    End Sub

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

    End Sub


    'Function to play the samples of the current column
    Public Sub PlayColumnSamples()

        'Go through all the tracks
        For trackIndex As Integer = 0 To NumberOfTacks - 1
            'trigger Highlight on current Note
            If TrackCollection.Item(trackIndex).beats.Item(CurrentBeatIndex).notes.Item(CurrentNoteIndex).IsHighlighted Then
                TrackCollection.Item(trackIndex).beats.Item(CurrentBeatIndex).notes.Item(CurrentNoteIndex).IsHighlighted = False
            Else
                TrackCollection.Item(trackIndex).beats.Item(CurrentBeatIndex).notes.Item(CurrentNoteIndex).IsHighlighted = True
            End If

            If TrackCollection.Item(trackIndex).beats.Item(CurrentBeatIndex).notes.Item(CurrentNoteIndex).checked Then
                Dim track As Track = TrackCollection.Item(trackIndex)
                PlaySample(track.stream, track.volume)
            End If
        Next

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

    End Sub

    Private Sub PlaySample(ByRef stream As System.IO.Stream, ByRef volume As Double)
        Dim sample As New MediaElement
        sample.SetSource(stream)
        sample.Volume = volume * ControlPropertiesObject.MasterVolume
        sample.AutoPlay = True
        mediaElementContainer.Children.Add(sample)
        AddHandler sample.MediaEnded, AddressOf SampleEnded
    End Sub

    Private Sub SampleEnded(sender As Object, e As RoutedEventArgs)
        Dim count As Integer = mediaElementContainer.Children.Count
        If count >= CleaningThreshold Then
            mediaElementContainer.Children.RemoveAt(0)
        End If
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