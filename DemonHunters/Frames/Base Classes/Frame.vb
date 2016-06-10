Public Class Frame
    Inherits Component
    Private Slots As New Dictionary(Of String, FrameSlot)
    Private UnitType As UnitType = 0
    Private ReadOnly Property IsLimbless As Boolean
        Get
            For Each slot In Slots.Values
                If slot.isLimb = True Then Return False
            Next
            Return True
        End Get
    End Property

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
            Return Cost.MergeList(total)
        End Get
    End Property

    Friend Shared Function Build(ByVal raw As Queue(Of String)) As Frame
        Dim frame As New Frame
        frame.Name = raw.Dequeue.Trim
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
    Friend Shared Function Build(ByVal targetName As String) As Frame
        Dim designRaw As Queue(Of String) = IO.BracketFileget(Pathnames.frames, targetName)
        If designRaw Is Nothing Then Return Nothing
        Return Build(designRaw)
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

    Friend Function BuildUnitLimbs() As List(Of UnitLimb)
        If IsLimbless = True Then
            Return New List(Of UnitLimb) From {UnitLimb.Build(Name, TotalEffects, TotalCosts)}
        Else
            Dim total As New List(Of UnitLimb)
            For Each slot In Slots.Values
                Dim limb As UnitLimb = slot.BuildLimb
                If limb Is Nothing = False Then total.Add(limb)
            Next
            Return total
        End If
    End Function
    Friend Function BuildUnitEffects() As List(Of Effect)
        Dim total As New List(Of Effect)
        total.AddRange(Me.Effects)
        For Each slot In Slots.Values
            Dim effects As List(Of Effect) = slot.BuildEffects
            If effects Is Nothing = False Then total.AddRange(effects)
        Next

        'strip effectAttacks
        For n = total.Count - 1 To 0 Step -1
            Dim effect As Effect = total(n)
            If TypeOf effect Is EffectAttack Then total.RemoveAt(n)
        Next
        Return total
    End Function

    Friend ReadOnly Property DesignReady As CheckReason
        Get
            For Each slot As FrameSlot In Slots.Values
                If slot.DesignReady Is Nothing = False Then Return slot.DesignReady
            Next
            Return Nothing
        End Get
    End Property
    Friend Function BuildDesign(ByVal designName As String) As Queue(Of String)
        Dim total As New Queue(Of String)
        With total
            .Enqueue(designName)
            .Enqueue("BaseFrame:" & Name)
            For Each slot In Slots.Values
                .Enqueue("Slot:" & slot.BuildDesign)
            Next
        End With
        Return total
    End Function
    Friend Shared Function BuildFromDesign(ByVal designRaw As Queue(Of String)) As Frame
        Dim designName As String = designRaw.Dequeue()
        Dim baseFrameName As String = designRaw.Dequeue.Split(":")(1)
        Dim baseFrame As Frame = Frame.Build(baseFrameName)
        If baseFrame Is Nothing Then Return Nothing

        With baseFrame
            .Name = designName

            While designRaw.Count > 0
                Dim currentLine As String = designRaw.Dequeue
                Dim s As String() = currentLine.Split(":")
                Dim sp As String() = s(1).Split("|")

                Dim slotName As String = sp(0)
                Dim slotEquipName As String = sp(1)
                If slotEquipName <> "-" Then
                    'sp(2) / sp.count = 3 only exists if |IsFrame flag is added
                    Dim component As Component
                    If sp.Count = 3 Then component = Frame.BuildFromDesign(slotEquipName) Else component = Subcomponent.Build(slotEquipName)
                    If component Is Nothing = False Then .Add(slotName, component) Else Throw New Exception("Invalid Slot BuildFromDesign")
                End If
            End While
        End With
        Return baseFrame
    End Function
    Friend Shared Function BuildFromDesign(ByVal designName As String) As Frame
        Dim designRaw As Queue(Of String) = IO.BracketFileget(Pathnames.designs, designName)
        If designRaw Is Nothing Then Return Nothing Else Return BuildFromDesign(designRaw)
    End Function
End Class

Public Class FrameSlot
    Private Name As String
    Private KeywordRequirements As New List(Of String)
    Private EquippedComponent As Component
    Private IsCompulsory As Boolean = False
    Friend ReadOnly Property IsLimb As Boolean
        Get
            Return _IsLimb
        End Get
    End Property
    Private _IsLimb As Boolean = False

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
            If .KeywordRequirements.Contains("IsLimb") Then
                .KeywordRequirements.Remove("IsLimb")
                ._IsLimb = True
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
        If _IsLimb = False Then Return Nothing
        If EquippedComponent Is Nothing Then Return Nothing
        Return UnitLimb.Build(Name, TotalEffects, TotalCosts)
    End Function
    Friend Function BuildEffects() As List(Of Effect)
        If _IsLimb = True Then Return Nothing
        If EquippedComponent Is Nothing Then Return Nothing
        Return TotalEffects()
    End Function

    Friend ReadOnly Property DesignReady As CheckReason
        Get
            If EquippedComponent Is Nothing = False Then Return Nothing
            If IsCompulsory = False Then Return Nothing

            Return New CheckReason("Empty Compulsory Slot", Name & " must be filled.")
        End Get
    End Property
    Friend Function BuildDesign() As String
        Dim total As String = Name
        If EquippedComponent Is Nothing Then
            total &= "|-"
        Else
            total &= "|" & EquippedComponent.Name
            If TypeOf EquippedComponent Is Frame Then total &= "|IsFrame"
        End If

        Return total
    End Function
End Class