Imports System.Collections.ObjectModel
Public Class Track
    Public Property name As String
    Public Property playSamples As New Microsoft.VisualBasic.Collection()
    Public Property sampleOptions As New Microsoft.VisualBasic.Collection()
    Public Property volume As Double
    Public Property beats As New Microsoft.VisualBasic.Collection()
    Public Property numberOfPlaySamples As Integer

    Private _sampleIndex As Integer
    Public Property sampleIndex As Integer
        Set(value As Integer)
            Me.playSamples.Clear()
            For sampleIndex As Integer = 0 To Me.numberOfPlaySamples - 1
                Dim sample As New MediaElement
                Dim st As System.Windows.Resources.StreamResourceInfo = Application.GetResourceStream(Me.sampleOptions.Item(value).uri)
                sample.SetSource(st.Stream)
                sample.AutoPlay = False
                Me.playSamples.Add(sample)
            Next
            _sampleIndex = value
        End Set
        Get
            Return _sampleIndex
        End Get
    End Property



End Class
