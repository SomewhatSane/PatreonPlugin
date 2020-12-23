using System.Collections.Generic;
using Exiled.API.Features;

namespace PatreonPlugin
{
    public class ItemManager
    {
        public static void GiveItems(Player Player, List<string> Items)
        {
            foreach (string Item in Items)
            {
                Player.AddItem(ParseItem(Item));
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

        public static string RoleToString(RoleType Role)
        {
            switch (Role)
            {
                case RoleType.Scp173:
                    return "Scp173";
                case RoleType.Scp106:
                    return "Scp106";
                case RoleType.Scp049:
                    return "Scp049";
                case RoleType.Scp079:
                    return "Scp079";
                case RoleType.Scp096:
                    return "Scp096";
                case RoleType.Scp0492:
                    return "Scp0492";
                case RoleType.Scp93953:
                    return "Scp93953";
                case RoleType.Scp93989:
                    return "Scp93989";
                case RoleType.NtfScientist:
                    return "NtfScientist";
                case RoleType.ChaosInsurgency:
                    return "ChaosInsurgency";
                case RoleType.NtfLieutenant:
                    return "NtfLieutenant";
                case RoleType.NtfCommander:
                    return "NtfCommander";
                case RoleType.NtfCadet:
                    return "NtfCadet";
                case RoleType.FacilityGuard:
                    return "FacilityGuard";
                case RoleType.Scientist:
                    return "Scientist";
                case RoleType.ClassD:
                    return "ClassD";
                case RoleType.Spectator:
                    return "Spectator";
                case RoleType.Tutorial:
                    return "Tutorial";
                case RoleType.None:
                    return "None";
                default:
                    Log.Info($"Unknown role {Role} passed to role parser. Returning None.");
                    return "None";
            }
        }
    }
}
