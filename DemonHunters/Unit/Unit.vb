Public Class Unit
    Private Property Limbs As New List(Of UnitLimb)
    Private Property InnateEffects As List(Of Effect)
    Private ReadOnly Property LimbEffects As List(Of Effect)
        Get
            Dim total As New List(Of Effect)
            For Each limb In Limbs
                total.AddRange(limb.Effects)
            Next
            Return total
        End Get
    End Property
    Private ReadOnly Property Attacks As List(Of Attack)
        Get
            Dim total As New List(Of Attack)
            For Each limb In Limbs
                If limb.Attack Is Nothing = False Then total.Add(limb.Attack)
            Next
            Return total
        End Get
    End Property

    Friend Shared Function BuildCheck(ByVal baseFrame As Frame) As CheckReason
        If baseFrame.BuildUnitLimbs.Count = 0 Then Return New CheckReason("Requires at least one limb component.")
    End Function
    Friend Shared Function Build(ByVal baseFrame As Frame) As Unit
        Dim unit As New Unit
        With unit
            .Limbs = baseFrame.BuildUnitLimbs
            .InnateEffects = Effect.MergeList(baseFrame.BuildUnitEffects)
        End With
        Return unit
    End Function
End Class
