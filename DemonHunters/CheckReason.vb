Public Class CheckReason
    Private Title As String
    Private Text As String

    Public Sub New(ByVal pTitle As String, Optional ByVal pText As String = "")
        Title = pTitle
        Text = pText
    End Sub
End Class
