
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JianxianS.Core
{
    public class Command
    {
        public Command()
        {
            Fougerite.Hooks.OnCommand += Hooks_OnCommand;
        }
        ~Command()
        {
            Fougerite.Hooks.OnCommand -= Hooks_OnCommand;
        }
        private void Hooks_OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
           if(cmd=="who") Who.opbjectWho(player);
           else if (cmd == "cvip")
            {
                if (player.Admin)
                {
                    if (args.Length == 2)
                    {
                        Fougerite.Player player1 = Fougerite.Player.FindByName(args[0]);
                        if (player1 == null)
                        {
                            player.Message("查询无果，找不到此玩家");
                            return;
                        }
                        else
                        {
                            Vip.SetVip(args[1], player1.SteamID);
                            player.Message("充值成功");
                            player1.Message("恭喜您充值成功！非常感谢您对服务器的支持![比心心]");
                            string cmdText;
                            cmdText = string.Concat(new string[]{
                                "恭喜玩家",
                                player1.Name,
                                "成功充值VIP",
                                args[1],
                                "突破[",
                                Vip.GetCH(args[1]),
                                "]"
                            });
                            Fougerite.Server.GetServer().BroadcastNotice(cmdText);
                        }
                    }
                    else player.Message("您输入有误[/cvip 玩家名 等级]");
                }
            }
           else if (cmd == "svip")
            {
                if (player.Admin)
                {
                    if (args.Length == 2)
                    {
                        Fougerite.Player player1 = Fougerite.Player.FindByName(args[0]);
                        if (player1 == null)
                        {
                            player.Message("查询无果，找不到此玩家");
                            return;
                        }
                        else
                        {
                            Vip.SetVip(args[1], player1.SteamID);
                            player.Message("设置成功");
                        }
                    }
                    else player.Message("您输入有误[/cvip 玩家名 等级]");
                }
            }
           else if (cmd == "kit")
            {
                string kitlist = string.Concat(new string[] {
                    "starter[小礼包]❀",
                    "xklb[小康礼包]❀",
                    "vip[VIP大礼包]❀",
                    "vipyf[VIP隐身衣]"
                });
                if (args.Length == 0) player.Message(kitlist);
                else if (args.Length == 1)
                {
                    if (args[0] == "starter")
                    {
                        Kit.starter(player);
                    }
                    else if (args[0] == "xklb")
                    {
                        Kit.xklb(player);
                    }
                    else if (args[0] == "vip")
                    {
                        
                        Kit.vip(player);
                    }
                    else if (args[0] == "vipyf")
                    {
                        Kit.vipyf(player);
                    }
                }
            }
           else if (cmd == "vipsearch")
            {
                if (args.Length == 0) Vip.Vipsearch(player);
                if (args.Length == 1) Vip.Vipsearch(Fougerite.Player.FindByName(args[0]));
            }
           else if (cmd == "remove")
            {
                if (args.Length == 0) Kit.remove(player, player.Name);
                if (args.Length == 1)
                {
                    Kit.remove(player,args[0]);
                }
            }
           else if (cmd == "fps")
            {
                NetUser Sender =NetUser.FindByUserID(player.UID);
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.ssaa false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.ssao false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.bloom false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.grain false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.shafts false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.tonemap false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.on false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.forceredraw false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.displacement false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.shadowcast false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.shadowreceive false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "render.level 0");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "render.vsync false");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "water.level -1");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "water.reflection false");
                player.Notice("优化完毕");
            }
           else if(cmd== "quality")
            {
                NetUser Sender = NetUser.FindByUserID(player.UID);
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.ssaa true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.ssao true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.bloom true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.grain true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.shafts true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "gfx.tonemap true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.on true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.forceredraw true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.displacement true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.shadowcast true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "grass.shadowreceive true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "render.level 1");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "render.vsync true");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "water.level 1");
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "water.reflection true");
                player.Notice("成功开启最高特效");
            }
           else if (cmd == "suicide")
            {
                NetUser Sender = NetUser.FindByUserID(player.UID);
                ConsoleNetworker.SendClientCommand(Sender.networkPlayer, "suicide");
            }
           else if(cmd == "dvip")
           {
                if (args.Length == 1)
                {
                    if (args[0] == "1")
                    {
                        if (Money.HasMoney(player, 1000))
                        {
                            Money.RemoveMoney(player, 1000);
                            Vip.SetVip(args[0], player.SteamID);
                            player.Message("恭喜您充值成功！非常感谢您对服务器的支持![比心心]");
                            string cmdText;
                            cmdText = string.Concat(new string[]{
                                "恭喜玩家",
                                player.Name,
                                "成功充值VIP",
                                args[0],
                                "突破[",
                                Vip.GetCH(args[0]),
                                "]"
                            });
                            Fougerite.Server.GetServer().BroadcastNotice(cmdText);
                        }
                        else player.Message("抱歉，您的斩仙币不足");
                    }
                    else if (args[0] == "2")
                    {
                        if (Money.HasMoney(player, 2000))
                        {
                            Money.RemoveMoney(player, 2000);
                            Vip.SetVip(args[0], player.SteamID);
                            player.Message("恭喜您充值成功！非常感谢您对服务器的支持![比心心]");
                            string cmdText;
                            cmdText = string.Concat(new string[]{
                                "恭喜玩家",
                                player.Name,
                                "成功充值VIP",
                                args[0],
                                "突破[",
                                Vip.GetCH(args[0]),
                                "]"
                            });
                            Fougerite.Server.GetServer().BroadcastNotice(cmdText);
                        }
                        else player.Message("抱歉，您的斩仙币不足");
                    }
                    else if (args[0] == "3")
                    {
                        if (Money.HasMoney(player, 3000))
                        {
                            Money.RemoveMoney(player, 3000);
                            Vip.SetVip(args[0], player.SteamID);
                            player.Message("恭喜您充值成功！非常感谢您对服务器的支持![比心心]");
                            string cmdText;
                            cmdText = string.Concat(new string[]{
                                "恭喜玩家",
                                player.Name,
                                "成功充值VIP",
                                args[0],
                                "突破[",
                                Vip.GetCH(args[0]),
                                "]"
                            });
                            Fougerite.Server.GetServer().BroadcastNotice(cmdText);
                        }
                        else player.Message("抱歉，您的斩仙币不足");
                    }
                }
                else
                {
                    player.Message("输入/dvip+空格+等级即可兑换（目前最高为3级）");
                    player.Message("VIP1-1000斩仙币");
                    player.Message("VIP2-2000斩仙币");
                    player.Message("VIP3-3000斩仙币");
                }
           }
           /*else if (cmd == "changeowner")
           {
                if (player.Admin)
                {
                    if (args.Length == 2)
                    {
                        Fougerite.Player player1 = Fougerite.Player.FindBySteamID(args[0]);
                        if (player1 != null)
                        {
                            foreach (Fougerite.Entity ob in Fougerite.World.GetWorld().Entities)
                            {
                                if (ob.OwnerID == args[1]) ob.ChangeOwner(player1);
                            }
                            player.Message("更改成功");
                        }
                        else player.Message("找不到玩家");
                    }
                    else player.Message("您输入有误");
                }
            }  */      
        }
    }
}