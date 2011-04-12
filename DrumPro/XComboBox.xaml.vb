Imports System.Windows.Data

Partial Public Class XComboBox
    Inherits ComboBox
    Private bE As BindingExpression
    Public Sub New()
        AddHandler Me.SelectionChanged, AddressOf XComboBox_SelectionChanged
    End Sub

    Private Sub XComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If bE Is Nothing Then
            bE = Me.GetBindingExpression(ComboBox.SelectedValueProperty)
        Else
            If Me.GetBindingExpression(ComboBox.SelectedValueProperty) Is Nothing Then
                Me.SetBinding(ComboBox.SelectedValueProperty, bE.ParentBinding)
            End If
        End If
    End Sub

End Class
