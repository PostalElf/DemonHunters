Public Class CheckReason
    Private Title As String
    Private Text As String

    Public Sub New(ByVal pTitle As String, Optional ByVal pText As String = "")
        Title = pTitle
        Text = pText
    End Sub
    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
