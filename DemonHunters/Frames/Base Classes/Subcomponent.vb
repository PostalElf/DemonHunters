Public Class Subcomponent
    Inherits Component

    Friend Shared Function Build(ByVal raw As Queue(Of String)) As Subcomponent
        Dim subcomponent As New Subcomponent
        subcomponent.Name = raw.Dequeue
        While raw.Count > 0
            Dim current As String() = raw.Dequeue.Split(":")
            If subcomponent.BuildBase(current, subcomponent) = False Then Throw New Exception("Invalid build string for Subcomponent.")
        End While
        Return subcomponent
    End Function
    Friend Shared Function Build(ByVal targetName As String) As Subcomponent
        Dim designRaw As Queue(Of String) = IO.BracketFileget(Pathnames.subcomponents, targetName)
        If designRaw Is Nothing Then Return Nothing
        Return Build(designRaw)
    End Function
    Friend Overrides Function Unbuild() As Queue(Of String)
        Dim total As New Queue(Of String)
        With total
            .Enqueue("Name:" & Name)
            .Enqueue("Keywords:" & Dev.ListWithCommas(Keywords))
            For Each Cost As Cost In Costs
                .Enqueue("Cost:" & Cost.Unbuild)
            Next
            For Each Effect As Effect In Effects
                .Enqueue(Effect.unbuild)
            Next
        End With
        Return total
    End Function
    Public Overrides Function ToString() As String
        Return MyBase.Name
    End Function

    Friend Overrides ReadOnly Property TotalCosts As List(Of Cost)
        Get
            Return MyBase.Costs
        End Get
    End Property
    Friend Overrides ReadOnly Property TotalEffects As List(Of Effect)
        Get
            Return Effects
        End Get
    End Property
End Class
