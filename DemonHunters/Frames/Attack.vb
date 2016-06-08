Public Class Attack
    Inherits System.Collections.DictionaryBase
    Private UseCost As New List(Of Cost)

    Public Sub New()
        For Each dt As EffectAttackDistance In System.Enum.GetValues(GetType(EffectAttackDistance))
            Dictionary.Add(dt, Nothing)
        Next
    End Sub
    Public Overrides Function ToString() As String
        Dim total As String = ""
        For Each dt As EffectAttackDistance In System.Enum.GetValues(GetType(EffectAttackDistance))
            Dim effect As EffectAttack = Dictionary(dt)
            If effect Is Nothing = False Then total &= effect.ToString & vbCrLf
        Next
        Return total
    End Function
    Public Shared Function Build(ByVal effectAttackList As List(Of EffectAttack), ByVal costList As List(Of Cost)) As Attack
        Dim total As New Attack

        'merge effectAttacks and add them to the dictionary
        Dim totalEffectAttacks As List(Of EffectAttack) = EffectAttack.MergeList(effectAttackList)
        For Each ea As EffectAttack In totalEffectAttacks
            total(ea.Distance) = ea
        Next

        'add all use cost
        total.UseCost = Cost.Retain(costList, PaymentTime.Use)

        Dim valueFound As Boolean = False
        For Each v As EffectAttack In total.Dictionary.Values
            If v Is Nothing = False Then
                valueFound = True
                Exit For
            End If
        Next
        If valueFound = True Then Return total Else Return Nothing
    End Function

    Default Public Property Item(ByVal key As EffectAttackDistance) As EffectAttack
        Get
            Return Dictionary(key)
        End Get
        Set(ByVal value As EffectAttack)
            Dictionary(key) = value
        End Set
    End Property
End Class
