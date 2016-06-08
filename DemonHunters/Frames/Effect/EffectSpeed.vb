Public Class EffectSpeed
    Inherits Effect
    Friend Property Walk As Integer
    Friend Property Stride As Integer
    Friend Property Run As Integer

    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect)) As EffectSpeed
        Dim r As String() = raw.Split("/")

        Dim total As New EffectSpeed
        With total
            total.Walk = r(0)
            total.Stride = r(1)
            total.Run = r(2)
        End With
        parent.Add(total)
        Return total
    End Function
    Friend Overrides Function Unbuild() As String
        Return "EffectSpeed:" & Walk & "/" & Stride & "/" & Run
    End Function
    Public Overrides Function ToString() As String
        Return "Speed:" & Walk & "/" & Stride & "/" & Run
    End Function
    Friend Overrides Function Merge(ByVal effect As Effect) As Boolean
        If TypeOf effect Is EffectSpeed = False Then Return False
        Dim es As EffectSpeed = CType(effect, EffectSpeed)
        With es
            Walk += .Walk
            Stride += .Stride
            Run += .Run
        End With
        Return True
    End Function
End Class
