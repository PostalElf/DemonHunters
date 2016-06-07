Public Class FrameDesign
    Private BaseFrame As Frame
    Private Attacks As New List(Of Attack)
    Private Effects As EffectsDictionary
    Private Costs As New List(Of Cost)

    Friend Shared Function Build(ByVal frame As Frame) As FrameDesign
        If frame.DesignReady = False Then Return Nothing

        Dim frameDesign As New FrameDesign
        With frameDesign
            .BaseFrame = frame
            .Attacks = frame.BuildUnitAttacks
            .Effects = frame.BuildUnitEffectsDictionary
            .Costs = Cost.Strip(Cost.Merge(frame.TotalCosts), PaymentTime.Use)
        End With
        Return frameDesign
    End Function
    Friend Function Unbuild() As Queue(Of String)
        Return BaseFrame.unbuild
    End Function
    Public Overrides Function ToString() As String
        Return BaseFrame.ToString
    End Function
End Class
