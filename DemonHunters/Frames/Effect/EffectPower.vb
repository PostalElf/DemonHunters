﻿Public Class EffectPower
    Inherits Effect
    Friend Property Quantity As Integer
    Friend Property Type As EffectPowerType

    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect))
        'effectPower:5 plasma
        Dim r As String() = raw.Trim.Split(" ")
        Dim effect As New EffectPower
        With effect
            .Quantity = CInt(r(0))
            .Type = Dev.StringToEnum(r(1), GetType(EffectPowerType))
        End With
        parent.Add(effect)
        Return effect
    End Function
    Friend Overrides Function Unbuild() As String
        Return "EffectPower:" & Quantity & " " & Type.ToString
    End Function
    Public Overrides Function ToString() As String
        Return "Power: " & Quantity & " " & Type.ToString
    End Function
    Friend Overrides Function Merge(ByVal effect As Effect) As Boolean
        If TypeOf effect Is EffectPower = False Then Return False

        Dim effectPower As EffectPower = CType(effect, EffectPower)
        With effectPower
            If .Type <> Type Then Return False
            Quantity += .Quantity
        End With
        Return True
    End Function
End Class

Public Enum EffectPowerType
    Conventional
    Plasma
    Arcane
End Enum