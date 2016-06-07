Public MustInherit Class Effect
    Friend Shared Function BuildBase(ByVal raw As String(), ByVal parent As List(Of Effect)) As Boolean
        Select Case raw(0).Trim.ToLower
            Case "effectattack" : EffectAttack.Build(raw(1), parent)
            Case "effectpower" : EffectPower.Build(raw(1), parent)
            Case Else : Return False
        End Select

        Return True
    End Function
    Public Overrides Function ToString() As String
        If TypeOf Me Is EffectAttack Then Return CType(Me, EffectAttack).ToString

        Return Nothing
    End Function
    Friend MustOverride Function Unbuild() As String
End Class
