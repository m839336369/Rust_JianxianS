using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Fougerite;
using Fougerite.Events;
using JianxianS.Core;
using MySql.Data.MySqlClient;
using RustBuster2016Server;

namespace JianxianS
{
    public class JianxianS : Module
    {
        private static JianxianS _inst;
        public static MySqlConnection connection;
        public static IniParser Config;
        public static string ModPath = System.IO.Directory.GetCurrentDirectory() + "\\save\\JianxianS";
        public static Core.Ini Ini = new Core.Ini();
        //初始化
        public  Core.Stats stats = new Core.Stats();
        //统计系统
        //public  Core.AuthMe.AuthMeServer AuthMe = new Core.AuthMe.AuthMeServer();
        //授权系统
        public Core.Rconcommand rconcommand = new Core.Rconcommand();
        //RCON系统
        public Core.Command command = new Core.Command();
        //Command系统
        public Core.Money money = new Core.Money();
        //斩仙商城
        public Core.connectpopu connectpopu = new Core.connectpopu();
        //VIP进服通报
        public Core.Vip Vip = new Core.Vip();
        //VIP
        public Core.Server_Event Server_Event = new Core.Server_Event();
        //服务器事件处理   
        public JianxianS()
        {
            _inst = this;
        }
        public override string Name
        {
            get { return "JianxianS"; }
        }
        public override string Author
        {
            get { return "Jianxian"; }
        }

        public override string Description
        {
            get { return "Jianxian's server plugin"; }
        }

        public override Version Version 
        {
            get { return new Version("1.0"); }
        }


        public override void Initialize()
        {
            Hooks.OnModulesLoaded +=  OnModulesLoaded;
        }
        private void OnModulesLoaded()
        {
            foreach (Fougerite.PluginLoaders.BasePlugin enumerator in Fougerite.PluginLoaders.PluginLoader.GetInstance().Plugins.Values)
            {
                    if (enumerator.Name.ToLower().Contains("rustbuster"))
                    {
                        API.OnRustBusterUserMessage += Server_Event.API_OnRustBusterUserMessage;
                        break;
                    }   
            }
        }


        public override void DeInitialize()
        {

        }
    }
}
