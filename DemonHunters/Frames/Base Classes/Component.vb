Public MustInherit Class Component
    Friend Name As String
    Protected Keywords As New List(Of String)
    Protected Costs As New List(Of Cost)
    Protected Effects As New List(Of Effect)

    Friend Function CheckKeyword(ByVal keyword As String) As Boolean
        If Keywords.Contains(keyword) Then Return True
        Return False
    End Function
    Friend Function CheckKeyword(ByVal keywordRequirements As List(Of String)) As Boolean
        'returns true only if all keywords are found

        For Each k In keywordRequirements
            If Keywords.Contains(k) = False Then Return False
        Next
        Return True
    End Function

    Protected Shared Function BuildBase(ByVal raw As String(), ByRef component As Component) As Boolean
        'returns false if nothing matches; build is then handled by child

        With component
            Dim current As String = raw(0).Trim.ToLower
            Select Case raw(0).Trim.ToLower
                Case "keywords" : .Keywords = Dev.ParseCommaList(raw(1).Trim)
                Case "cost" : Cost.Build(raw(1).Trim, .Costs)
                Case Else : If Effect.BuildBase(raw, .Effects) = False Then Return False
            End Select
        End With
        Return True
    End Function
    Friend Shared Function BuildBase(ByVal targetName As String) As Component
        Dim q As Queue(Of String) = IO.BracketFileget(Pathnames.frames, targetName)
        If q Is Nothing = False Then Return Frame.Build(q)
        q = IO.BracketFileget(Pathnames.subcomponents, targetName)
        If q Is Nothing = False Then Return Subcomponent.Build(q)

        Return Nothing
    End Function
    Friend MustOverride Function Unbuild() As Queue(Of String)
    Friend MustOverride ReadOnly Property TotalCosts As List(Of Cost)
    Friend MustOverride ReadOnly Property TotalEffects As List(Of Effect)
End Class
