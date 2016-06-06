Public Class Effect
    Friend Property Distance As EffectDistance
    Friend Property Accuracy As Integer
    Friend Property Damages As Damages

    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect)) As Effect
        'effect:melee|50|5-10 kinetic_hard, 5-5 kinetic_soft

        Dim r As String() = raw.Split("|")
        Dim effect As New Effect
        With effect
            .Distance = Dev.StringToEnum(r(0).Trim, GetType(EffectDistance))
            .Accuracy = CInt(r(1))
            .Damages = Damages.Build(r(2))
        End With
        parent.Add(effect)
        Return effect
    End Function
    Public Overrides Function ToString() As String
        Dim total As String = "[" & Distance.ToString & "] "
        total &= Accuracy & "% - "
        total &= Damages.ToString
        Return total
    End Function
End Class

Public Enum EffectDistance
    Melee = 0
    Close
    Medium
    Far
End Enum

Public Enum DamageType
    None

    Kinetic_Hard
    Energy_Hard
    Explosive_Hard

    Kinetic_Soft
    Energy_Soft
    Explosive_Soft

    Arcane
End Enum

Public Class Damages
    Inherits System.Collections.DictionaryBase
    Public Sub New()
        Dim damageTypes As Array = System.Enum.GetValues(GetType(DamageType))
        For Each d In damageTypes
            Dictionary.Add(d, New Range(0, 0))
        Next
    End Sub
    Friend Shared Function Build(ByVal raw As String) As Damages
        '5-10 kinetic_hard, 5-5 kinetic_soft

        Dim damages As New Damages
        Dim rawsplit As String() = raw.Split(",")
        For Each rs In rawsplit
            '5-10 kinetic_hard
            Dim r As String() = rs.Split(" ")
            Dim rz As String() = r(0).Split("-")
            Dim min As Integer = CInt(rz(0))
            Dim max As Integer = CInt(rz(1))
            Dim damageType As DamageType = Dev.StringToEnum(r(1).Trim, GetType(DamageType))
            damages(damageType) = New Range(min, max)
        Next
        Return damages
    End Function


    Default Friend Property Item(ByVal key As DamageType) As Range
        Get
            Return Dictionary(key)
        End Get
        Set(ByVal value As Range)
            Dictionary(key) = value
        End Set
    End Property
    Private Function ContainsKey(ByVal key As DamageType) As Boolean
        For Each tkey In Dictionary.Keys
            If tkey = key Then Return True
        Next
        Return False
    End Function
    Public Overrides Function ToString() As String
        Dim total As String = ""
        For Each dt As DamageType In System.Enum.GetValues(GetType(DamageType))
            If Dictionary(dt) Is Nothing = False Then
                Dim range As Range = Dictionary(dt)
                If range.min = 0 AndAlso range.max = 0 Then
                    'do nothing
                Else
                    total &= dt.ToString & ": " & Dictionary(dt).ToString & ", "
                End If
            End If
        Next
        If total = "" Then Return "0"
        total = total.Remove(total.Length - 2, 2)
        Return total
    End Function

    Public Shared Operator +(ByVal v1 As Damages, ByVal v2 As Damages) As Damages
        Dim damageTypes As Array = System.Enum.GetValues(GetType(DamageType))

        Dim total As New Damages
        For Each dt As DamageType In damageTypes
            If dt = DamageType.None Then Continue For
            If v1 Is Nothing = False AndAlso v1.ContainsKey(dt) = True Then total(dt) += v1(dt)
            If v2 Is Nothing = False AndAlso v2.ContainsKey(dt) = True Then total(dt) += v2(dt)
        Next
        Return total
    End Operator
End Class