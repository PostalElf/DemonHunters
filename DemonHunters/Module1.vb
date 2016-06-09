Module Module1

    Sub Main()
        'FrameTest.Main()

        Dim hunterFrame As Frame = Frame.BuildFromDesign("Malleus Hunter Mk I")
    End Sub
    Private Function DesignTank() As Queue(Of String)
        Dim tank As Frame = Frame.Build("Vanquisher Tank")
        tank.Add("Main Tank Weapon", Subcomponent.Build("Nova Cannon"))
        tank.Add("Tank Motive System", Subcomponent.Build("Tank Treads"))
        tank.Add("Tank Armour", Subcomponent.Build("Soulsteel Plating"))
        tank.Add("Tank Power Source", Subcomponent.Build("Diesel Engine"))
        Return tank.BuildDesign("Vanquisher Tank Mk I")
    End Function
    Private Function DesignFalchion() As Queue(Of String)
        Dim falchion As Frame = Frame.Build("Falchion")
        falchion.Add("Blade", Subcomponent.Build("Soulsteel Blade"))
        falchion.Add("Enchantment 1", Subcomponent.Build("Hellfire Hex"))
        Return falchion.BuildDesign("Falchion Mk I")
    End Function
    Private Function DesignHunter() As Queue(Of String)
        Dim hunter As Frame = Frame.Build("Malleus Hunter")
        hunter.Add("Sarcophagus", Subcomponent.Build("Sarcophagus"))
        hunter.Add("Right Hand", Frame.BuildFromDesign("Falchion Mk I"))
        Return hunter.BuildDesign("Malleus Hunter Mk I")
    End Function
End Module
