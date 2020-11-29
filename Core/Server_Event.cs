using Facepunch.Movement;
using Fougerite;
using JianxianS;
using JianxianS.Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JianxianS.Core
{
    public class Server_Event
    {
        public  Server_Event()
        {

        }

        public void API_OnRustBusterUserMessage(RustBuster2016Server.API.RustBusterUserAPI user, RustBuster2016Server.Message msgc)
        {
            try
            {
                if (msgc.PluginSender == "JianxianC")
                {
                    string[] msg = msgc.MessageByClient.Split('|');
                    if (msg.Length == 3 && msg[0] == "Login")
                    {
                        string cmdText = string.Concat(new string[]
                        {
                    "SELECT id FROM ",
                    "dlq",
                    " WHERE zhanghao='",
                     msg[1],
                     "' and mima='",
                     msg[2],
                     "'"
                        });
                        if (Stats.OpenConnection())
                        {
                            MySqlCommand cmd = new MySqlCommand(cmdText, JianxianS.connection);//新建立1个数据库命令对象用来执行你下达的命令,命令就是上面的命令字符串SQL
                            MySqlDataAdapter dd = new MySqlDataAdapter(cmd);//只有查询数据库才会用到适配器对象,用来存储上面的命令执行后得到的数据内容.
                            DataSet dset = new DataSet();//新建1个数据集
                            dd.Fill(dset);//然后把内容填充到数据集对象
                            Stats.CloseConnection();//操作完成关闭数据库
                            if (dset.Tables[0].Rows.Count > 0)
                            {

                                string steamid = dset.Tables[0].Rows[0][0].ToString();
                                if (user.SteamID != steamid)
                                {
                                    foreach (Fougerite.Entity item in (from Fougerite.Entity temp in Fougerite.World.GetWorld().Entities where temp.OwnerID == steamid select temp))
                                    {
                                        item.ChangeOwner(user.Player);
  
                                    }
                                    cmdText = string.Concat(new object[]
{
                                   "UPDATE dlq SET id='",
                                    user.SteamID,
                                    "' WHERE id='",
                                    steamid,
                                    "'"
                                    });
                                    if (Stats.OpenConnection())
                                    {
                                        MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                        mySqlCommand.ExecuteNonQuery();
                                        Stats.CloseConnection();
                                    }
                                }
                                msgc.ReturnMessage = "Login|Yes";
                            }
                            else
                            {
                                msgc.ReturnMessage = "Login|No";
                                user.Player.Disconnect(true, NetError.Facepunch_Approval_ServerLoginException);
                            }
                        }   
                    }
                }
            }
            catch(Exception err)
            {
                Debug.WriteLog(err.Message);
            }
        }
    }
}
