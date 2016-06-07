Public Class EffectAttack
    Inherits Effect
    Friend Property Distance As EffectAttackDistance
    Friend Property Accuracy As Integer
    Friend Property Damages As Damages

    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect)) As EffectAttack
        'effectattack:melee|50|5-10 kinetic_hard, 5-5 kinetic_soft

        Dim r As String() = raw.Split("|")
        Dim effect As New EffectAttack
        With effect
            .Distance = Dev.StringToEnum(r(0).Trim, GetType(EffectAttackDistance))
            .Accuracy = CInt(r(1))
            .Damages = Damages.Build(r(2))
        End With
        parent.Add(effect)
        Return effect
    End Function
    Friend Overrides Function Unbuild() As String
        Dim total As String = "EffectAttack:"
        total &= Distance.ToString
        total &= "|"
        total &= Accuracy
        total &= "|"
        total &= Damages.unbuild()
        Return total
    End Function
    Public Overrides Function ToString() As String
        Dim total As String = "[" & Distance.ToString & "] "
        total &= Accuracy & "% - "
        total &= Damages.ToString
        Return total
    End Function
End Class

Public Enum EffectAttackDistance
    Melee = 0
    Close
    Medium
    Far
End Enum

