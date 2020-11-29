
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Fougerite;

namespace JianxianS.Core
{
    public class Kit
    {
        static int startercd = 180;
        static object temp;
        static int xklbcd = 10800;
        static int vipcd = 86400;
        static TimeSpan timeSpan;
        private static DataStore ds = DataStore.GetInstance();
        public static void starter(Fougerite.Player player)
        {
            temp = ds.Get("starter", player.SteamID);
            if (temp == null)
            {               
                ds.Add("starter", player.SteamID, DateTime.Now);
                player.Inventory.AddItem("Hatchet", 1);
                player.Inventory.AddItem("Pick Axe", 1);
                player.Inventory.AddItem("Cooked Chicken Breast", 20);
                player.Message("领取成功");
            }
            else
            {
                timeSpan = DateTime.Now - (DateTime)temp;
                if ((int)timeSpan.TotalSeconds >= startercd)
                {
                    ds.Add("starter", player.SteamID, DateTime.Now);
                    player.Inventory.AddItem("Hatchet", 1);
                    player.Inventory.AddItem("Pick Axe", 1);
                    player.Inventory.AddItem("Cooked Chicken Breast", 20);
                    player.Message("领取成功");
                }
                else player.Message("您还需要" + (startercd - (int)timeSpan.TotalSeconds) + "秒方可再次领取");
            }
        }
        public static void xklb(Fougerite.Player player)
        {
            temp = ds.Get("xklb", player.SteamID);

            if (temp == null)
            {
                ds.Add("xklb", player.SteamID, DateTime.Now);
                player.Inventory.AddItem("Wood Planks", 1);
                player.Inventory.AddItem("Workbench", 1);
                player.Inventory.AddItem("Large Wood Storage", 1);
                player.Inventory.AddItem("Furnace", 1);
                player.Inventory.AddItem("Metal Door", 1);
                player.Inventory.AddItem("Cloth Helmet", 1);
                player.Inventory.AddItem("Cloth Vest", 1);
                player.Inventory.AddItem("Cloth Pants", 1);
                player.Inventory.AddItem("Cloth Boots", 1);
                player.Inventory.AddItem("Hunting Bow", 1);
                player.Inventory.AddItem("Arrow", 50);
                player.Inventory.AddItem("Pick Axe", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("9mm Ammo", 30);
                player.Inventory.AddItem("Large Medkit", 10);
                player.Message("领取成功");
            }
            else
            {
                timeSpan = DateTime.Now - (DateTime)temp;
                if ((int)timeSpan.TotalSeconds >= xklbcd)
                {
                    ds.Add("starter", player.SteamID, DateTime.Now);
                    player.Inventory.AddItem("Wood Planks", 1);
                    player.Inventory.AddItem("Workbench", 1);
                    player.Inventory.AddItem("Large Wood Storage", 1);
                    player.Inventory.AddItem("Furnace", 1);
                    player.Inventory.AddItem("Metal Door", 1);
                    player.Inventory.AddItem("Cloth Helmet", 1);
                    player.Inventory.AddItem("Cloth Vest", 1);
                    player.Inventory.AddItem("Cloth Pants", 1);
                    player.Inventory.AddItem("Cloth Boots", 1);
                    player.Inventory.AddItem("Hunting Bow", 1);
                    player.Inventory.AddItem("Arrow", 50);
                    player.Inventory.AddItem("Pick Axe", 1);
                    player.Inventory.AddItem("P250", 1);
                    player.Inventory.AddItem("9mm Ammo", 30);
                    player.Inventory.AddItem("Large Medkit", 10);
                    player.Message("领取成功");
                }
                else player.Message("您还需要" + (xklbcd - (int)timeSpan.TotalSeconds) + "秒方可再次领取");
            }
        }
        public static void vip(Fougerite.Player player)
        {
            temp = ds.Get("vip", player.SteamID);
            string playervip=Vip.GetVip(player.SteamID);
            if (playervip == "0")
            {
                player.Message("您未充值VIP，无法领取，欢迎您赞助服务器=-=!");
                return;
            }
            else if (Vip.VipSure(player.SteamID) == false)
            {
                player.Message("您VIP已经到期，无法领取，欢迎您续费，赞助服务器=-=!Tip:感谢您之前对服务器的支持！(比心心)");
                return;
            }
            else if (temp != null)
            {
                timeSpan = DateTime.Now - (DateTime)temp;
                if ((int)timeSpan.TotalSeconds <= vipcd)
                {
                    player.Message("您还需要" + (vipcd - (int)timeSpan.TotalSeconds) + "秒方可再次领取");
                    return;
                }
            }
            ds.Add("vip", player.SteamID, DateTime.Now);
            if (playervip == "1")
            {
                player.Inventory.AddItem("Raw Chicken Breast", 250);
                player.Inventory.AddItem("Metal Fragments", 250);
                player.Inventory.AddItem("Large Medkit", 5);
                player.Inventory.AddItem("Wood Planks", 100);
                player.Inventory.AddItem("Gunpowder", 50);
                player.Inventory.AddItem("Rad Suit Helmet", 1);
                player.Inventory.AddItem("Rad Suit Vest", 1);
                player.Inventory.AddItem("Rad Suit Pants", 1);
                player.Inventory.AddItem("Rad Suit Boots", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 250);
                Money.AddMoney(player, 100);
            }
            else if (playervip == "2")
            {
                player.Inventory.AddItem("Raw Chicken Breast", 250);
                player.Inventory.AddItem("Metal Fragments", 500);
                player.Inventory.AddItem("Large Medkit", 10);
                player.Inventory.AddItem("Wood Planks", 600);
                player.Inventory.AddItem("Gunpowder", 100);
                player.Inventory.AddItem("Rad Suit Helmet", 1);
                player.Inventory.AddItem("Rad Suit Vest", 1);
                player.Inventory.AddItem("Rad Suit Pants", 1);
                player.Inventory.AddItem("Rad Suit Boots", 1);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 250);
                player.Inventory.AddItem("Shotgun Shells", 100);
                player.Inventory.AddItem("Holo sight", 1);
                Money.AddMoney(player, 200);
            }
            else if (playervip == "3")
            {
                player.Inventory.AddItem("Low Quality Metal", 30);
                player.Inventory.AddItem("Large Medkit", 20);
                player.Inventory.AddItem("Wood Planks", 700);
                player.Inventory.AddItem("Metal Door", 3);
                player.Inventory.AddItem("Research Kit 1", 2);
                player.Inventory.AddItem("Gunpowder", 150);
                player.Inventory.AddItem("Rad Suit Helmet", 1);
                player.Inventory.AddItem("Rad Suit Vest", 1);
                player.Inventory.AddItem("Rad Suit Pants", 1);
                player.Inventory.AddItem("Rad Suit Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 10);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 250);
                player.Inventory.AddItem("556 Ammo", 250);
                player.Inventory.AddItem("Shotgun Shells", 100);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                Money.AddMoney(player, 300);
            }
            else if (playervip == "4")
            {
                player.Inventory.AddItem("Low Quality Metal", 150);
                player.Inventory.AddItem("Large Medkit", 30);
                player.Inventory.AddItem("Wood Planks", 700);
                player.Inventory.AddItem("Metal Door", 5);
                player.Inventory.AddItem("Research Kit 1", 2);
                player.Inventory.AddItem("Gunpowder", 300);
                player.Inventory.AddItem("Leather Helmet", 1);
                player.Inventory.AddItem("Leather Vest", 1);
                player.Inventory.AddItem("Leather Pants", 1);
                player.Inventory.AddItem("Leather Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 20);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 400);
                player.Inventory.AddItem("556 Ammo", 400);
                player.Inventory.AddItem("Shotgun Shells", 200);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                Money.AddMoney(player, 400);
            }
            else if (playervip == "5")
            {
                player.Inventory.AddItem("Low Quality Metal", 200);
                player.Inventory.AddItem("Large Medkit", 50);
                player.Inventory.AddItem("Wood Planks", 700);
                player.Inventory.AddItem("Metal Door", 10);
                player.Inventory.AddItem("Research Kit 1", 2);
                player.Inventory.AddItem("Gunpowder", 500);
                player.Inventory.AddItem("Kevlar Helmet", 1);
                player.Inventory.AddItem("Kevlar Vest", 1);
                player.Inventory.AddItem("Kevlar Pants", 1);
                player.Inventory.AddItem("Kevlar Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 30);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("Bolt Action Rifle", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 400);
                player.Inventory.AddItem("556 Ammo", 400);
                player.Inventory.AddItem("Shotgun Shells", 200);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                Money.AddMoney(player, 500);
            }
            else if (playervip == "6")
            {
                player.Inventory.AddItem("Low Quality Metal", 300);
                player.Inventory.AddItem("Large Medkit", 50);
                player.Inventory.AddItem("Wood Planks", 700);
                player.Inventory.AddItem("Metal Door", 20);
                player.Inventory.AddItem("Research Kit 1", 10);
                player.Inventory.AddItem("Gunpowder", 500);
                player.Inventory.AddItem("Kevlar Helmet", 1);
                player.Inventory.AddItem("Kevlar Vest", 1);
                player.Inventory.AddItem("Kevlar Pants", 1);
                player.Inventory.AddItem("Kevlar Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 50);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("Bolt Action Rifle", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 500);
                player.Inventory.AddItem("556 Ammo", 500);
                player.Inventory.AddItem("Shotgun Shells", 300);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Explosive Charge", 1);
                Money.AddMoney(player, 600);
            }
            else if (playervip == "7")
            {
                player.Inventory.AddItem("Low Quality Metal", 400);
                player.Inventory.AddItem("Large Medkit", 100);
                player.Inventory.AddItem("Wood Planks", 750);
                player.Inventory.AddItem("Metal Door", 50);
                player.Inventory.AddItem("Research Kit 1", 3);
                player.Inventory.AddItem("Gunpowder", 500);
                player.Inventory.AddItem("Kevlar Helmet", 1);
                player.Inventory.AddItem("Kevlar Vest", 1);
                player.Inventory.AddItem("Kevlar Pants", 1);
                player.Inventory.AddItem("Kevlar Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 80);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("Bolt Action Rifle", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 500);
                player.Inventory.AddItem("556 Ammo", 500);
                player.Inventory.AddItem("Shotgun Shells", 400);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                player.Inventory.AddItem("Explosive Charge", 3);
                Money.AddMoney(player, 700);
            }
            else if (playervip == "8")
            {
                player.Inventory.AddItem("Low Quality Metal", 500);
                player.Inventory.AddItem("Large Medkit", 200);
                player.Inventory.AddItem("Wood Planks", 750);
                player.Inventory.AddItem("Metal Door", 100);
                player.Inventory.AddItem("Research Kit 1", 3);
                player.Inventory.AddItem("Gunpowder", 500);
                player.Inventory.AddItem("Kevlar Helmet", 1);
                player.Inventory.AddItem("Kevlar Vest", 1);
                player.Inventory.AddItem("Kevlar Pants", 1);
                player.Inventory.AddItem("Kevlar Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 100);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("Bolt Action Rifle", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 500);
                player.Inventory.AddItem("556 Ammo", 500);
                player.Inventory.AddItem("Shotgun Shells", 500);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                player.Inventory.AddItem("Explosive Charge", 5);
                Money.AddMoney(player, 800);
            }
            else if (playervip == "9")
            {
                player.Inventory.AddItem("Low Quality Metal", 700);
                player.Inventory.AddItem("Large Medkit", 250);
                player.Inventory.AddItem("Wood Planks", 750);
                player.Inventory.AddItem("Metal Door", 250);
                player.Inventory.AddItem("Research Kit 1", 4);
                player.Inventory.AddItem("Gunpowder", 500);
                player.Inventory.AddItem("Kevlar Helmet", 1);
                player.Inventory.AddItem("Kevlar Vest", 1);
                player.Inventory.AddItem("Kevlar Pants", 1);
                player.Inventory.AddItem("Kevlar Boots", 1);
                player.Inventory.AddItem("F1 Grenade", 250);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("M4", 1);
                player.Inventory.AddItem("Bolt Action Rifle", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 500);
                player.Inventory.AddItem("556 Ammo", 500);
                player.Inventory.AddItem("Shotgun Shells", 500);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                player.Inventory.AddItem("Explosive Charge", 10);
                Money.AddMoney(player, 900);
            }
            else if (playervip == "10")
            {
                player.Inventory.AddItem("Raw Chicken Breast", 20);
                player.Inventory.AddItem("Metal Fragments", 500);
                player.Inventory.AddItem("Large Medkit", 10);
                player.Inventory.AddItem("Wood Planks", 600);
                player.Inventory.AddItem("Gunpowder", 100);
                player.Inventory.AddItem("Rad Suit Helmet", 1);
                player.Inventory.AddItem("Rad Suit Vest", 1);
                player.Inventory.AddItem("Rad Suit Pants", 1);
                player.Inventory.AddItem("Rad Suit Boots", 1);
                player.Inventory.AddItem("Shotgun", 1);
                player.Inventory.AddItem("P250", 1);
                player.Inventory.AddItem("MP5A4", 1);
                player.Inventory.AddItem("9mm Ammo", 250);
                player.Inventory.AddItem("Shotgun Shells", 100);
                player.Inventory.AddItem("Holo sight", 1);
                player.Inventory.AddItem("Laser Sight", 1);
                player.Inventory.AddItem("Silencer", 1);
                player.Inventory.AddItem("Flashlight Mod", 1);
                Money.AddMoney(player, 1000);
            }
            player.Message("工资领取成功");
            player.Message("服装领取成功");
            player.Message("武器领取成功");
            player.Message("物资领取成功");
        }
        public static void vipyf(Fougerite.Player player)
        {
            string playervip = Vip.GetVip(player.SteamID);
            if(playervip=="0")
            {
                player.Message("您未充值VIP，隐身衣的领取下限为VIP4");
                return;
            }
            else if (playervip == "1")
            {
                player.Message("您的VIP权限不足，隐身衣的领取下限为VIP4");
                return;
            }
            else if (playervip == "2")
            {
                player.Message("您的VIP权限不足，隐身衣的领取下限为VIP4");
                return;
            }
            else if (playervip == "3")
            {
                player.Message("您的VIP权限不足，隐身衣的领取下限为VIP4");
                return;
            }
            else if (playervip == "4") player.Inventory.AddItem("Invisible Boots", 1);
            else if (playervip == "5") player.Inventory.AddItem("Invisible Pants", 1);
            else if (playervip == "6") player.Inventory.AddItem("Invisible Vest", 1);
            else if (playervip == "7") player.Inventory.AddItem("Invisible Helmet", 1);
            else if (playervip == "8")
            {
                player.Inventory.AddItem("Invisible Boots", 1);
                player.Inventory.AddItem("Invisible Helmet", 1);
            }
            else if (playervip == "9")
            {
                player.Inventory.AddItem("Invisible Pants", 1);
                player.Inventory.AddItem("Invisible Helmet", 1);
            }
            else if (playervip == "10")
            {
                player.Message("您的VIP权限不足，隐身衣的领取下限为VIP4");
                return;
            }
            else player.Message("您未充值VIP，无法领取，欢迎您赞助服务器=-=!");
            player.Message("领取成功");
        }
        public static void remove(Fougerite.Player player,string player1)
        {
            if (player.Admin)
            {                
                ds.Remove("starter", Fougerite.Player.FindByName(player1).SteamID);
                ds.Remove("vip", Fougerite.Player.FindByName(player1).SteamID);
                ds.Remove("xklb", Fougerite.Player.FindByName(player1).SteamID);
                player.Message("清理成功");
            }          
        }
    }
}