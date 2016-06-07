Public Class EffectAttack
    Inherits Effect
    Friend Property Distance As EffectAttackDistance
    Friend Property Accuracy As Integer
    Friend Property Damages As Damages

    Public Sub New(ByVal dist As EffectAttackDistance)
        Distance = dist
    End Sub
    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Effect)) As EffectAttack
        'effectattack:melee|50|5-10 kinetic_hard, 5-5 kinetic_soft

        Dim r As String() = raw.Split("|")
        Dim distance As EffectAttackDistance = Dev.StringToEnum(r(0).Trim, GetType(EffectAttackDistance))

        Dim effect As New EffectAttack(distance)
        effect.Accuracy = CInt(r(1))
        effect.Damages = Damages.Build(r(2))

        parent.Add(effect)
        Return effect
    End Function
    Friend Overrides Function Unbuild() As String
        Dim total As String = "EffectAttack:"
        total &= Distance.ToString
        total &= "|"
        total &= Accuracy
        total &= "|"
        total &= Damages.Unbuild()
        Return total
    End Function
    Public Overrides Function ToString() As String
        Dim total As String = "[" & Distance.ToString & "] "
        total &= Accuracy & "% - "
        total &= Damages.ToString
        Return total
    End Function

    Public Shared Function MergeList(ByVal EffectAttackList As List(Of EffectAttack)) As List(Of EffectAttack)
        Dim total As New List(Of EffectAttack)
        For Each e As EffectAttack In EffectAttackList
            Dim matchFound As Boolean = False
            For Each ea As EffectAttack In total
                If e.Distance = ea.Distance Then
                    ea.Merge(e)
                    matchFound = True
                    Exit For
                End If
            Next
            If matchFound = False Then total.Add(e)
        Next
        Return total
    End Function
    Friend Overrides Sub Merge(ByVal effect As Effect)
        If TypeOf effect Is EffectAttack = False Then Exit Sub

        Dim effectAttack As EffectAttack = CType(effect, EffectAttack)
        With effectAttack
            If .Distance <> Distance Then Exit Sub
            Accuracy += .Accuracy
            Damages += .Damages
        End With
    End Sub
End Class

Public Enum EffectAttackDistance
    Melee = 0
    Close
    Medium
    Far
End Enum

