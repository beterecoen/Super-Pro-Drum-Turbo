Imports System.Collections.ObjectModel
Public Class Track
    Public Property stream As System.IO.Stream
    Public Property sampleOptions As New Microsoft.VisualBasic.Collection()
    Public Property volume As Double
    Public Property beats As New Microsoft.VisualBasic.Collection()

    Private _sampleIndex As Integer
    Public Property sampleIndex As Integer
        Set(value As Integer)
            Dim st As System.Windows.Resources.StreamResourceInfo = Application.GetResourceStream(sampleOptions.Item(value).uri)
            stream = st.Stream
            _sampleIndex = value
        End Set
        Get
            Return _sampleIndex
        End Get
    End Property



End Class
