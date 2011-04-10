Imports System.Collections.ObjectModel
Public Class Track
    Public Property name As String
    Public Property CurrentSampleUri As Uri
    Public Property sampleOptions As New Microsoft.VisualBasic.Collection()
    Public Property volume As Double
    Public Property beats As New Microsoft.VisualBasic.Collection()
    Public Property numberOfPlaySamples As Integer

    Private _sampleIndex As Integer
    Public Property sampleIndex As Integer
        Set(value As Integer)
            CurrentSampleUri = sampleOptions.Item(value).uri
            _sampleIndex = value
        End Set
        Get
            Return _sampleIndex
        End Get
    End Property



End Class
