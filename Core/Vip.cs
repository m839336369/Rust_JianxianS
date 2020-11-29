using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Fougerite;
using MySql.Data.MySqlClient;

namespace JianxianS.Core
{
    public  class Vip
    {
        static MySqlDataReader reader;
        static int Vipcd=30;
        static private DataStore ds = DataStore.GetInstance();
        static DataTable dataTable = new DataTable();
        static TimeSpan timeSpan;
        static DateTime dateTime;
        static string _temp;
        public Vip()
        {
           // Fougerite.Hooks.OnPlayerSpawned += Hooks_OnPlayerSpawned;
        }

        private void Hooks_OnPlayerSpawned(Fougerite.Player player, Fougerite.Events.SpawnEvent se) 
        {
              if (Int32.Parse(GetVip(player.SteamID)) < 8)
              {               
                  player.Inventory.AddItem("Pipe Shotgun", 1);
                  player.Inventory.AddItem("Handmade Shell", 3);
              }
              else if (Int32.Parse(GetVip(player.SteamID)) == 8 )
              {
                  player.Inventory.AddItem("Revolver", 1);
                  player.Inventory.AddItem("9mm Ammo", 20);
              }
              else if (Int32.Parse(GetVip(player.SteamID)) == 9)
              {
                  player.Inventory.AddItem("P250", 1);
                  player.Inventory.AddItem("9mm Ammo", 20);
              }
         }
        public static string  GetVip(string steamID)
        {
            string cmdText = string.Concat(new string[]
{
                    "SELECT vip FROM ",
                    "stats",
                    " WHERE steamid=",
                    steamID,
});
            if (Stats.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, JianxianS.connection);//新建立1个数据库命令对象用来执行你下达的命令,命令就是上面的命令字符串SQL
                MySqlDataAdapter dd = new MySqlDataAdapter(cmd);//只有查询数据库才会用到适配器对象,用来存储上面的命令执行后得到的数据内容.
                DataSet dset = new DataSet();//新建1个数据集
                dd.Fill(dset);//然后把内容填充到数据集对象
                Stats.CloseConnection();//操作完成关闭数据库
                 _temp= dset.Tables[0].Rows[0][0].ToString();
            }
            return _temp;
        }
        public static void SetVip(string vip,string steamID)
        {
            string cmdText = string.Concat(new object[]
            {
                        "UPDATE stats SET vip='",
                        vip,
                        "',date='",
                        DateTime.Now,               
                        "' WHERE steamid='",
                        steamID,
                        "'"
                });
                if (Stats.OpenConnection())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                    mySqlCommand.ExecuteNonQuery();
                    Stats.CloseConnection();
                }
        }
        public static bool VipSure(string steamID)
        {
            string cmdText = string.Concat(new string[]
{
                    "SELECT date FROM ",
                    "stats",
                    " WHERE steamid=",
                    steamID,
});
            if (Stats.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, JianxianS.connection);//新建立1个数据库命令对象用来执行你下达的命令,命令就是上面的命令字符串SQL
                MySqlDataAdapter dd = new MySqlDataAdapter(cmd);//只有查询数据库才会用到适配器对象,用来存储上面的命令执行后得到的数据内容.
                DataSet dset = new DataSet();//新建1个数据集
                dd.Fill(dset);//然后把内容填充到数据集对象
                dateTime = DateTime.Parse((string)dset.Tables[0].Rows[0][0]);
                if (dateTime != null) timeSpan = DateTime.Now.Date - dateTime;
                else return false;
            }
            if ((timeSpan.Days) > 30) return false;
            else return true;           
        }
        public static void Vipsearch(Fougerite.Player player)
        {
            string cmdText = string.Concat(new string[]
{
                    "SELECT date FROM ",
                    "stats",
                    " WHERE steamid=",
                    player.SteamID,
});
            if (Stats.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, JianxianS.connection);//新建立1个数据库命令对象用来执行你下达的命令,命令就是上面的命令字符串SQL
                MySqlDataAdapter dd = new MySqlDataAdapter(cmd);//只有查询数据库才会用到适配器对象,用来存储上面的命令执行后得到的数据内容.
                DataSet dset = new DataSet();//新建1个数据集
                dd.Fill(dset);//然后把内容填充到数据集对象
                Stats.CloseConnection();//操作完成关闭数据库
                dateTime = DateTime.Parse((string)dset.Tables[0].Rows[0][0]);
            }           
            TimeSpan timeSpan = DateTime.Now.Date - dateTime;
            if ((timeSpan.Days) <= 30) player.Message("您的会员还有" + (Vipcd - (timeSpan.Days)).ToString() + "天");
            else player.Message("会员已到期");
        }
        public static string GetCH(string vip)
        {
            if (vip == "0") return "剑侠";
            else if (vip == "1") return "剑气";
            else if (vip == "2") return "剑段";
            else if (vip == "3") return "剑宗";
            else if (vip == "4") return "剑灵";
            else if (vip == "5") return "剑皇";
            else if (vip == "6") return "剑王";
            else if (vip == "7") return "剑帝";
            else if (vip == "8") return "剑圣";
            else if (vip == "9") return "剑仙";
            else if (vip == "10") return "【仙域】主播";
            else if (vip == "11") return "【仙域】仙罚者";
            else if (vip == "12") return "【仙域】剑仙";
            return "剑侠";
        }
    }
}