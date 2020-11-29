using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RustBuster2016Server;
using System.Data;

namespace JianxianS.Core
{
    public class connectpopu
    {
        bool FoundRB;
        public  connectpopu()
        {
            Fougerite.Hooks.OnPlayerConnected += Hooks_OnPlayerConnected;
        }


        ~connectpopu()
        {
             Fougerite.Hooks.OnPlayerConnected -= Hooks_OnPlayerConnected;
        }

        private void Hooks_OnPlayerConnected(Fougerite.Player player)
        {
            string vip, ch;
            vip=Vip.GetVip(player.SteamID);
            ch = Vip.GetCH(vip);
            if (int.Parse(vip) > 0)
            {
                Fougerite.Server.GetServer().BroadcastNotice("剑客:"+player.Name+"苏醒"+"|剑级【"+ch+"】");       
            }
            else Fougerite.Server.GetServer().Broadcast("剑客:" + player.Name + "苏醒" + "|剑级【" + ch + "】");
            
        }

    }
}