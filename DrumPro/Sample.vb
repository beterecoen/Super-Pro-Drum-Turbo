Public Class Sample
    Public Property uri As Uri
    Public Property name As String
    Public Property index As Integer
    Public Sub New(ByVal _uri As Uri, ByVal _name As String, ByVal _index As Integer)
        uri = _uri
        name = _name
        index = _index
    End Sub
End Class
