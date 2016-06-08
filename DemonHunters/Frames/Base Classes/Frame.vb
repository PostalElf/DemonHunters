Public Class Frame
    Inherits Component
    Private Slots As New Dictionary(Of String, FrameSlot)
    Private UnitType As UnitType = 0

    Friend Overrides ReadOnly Property TotalEffects As List(Of Effect)
        Get
            Dim total As New List(Of Effect)
            total.AddRange(Effects)
            For Each slot As FrameSlot In Slots.Values
                If slot Is Nothing Then Continue For
                Dim slotEffects As List(Of Effect) = slot.TotalEffects
                If slotEffects Is Nothing = False Then total.AddRange(slotEffects)
            Next
            Return total
        End Get
    End Property
    Friend Overrides ReadOnly Property TotalCosts As List(Of Cost)
        Get
            Dim total As New List(Of Cost)
            total.AddRange(MyBase.Costs)
            For Each slot As FrameSlot In Slots.Values
                If slot Is Nothing Then Continue For
                Dim slotCosts As List(Of Cost) = slot.TotalCosts
                If slotCosts Is Nothing = False Then total.AddRange(slotCosts)
            Next
            Return total
        End Get
    End Property

    Friend Shared Function Build(ByVal raw As Queue(Of String)) As Frame
        Dim frame As New Frame
        While raw.Count > 0
            Dim current As String() = raw.Dequeue.Split(":")
            If frame.BuildBase(current, frame) = False Then
                With frame
                    Select Case current(0).Trim.ToLower
                        Case "slot" : FrameSlot.Build(current(1).Trim, .Slots)
                        Case "unittype" : .UnitType = Dev.StringToEnum(current(1).Trim, GetType(UnitType))
                        Case Else : Throw New Exception("Invalid build string for Frame")
                    End Select
                End With
            End If
        End While
        Return frame
    End Function
    Friend Overrides Function Unbuild() As Queue(Of String)
        Dim total As New Queue(Of String)
        With total
            .Enqueue("Name:" & Name)
            .Enqueue("UnitType:" & UnitType.ToString)
            .Enqueue("Keywords:" & Dev.ListWithCommas(Keywords))
            For Each Cost As Cost In Costs
                .Enqueue("Cost:" & Cost.Unbuild)
            Next
            For Each slot As FrameSlot In Slots.Values
                .Enqueue("Slot:" & slot.Unbuild)
            Next
        End With
        Return total
    End Function
    Public Overrides Function ToString() As String
        Return Name
    End Function

    Friend Function AddCheck(ByVal slotName As String, ByRef component As Component) As Boolean
        If Slots.ContainsKey(slotName) = False Then Return False
        If Slots(slotName) Is Nothing = True Then Throw New Exception("Frame Component: invalid slotname")
        If Slots(slotName).AddCheck(component) = False Then Return False

        Return True
    End Function
    Friend Sub Add(ByVal slotName As String, ByRef component As Component)
        Slots(slotName).Add(component)
    End Sub
    Friend Function RemoveCheck(ByVal slotName As String) As Boolean
        If Slots.ContainsKey(slotName) = False Then Return False
        If Slots(slotName) Is Nothing Then Return False
        If Slots(slotName).RemoveCheck = False Then Return False

        Return True
    End Function
    Friend Function Remove(ByVal slotName As String) As Component
        Remove = Slots(slotName).Remove
    End Function

    Friend ReadOnly Property DesignReady As CheckReason
        Get
            For Each slot As FrameSlot In Slots.Values
                If slot.DesignReady Is Nothing = False Then Return slot.DesignReady
            Next
            Return Nothing
        End Get
    End Property
    Friend Function BuildUnitLimbs() As List(Of UnitLimb)
        Dim total As New List(Of UnitLimb)
        For Each slot In Slots.Values
            Dim limb As UnitLimb = slot.BuildLimb
            If limb Is Nothing = False Then total.Add(limb)
        Next
        Return total
    End Function
    Friend Function BuildUnitEffectsDictionary() As EffectsDictionary
        Dim total As New EffectsDictionary
        For Each Effect As Effect In TotalEffects
            total.Add(Effect)
        Next
        Return total
    End Function
End Class

Public Class FrameSlot
    Private Name As String
    Private KeywordRequirements As New List(Of String)
    Private EquippedComponent As Component
    Private IsCompulsory As Boolean = False

    Friend Shared Function Build(ByVal raw As String, ByRef parent As Dictionary(Of String, FrameSlot)) As FrameSlot
        'slot:hunter right hand|hunter hand, melee

        Dim r As String() = raw.Split("|")
        Dim slot As New FrameSlot
        With slot
            .Name = r(0).Trim
            .KeywordRequirements = Dev.ParseCommaList(r(1))
            If .KeywordRequirements.Contains("Compulsory") Then
                .KeywordRequirements.Remove("Compulsory")
                .IsCompulsory = True
            End If
        End With
        parent.Add(slot.Name, slot)
        Return slot
    End Function
    Friend Function Unbuild() As String
        Dim total As String = ""
        total &= Name
        total &= "|"
        total &= Dev.ListWithCommas(KeywordRequirements)
        If IsCompulsory = True Then total &= ", Compulsory"
        Return total
    End Function
    Public Overrides Function ToString() As String
        If EquippedComponent Is Nothing Then Return "-" Else Return EquippedComponent.ToString
    End Function

    Friend Function AddCheck(ByRef component As Component) As Boolean
        If EquippedComponent Is Nothing = False Then Return False
        If component.CheckKeyword(KeywordRequirements) = False Then Return False

        Return True
    End Function
    Friend Sub Add(ByRef component As Component)
        EquippedComponent = component
    End Sub
    Friend Function RemoveCheck() As Boolean
        If EquippedComponent Is Nothing Then Return False
        Return True
    End Function
    Friend Function Remove() As Component
        Remove = EquippedComponent
        EquippedComponent = Nothing
    End Function
    Friend Function TotalEffects() As List(Of Effect)
        If EquippedComponent Is Nothing Then Return Nothing Else Return EquippedComponent.TotalEffects
    End Function
    Friend Function TotalCosts() As List(Of Cost)
        If EquippedComponent Is Nothing Then Return Nothing Else Return EquippedComponent.TotalCosts
    End Function
    Friend Function BuildLimb() As UnitLimb
        If EquippedComponent Is Nothing Then Return Nothing
        Return EquippedComponent.BuildLimb
    End Function
    Friend ReadOnly Property DesignReady As CheckReason
        Get
            If EquippedComponent Is Nothing = False Then Return Nothing
            If IsCompulsory = False Then Return Nothing

            Return New CheckReason("Empty Compulsory Slot", Name & " must be filled.")
        End Get
    End Property
End Class