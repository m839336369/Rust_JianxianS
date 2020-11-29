using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.PluginLoaders;

namespace JianxianS.Core
{
    public  class Rconcommand
    {
        public Rconcommand()
        {
            Fougerite.Hooks.OnConsoleReceived += Hooks_OnConsoleReceived;
        }
        ~Rconcommand()
        {
            Fougerite.Hooks.OnConsoleReceived -= Hooks_OnConsoleReceived;
        }
        private void Hooks_OnConsoleReceived(ref ConsoleSystem.Arg arg, bool external1)
        {
            StringComparison ic = StringComparison.InvariantCultureIgnoreCase;
            bool external = arg.argUser == null;
            bool adminRights = (arg.argUser != null && arg.argUser.admin) || external;
            string userid = "[external][external]";
            if (adminRights && !external)
                userid = string.Format("[{0}][{1}]", arg.argUser.displayName, arg.argUser.userID.ToString());
            string text = "";
            string text2 = text;
            string logmsg = string.Format("[ConsoleReceived] userid={0} adminRights={1} command={2}.{3} args={4}", userid, adminRights.ToString(), arg.Class, arg.Function, (arg.HasArgs(1) ? arg.ArgsStr : "none"));
            if (arg.Class.Equals("JianxianS", ic) && arg.Function.Equals("users", ic))
            {
                if (adminRights)
                {
                    if (arg.HasArgs(1))
                    {
                        arg.ReplyWith("您多输入了参数了");
                    }
                    else
                    {
                        int num = 0;
                        
                        foreach (uLink.NetworkPlayer networkPlayer in NetCull.connections)
                        {
                            text2 = "[Player]||\n";
                            object localData = networkPlayer.GetLocalData();
                            if (localData is NetUser)
                            {
                                NetUser netUser = (NetUser)localData;
                                text = string.Concat(new object[]
                                {
                                text2,
                                netUser.networkPlayer.id,
                                ":\"",
                                netUser.displayName,
                                "\"||\n"
                                });
                                num++;
                            }
                        }
                        text = text + num.ToString() + "users\n";
                        arg.ReplyWith(text);
                    }
                }
            }
            else if (arg.Class.Equals("JianxianS", ic) && arg.Function.Equals("kick", ic))
            {
                if (adminRights)
                {
                    if (arg.HasArgs(1))
                    {
                        global::PlayerClient[] playerClients = arg.GetPlayerClients(0);
                        foreach (global::PlayerClient playerClient in playerClients)
                        {
                            global::NetUser netUser = playerClient.netUser;
                            if (netUser != null)
                            {
                                netUser.Kick(global::NetError.Facepunch_Kick_RCON, true);
                            }
                        }
                        if (playerClients.Length > 0)
                        {
                            arg.ReplyWith("Kicked " + playerClients.Length + " users!");
                            return;
                        }
                        arg.ReplyWith("Couldn't find anyone!");
                    }
                    else
                    {
                        text = "您少输入参数了";
                        arg.ReplyWith(text);
                    }
                }
            }
            else if (arg.Class.Equals("JianxianS", ic) && arg.Function.Equals("money", ic))
            {
                if (adminRights)
                {
                        string[] args = arg.Args;
                        Fougerite.Player jogador = Fougerite.Player.FindByName(args[1]);
                        switch (args[0])
                        {
                            case "+qian":
                                if (jogador != null)
                                {
                                    int valor = int.Parse(args[2]);
                                    Money.AddMoney(jogador, valor);
                                    Fougerite.Server.GetServer().Broadcast("[color#99CC]剑客[ " + jogador.Name + " ][color#FF6633] 充值了 [color#99FF]" + valor + " [color#FF6633]斩仙币  -  !尽情的享受购物吧!");
                                    arg.ReplyWith("充值成功");
                                }
                                else
                                {
                                    arg.ReplyWith("[color#99FF]剑客 " + args[0] + "查无此人!");
                                }
                                break;
                            case "zero":
                                if (jogador != null)
                                {
                                    int valor = 0;
                                    Money.SetMoney(jogador, valor);
                                    arg.ReplyWith("[color#99FF]剑客: " + jogador.Name + " 斩仙币被系统归零!");
                                }
                                else
                                {
                                    arg.ReplyWith("[color#99CC]剑客 " + args[0] + "查无此人!");
                                }
                                break;
                            case "-qian":
                                if (jogador != null)
                                {
                                    int valor = int.Parse(args[2]);
                                    Money.RemoveMoney(jogador, valor);
                                    arg.ReplyWith("[color#99FF]剑客" + jogador.Name + "被系统扣除了  " + valor + "  斩仙币");
                                }
                                else
                                {
                                    arg.ReplyWith("[color#99FF]剑客 " + args[0] + "沒找到哦");
                                }
                                break;
                        }
                }
            }
        }
    }
}