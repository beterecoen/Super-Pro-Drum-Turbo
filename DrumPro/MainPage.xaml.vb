'Imports Microsoft.DirectX.AudioVideoPlayback
Imports System.Threading
Imports System.Collections.ObjectModel

Partial Public Class MainPage
    Inherits UserControl

    'Related to the data grid
    Dim NotesPerBeat As Integer = 4
    Dim NumberOfBeats As Integer = 4
    Dim NumberOfTacks As Integer = 10

    'Related to playback
    Dim CurrentPlayIndex As Integer = 0
    Dim CleaningThreshold As Integer = 25

    'Create Tracks Collection
    Dim TrackCollection As New ObservableCollection(Of Track)
    Dim mediaElementContainer As New Canvas
    Dim ControlPropertiesObject As New ControlProperties()
    Dim AudioTimer As New System.Windows.Threading.DispatcherTimer()

    Public Sub New()
        InitializeComponent()
        InitializeTracks()
        UpdateAudioSpacing()
        LayoutRoot.Children.Add(mediaElementContainer)
        mediaElementContainer.Children.Add(New MediaElement)

        MasterVolumeSilder.DataContext = ControlPropertiesObject
        BMPControl.DataContext = ControlPropertiesObject
        PresetSelection.DataContext = ControlPropertiesObject

        AddHandler ControlPropertiesObject.onBMPChanged, AddressOf UpdateAudioSpacing
        AddHandler ControlPropertiesObject.onPresetChanged, AddressOf UpdateSampleCollection
        AddHandler AudioTimer.Tick, AddressOf PlayColumnSamples
    End Sub

    Sub UpdateAudioSpacing()
        AudioTimer.Interval = New TimeSpan((1 / (ControlPropertiesObject.BPM * 4 / 60) * 10000000))
    End Sub

    Sub UpdateSampleCollection(ByVal SampleCollection As SampleColletion)
        For Each track As Track In TrackCollection
            track.sampleOptions = SampleCollection.samples
        Next
    End Sub

    'Function to initialize the Tracks
    Sub InitializeTracks()
        For trackIndex As Integer = 0 To NumberOfTacks - 1
            Dim track As New Track
            track.volume = 0.3
            track.sampleOptions = ControlPropertiesObject.currentSampleCollection.samples
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

    'The click callback on the Play/Stop button
    Private Sub Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Play.Click
        CurrentPlayIndex = 0
        AudioTimer.Start()
    End Sub

    Private Sub Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles [Stop].Click
        AudioTimer.Stop()
    End Sub

    'Function to visualise the current playing column
    Sub UpdateScollPosition()
        Dim scrollviewer = TrackTilesPanel.GetScrollHost()
        If scrollviewer.HorizontalScrollBarVisibility Then
            Dim scrollSetpWidth As Double = scrollviewer.ScrollableWidth / (NumberOfBeats * NotesPerBeat)
            scrollviewer.ScrollToHorizontalOffset(scrollSetpWidth * CurrentPlayIndex)
        End If
    End Sub


    'Function to play the samples of the current column
    Public Sub PlayColumnSamples()
        'Go through all the tracks
        For Each track As Track In TrackCollection
            If track.isNoteChecked(CurrentPlayIndex) Then
                PlaySample(track.getSample, track.volume)
            End If
            'track.HighlightCurrent()
        Next
        'UpdateScollPosition()
        For Each track As Track In TrackCollection
            track.loadNextSample()
        Next
        incrementPlayIndex()
    End Sub

    Private Sub incrementPlayIndex()
        If CurrentPlayIndex < (NumberOfBeats * NotesPerBeat) - 1 Then
            CurrentPlayIndex += 1
        Else
            CurrentPlayIndex = 0
        End If
    End Sub

    Private Sub PlaySample(ByRef sample As MediaElement, ByRef volume As Double)
        sample.Volume = volume * ControlPropertiesObject.MasterVolume

        'Make sure the sample is not the same as the previous one
        If Not mediaElementContainer.Children.Contains(sample) Then
            mediaElementContainer.Children.Add(sample)
            sample.Play()
        End If

        AddHandler sample.MediaEnded, AddressOf SampleEnded
    End Sub

    Private Sub SampleEnded(sender As Object, e As RoutedEventArgs)
        Dim count As Integer = mediaElementContainer.Children.Count
        If count >= CleaningThreshold Then
            mediaElementContainer.Children.RemoveAt(0)
        End If
    End Sub

    Private Sub Close_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Close.Click
        If Application.Current.IsRunningOutOfBrowser AndAlso Application.Current.HasElevatedPermissions Then
            Application.Current.MainWindow.Close()
        End If
    End Sub

End Class