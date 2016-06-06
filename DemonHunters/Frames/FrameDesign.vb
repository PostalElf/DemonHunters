Public Class FrameDesign
    Private BaseFrame As Frame
    Private Attacks As New List(Of Attack)
    Private Costs As New List(Of Cost)

    Friend Shared Function Build(ByVal frame As Frame) As FrameDesign
        If frame.DesignReady = False Then Return Nothing

        Dim frameDesign As New FrameDesign
        With frameDesign
            .BaseFrame = frame
            .Attacks = frame.BuildUnitAttacks
            .Costs = Cost.Strip(Cost.Total(frame.TotalCosts), PaymentTime.Use)
        End With
        Return frameDesign
    End Function
End Class
