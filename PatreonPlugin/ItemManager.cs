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
                case "KeycardScientistMajor":
                    return ItemType.KeycardScientistMajor;
                case "KeycardZoneManager":
                    return ItemType.KeycardZoneManager;
                case "KeycardGuard":
                    return ItemType.KeycardGuard;
                case "KeycardSeniorGuard":
                    return ItemType.KeycardSeniorGuard;
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
                case "WeaponManagerTablet":
                    return ItemType.WeaponManagerTablet;
                case "GunE11SR":
                    return ItemType.GunE11SR;
                case "GunProject90":
                    return ItemType.GunProject90;
                case "Ammo556":
                    return ItemType.Ammo556;
                case "GunMP7":
                    return ItemType.GunMP7;
                case "GunLogicer":
                    return ItemType.GunLogicer;
                case "GrenadeFrag":
                    return ItemType.GrenadeFrag;
                case "GrenadeFlash":
                    return ItemType.GrenadeFlash;
                case "Disarmer":
                    return ItemType.Disarmer;
                case "Ammo762":
                    return ItemType.Ammo762;
                case "Ammo9mm":
                    return ItemType.Ammo9mm;
                case "GunUSP":
                    return ItemType.GunUSP;
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
                default:
                    Log.Warn($"Unknown item ({Item}) passed to item parser. Returning none.");
                    return ItemType.None;
            }
        }
    }
}
