Public Class EffectsDictionary
    Inherits System.Collections.DictionaryBase

    Public Sub New()
        For Each t As Type In Effect.EffectTypeList
            Dictionary.Add(t, New List(Of Effect))
        Next
    End Sub
    Friend Shared Function Build(ByVal effects As List(Of Effect)) As EffectsDictionary
        Dim total As New EffectsDictionary
        With total
            For Each Effect As Effect In effects
                .Add(Effect)
            Next
        End With
        Return total
    End Function
    Public Overrides Function ToString() As String
        Dim total As Integer = 0
        For Each l As List(Of Effect) In Dictionary.Values
            total += l.Count
        Next
        Return "Count: " & total
    End Function

    Default Friend Property Item(ByVal EffectType As Type) As List(Of Effect)
        Get
            Return Dictionary(EffectType)
        End Get
        Set(ByVal value As List(Of Effect))
            Dim l As List(Of Effect) = Dictionary(EffectType)
            l = value
        End Set
    End Property
    Friend Sub Add(ByVal ef As Effect)
        'effectattacks are handled differently, by buildUnitAttack instead of buildUnitEffectDictionary
        If TypeOf ef Is EffectAttack Then Exit Sub

        'merge with existing effect; if none found, add to list
        Dim l As List(Of Effect) = Dictionary(ef.GetType)
        Dim matchFound As Boolean = False
        For Each e As Effect In l
            If e.GetType = ef.GetType Then
                matchFound = True
                e.Merge(ef)
                Exit For
            End If
        Next
        If matchFound = False Then l.Add(ef)
    End Sub
    Friend Sub Remove(ByVal effect As Effect)
        Dim t As Type = GetType(Effect)
        Dim l As List(Of Effect) = Dictionary(t)
        If l.Contains(effect) Then l.Remove(effect)
    End Sub
    Friend ReadOnly Property DesignReady As CheckReason
        Get
            'check power
            Dim powers As List(Of Effect) = Item(GetType(EffectPower))
            For Each power As EffectPower In powers
                If power.Quantity < 0 Then Return New CheckReason("Insufficient " & power.Type.ToString & " Power")
            Next

            'check speed
            Dim speeds As List(Of Effect) = Item(GetType(EffectSpeed))
            If speeds.Count = 0 Then Return New CheckReason("Insufficient Speed")
            Dim speed As EffectSpeed = speeds(0)
            If speed.Walk <= 0 AndAlso speed.Stride <= 0 AndAlso speed.Run <= 0 Then Return New CheckReason("Insufficient Speed")


            Return Nothing
        End Get
    End Property
End Class
