Public Class FrameDesign
    Private BaseFrame As Frame
    Private Attacks As New List(Of Attack)
    Private Costs As New List(Of Cost)

    Friend Shared Function Build(ByVal frame As Frame) As FrameDesign
        Dim frameDesign As New FrameDesign
        With frameDesign
            .BaseFrame = frame
            .Attacks = frame.BuildUnitAttacks
            .Costs = Cost.Total(frame.TotalCosts)
        End With
        Return frameDesign
    End Function
End Class
