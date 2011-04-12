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

End Class
