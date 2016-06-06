Public MustInherit Class Component
    Protected Name As String
    Protected Keywords As New List(Of String)
    Protected Costs As New List(Of Cost)
    Protected IsAttack As Boolean = False

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
            Select Case raw(0).Trim.ToLower
                Case "name" : .Name = raw(1).Trim
                Case "keywords" : .Keywords = Dev.ParseCommaList(raw(1).Trim)
                Case "cost" : Cost.Build(raw(1).Trim, .Costs)
                Case "isattack" : .IsAttack = True
                Case Else : Return False
            End Select
        End With
        Return True
    End Function
    Friend MustOverride Function BuildAttack() As Attack
    Friend MustOverride ReadOnly Property TotalCosts As List(Of Cost)
    Friend MustOverride ReadOnly Property TotalEffects As List(Of Effect)
End Class
