﻿Public Class FrameTest
    Shared Sub Main()
        HunterTest()
        TankTest()
    End Sub
    Shared Sub HunterTest()
        Dim hunter As Frame = BuildHunter()
        Dim comps As New Dictionary(Of String, Component)
        With comps
            .Add("Hunter Right Hand", BuildFalchion)
            .Add("Hunter Left Hand", BuildPistol)
        End With

        For Each kvp As KeyValuePair(Of String, Component) In comps
            If hunter.AddCheck(kvp.Key, kvp.Value) = False Then Throw New Exception
            hunter.Add(kvp.Key, kvp.Value)
        Next

        Dim hunterDesign As FrameDesign = FrameDesign.Build(hunter)
        Dim hunterUnbuilt As Queue(Of String) = hunter.Unbuild
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
            .Enqueue("EffectAttack:Melee|50|3-5 kinetic_hard")
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
            .Enqueue("EffectAttack:Melee|0|5-5 arcane")
        End With
        Dim hellfire As Subcomponent = Subcomponent.Build(hellfireRaw)
        If falchion.AddCheck("Enchantment 1", hellfire) = False Then Throw New Exception
        falchion.Add("Enchantment 1", hellfire)

        Dim truestrikeRaw As New Queue(Of String)
        With truestrikeRaw
            .Enqueue("Name:Truestriking")
            .Enqueue("Keywords:Melee Enchantment")
            .Enqueue("Cost:5 mana at production")
            .Enqueue("EffectAttack:Melee|20|1-1 kinetic_hard")
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
            .Enqueue("EffectAttack:Close|35|3-5 kinetic_hard")
            .Enqueue("EffectAttack:Medium|10|1-3 kinetic_hard")
        End With
        Dim pistolframe As Subcomponent = Subcomponent.Build(pistolframeRaw)
        If pistol.AddCheck("Pistol Frame", pistolframe) = False Then Throw New Exception
        pistol.Add("Pistol Frame", pistolframe)

        Dim caselessRaw As New Queue(Of String)
        With caselessRaw
            .Enqueue("Name:Standard Gauss Rounds")
            .Enqueue("Keywords:Caseless Ammo")
            .Enqueue("Cost:1 supplies at use")
            .Enqueue("EffectAttack:Close|5|0-0 none")
        End With
        Dim caseless As Subcomponent = Subcomponent.Build(caselessRaw)
        If pistol.AddCheck("Caseless Ammo", caseless) = False Then Throw New Exception
        pistol.Add("Caseless Ammo", caseless)

        Return pistol
    End Function

    Shared Sub TankTest()
        Dim tank As Frame = BuildTank()
        Dim comps As New Dictionary(Of String, Component)
        With comps
            .Add("Main Tank Weapon", BuildNova)
            .Add("Tank Motive System", BuildTreads)
            .Add("Tank Armour", BuildArmour)
            .Add("Tank Power Source", BuildDiesel)
        End With

        For Each kvp As KeyValuePair(Of String, Component) In comps
            If tank.AddCheck(kvp.Key, kvp.Value) = False Then Throw New Exception
            tank.Add(kvp.Key, kvp.Value)
        Next

        Dim tankDesign As FrameDesign = FrameDesign.Build(tank)
        Dim tankUnbuilt As Queue(Of String) = tank.Unbuild
        tank = Nothing
    End Sub
    Private Shared Function BuildTank() As Frame
        Dim tankRaw As New Queue(Of String)
        With tankRaw
            .Enqueue("Name:Vanquisher Tank")
            .Enqueue("UnitType:Tank")
            .Enqueue("Keywords:Tank")
            .Enqueue("Cost:5 supplies at production")
            .Enqueue("Cost:5 soldiers at deployment")
            .Enqueue("Slot:Main Tank Weapon|Main Tank Weapon,Compulsory")
            .Enqueue("Slot:Secondary Tank Weapon|Secondary Tank Weapon")
            .Enqueue("Slot:Tank Motive System|Tank Motive System,Compulsory")
            .Enqueue("Slot:Tank Armour|Tank Armour")
            .Enqueue("Slot:Tank Power Source|Tank Power Source,Compulsory")
        End With
        Dim tank As Frame = Frame.Build(tankRaw)
        Return tank
    End Function
    Private Shared Function BuildNova() As Subcomponent
        Dim novaRaw As New Queue(Of String)
        With novaRaw
            .Enqueue("Name:Nova Cannon")
            .Enqueue("Keywords:Main Tank Weapon")
            .Enqueue("Cost:3 supplies at production")
            .Enqueue("Cost:1 labour at production")
            .Enqueue("EffectAttack:Close|45|4-6 energy_hard")
            .Enqueue("EffectAttack:Medium|40|3-5 energy_hard")
            .Enqueue("IsAttack")
        End With
        Dim nova As Subcomponent = Subcomponent.Build(novaRaw)
        Return nova
    End Function
    Private Shared Function BuildTreads() As Subcomponent
        Dim treadsRaw As New Queue(Of String)
        With treadsRaw
            .Enqueue("Name:Tank Treads")
            .Enqueue("Keywords:Tank Motive System")
            .Enqueue("Cost:1 labour at production")
        End With
        Dim treads As Subcomponent = Subcomponent.Build(treadsRaw)
        Return treads
    End Function
    Private Shared Function BuildArmour() As Subcomponent
        Dim armourRaw As New Queue(Of String)
        With armourRaw
            .Enqueue("Name:Soulsteel Plating")
            .Enqueue("Keywords:Tank Armour, Hunter Armour")
            .Enqueue("Cost:3 supplies at production")
        End With
        Dim armour As Subcomponent = Subcomponent.Build(armourRaw)
        Return armour
    End Function
    Private Shared Function BuildDiesel() As Subcomponent
        Dim raw As New Queue(Of String)
        With raw
            .Enqueue("Name:Diesel Engine")
            .Enqueue("Keywords:Tank Power Source")
            .Enqueue("Cost:1 supplies at production")
        End With
        Return Subcomponent.Build(raw)
    End Function
End Class
