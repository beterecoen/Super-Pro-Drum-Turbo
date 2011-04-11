Imports System.Collections.ObjectModel
Public Class Track

    Public Property sampleOptions As New Microsoft.VisualBasic.Collection()
    Public Property volume As Double
    Public Property beats As New Microsoft.VisualBasic.Collection()

    Public Property sampleIndex As Integer
        Set(value As Integer)
            Dim st As System.Windows.Resources.StreamResourceInfo = Application.GetResourceStream(sampleOptions.Item(value).uri)
            _stream = st.Stream
            _sampleIndex = value
        End Set
        Get
            Return _sampleIndex
        End Get
    End Property

    Private ReadOnly Property numberOfBeats As Integer
        Get
            Return beats.Count
        End Get
    End Property

    Private ReadOnly Property numberOfNotes As Integer
        Get
            Return beats.Item(1).notes.Count()
        End Get
    End Property

    Private Property _stream As System.IO.Stream
    Private Property _currentSample As MediaElement
    Private Property _sample As MediaElement
    Private Property _sampleIndex As Integer
    Private Property _currentNoteIndex As Integer
    Private Property _currentBeatIndex As Integer
    Private Property _nextNoteIndex As Integer
    Private Property _nextBeatIndex As Integer

    Public Function isNoteChecked(ByVal CurrentPlayIndex As Integer) As Boolean
        updateNoteBeatIndex(CurrentPlayIndex)
        If beats.Item(_nextBeatIndex).notes.Item(_nextNoteIndex).checked Then
            loadNextSample()
        End If
        If beats.Item(_currentBeatIndex).notes.Item(_currentNoteIndex).checked Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub loadNextSample()
        _sample = New MediaElement
        _sample.SetSource(_stream)
        _sample.AutoPlay = False
    End Sub

    Public ReadOnly Property getSample() As MediaElement
        Get
            Return _sample
        End Get
    End Property

    Private Sub updateNoteBeatIndex(ByVal CurrentPlayIndex As Integer)
        _currentBeatIndex = Int(CurrentPlayIndex / numberOfNotes) + 1
        _currentNoteIndex = (CurrentPlayIndex Mod numberOfBeats) + 1
        _nextNoteIndex = ((CurrentPlayIndex + 1) Mod numberOfBeats) + 1
        If CurrentPlayIndex = (numberOfBeats * numberOfNotes) - 1 Then
            _nextBeatIndex = 1
        Else
            _nextBeatIndex = Int((CurrentPlayIndex + 1) / numberOfNotes) + 1
        End If
    End Sub

End Class
