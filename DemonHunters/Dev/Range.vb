<DebuggerStepThrough()> Public Structure Range
    Public Property min As Integer
    Public Property max As Integer

    Public Sub New(ByVal _min As Integer, ByVal _max As Integer)
        min = _min
        max = _max
    End Sub
    Public Overrides Function ToString() As String
        If min = max Then Return min Else Return min & "-" & max
    End Function

    Public Function Roll(ByRef rng As Random) As Integer
        If rng Is Nothing Then rng = New Random
        Return rng.Next(min, max + 1)
    End Function
    Public Function IsWithin(ByVal value As Integer) As Boolean
        If value >= min AndAlso value <= max Then Return True Else Return False
    End Function

    Public Shared Operator +(ByVal v1 As Range, ByVal v2 As Range) As Range
        Dim min As Integer = v1.min + v2.min
        Dim max As Integer = v1.max + v2.max
        Return New Range(min, max)
    End Operator
    Public Shared Operator -(ByVal v1 As Range, ByVal v2 As Range) As Range
        Dim min As Integer = v1.min - v2.min
        Dim max As Integer = v2.max - v2.max
        Return New Range(min, max)
    End Operator
End Structure
