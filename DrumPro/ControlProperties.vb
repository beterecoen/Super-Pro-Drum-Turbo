Imports System.ComponentModel

Public Class ControlProperties
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
    Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Private masterVolumeValue As Double = 0.5
    Private BPMValue As Integer = 120

    Public Property MasterVolume() As Double
        Get
            Return Me.masterVolumeValue
        End Get
        Set(value As Double)
            If value <> Me.masterVolumeValue Then
                Me.masterVolumeValue = value
                NotifyPropertyChanged("MasterVolume")
            End If
        End Set
    End Property

    Public Property BPM() As Integer
        Get
            Return Me.BPMValue
        End Get
        Set(value As Integer)
            If value <> Me.BPMValue Then
                Me.BPMValue = value
                NotifyPropertyChanged("BPM")
                RaiseEvent onBMPChanged()
            End If
        End Set
    End Property

    Public Event onBMPChanged()

    Private Property NumberOfBeatsValue As Integer
    Public Property NumberOfBeats As Integer
        Set(value As Integer)
            NumberOfBeatsValue = value
            NotifyPropertyChanged("NumberOfBeats")
        End Set
        Get
            Return NumberOfBeatsValue
        End Get
    End Property

    Private Property NotesPerBeatValue As Integer
    Public Property NotesPerBeat As Integer
        Set(value As Integer)
            NotesPerBeatValue = value
            NotifyPropertyChanged("NotesPerBeat")
        End Set
        Get
            Return NotesPerBeatValue
        End Get
    End Property

    Private Property localPresetCollection As New Microsoft.VisualBasic.Collection()

    Public ReadOnly Property PresetCollection As Microsoft.VisualBasic.Collection
        Get
            Return localPresetCollection
        End Get
    End Property

    Private Property _presetIndex As Integer = 1

    Public Property presetIndex As Integer
        Set(value As Integer)
            RaiseEvent onPresetChanged(localPresetCollection.Item(value))
            _presetIndex = value
            NotifyPropertyChanged("presetIndex")
        End Set
        Get
            Return _presetIndex
        End Get
    End Property

    Public Event onPresetChanged(ByVal SampleCollection As SampleColletion)

    Public ReadOnly Property currentSampleCollection As SampleColletion
        Get
            Return localPresetCollection.Item(_presetIndex)
        End Get
    End Property

    Public Sub New()
        localPresetCollection.Add(loadPresetOne())
        localPresetCollection.Add(loadPresetTwo())
        localPresetCollection.Add(loadPresetThree())
    End Sub

    Private Function loadPresetOne()
        Dim SampleCollection As New SampleColletion
        SampleCollection.name = "Vinyl"
        SampleCollection.index = 1
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_HiHat.wma", UriKind.Relative), "Vinyl HiHat 1", 1))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_HiHat_2.wma", UriKind.Relative), "Vinyl HiHat 2", 2))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_open_hihat.wma", UriKind.Relative), "Vinyl open hihat", 3))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Kick.wma", UriKind.Relative), "Vinyl Kick 1", 4))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Kick_2.wma", UriKind.Relative), "Vinyl Kick 2", 5))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Ride.wma", UriKind.Relative), "Vinyl Ride 1", 6))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Ride_2.wma", UriKind.Relative), "Vinyl Ride 2", 7))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Snare.wma", UriKind.Relative), "Vinyl Snare", 8))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Tom_2.wma", UriKind.Relative), "Vinyl Tom 1", 9))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Vinyl_Tom_3.wma", UriKind.Relative), "Vinyl Tom 2", 10))
        Return SampleCollection
    End Function

    Private Function loadPresetTwo()
        Dim SampleCollection As New SampleColletion
        SampleCollection.name = "FreakyKit"
        SampleCollection.index = 2
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Clap.wma", UriKind.Relative), "FreakyKit Clap", 1))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Crash 1.wma", UriKind.Relative), "FreakyKit Crash 1", 2))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Crash 2.wma", UriKind.Relative), "FreakyKit Crash 2", 3))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Kick 1.wma", UriKind.Relative), "FreakyKit Kick 1", 4))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Kick 2.wma", UriKind.Relative), "FreakyKit Kick 2", 5))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Perc 1.wma", UriKind.Relative), "FreakyKit Perc 1", 6))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Perc 2.wma", UriKind.Relative), "FreakyKit Perc 2", 7))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Perc 3.wma", UriKind.Relative), "FreakyKit Perc 3", 8))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Perc 4.wma", UriKind.Relative), "FreakyKit Perc 4", 9))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/FreakyKit Perc 5.wma", UriKind.Relative), "FreakyKit Perc 5", 10))
        Return SampleCollection
    End Function

    Private Function loadPresetThree()
        Dim SampleCollection As New SampleColletion
        SampleCollection.name = "Electro"
        SampleCollection.index = 3
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Clap 1.wma", UriKind.Relative), "Electro Clap 1", 1))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Clap 2.wma", UriKind.Relative), "Electro Clap 2", 2))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro FX 1.wma", UriKind.Relative), "Electro FX 1", 3))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro FX 2.wma", UriKind.Relative), "Electro FX 2", 4))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Kick 1.wma", UriKind.Relative), "Electro Kick 1", 5))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Kick 2.wma", UriKind.Relative), "Electro Kick 2", 6))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Kick 3.wma", UriKind.Relative), "Electro Kick 3", 7))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Open HiHat.wma", UriKind.Relative), "Electro Open HiHat", 8))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Snare 1.wma", UriKind.Relative), "Electro Snare 1", 9))
        SampleCollection.samples.Add(New Sample(New Uri("Samples/Electro Snare 2.wma", UriKind.Relative), "Electro Snare 2", 10))
        Return SampleCollection
    End Function

End Class
