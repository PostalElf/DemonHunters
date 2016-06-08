Public MustInherit Class Effect
    Public Shared EffectTypeList As New List(Of Type) From {GetType(EffectPower), GetType(EffectSpeed)}

    Friend Shared Function BuildBase(ByVal raw As String(), ByVal parent As List(Of Effect)) As Boolean
        Select Case raw(0).Trim.ToLower
            Case "effectattack" : EffectAttack.Build(raw(1), parent)
            Case "effectpower" : EffectPower.Build(raw(1), parent)
            Case "effectspeed" : EffectSpeed.Build(raw(1), parent)
            Case Else : Return False
        End Select

        Return True
    End Function
    Public Overrides Function ToString() As String
        If TypeOf Me Is EffectAttack Then Return CType(Me, EffectAttack).ToString

        Return Nothing
    End Function
    Friend MustOverride Function Unbuild() As String
    Friend MustOverride Function Merge(ByVal effect As Effect) As Boolean
    Friend Shared Function MergeList(ByVal effects As List(Of Effect)) As List(Of Effect)
        Dim total As New List(Of Effect)
        For Each e As Effect In effects
            Dim matchFound As Boolean = False
            For Each ef As Effect In total
                If ef.Merge(e) = True Then
                    matchFound = True
                    Exit For
                End If
            Next
            If matchFound = False Then total.Add(e)
        Next
        Return total
    End Function
End Class
