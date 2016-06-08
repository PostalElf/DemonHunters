Public Class UnitLimb
    Friend Name As String
    Friend Attack As Attack
    Friend Effects As New List(Of Effect)

    Public Sub New()
        For Each p In System.Enum.GetValues(GetType(EffectPowerType))
            Power.Add(p, 0)
        Next
    End Sub
    Friend Shared Function Build(ByVal pName As String, ByVal totalEffects As List(Of Effect), ByVal totalCosts As List(Of Cost)) As UnitLimb
        Dim effectList As New List(Of Effect)
        Dim attackList As New List(Of EffectAttack)
        For Each e As Effect In totalEffects
            If TypeOf e Is EffectAttack Then attackList.Add(e) Else effectList.Add(e)
        Next

        Dim total As New UnitLimb
        With total
            .Name = pName
            .Attack = Attack.Build(EffectAttack.MergeList(attackList), totalCosts)
            .Effects = Effect.MergeList(effectList)
            .HandleEffects()
        End With
        Return total
    End Function
    Public Overrides Function ToString() As String
        Return Name
    End Function

    Private Power As New Dictionary(Of EffectPowerType, Integer)
    Private Speed(SpeedRun) As Integer
    Const SpeedWalk As Integer = 0
    Const SpeedStride As Integer = 1
    Const SpeedRun As Integer = 2
    Private Sub HandleEffects()
        For n = 0 To Effects.Count - 1
            Dim handled As Boolean = False
            Dim effect As Effect = Effects(n)
            Select Case effect.GetType
                Case GetType(EffectPower)
                    Dim ep As EffectPower = CType(effect, EffectPower)
                    Power(ep.Type) += ep.Quantity
                    handled = True
                Case GetType(EffectSpeed)
                    Dim es As EffectSpeed = CType(effect, EffectSpeed)
                    Speed(SpeedWalk) += es.Walk
                    Speed(SpeedStride) += es.Stride
                    Speed(SpeedRun) += es.Run
                    handled = True
            End Select

            If handled = True Then Effects.RemoveAt(n)
        Next
    End Sub
    Friend ReadOnly Property DesignReady As CheckReason
        Get
            For Each kvp In Power
                If kvp.Value < 0 Then Return New CheckReason("Insufficient " & kvp.Key.ToString & " Power")
            Next

            Return Nothing
        End Get
    End Property
End Class
