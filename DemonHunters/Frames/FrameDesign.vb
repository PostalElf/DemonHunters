Public Class FrameDesign
    Private BaseFrame As Frame
    Private Effects As EffectsDictionary
    Private Costs As New List(Of Cost)

    Friend Shared Function BuildCheck(ByVal frame As Frame) As CheckReason
        Dim effectsDictionary As EffectsDictionary = frame.BuildUnitEffectsDictionary
        Return effectsDictionary.DesignReady
    End Function
    Friend Shared Function Build(ByVal frame As Frame) As FrameDesign
        If frame.DesignReady Is Nothing = False Then Return Nothing

        Dim frameDesign As New FrameDesign
        With frameDesign
            .BaseFrame = frame
            .Effects = frame.BuildUnitEffectsDictionary
            .Costs = Cost.Strip(Cost.Merge(frame.TotalCosts), PaymentTime.Use)
        End With
        Return frameDesign
    End Function
    Friend Function Unbuild() As Queue(Of String)
        Return BaseFrame.Unbuild
    End Function
    Public Overrides Function ToString() As String
        Return BaseFrame.ToString
    End Function
End Class
