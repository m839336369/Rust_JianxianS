using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using MySql.Data.MySqlClient;

namespace JianxianS.Core
{
    public class Money
    {
        private static DataStore ds = DataStore.GetInstance();
        public Money()
        {
            Fougerite.Hooks.OnCommand += Hooks_OnCommand;
            Fougerite.Hooks.OnNPCKilled += Hooks_OnNPCKilled;
            Fougerite.Hooks.OnPlayerKilled += Hooks_OnPlayerKilled;
        }
        ~Money()
        {
            Fougerite.Hooks.OnCommand -= Hooks_OnCommand;
            Fougerite.Hooks.OnNPCKilled -= Hooks_OnNPCKilled;
            Fougerite.Hooks.OnPlayerKilled -= Hooks_OnPlayerKilled;
        }


        private void Hooks_OnPlayerKilled(Fougerite.Events.DeathEvent de)
        {
            if (de.Attacker != null && de.AttackerIsPlayer && de.VictimIsPlayer && de.Victim!=null)
            {
                //Example to add 1 money to the killer
                Fougerite.Player killer = (Fougerite.Player)de.Attacker;
                if (killer.SteamID != "")
                {
                    if (de.Attacker != de.Victim)
                    {
                        AddMoney(killer, 1);
                        killer.Message("[color#99FF]成功击杀一个剑客,获得1斩仙币 /zxsc 查询斩仙商店");
                    }
                }
            }
        }

        private void Hooks_OnNPCKilled(Fougerite.Events.DeathEvent de)
        {
            if (de.Attacker != null && de.Victim != null && de.AttackerIsPlayer)
            {
                NPC npc = (NPC)de.Victim;
                if (npc.Name== "MutantBear"||npc.Name== "MutantWolf")
                {
                    Fougerite.Player killer = (Fougerite.Player)de.Attacker;
                    AddMoney(killer, 1);
                    killer.Message("[color#99FF]成功击杀一个异兽,获得1斩仙币 /zxsc 查询斩仙商店");
                }
            }
        }

        private void Hooks_OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            switch (cmd)
            {
                case "money":
                    var money = GetMoney(player);
                    player.Message("[color#99FF]您的账户拥有: " + money + "  斩仙币");
                    break;
                case "+qian":
                    if (player.Admin == true)
                    {
                        if (args.Length != 2)
                        {
                            player.Message("[color#99FF]命令错啦 /+qian 剑客 数量");
                        }
                        else
                        {
                            Fougerite.Player jogador = Fougerite.Player.FindByName(args[0]);
                            if (jogador != null)
                            {
                                int valor = int.Parse(args[1]);
                                AddMoney(jogador, valor);
                                Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客[ " + jogador.Name + " ][color#FF6633] 充值了 [color#99FF]" + valor + " [color#FF6633]斩仙币  -  !尽情的享受购物吧!");
                                jogador.Message("[color#99FF]系统给您发放了【" + valor + "】斩仙币");

                            }
                            else
                            {
                                player.Message("[color#99FF]剑客 " + args[0] + "查无此人!");
                            }
                        }
                    }
                    else
                    {
                        player.Message("[color#99CC]你现在好像没有权限!");
                    }
                    break;
                case "zero":
                    if (player.Admin == true)
                    {
                        if (args.Length != 1)
                        {
                            player.Message("[color#99CC]管理员权限代码 /zero \"player\"");
                        }
                        else
                        {
                            Fougerite.Player jogador = Fougerite.Player.FindByName(args[0]);
                            if (jogador != null)
                            {
                                int valor = 0;
                                SetMoney(jogador, valor);
                                player.Message("[color#99FF]剑客: " + jogador.Name + " 斩仙币被系统归零!");
                            }
                            else
                            {
                                player.Message("[color#99CC]剑客 " + args[0] + "查无此人!");
                            }
                        }
                    }
                    else
                    {
                        player.Message("[color#99CC]你现在好像没有权限");
                    }
                    break;
                case "-qian":
                    if (player.Admin == true)
                    {
                        if (args.Length != 2)
                        {
                            player.Message("[color#99FF]命令错啦 /-qian 剑客 数量");
                        }
                        else
                        {
                            Fougerite.Player jogador = Fougerite.Player.FindByName(args[0]);
                            if (jogador != null)
                            {
                                int valor = int.Parse(args[1]);
                                RemoveMoney(jogador, valor);
                                player.Message("[color#99FF]剑客" + jogador.Name + "被系统扣除了  " + valor + "  斩仙币");
                            }
                            else
                            {
                                player.Message("[color#99FF]剑客 " + args[0] + "沒找到哦");
                            }
                        }
                    }
                    else
                    {
                        player.Message("[color#99FF]你现在好像没有权限!");
                    }
                    break;
                case "zxsc":
                    if (args.Length == 0)
                    {
                        player.Message("[color#99FF]充值斩仙币【1元=5斩仙币】请联系服主,服主剑仙QQ 839336369");
                        player.Message("[color#99FF]购买物品代码：/zxsc[空格]编号");
                        player.Message("[color#99FF]剑仙倾力打造中文商城【修复版】");
                        player.Message("[color#99FF]比如买武器专栏, 用 /zxsc wuqi.");
                        player.Message("==============================================");
                        player.Message("[color#FF6633]【礼包区】      按T输入[color#33CC99] /zxsc lb");
                        player.Message("[color#FF6633]【武器店】      按T输入[color#33CC99] /zxsc wq");
                        player.Message("[color#FF6633]【防具店】      按T输入[color#33CC99] /zxsc yf");
                        player.Message("[color#FF6633]【弹药区】      按T输入[color#33CC99] /zxsc dy");
                        player.Message("[color#FF6633]【医疗区】      按T输入[color#33CC99] /zxsc yl");
                        player.Message("[color#FF6633]【配件区】      按T输入[color#33CC99] /zxsc pj");
                        player.Message("[color#FF6633]【 其  他  】      按T输入[color#33CC99] /zxsc qt");
                        player.Message("==============================================");
                    }
                    if (args.Length == 1)
                    {
                        string argumentos = args[0];
                        if (argumentos == "wq")
                        {
                            player.Message("[color#FF6EB4]##您正在预览武器 - 所有武器填有弹药##");
                            player.Message("[color#FF6633]物品:[color#33CC99]9毫米手枪*1 ￥价格:[50斩仙币]  [color#FFFF33]—编号: 1");
                            player.Message("[color#FF6633]物品:[color#33CC99]P250手枪*1 ￥价格:[50斩仙币]  [color#FFFF33]-编号: 2");
                            player.Message("[color#FF6633]物品:[color#33CC99]M4自动步枪*1 ￥价格:[600斩仙币]  [color#FFFF33]-编号: 3 ");
                            player.Message("[color#FF6633]物品:[color#33CC99]喷子*1 ￥价格:[300斩仙币]  [color#FFFF33]-编号: 4 ");
                            player.Message("[color#FF6633]物品:[color#33CC99]MP5*1 ￥价格:[300斩仙币]  [color#FFFF33]-编号: 5 ");
                            player.Message("[color#FF6633]物品:[color#33CC99]狙击枪*1 ￥价格:[600斩仙币]  [color#FFFF33]-编号: 6");
                            player.Message("[color#FF6633]物品:[color#33CC99]手雷*10 ￥价格:[200斩仙币]  [color#FFFF33]-编号: 7");
                            player.Message("[color#FF6633]物品:[color#33CC99]炸药*3 ￥价格:[1000斩仙币][color#FFFF33]-编号: 8");
                        }
                        if (argumentos == "yf")
                        {
                            player.Message("[color#FF6EB4]##您正在预览防具 - 防具已整套出售##");
                            player.Message("[color#FF6633]亚麻布套*1￥价格:[5斩仙币]   [color#FFFF33]-编号: 9 ");
                            player.Message("[color#FF6633]狼皮套装*1￥价格:[200斩仙币]   [color#FFFF33]-编号:10 ");
                            player.Message("[color#FF6633]防辐射套*1￥价格:[200斩仙币]   [color#FFFF33]-编号:11  ");
                            player.Message("[color#FF6633]潜行者套*1￥价格:[600斩仙币]   [color#FFFF33]-编号:12");
                        }
                        if (argumentos == "yl")
                        {
                            player.Message("[color#FF6EB4]##您正在预览医疗 - 医疗已整套出售##");
                            player.Message("[color#FF6633]小医疗礼包*20 ￥价格:[1斩仙币]　[color#FFFF33]-编号:25");
                            player.Message("[color#FF6633]中医疗礼包*5￥价格:[5斩仙币]　[color#FFFF33]-编号:26");
                            player.Message("[color#FF6633]大医疗礼包*10￥价格:[10斩仙币]　[color#FFFF33]-编号:27");

                        }
                        if (argumentos == "dy")
                        {
                            player.Message("[color#FF6EB4]##您正在预览子弹 - 弹药及其他物品");
                            player.Message("[color#FF6633]9mm子弹*20￥价格:[10斩仙币]　[color#FFFF33]-编号:20");
                            player.Message("[color#FF6633]556子弹*40￥价格:[10斩仙币]　[color#FFFF33]-编号:21");
                            player.Message("[color#FF6633]喷子子弹*10￥价格:[10斩仙币]　[color#FFFF33]-编号:22");

                        }
                        if (argumentos == "pj")
                        {
                            player.Message("[color#FF6EB4]##您正在预览配件 - 配件及其他物品");
                            player.Message("[color#FF6633]武器附件-手电筒*1￥价格:[20斩仙币]　[color#FFFF33]-编号:15");
                            player.Message("[color#FF6633]武器附件-瞄具*1￥价格:[20斩仙币]　[color#FFFF33]-编号:16");
                            player.Message("[color#FF6633]武器附件-消音*1￥价格:[20斩仙币]　[color#FFFF33]-编号:17");
                            player.Message("[color#FF6633]武器附件-红外线*1￥价格:[20斩仙币]　[color#FFFF33]-编号:18");

                        }
                        if (argumentos == "qt")
                        {
                            player.Message("[color#FF6EB4]##您正在预览其他 - 其他及其他物品");
                            player.Message("[color#FF6633]十字稿*1￥价格:[5斩仙币]　[color#FFFF33]-编号:13");
                            player.Message("[color#FF6633]信号弹*1￥价格:[5斩仙币]　[color#FFFF33]-编号:14");
                            player.Message("[color#FF6633]工具箱*1￥价格:[20斩仙币]　[color#FFFF33]-编号:19");

                        }
                        if (argumentos == "lb")
                        {
                            player.Message("[color#FF6EB4]##您正在预览其他 - 其他及其他物品");
                            player.Message("[color#FF6633]物资大礼包*1￥价格:[50斩仙币]　[color#FFFF33]-编号:28");
                            player.Message("[color#FF6633]家具大礼包*1￥价格:[5斩仙币]　[color#FFFF33]-编号:29");
                            player.Message("[color#FF6633]作战必备礼包*1￥价格:[5斩仙币]　[color#FFFF33]-编号:30");
                            player.Message("[color#FF6633]M4作战礼包[优惠]*1￥价格:[1000斩仙币]　[color#FFFF33]-编号:23");
                            player.Message("[color#FF6633]狙击作战礼包[优惠]*1￥价格:[1000斩仙币]　[color#FFFF33]-编号:24");

                        }
                        //As vendas em si
                        switch (argumentos)
                        {
                            case "1":
                                if (HasMoney(player, 50))
                                {
                                    player.Inventory.AddItem("9mm Pistol", 1);
                                    player.Inventory.AddItem("9mm Ammo", 16);
                                    FinalizaCompra(player, 50);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$20斩仙币购买  [color#99FF]▶ 9mm Pistol【附赠16发子弹】 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "2":
                                if (HasMoney(player, 50))
                                {
                                    player.Inventory.AddItem("P250", 1);
                                    player.Inventory.AddItem("9mm Ammo", 16);
                                    FinalizaCompra(player, 50);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$20斩仙币购买  [color#99FF]▶ P250【附赠16发子弹】 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "3":
                                if (HasMoney(player, 600))
                                {
                                    player.Inventory.AddItem("M4", 1);
                                    player.Inventory.AddItem("556 Ammo", 48);
                                    FinalizaCompra(player, 600);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$100斩仙币购买  [color#99FF]▶ M4步枪【附赠48发子弹】 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "4":
                                if (HasMoney(player, 300))
                                {
                                    player.Inventory.AddItem("Shotgun", 1);
                                    player.Inventory.AddItem("Shotgun Shells", 16);
                                    FinalizaCompra(player, 300);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$30斩仙币购买  [color#99FF]▶ Shotgun【附赠16发子弹】◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "5":
                                if (HasMoney(player, 300))
                                {
                                    player.Inventory.AddItem("MP5A4", 1);
                                    player.Inventory.AddItem("9mm Ammo", 60);
                                    FinalizaCompra(player, 300);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$30斩仙币购买  [color#99FF]▶ MP5A4 【附赠60发子弹】◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "6":
                                if (HasMoney(player, 600))
                                {
                                    player.Inventory.AddItem("Bolt Action Rifle", 1);
                                    player.Inventory.AddItem("556 Ammo", 30);
                                    FinalizaCompra(player, 600);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$50斩仙币购买  [color#99FF]▶ Bolt Action Rifle【附赠30发子弹】 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "7":
                                if (HasMoney(player, 200))
                                {
                                    player.Inventory.AddItem("F1 Grenade", 10);
                                    FinalizaCompra(player, 200);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$20斩仙币购买  [color#99FF]▶ 十颗手雷 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "8":
                                if (HasMoney(player, 1000))
                                {
                                    player.Inventory.AddItem("Explosive Charge", 3);
                                    FinalizaCompra(player, 1000);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$100斩仙币购买  [color#99FF]▶ 三个C4炸药 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "9":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Cloth Helmet", 1);
                                    player.Inventory.AddItem("Cloth Vest", 1);
                                    player.Inventory.AddItem("Cloth Pants", 1);
                                    player.Inventory.AddItem("Cloth Boots", 1);
                                    FinalizaCompra(player, 5);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "10":
                                if (HasMoney(player, 200))
                                {
                                    player.Inventory.AddItem("Leather Helmet", 1);
                                    player.Inventory.AddItem("Leather Vest", 1);
                                    player.Inventory.AddItem("Leather Pants", 1);
                                    player.Inventory.AddItem("Leather Boots", 1);
                                    FinalizaCompra(player, 200);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$40斩仙币购买  [color#99FF]▶ 狼皮套装 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "11":
                                if (HasMoney(player, 200))
                                {
                                    player.Inventory.AddItem("Rad Suit Helmet", 1);
                                    player.Inventory.AddItem("Rad Suit Vest ", 1);
                                    player.Inventory.AddItem("Rad Suit Pants", 1);
                                    player.Inventory.AddItem("Rad Suit Boots", 1);
                                    FinalizaCompra(player, 200);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$40斩仙币购买  [color#99FF]▶ 防辐射套装 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "12":
                                if (HasMoney(player, 600))
                                {
                                    player.Inventory.AddItem("Kevlar Helmet", 1);
                                    player.Inventory.AddItem("Kevlar Vest", 1);
                                    player.Inventory.AddItem("Kevlar Pants", 1);
                                    player.Inventory.AddItem("Kevlar Boots", 1);
                                    FinalizaCompra(player, 600);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$80斩仙币购买  [color#99FF]▶ 潜行者套装 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "13":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Pick Axe", 1);
                                    FinalizaCompra(player, 5);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "14":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Flare", 3);
                                    FinalizaCompra(player, 5);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "15":
                                if (HasMoney(player, 20))
                                {
                                    player.Inventory.AddItem("Flashlight Mod", 1);
                                    FinalizaCompra(player, 20);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "16":
                                if (HasMoney(player, 20))
                                {
                                    player.Inventory.AddItem("Holo sight", 1);
                                    FinalizaCompra(player, 20);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "17":
                                if (HasMoney(player, 20))
                                {
                                    player.Inventory.AddItem("Silencer", 1);
                                    FinalizaCompra(player, 20);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "18":
                                if (HasMoney(player, 20))
                                {
                                    player.Inventory.AddItem("Laser Sight", 1);
                                    FinalizaCompra(player, 20);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "19":
                                if (HasMoney(player, 20))
                                {
                                    player.Inventory.AddItem("Research Kit 1", 1);
                                    FinalizaCompra(player, 20);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$20斩仙币购买  [color#99FF]▶ 工具箱 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "20":
                                if (HasMoney(player, 10))
                                {
                                    player.Inventory.AddItem("9mm Ammo", 20);
                                    FinalizaCompra(player, 10);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "21":
                                if (HasMoney(player, 10))
                                {
                                    player.Inventory.AddItem("556 Ammo", 40);
                                    FinalizaCompra(player, 10);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$10斩仙币购买[color#99FF]40发556子弹[color#FF6633] [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "22":
                                if (HasMoney(player, 150))
                                {
                                    player.Inventory.AddItem("Shotgun Shells", 10);
                                    FinalizaCompra(player, 150);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "23":
                                if (HasMoney(player, 1000))
                                {
                                    player.Inventory.AddItem("Kevlar Helmet", 1);
                                    player.Inventory.AddItem("Kevlar Vest", 1);
                                    player.Inventory.AddItem("Kevlar Pants", 1);
                                    player.Inventory.AddItem("Kevlar Boots", 1);
                                    player.Inventory.AddItem("M4", 1);
                                    player.Inventory.AddItem("556 Ammo", 250);
                                    player.Inventory.AddItem("Small Medkit", 5);
                                    player.Inventory.AddItem("Large Medkit", 2);
                                    player.Inventory.AddItem("Anti-Radiation pills", 5);
                                    player.Inventory.AddItem("Sleeping Bag", 1);
                                    FinalizaCompra(player, 1000);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]仙·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$1000斩仙币购买  [color#99FF]▶ M4作战套装 ◀[color#FF6633]  [color#99FF]这是要大开杀戒的节奏?");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "24":
                                if (HasMoney(player, 1000))
                                {
                                    player.Inventory.AddItem("Kevlar Helmet", 1);
                                    player.Inventory.AddItem("Kevlar Vest", 1);
                                    player.Inventory.AddItem("Kevlar Pants", 1);
                                    player.Inventory.AddItem("Kevlar Boots", 1);
                                    player.Inventory.AddItem("Bolt Action Rifle", 1);
                                    player.Inventory.AddItem("556 Ammo", 150);
                                    player.Inventory.AddItem("Small Medkit", 5);
                                    player.Inventory.AddItem("Large Medkit", 2);
                                    player.Inventory.AddItem("Anti-Radiation pills", 5);
                                    player.Inventory.AddItem("Sleeping Bag", 1);
                                    FinalizaCompra(player, 1000);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]仙·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$1000斩仙币购买  [color#99FF]▶ 狙击作战套装 ◀[color#FF6633]  [color#99FF]谁敢与我一战,小小屌丝靠边站!");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "25":
                                if (HasMoney(player, 1))
                                {
                                    player.Inventory.AddItem("Bandage", 20);
                                    FinalizaCompra(player, 1);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "26":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Small Medkit", 5);
                                    FinalizaCompra(player, 5);
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "27":
                                if (HasMoney(player, 10))
                                {
                                    player.Inventory.AddItem("Large Medkit", 10);
                                    FinalizaCompra(player, 10);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$10斩仙币购买10个  [color#99FF]▶ 大型医疗包 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "28":
                                if (HasMoney(player, 50))
                                {
                                    player.Inventory.AddItem("Large Medkit", 5);
                                    player.Inventory.AddItem("Wood Planks", 2500);
                                    player.Inventory.AddItem("Gunpowder", 1000);
                                    player.Inventory.AddItem("Cloth", 1000);
                                    player.Inventory.AddItem("Leather", 1000);
                                    player.Inventory.AddItem("Low Grade Fuel", 1000);
                                    player.Inventory.AddItem("Low Quality Metal", 1000);
                                    player.Inventory.AddItem("Sulfur", 1000);
                                    player.Inventory.AddItem("Wood", 1000);
                                    player.Inventory.AddItem("Charcoal", 1000); player.Inventory.AddItem("Blood", 1000);
                                    player.Inventory.AddItem("Small Medkit", 1000);
                                    player.Inventory.AddItem("Animal Fat", 1000);
                                    FinalizaCompra(player, 50);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]金·剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$50斩仙币购买1个  [color#99FF]▶ 物资大礼包 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "29":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Sleeping Bag", 1);
                                    player.Inventory.AddItem("Repair Bench", 1);
                                    player.Inventory.AddItem("Spike Wall", 1);
                                    player.Inventory.AddItem("Wood Barricade", 1);
                                    player.Inventory.AddItem("Wood Gate", 1);
                                    player.Inventory.AddItem("Wood Gateway", 1);
                                    player.Inventory.AddItem("Wood Shelter", 5);
                                    player.Inventory.AddItem("Wood Storage Box", 5);
                                    player.Inventory.AddItem("Wooden Door", 5);
                                    player.Inventory.AddItem("Workbench", 1); player.Inventory.AddItem("Furnace", 1);
                                    player.Inventory.AddItem("Camp Fire", 1);
                                    player.Inventory.AddItem("Metal Door", 5);
                                    FinalizaCompra(player, 5);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$5斩仙币购买1个  [color#99FF]▶ 家具大礼包 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                            case "30":
                                if (HasMoney(player, 5))
                                {
                                    player.Inventory.AddItem("Wood Barricade", 5);
                                    player.Inventory.AddItem("Wood Shelter", 5);
                                    player.Inventory.AddItem("Wooden Door", 5);
                                    FinalizaCompra(player, 5);
                                    Fougerite.Server.GetServer().Broadcast("[color#FFCC66]剑客[color#99CC]▶ " + player.Name + " ◀[color#FF6633]在斩仙商城花费$5斩仙币购买1个  [color#99FF]▶ 作战必备礼包 ◀[color#FF6633]  [color#FF9933]【按/zxsc 打开斩仙商城】");
                                }
                                else
                                {
                                    SemDinheiro(player);
                                }
                                break;
                        }
                        int moneytemp = GetMoney(player);
                        player.Message("[color#99FF]您的账户拥有: " + moneytemp + "  斩仙币");
                    }
                    if (args.Length >= 2)
                    {
                        player.Message("[color#99FF]用 /zxsc 看下帮助.");
                    }
                    break;

            }
        }
        public static void FinalizaCompra(Fougerite.Player Player, int Valor)
        {
            RemoveMoney(Player, Valor);
            Player.Message("[color#99FF]感谢您的购买,物品已经发送到您的背包!");
        }
        public static void SemDinheiro(Fougerite.Player player)
        {
            player.Message("[color#99FF]您没有足够的斩仙币购买此物品!");
            player.Message("[color#99FF]充值联系服主 剑仙    充值比例 1 元 = 5 斩仙币");
        }
        public static int GetMoney(Fougerite.Player player)
        {
            int money;
            object temp = ds.Get("magma_dollar", player.SteamID);
            if (temp == null)
            {
                money = 0;
                ds.Add("magma_dollar", player.SteamID, money);
            }
            else money =  (int)temp;
            return money;
        }
        public static void AddMoney(Fougerite.Player player, int amount)
        {
            int saldo = GetMoney(player);
            int quantidade = amount;
            int money = saldo + quantidade;
            ds.Add("magma_dollar", player.SteamID, money);
            Update(player); 
        }
        public static void RemoveMoney(Fougerite.Player player, int amount)
        {
            var saldo = GetMoney(player);
            var quantidade = amount;
            var money = saldo - quantidade;
            ds.Add("magma_dollar", player.SteamID, money);
            Update(player);
        }
        public static void SetMoney(Fougerite.Player player, int amount)
        {
            ds.Add("magma_dollar", player.SteamID, amount);
            Update(player);
        }
        public static bool HasMoney(Fougerite.Player player, int amount)
        {
            var money = GetMoney(player);
            return money >= amount;
        }
        private static void Update(Fougerite.Player player)
        {
            //Player.message(String(Player.steamid));
            int money = GetMoney(player);
            ds.Add("Money", player.SteamID, money);
            moneysql(player);
        }
        private static void moneysql(Fougerite.Player player)
        {
            if (Ini.useMysql)
            {
                string cmdText = string.Concat(new object[]
                {
                                "UPDATE stats SET money='",
                                GetMoney(player),
                                "' WHERE steamid='",
                                player.SteamID,
                                "'"
                });
                if (Stats.OpenConnection())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                    mySqlCommand.ExecuteNonQuery();
                    Stats.CloseConnection();
                }
            }
        }
    }
}
