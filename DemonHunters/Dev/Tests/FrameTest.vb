Public Class FrameTest
    Shared Sub Main()
        'Dim tank As Unit = Unit.BuildFromDesign("Vanquisher Tank Mk I")
        IO.BracketFilesave(Pathnames.designs, DesignHunter)
        Dim hunter As Unit = Unit.BuildFromDesign("Malleus Hunter Mk II")
    End Sub

    Private Shared Function DesignTank() As Queue(Of String)
        Dim tank As Frame = Frame.Build("Vanquisher Tank")
        tank.Add("Main Tank Weapon", Component.BuildBase("Nova Cannon"))
        tank.Add("Tank Motive System", Component.BuildBase("Tank Treads"))
        tank.Add("Tank Armour", Component.BuildBase("Soulsteel Plating"))
        tank.Add("Tank Power Source", Component.BuildBase("Diesel Engine"))
        Return tank.BuildDesign("Vanquisher Tank Mk I")
    End Function
    Private Shared Function DesignFalchion() As Queue(Of String)
        Dim falchion As Frame = Frame.Build("Falchion")
        falchion.Add("Blade", Subcomponent.Build("Soulsteel Blade"))
        falchion.Add("Enchantment 1", Subcomponent.Build("Hellfire Hex"))
        Return falchion.BuildDesign("Falchion Mk I")
    End Function
    Private Shared Function DesignPistol() As Queue(Of String)
        Dim pistol As Frame = Component.BuildBase("Gauss Pistol")
        pistol.Add("Frame", Component.BuildBase("Heavyduty Pistol Frame"))
        pistol.Add("Caseless Ammo", Component.BuildBase("Standard Gauss Rounds"))
        Return pistol.BuildDesign("Gauss Pistol Mk I")
    End Function
    Private Shared Function DesignHunter() As Queue(Of String)
        Dim hunter As Frame = Frame.Build("Malleus Hunter")
        hunter.Add("Sarcophagus", Subcomponent.Build("Sarcophagus"))
        hunter.Add("Right Hand", Frame.BuildFromDesign("Falchion Mk I"))
        hunter.Add("Left Hand", Frame.BuildFromDesign("Gauss Pistol Mk I"))
        Return hunter.BuildDesign("Malleus Hunter Mk II")
    End Function

End Class
