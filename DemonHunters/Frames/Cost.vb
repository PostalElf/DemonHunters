Public Class Cost
    Friend Property PaymentTime As PaymentTime
    Friend Property PaymentNature As PaymentNature
    Friend Property CrewType As CrewType
    Friend Property Quantity As Integer

    Public Sub New()
    End Sub
    Public Sub New(ByVal template As Cost)
        With template
            PaymentTime = .PaymentTime
            PaymentNature = .PaymentNature
            CrewType = .CrewType
            Quantity = .Quantity
        End With
    End Sub
    Friend Shared Function Build(ByVal raw As String, ByRef parent As List(Of Cost)) As Cost
        'cost:5 mana at production

        Dim cost As New Cost
        With cost
            Dim r As String() = raw.Split(" ")
            .Quantity = CInt(r(0))
            Select Case r(1).ToLower
                Case "labour" : .PaymentNature = PaymentNature.Labour
                Case "mana" : .PaymentNature = PaymentNature.Mana
                Case "supplies" : .PaymentNature = PaymentNature.Supplies
                Case Else
                    .CrewType = Dev.StringToEnum(r(1).Trim, GetType(CrewType))
                    If .CrewType = 0 Then Throw New Exception("Invalid crewtype for cost.") Else .PaymentNature = PaymentNature.Crew
            End Select
            .PaymentTime = Dev.StringToEnum(r(3).Trim, GetType(PaymentTime))
        End With
        parent.Add(cost)
        Return cost
    End Function
    Friend Function Unbuild() As String
        Dim total As String = ""
        total &= Quantity & " "
        If PaymentNature = DemonHunters.PaymentNature.Crew Then total &= CrewType.ToString Else total &= PaymentNature.ToString
        total &= " at "
        total &= PaymentTime.ToString
        Return total
    End Function
    Friend Shared Function Strip(ByVal inputList As List(Of Cost), ByVal paymentTime As PaymentTime) As List(Of Cost)
        Dim total As New List(Of Cost)
        For Each input As Cost In inputList
            If input.PaymentTime <> paymentTime Then total.Add(input)
        Next
        Return total
    End Function
    Friend Shared Function Retain(ByVal inputList As List(Of Cost), ByVal paymentTime As PaymentTime) As List(Of Cost)
        Dim total As New List(Of Cost)
        For Each input As Cost In inputList
            If input.PaymentTime = paymentTime Then total.Add(input)
        Next
        Return total
    End Function

    Friend Shared Function Merge(ByVal inputList As List(Of Cost)) As List(Of Cost)
        Dim t As New List(Of Cost)
        For Each input As Cost In inputList
            Dim matchFound As Boolean = False
            For n = 0 To t.Count - 1
                If t(n) = input Then
                    t(n).Merge(input)
                    matchFound = True
                    Exit For
                End If
            Next
            If matchFound = False Then t.Add(New Cost(input))
        Next
        Return t
    End Function
    Friend Sub Merge(ByVal cost As Cost)
        If cost <> Me Then Exit Sub

        Quantity += cost.Quantity
    End Sub
    Public Shared Operator =(ByVal v1 As Cost, ByVal v2 As Cost) As Boolean
        If v1.PaymentTime = v2.PaymentTime AndAlso v1.PaymentNature = v2.PaymentNature AndAlso v1.CrewType = v1.CrewType Then Return True
        Return False
    End Operator
    Public Shared Operator <>(ByVal v1 As Cost, ByVal v2 As Cost) As Boolean
        If v1 = v2 Then Return False Else Return True
    End Operator
    Public Overrides Function ToString() As String
        Dim t As String = Quantity

        If PaymentNature = PaymentNature.Crew Then t &= " " & CrewType.ToString Else t &= " " & PaymentNature.ToString

        Select Case PaymentTime
            Case PaymentTime.Use : t &= " when used."
            Case PaymentTime.Production : t &= " production costs."
            Case PaymentTime.Deployment : t &= " when deployed."
            Case PaymentTime.Refundable : t &= " when deployed (refundable)."
        End Select

        Return t
    End Function

End Class

Public Enum PaymentTime
    Production = 1
    Deployment
    Refundable
    Use
End Enum

Public Enum PaymentNature
    Labour = 1
    Crew
    Mana
    Supplies
End Enum