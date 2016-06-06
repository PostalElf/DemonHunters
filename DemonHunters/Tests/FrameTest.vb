Public Class FrameTest
    Shared Sub Main()
        HunterTest()
        TankTest()
    End Sub
    Shared Sub HunterTest()
        Dim hunter As Frame = BuildHunter()
        Dim falchion As Frame = BuildFalchion()
        Dim pistol As Frame = BuildPistol()

        'Dim falchionAttack As Attack = falchion.BuildAttack
        'Dim pistolAttack As Attack = pistol.BuildAttack

        hunter.AddCheck("Hunter Right Hand", falchion)
        hunter.Add("Hunter Right Hand", falchion)
        hunter.AddCheck("Hunter Left Hand", pistol)
        hunter.Add("Hunter Left Hand", pistol)

        Dim hunterDesign As FrameDesign = FrameDesign.Build(hunter)
        hunter = Nothing
    End Sub
    Private Shared Function BuildHunter() As Frame
        Dim hunterRaw As New Queue(Of String)
        With hunterRaw
            .Enqueue("Name:Malleus Hunter")
            .Enqueue("UnitType:Hunter")
            .Enqueue("Keywords:Hunter")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Cost:10 labour at production")
            .Enqueue("Cost:10 mana at deployment")
            .Enqueue("Cost:1 recruits at deployment")
            .Enqueue("Slot:Hunter Right Hand|Hunter Hand,Melee")
            .Enqueue("Slot:Hunter Left Hand|Hunter Hand")
            .Enqueue("Slot:Hunter Right Shoulder|Hunter Shoulder")
        End With
        Dim hunter As Frame = Frame.Build(hunterRaw)

        Return hunter
    End Function
    Private Shared Function BuildFalchion() As Frame
        Dim falchionRaw As New Queue(Of String)
        With falchionRaw
            .Enqueue("Name:Falchion")
            .Enqueue("Keywords:Hunter Hand,Melee")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Cost:1 labour at production")
            .Enqueue("Slot:Blade|Hard Blade")
            .Enqueue("Slot:Enchantment 1|Melee Enchantment")
            .Enqueue("Slot:Enchantment 2|Melee Enchantment")
            .Enqueue("IsAttack")
        End With
        Dim falchion As Frame = Frame.Build(falchionRaw)

        Dim bladeRaw As New Queue(Of String)
        With bladeRaw
            .Enqueue("Name:Soulsteel Blade")
            .Enqueue("Keywords:Hard Blade")
            .Enqueue("Effect:Melee|50|3-5 kinetic_hard")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Cost:1 labour at production")
        End With
        Dim blade As Subcomponent = Subcomponent.Build(bladeRaw)
        If falchion.AddCheck("Blade", blade) = False Then Throw New Exception
        falchion.Add("Blade", blade)

        Dim hellfireRaw As New Queue(Of String)
        With hellfireRaw
            .Enqueue("Name:Hellfire Hex")
            .Enqueue("Keywords:Melee Enchantment")
            .Enqueue("Cost:5 mana at production")
            .Enqueue("Cost:1 labour at production")
            .Enqueue("Effect:Melee|0|5-5 arcane")
        End With
        Dim hellfire As Subcomponent = Subcomponent.Build(hellfireRaw)
        If falchion.AddCheck("Enchantment 1", hellfire) = False Then Throw New Exception
        falchion.Add("Enchantment 1", hellfire)

        Dim truestrikeRaw As New Queue(Of String)
        With truestrikeRaw
            .Enqueue("Name:Truestriking")
            .Enqueue("Keywords:Melee Enchantment")
            .Enqueue("Cost:5 mana at production")
            .Enqueue("Effect:Melee|20|1-1 kinetic_hard")
        End With
        Dim truestrike As Subcomponent = Subcomponent.Build(truestrikeRaw)
        If falchion.AddCheck("Enchantment 2", truestrike) = False Then Throw New Exception
        falchion.Add("Enchantment 2", truestrike)

        Return falchion
    End Function
    Private Shared Function BuildPistol() As Frame
        Dim pistolRaw As New Queue(Of String)
        With pistolRaw
            .Enqueue("Name:Gauss Pistol")
            .Enqueue("Keywords:Hunter Hand")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Slot:Pistol Frame|Pistol Frame")
            .Enqueue("Slot:Caseless Ammo|Caseless Ammo")
            .Enqueue("IsAttack")
        End With
        Dim pistol As Frame = Frame.Build(pistolRaw)

        Dim pistolframeRaw As New Queue(Of String)
        With pistolframeRaw
            .Enqueue("Name:Heavyduty Pistol Frame")
            .Enqueue("Keywords:Pistol Frame")
            .Enqueue("Effect:Close|35|3-5 kinetic_hard")
            .Enqueue("Effect:Medium|10|1-3 kinetic_hard")
        End With
        Dim pistolframe As Subcomponent = Subcomponent.Build(pistolframeRaw)
        If pistol.AddCheck("Pistol Frame", pistolframe) = False Then Throw New Exception
        pistol.Add("Pistol Frame", pistolframe)

        Dim caselessRaw As New Queue(Of String)
        With caselessRaw
            .Enqueue("Name:Standard Gauss Rounds")
            .Enqueue("Keywords:Caseless Ammo")
            .Enqueue("Cost:1 supplies at use")
            .Enqueue("Effect:Close|5|0-0 none")
        End With
        Dim caseless As Subcomponent = Subcomponent.Build(caselessRaw)
        If pistol.AddCheck("Caseless Ammo", caseless) = False Then Throw New Exception
        pistol.Add("Caseless Ammo", caseless)

        Return pistol
    End Function

    Shared Sub TankTest()
        Dim tankRaw As New Queue(Of String)
        With tankRaw
            .Enqueue("Name:Vanquisher Tank")
            .Enqueue("UnitType:Tank")
            .Enqueue("Keywords:Tank")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Cost:5 soldiers at deployment")
            .Enqueue("Slot:Main Tank Weapon|Main Tank Weapon")
            .Enqueue("Slot:Secondary Tank Weapon|Secondary Tank Weapon")
            .Enqueue("Slot:Tank Motive System|Tank Motive System")
            .Enqueue("Slot:Tank Armour|Tank Armour")
        End With
        Dim tank As Frame = Frame.Build(tankRaw)

        Dim gunRaw As New Queue(Of String)
        With gunRaw
            .Enqueue("Name:Nova Cannon")
            .Enqueue("Keywords:Main Tank Weapon")
            .Enqueue("Cost:3 supplies at production")
            .Enqueue("Cost:1 labour at production")
            .Enqueue("Effect:Close|45|4-6 energy_hard")
            .Enqueue("Effect:Medium|40|3-5 energy_hard")
            .Enqueue("IsAttack")
        End With
        Dim gun As Subcomponent = Subcomponent.Build(gunRaw)
        If tank.AddCheck("Main Tank Weapon", gun) = False Then Throw New Exception
        tank.Add("Main Tank Weapon", gun)

        Dim tankDesign As FrameDesign = FrameDesign.Build(tank)
        tank = Nothing
    End Sub
End Class
