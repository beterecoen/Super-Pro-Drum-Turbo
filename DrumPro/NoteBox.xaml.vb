Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes

Partial Public Class NoteBox 
    Inherits CheckBox

	Public Sub New()
		' Required to initialize variables
		InitializeComponent()
    End Sub

    Public Shared ReadOnly IsHighlightedProperty As DependencyProperty = _
    DependencyProperty.Register("IsHighlighted", _
    GetType(Boolean), _
    GetType(NoteBox), _
    New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsHighlightedChanged)))

    Public Property IsHighlighted() As Boolean
        Get
            Return CBool(GetValue(IsHighlightedProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsHighlightedProperty, value)
        End Set
    End Property

    Private Shared Sub OnIsHighlightedChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim nb As CheckBox = DirectCast(d, CheckBox)
        VisualStateManager.GoToState(nb, "Fire", True)
    End Sub

End Class
