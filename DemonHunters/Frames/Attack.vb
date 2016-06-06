Public Class Attack
    Inherits System.Collections.DictionaryBase
    Private UseCost As New List(Of Cost)

    Public Sub New()
        For Each dt As EffectDistance In System.Enum.GetValues(GetType(EffectDistance))
            Dictionary.Add(dt, Nothing)
        Next
    End Sub
    Public Overrides Function ToString() As String
        Dim total As String = ""
        For Each dt As EffectDistance In System.Enum.GetValues(GetType(EffectDistance))
            Dim effect As Effect = Dictionary(dt)
            If effect Is Nothing = False Then total &= effect.ToString & vbCrLf
        Next
        Return total
    End Function
    Public Shared Function Build(ByVal effectList As List(Of Effect), ByVal costList As List(Of Cost)) As Attack
        Dim total As New Attack
        For Each e As Effect In effectList
            Dim distance As EffectDistance = e.Distance
            If total(distance) Is Nothing Then total(distance) = New Effect
            With total(distance)
                .Distance = distance
                .Accuracy += e.Accuracy
                .Damages += e.Damages
            End With
        Next

        'constrain accuracy to within 5 and 95
        For Each dt As EffectDistance In System.Enum.GetValues(GetType(EffectDistance))
            If total(dt) Is Nothing = False Then total(dt).Accuracy = Dev.Constrain(total(dt).Accuracy, 5, 95)
        Next

        'add all use cost
        For Each Cost As Cost In costList
            If Cost.PaymentTime = PaymentTime.Use Then total.UseCost.Add(Cost)
        Next

        Return total
    End Function

    Public Sub Add(ByVal key As EffectDistance, ByVal value As Effect)
        Dictionary.Add(key, value)
    End Sub
    Public Function ContainsKey(ByVal key As EffectDistance) As Boolean
        For Each k As EffectDistance In Dictionary.Keys
            If k = key Then Return True
        Next
        Return False
    End Function
    Default Public Property Item(ByVal key As EffectDistance) As Effect
        Get
            Return Dictionary(key)
        End Get
        Set(ByVal value As Effect)
            Dictionary(key) = value
        End Set
    End Property
End Class
