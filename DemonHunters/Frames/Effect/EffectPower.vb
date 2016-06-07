Public Class EffectPower
    Inherits Effect
    Friend Property Quantity As Integer
    Friend Property Type As EffectPowerType

    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect))
        'effectPower:5 plasma
        Dim r As String() = raw.Split(" ")
        Dim effect As New EffectPower
        With effect
            .Quantity = CInt(r(0))
            .Type = Dev.StringToEnum(r(1), GetType(EffectPowerType))
        End With
        Return effect
    End Function
    Friend Overrides Function Unbuild() As String
        Return "EffectPower:" & Quantity & " " & Type.ToString
    End Function
End Class

Public Enum EffectPowerType
    Conventional
    Plasma
    Arcane
End Enum