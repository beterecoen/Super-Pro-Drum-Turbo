Imports System.ComponentModel

Public Class Note
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
    Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Private checkedValue As Boolean = False
    Private isHighlightedValue As Boolean = False

    Public Property Checked() As Boolean
        Get
            Return Me.checkedValue
        End Get
        Set(value As Boolean)
            If value <> Me.checkedValue Then
                Me.checkedValue = value
                NotifyPropertyChanged("Checked")
            End If
        End Set
    End Property

    Public Property IsHighlighted() As Boolean
        Get
            Return Me.isHighlightedValue
        End Get
        Set(value As Boolean)
            If value <> Me.isHighlightedValue Then
                Me.isHighlightedValue = value
                NotifyPropertyChanged("IsHighlighted")
            End If
        End Set
    End Property

    Public Sub ToggelHighlight()
        Me.IsHighlighted = Not (Me.IsHighlighted)
    End Sub

End Class
