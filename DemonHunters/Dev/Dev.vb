<DebuggerStepThrough()> Public Class Dev
    Public Shared Function Constrain(ByVal value As Integer, Optional ByVal min As Integer = 0, Optional ByVal max As Integer = 100) As Integer
        If value < min Then value = min
        If value > max Then value = max
        Return value
    End Function
    Public Shared Function vbSpace(Optional ByVal indent As Integer = 1) As String
        Const space As String = "  "

        Dim total As String = ""
        For n = 1 To indent
            total &= space
        Next
        Return total
    End Function

    Public Shared Function ListWithCommas(ByVal inputList As List(Of String)) As String
        Dim total As String = ""
        For n = 0 To inputList.Count - 1
            total &= inputList(n)
            If n < inputList.Count - 1 Then total &= ", "
        Next
        Return total
    End Function
    Public Shared Function ParseCommaList(ByVal raw As String) As List(Of String)
        Dim r As String() = raw.Split(",")
        Dim total As New List(Of String)
        For Each entry In r
            total.Add(entry.Trim)
        Next
        Return total
    End Function
    Public Shared Function StringToEnum(ByVal raw As String, ByVal enumType As Type) As [Enum]
        Dim arr As Array = System.Enum.GetValues(enumType)
        For Each a In arr
            If a.ToString.ToLower = raw.ToLower Then Return a
        Next
        Return Nothing
    End Function
End Class
