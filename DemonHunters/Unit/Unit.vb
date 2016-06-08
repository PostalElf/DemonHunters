Public Class Unit
    Private Property Limbs As New List(Of UnitLimb)
    Private ReadOnly Property Attacks As List(Of Attack)
        Get
            Dim total As New List(Of Attack)
            For Each limb In Limbs
                If limb.Attack Is Nothing = False Then total.Add(limb.Attack)
            Next
            Return total
        End Get
    End Property

    Friend Shared Function Build(ByVal baseFrame As Frame) As Unit
        Dim unit As New Unit
        With unit
            .Limbs = baseFrame.BuildUnitLimbs
        End With
        Return unit
    End Function
End Class
