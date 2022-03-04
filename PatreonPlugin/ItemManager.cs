using System;
using System.Collections.Generic;
using Exiled.API.Features;

namespace PatreonPlugin
{
    public static class ItemManager
    {
        public static void GiveItems(Player Player, List<string> Items)
        {
            Random random = new Random();
            foreach (string Item in Items)
            {
                string[] ItemArray = Item.Split(':');
                int randomNumber = random.Next(0, 100);

                try
                {
                    int chance = int.Parse(ItemArray[1]);
                    if (randomNumber <= chance)
                        Player.AddItem(ParseItem(ItemArray[0]));
                }
                catch
                {
                    Log.Error($"Cannot parse {ItemArray[1]}. {ItemArray[1]} is not a valid integer.");
                }
            }
            
        }

        public static ItemType ParseItem(string Item)
        {

            switch (Item)
            {
                case "None":
                    return ItemType.None;
                case "KeycardJanitor":
                    return ItemType.KeycardJanitor;
                case "KeycardScientist":
                    return ItemType.KeycardScientist;
                case "KeycardResearchCoordinator":
                    return ItemType.KeycardResearchCoordinator;
                case "KeycardZoneManager":
                    return ItemType.KeycardZoneManager;
                case "KeycardGuard":
                    return ItemType.KeycardGuard;
                case "KeycardNTFOfficer":
                    return ItemType.KeycardNTFOfficer;
                case "KeycardContainmentEngineer":
                    return ItemType.KeycardContainmentEngineer;
                case "KeycardNTFLieutenant":
                    return ItemType.KeycardNTFLieutenant;
                case "KeycardNTFCommander":
                    return ItemType.KeycardNTFCommander;
                case "KeycardFacilityManager":
                    return ItemType.KeycardFacilityManager;
                case "KeycardChaosInsurgency":
                    return ItemType.KeycardChaosInsurgency;
                case "KeycardO5":
                    return ItemType.KeycardO5;
                case "Radio":
                    return ItemType.Radio;
                case "GunCOM15":
                    return ItemType.GunCOM15;
                case "Medkit":
                    return ItemType.Medkit;
                case "Flashlight":
                    return ItemType.Flashlight;
                case "MicroHID":
                    return ItemType.MicroHID;
                case "SCP500":
                    return ItemType.SCP500;
                case "SCP207":
                    return ItemType.SCP207;
                case "Ammo12gauge":
                    return ItemType.Ammo12gauge;
                case "GunE11SR":
                    return ItemType.GunE11SR;
                case "GunCrossvec":
                    return ItemType.GunCrossvec;
                case "Ammo556x45":
                    return ItemType.Ammo556x45;
                case "GunFSP9":
                    return ItemType.GunFSP9;
                case "GunLogicer":
                    return ItemType.GunLogicer;
                case "GrenadeHE":
                    return ItemType.GrenadeHE;
                case "GrenadeFlash":
                    return ItemType.GrenadeFlash;
                case "Ammo44cal":
                    return ItemType.Ammo44cal;
                case "Ammo762x39":
                    return ItemType.Ammo762x39;
                case "Ammo9x19":
                    return ItemType.Ammo9x19;
                case "GunCOM18":
                    return ItemType.GunCOM18;
                case "SCP018":
                    return ItemType.SCP018;
                case "SCP268":
                    return ItemType.SCP268;
                case "Adrenaline":
                    return ItemType.Adrenaline;
                case "Painkillers":
                    return ItemType.Painkillers;
                case "Coin":
                    return ItemType.Coin;
                case "ArmorLight":
                    return ItemType.ArmorLight;
                case "ArmorCombat":
                    return ItemType.ArmorCombat;
                case "ArmorHeavy":
                    return ItemType.ArmorHeavy;
                case "GunRevolver":
                    return ItemType.GunRevolver;
                case "GunAK":
                    return ItemType.GunAK;
                case "GunShotgun":
                    return ItemType.GunShotgun;
                default:
                    Log.Warn($"Unknown item ({Item}) passed to item parser. Returning none.");
                    return ItemType.None;
            }
        }
    }
}
