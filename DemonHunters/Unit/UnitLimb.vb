Public Class UnitLimb
    Friend Name As String
    Friend Attack As Attack
    Private EffectsDictionary As EffectsDictionary

    Friend Shared Function Build(ByVal pName As String, ByVal totalEffects As List(Of Effect), ByVal totalCosts As List(Of Cost)) As UnitLimb
        Dim effectList As New List(Of Effect)
        Dim attackList As New List(Of EffectAttack)
        For Each e As Effect In totalEffects
            If TypeOf e Is EffectAttack Then attackList.Add(e) Else effectList.Add(e)
        Next

        Dim total As New UnitLimb
        With total
            .Name = pName
            .Attack = Attack.Build(attackList, totalCosts)
            .EffectsDictionary = EffectsDictionary.Build(effectList)
        End With
        Return total
    End Function
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
