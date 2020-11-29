using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JianxianS.Core
{
    public  class Ini
    {
        public static int mysqlPort = 3306;
        public static string ServerAddress = "localhost";
        public static string _username = "dlq";
        public static string DataBase = "dlq";
        public static string _password = "woainiq1";
        public static string extraarg = "";
        public static string mysqlTable = "stats";
        public static bool useMysql = true;
        public static string connectionString;
        public Ini()
        {
            LoadConfig();
            if (useMysql)
            {
                connectionString = string.Concat(new string[]
                {
                    "SERVER=",
                   ServerAddress,
                    ";DATABASE=",
                   DataBase,
                    ";UID=",
                   _username,
                    ";PASSWORD=",
                   _password,
                    ";",
                   extraarg
                });
                JianxianS.connection = new MySqlConnection(connectionString);
            }
            config();
        }
        public static void LoadConfig()
        {
            if (!File.Exists(Path.Combine(JianxianS.ModPath, "Config.ini")))
            {
                File.Create(Path.Combine(JianxianS.ModPath, "Config.ini")).Dispose();
                JianxianS.Config = new IniParser(Path.Combine(JianxianS.ModPath, "Config.ini"));
                JianxianS.Config.AddSetting("Mysql", "mysqlPort", mysqlPort.ToString());
                JianxianS.Config.AddSetting("Mysql", "ServerAddress", ServerAddress.ToString());
                JianxianS.Config.AddSetting("Mysql", "_username", _username.ToString());
                JianxianS.Config.AddSetting("Mysql", "DataBase", DataBase.ToString());
                JianxianS.Config.AddSetting("Mysql", "_password", _password.ToString());
                JianxianS.Config.AddSetting("Mysql", "mysqlTable", mysqlTable.ToString());
                JianxianS.Config.AddSetting("Mysql", "useMysql", useMysql.ToString());
                JianxianS.Config.AddSetting("AuthMe", "YouNeedToBeLoggedIn", Core.AuthMe.AuthMeServer.YouNeedToBeLoggedIn);
                JianxianS.Config.AddSetting("AuthMe", "CredsReset", Core.AuthMe.AuthMeServer.CredsReset);
                JianxianS.Config.AddSetting("AuthMe", "TimeToLogin", Core.AuthMe.AuthMeServer.TimeToLogin.ToString());
                JianxianS.Config.AddSetting("AuthMe", "SocialSiteForHelp", Core.AuthMe.AuthMeServer.SocialSiteForHelp);
                JianxianS.Config.AddSetting("AuthMe", "RestrictedCommands", "home,tpa,tpaccept,hg");
                JianxianS.Config.Save();
            }
            else
            {
                JianxianS.Config = new IniParser(Path.Combine(JianxianS.ModPath, "Config.ini"));
                mysqlPort = int.Parse(JianxianS.Config.GetSetting("Mysql", "mysqlPort"));
                ServerAddress = JianxianS.Config.GetSetting("Mysql", "ServerAddress");
                _username = JianxianS.Config.GetSetting("Mysql", "_username");
                DataBase = JianxianS.Config.GetSetting("Mysql", "DataBase");
                _password = JianxianS.Config.GetSetting("Mysql", "_password");
                mysqlTable = JianxianS.Config.GetSetting("Mysql", "mysqlTable");
                useMysql = bool.Parse(JianxianS.Config.GetSetting("Mysql", "useMysql"));
            }
        }
        public static void config()
        {
            JianxianS.Config.AddSetting("mysql", "mysqlPort",mysqlPort.ToString());
            JianxianS.Config.AddSetting("mysql", "ServerAddress", ServerAddress.ToString());
            JianxianS.Config.AddSetting("mysql", "_username", _username.ToString());
            JianxianS.Config.AddSetting("mysql", "DataBase", DataBase.ToString());
            JianxianS.Config.AddSetting("mysql", "_password", _password.ToString());
            JianxianS.Config.AddSetting("mysql", "mysqlTable", mysqlTable.ToString());
            JianxianS.Config.AddSetting("mysql", "useMysql", useMysql.ToString());
        }
    }
}
