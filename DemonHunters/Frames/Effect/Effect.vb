Public MustInherit Class Effect
    'note: general effect.build is handled within subcomponent.build

    Public Overrides Function ToString() As String
        If TypeOf Me Is EffectAttack Then Return CType(Me, EffectAttack).ToString

        Return Nothing
    End Function
    Friend MustOverride Function Unbuild() As String
End Class
