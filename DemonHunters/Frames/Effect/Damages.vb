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
    Friend Function Unbuild() As String
        Dim total As String = ""
        For Each d In System.Enum.GetValues(GetType(DamageType))
            Dim range As Range = Dictionary(d)
            If range.min = 0 AndAlso range.max = 0 Then
                'do nothing
            Else
                total &= range.min
                total &= "-"
                total &= range.max
                total &= " "
                total &= d.ToString
                total &= ", "
            End If
        Next
        If total = "" Then Return "0-0 None"
        total = total.Remove(total.Length - 2, 2)
        Return total
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
                    total &= range.ToString & " " & dt.ToString & ", "
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
