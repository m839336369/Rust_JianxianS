using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Fougerite;
using Fougerite.Events;
using MySql.Data.MySqlClient;
using POSIX;

namespace JianxianS.Core
{
    public class Stats
    {
        public IniParser Bodies;
        public Stats()
        {
            Bodies = new IniParser(Path.Combine(JianxianS.ModPath, "BodyParts.ini"));
            Hooks.OnCommand += OnCommand;
            Hooks.OnPlayerConnected += OnPlayerConnected;
            Hooks.OnPlayerDisconnected += OnPlayerDisconnected;
            Hooks.OnPlayerKilled += OnPlayerKilled;
            Hooks.OnPlayerGathering += OnPlayerGathering;
            Hooks.OnNPCKilled += new Hooks.KillHandlerDelegate(OnNPCKilled);
        }
        public void UnStats()
        {
            Hooks.OnCommand -= OnCommand;
            Hooks.OnPlayerConnected -= OnPlayerConnected;
            Hooks.OnPlayerDisconnected -= OnPlayerDisconnected;
            Hooks.OnPlayerKilled -= OnPlayerKilled;
            Hooks.OnPlayerGathering -= OnPlayerGathering;
            Hooks.OnNPCKilled -= new Hooks.KillHandlerDelegate(OnNPCKilled);
        }
        public static bool OpenConnection()
        {
            bool result;
            try
            {
                JianxianS.connection.Close();
                JianxianS.connection.Open();
                result = true;
            }
            catch (MySqlException ex)
            {
                int number = ex.Number;
                if (number != 0)
                {
                    if (number == 1045)
                    {
                        Logger.Log("Invalid username/password, please try again", null);
                    }
                }
                else
                {
                    Logger.Log("Cannot connect to server.  Contact administrator", null);
                }
                result = false;
            }
            return result;
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000022A0 File Offset: 0x000004A0
        public  static bool CloseConnection()
        {
            bool result;
            try
            {
                JianxianS.connection.Close();
                result = true;
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message, null);
                result = false;
            }
            return result;
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000022E4 File Offset: 0x000004E4
        public void Delete()
        {
            string cmdText = "DELETE FROM stats";
            if (OpenConnection())
            {
                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                mySqlCommand.ExecuteNonQuery();
                CloseConnection();
            }
            
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00002324 File Offset: 0x00000524


        // Token: 0x0600000B RID: 11 RVA: 0x00002574 File Offset: 0x00000774
        public void OnCommand(Fougerite.Player pl, string cmd, string[] args)
        {
            if (cmd != null)
            {
                if (!(cmd == "stat"))
                {
                    if (!(cmd == "stats"))
                    {
                        if (cmd == "top")
                        {
                            if (args.Length == 1)
                            {
                                string text = args[0];
                                switch (text)
                                {
                                    case "kills":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary = this.calcTop("Kills");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most Kills ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary[0].count,
                                        this.white,
                                        " kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary[1].count,
                                        this.white,
                                        " kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary[2].count,
                                        this.white,
                                        " kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary[3].count,
                                        this.white,
                                        " kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary[4].count,
                                        this.white,
                                        " kills)"
                                            }));
                                            return;
                                        }
                                    case "deaths":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary2 = this.calcTop("Deaths");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most Deaths ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary2[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary2[0].count,
                                        this.white,
                                        " deaths)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary2[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary2[1].count,
                                        this.white,
                                        " deaths)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary2[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary2[2].count,
                                        this.white,
                                        " deaths)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary2[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary2[3].count,
                                        this.white,
                                        " deaths)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary2[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary2[4].count,
                                        this.white,
                                        " deaths)"
                                            }));
                                            return;
                                        }
                                    case "headshots":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary3 = this.calcTop("Headshots");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most Headshots ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary3[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary3[0].count,
                                        this.white,
                                        " headshots)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary3[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary3[1].count,
                                        this.white,
                                        " headshots)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary3[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary3[2].count,
                                        this.white,
                                        " headshots)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary3[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary3[3].count,
                                        this.white,
                                        " headshots)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary3[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary3[4].count,
                                        this.white,
                                        " headshots)"
                                            }));
                                            return;
                                        }
                                    case "time":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary4 = this.calcTop("Time");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most Time Played ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary4[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        this.msToTime((double)dictionary4[0].count),
                                        this.white,
                                        ")"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary4[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        this.msToTime((double)dictionary4[1].count),
                                        this.white,
                                        ")"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary4[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        this.msToTime((double)dictionary4[2].count),
                                        this.white,
                                        ")"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary4[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        this.msToTime((double)dictionary4[3].count),
                                        this.white,
                                        ")"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary4[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        this.msToTime((double)dictionary4[4].count),
                                        this.white,
                                        ")"
                                            }));
                                            return;
                                        }
                                    case "pve":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary5 = this.calcTop("PVE");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most PVE Kills ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary5[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary5[0].count,
                                        this.white,
                                        " PVE Kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary5[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary5[1].count,
                                        this.white,
                                        " PVE Kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary5[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary5[2].count,
                                        this.white,
                                        " PVE Kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary5[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary5[3].count,
                                        this.white,
                                        " PVE Kills)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary5[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary5[4].count,
                                        this.white,
                                        " PVE Kills)"
                                            }));
                                            return;
                                        }
                                    case "gathering":
                                        {
                                            Dictionary<int, Stats.TopPlayer> dictionary6 = this.calcTop("Gathering");
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        "TOP 5 Most Resources Gathered ",
                                        this.white,
                                        "------"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "1. ",
                                        this.green,
                                        dictionary6[0].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary6[0].count,
                                        this.white,
                                        " resources)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "2. ",
                                        this.green,
                                        dictionary6[1].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary6[1].count,
                                        this.white,
                                        " resources)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "3. ",
                                        this.green,
                                        dictionary6[2].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary6[2].count,
                                        this.white,
                                        " resources)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "4. ",
                                        this.green,
                                        dictionary6[3].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary6[3].count,
                                        this.white,
                                        " resources)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.white,
                                        "5. ",
                                        this.green,
                                        dictionary6[4].name,
                                        this.white,
                                        " (",
                                        this.green,
                                        dictionary6[4].count,
                                        this.white,
                                        " resources)"
                                            }));
                                            return;
                                        }
                                }
                                pl.MessageFrom("仙榜", this.white + "Valid TOPs : " + this.green + " kills, deaths, headshots, time, pve, gathering");
                            }
                            else
                            {
                                pl.MessageFrom("仙榜", this.white + "Valid TOPs : " + this.green + " kills, deaths, headshots, time, pve, gathering");
                            }
                        }
                    }
                    else
                    {
                        switch (args.Length)
                        {
                            case 0:
                                {
                                    string steamID = pl.SteamID;
                                    double num2 = double.Parse(this.ds.Get("StatsKills", steamID).ToString());
                                    double num3 = double.Parse(this.ds.Get("StatsDeaths", steamID).ToString());
                                    double num4 = double.Parse(this.ds.Get("StatsHeadshots", steamID).ToString());
                                    double num5 = double.Parse(this.ds.Get("StatsSuicides", steamID).ToString());
                                    double ms = double.Parse(this.ds.Get("StatsTime", steamID).ToString());
                                    double num6 = double.Parse(this.ds.Get("StatsPVE", steamID).ToString());
                                    double num7 = double.Parse(this.ds.Get("StatsGathering", steamID).ToString());
                                    double num8 = Math.Round(num4 / num2 * 100.0, 1);
                                    double num9 = (num3 != 0.0) ? Math.Round(num2 / num3, 2) : num2;
                                    pl.MessageFrom("仙榜", string.Concat(new string[]
                                    {
                                this.white,
                                "------ ",
                                this.green,
                                pl.Name,
                                " Stats ",
                                this.white,
                                "-----"
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "Kill : ",
                                this.white,
                                num2
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "Death : ",
                                this.white,
                                num3
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "Headshots : ",
                                this.white,
                                num4,
                                this.green,
                                " ( ",
                                num8,
                                " %)"
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "Suicides : ",
                                this.white,
                                num5
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "K/D Ratio : ",
                                this.white,
                                num9
                                    }));
                                    pl.MessageFrom("仙榜", this.green + "Time Played : " + this.white + this.msToTime(ms));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "PVE Kills : ",
                                this.white,
                                num6
                                    }));
                                    pl.MessageFrom("仙榜", string.Concat(new object[]
                                    {
                                this.green,
                                "Resources Gathered : ",
                                this.white,
                                num7
                                    }));
                                    break;
                                }
                            case 1:
                                {
                                    bool flag = false;
                                    foreach (object key in this.ds.Keys("LastName"))
                                    {
                                        string text2 = this.ds.Get("LastName", key).ToString();
                                        if (text2.Equals(args[0], StringComparison.OrdinalIgnoreCase))
                                        {
                                            flag = true;
                                            double num10 = double.Parse(this.ds.Get("StatsKills", key).ToString());
                                            double num11 = double.Parse(this.ds.Get("StatsDeaths", key).ToString());
                                            double num12 = double.Parse(this.ds.Get("StatsHeadshots", key).ToString());
                                            double num13 = double.Parse(this.ds.Get("StatsSuicides", key).ToString());
                                            double ms2 = double.Parse(this.ds.Get("StatsTime", key).ToString());
                                            double num14 = double.Parse(this.ds.Get("StatsPVE", key).ToString());
                                            double num15 = double.Parse(this.ds.Get("StatsGathering", key).ToString());
                                            double num16 = Math.Round(num12 / num10 * 100.0, 1);
                                            double num17 = (num11 != 0.0) ? Math.Round(num10 / num11, 2) : num10;
                                            pl.MessageFrom("仙榜", string.Concat(new string[]
                                            {
                                        this.white,
                                        "------ ",
                                        this.green,
                                        text2,
                                        " Stats ",
                                        this.white,
                                        "-----"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "Killtest : ",
                                        this.white,
                                        num10
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "Death : ",
                                        this.white,
                                        num11
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "Headshots : ",
                                        this.white,
                                        num12,
                                        this.green,
                                        " ( ",
                                        num16,
                                        " %)"
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "Suicides : ",
                                        this.white,
                                        num13
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "K/D Ratio : ",
                                        this.white,
                                        num17
                                            }));
                                            pl.MessageFrom("仙榜", this.green + "Time Played : " + this.white + this.msToTime(ms2));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "PVE Kills : ",
                                        this.white,
                                        num14
                                            }));
                                            pl.MessageFrom("仙榜", string.Concat(new object[]
                                            {
                                        this.green,
                                        "Resources Gathered : ",
                                        this.white,
                                        num15
                                            }));
                                        }
                                    }
                                    if (!flag)
                                    {
                                        pl.MessageFrom("仙榜", string.Concat(new string[]
                                        {
                                    this.white,
                                    "The player ",
                                    this.green,
                                    args[1],
                                    this.white,
                                    " was not found"
                                        }));
                                    }
                                    break;
                                }
                            default:
                                pl.MessageFrom("仙榜", string.Concat(new string[]
                                {
                                this.white,
                                "The player ",
                                this.green,
                                args[1],
                                this.white,
                                " was not found"
                                }));
                                break;
                        }
                    }
                }
                else
                {
                    switch (args.Length)
                    {
                        case 0:
                            pl.MessageFrom("仙榜", string.Concat(new string[]
                            {
                            this.green,
                            "---------- Stats",
                            this.white,
                            " by Pompeyo ",
                            this.green,
                            "----------"
                            }));
                            pl.MessageFrom("仙榜", "Use '/stat' Help");
                            pl.MessageFrom("仙榜", "Use '/stats' Check your stats");
                            pl.MessageFrom("仙榜", "Use '/stats playername' Check a player's stats");
                            pl.MessageFrom("仙榜", "Use '/top category' See the top 5 most valuable players");
                            pl.MessageFrom("仙榜", "Valid Top Categories : kills, deaths, headshots, time, pve, gathering");
                            if (pl.Admin)
                            {
                                pl.MessageFrom("仙榜", this.orange + "---------- Admin Only Commands ----------");
                                pl.MessageFrom("仙榜", "Use '/stat delete' Delete all stats");
                            }
                            break;
                        case 1:
                            if (pl.Admin)
                            {
                                string text = args[0];
                                if (text != null)
                                {
                                    if (text == "delete")
                                    {
                                        this.deleteStats();
                                        pl.MessageFrom("仙榜", this.green + "Global Stats deleted");
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
        // Token: 0x0600000C RID: 12 RVA: 0x0000442C File Offset: 0x0000262C

        public void OnPlayerConnected(Fougerite.Player pl)
        {
            string name = pl.Name;
            string steamID = pl.SteamID;
            string ip = pl.IP;
            this.ds.Add("LastplayerName", steamID, name);
            object obj = this.ds.Get("StatsKills", steamID);
            object obj2 = this.ds.Get("StatsDeaths", steamID);
            object obj3 = this.ds.Get("StatsHeadshots", steamID);
            object obj4 = this.ds.Get("StatsSuicides", steamID);
            object obj5 = this.ds.Get("StatsTime", steamID);
            object obj6 = this.ds.Get("StatsPVE", steamID);
            object obj7 = this.ds.Get("StatsBear", steamID);
            object obj8 = this.ds.Get("StatsWolf", steamID);
            object obj9 = this.ds.Get("StatsStag", steamID);
            object obj10 = this.ds.Get("StatsBoar", steamID);
            object obj11 = this.ds.Get("StatsChicken", steamID);
            object obj12 = this.ds.Get("StatsRabbit", steamID);
            object obj13 = this.ds.Get("StatsMutantBear", steamID);
            object obj14 = this.ds.Get("StatsMutantWolf", steamID);
            object obj15 = this.ds.Get("StatsGathering", steamID);
            if (obj == null)
            {
                this.ds.Add("StatsKills", steamID, "0");
            }
            if (obj2 == null)
            {
                this.ds.Add("StatsDeaths", steamID, "0");
            }
            if (obj3 == null)
            {
                this.ds.Add("StatsHeadshots", steamID, "0");
            }
            if (obj4 == null)
            {
                this.ds.Add("StatsSuicides", steamID, "0");
            }
            if (obj5 == null)
            {
                this.ds.Add("StatsTime", steamID, "1");
            }
            if (obj6 == null)
            {
                this.ds.Add("StatsPVE", steamID, "0");
            }
            if (obj7 == null)
            {
                this.ds.Add("StatsBear", steamID, "0");
            }
            if (obj8 == null)
            {
                this.ds.Add("StatsWolf", steamID, "0");
            }
            if (obj9 == null)
            {
                this.ds.Add("StatsStag", steamID, "0");
            }
            if (obj10 == null)
            {
                this.ds.Add("StatsBoar", steamID, "0");
            }
            if (obj11 == null)
            {
                this.ds.Add("StatsChicken", steamID, "0");
            }
            if (obj12 == null)
            {
                this.ds.Add("StatsRabbit", steamID, "0");
            }
            if (obj13 == null)
            {
                this.ds.Add("StatsMutantBear", steamID, "0");
            }
            if (obj14 == null)
            {
                this.ds.Add("StatsMutantWolf", steamID, "0");
            }
            if (obj15 == null)
            {
                this.ds.Add("StatsGathering", steamID, "0");
            }
            if (Ini.useMysql)
            {
                string cmdText = "SELECT * FROM stats WHERE steamid='" + steamID + "'";
                if (OpenConnection())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    if (mySqlDataReader == null || !mySqlDataReader.HasRows)
                    {
                        mySqlDataReader.Close();
                        string cmdText2 = string.Concat(new object[]
                        {
                            "INSERT INTO stats (player, steamid, ip, kills, deaths, headshoots, suicides, time, pve, gathering, clan, bear, wolf, stag, boar, chicken, rabbit, mutantbear, mutantwolf, vip, date) VALUES('",
                            name,
                            "', '",
                            steamID,
                            "', '",
                            ip,
                            "', '",
                            obj,
                            "', '",
                            obj2,
                            "', '",
                            obj3,
                            "', '",
                            obj4,
                            "', '",
                            obj5,
                            "', '",
                            obj6,
                            "', '",
                            obj15,
                            "', 'clan', '",
                            obj7,
                            "', '",
                            obj8,
                            "', '",
                            obj9,
                            "', '",
                            obj10,
                            "', '",
                            obj11,
                            "', '",
                            obj12,
                            "', '",
                            obj13,
                            "', '",
                            obj14,
                            "', '",
                            0,
                            "', '",
                            DateTime.Now,
                            "')"
                        });
                        MySqlCommand mySqlCommand2 = new MySqlCommand(cmdText2, JianxianS.connection);
                        mySqlCommand2.ExecuteNonQuery();
                    }
                    CloseConnection();
                }
            }
        }


        public void OnPlayerDisconnected(Fougerite.Player pl)
        {
            string name = pl.Name;
            string steamID = pl.SteamID;
            this.ds.Add("LastName", steamID, name);
            double ms = (double)pl.TimeOnline + double.Parse(this.ds.Get("StatsTime", steamID).ToString());
            this.ds.Add("StatsTime", steamID, ms.ToString(CultureInfo.InvariantCulture));
            if (Ini.useMysql)
            {
                string text = this.msToTime(ms);
                string cmdText = string.Concat(new string[]
                {
                    "UPDATE stats SET player='",
                    name,
                    "', time='",
                    text,
                    "' WHERE steamid='",
                    steamID,
                    "'"
                });
                if (OpenConnection())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                    mySqlCommand.ExecuteNonQuery();
                    CloseConnection();
                }
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00004A9C File Offset: 0x00002C9C
        public void OnPlayerKilled(DeathEvent de)
        {
            if (de.Victim != null && de.Attacker != null)
            {
                Fougerite.Player player = (Fougerite.Player)de.Victim;
                string steamID = player.SteamID;
                double num = double.Parse(this.ds.Get("StatsDeaths", steamID).ToString());
                this.ds.Add("StatsDeaths", steamID, num + 1.0);
                if (Ini.useMysql)
                {
                    double num2 = num + 1.0;
                    string cmdText = string.Concat(new object[]
                    {
                        "UPDATE stats SET deaths='",
                        num2,
                        "' WHERE steamid='",
                        steamID,
                        "'"
                    });
                    if (OpenConnection())
                    {
                        MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                        mySqlCommand.ExecuteNonQuery();
                        CloseConnection();
                    }
                }
                if (de.AttackerIsPlayer)
                {
                    Fougerite.Player player2 = (Fougerite.Player)de.Attacker;
                    if (player2 != player)
                    {
                        string steamID2 = player2.SteamID;
                        double num3 = double.Parse(this.ds.Get("StatsKills", steamID2).ToString());
                        this.ds.Add("StatsKills", steamID2, num3 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num4 = num3 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET kills='",
                                num4,
                                "' WHERE steamid='",
                                steamID2,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                        
                        string setting = Bodies.GetSetting("bodyparts", de.DamageEvent.bodyPart.ToString());
                        DamageEvent damageEvent = de.DamageEvent;
                        if (setting == "Head")
                        {
                            double num5 = double.Parse(this.ds.Get("StatsHeadshots", steamID2).ToString());
                            this.ds.Add("StatsHeadshots", steamID2, num5 + 1.0);
                            if (Ini.useMysql)
                            {
                                double num6 = num5 + 1.0;
                                string cmdText = string.Concat(new object[]
                                {
                                    "UPDATE stats SET headshoots='",
                                    num6,
                                    "' WHERE steamid='",
                                    steamID2,
                                    "'"
                                });
                                if (OpenConnection())
                                {
                                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                    mySqlCommand.ExecuteNonQuery();
                                    CloseConnection();
                                }
                            }
                        }
                    }
                    else
                    {
                        double num7 = double.Parse(this.ds.Get("StatsSuicides", steamID).ToString());
                        this.ds.Add("StatsSuicides", steamID, num7 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num8 = num7 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET suicides='",
                                num8,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00004E94 File Offset: 0x00003094
        public void OnPlayerGathering(Fougerite.Player pl, GatherEvent ge)
        {
            string steamID = pl.SteamID;
            double num = double.Parse(this.ds.Get("StatsGathering", steamID).ToString());
            this.ds.Add("StatsGathering", steamID, num + 1.0);
            if (Ini.useMysql)
            {
                double num2 = num + 1.0;
                string cmdText = string.Concat(new object[]
                {
                    "UPDATE stats SET gathering='",
                    num2,
                    "' WHERE steamid='",
                    steamID,
                    "'"
                });
                if (OpenConnection())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                    mySqlCommand.ExecuteNonQuery();
                    CloseConnection();
                }
            }
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00004F6C File Offset: 0x0000316C
        public void OnNPCKilled(HurtEvent he)
        {

            if (he.Attacker is Fougerite.Player && he.Attacker != null)
            {

                Fougerite.Player player = (Fougerite.Player)he.Attacker;
                string name = ((NPC)he.Victim).Name;
                if (player != null)
                {
                    string steamID = player.SteamID;
                    double num = double.Parse(this.ds.Get("StatsPVE", steamID).ToString());
                    this.ds.Add("StatsPVE", steamID, num + 1.0);
                    if (Ini.useMysql)
                    {
                        double num2 = num + 1.0;
                        string cmdText = string.Concat(new object[]
                        {
                            "UPDATE stats SET pve='",
                            num2,
                            "' WHERE steamid='",
                            steamID,
                            "'"
                        });
                        if (OpenConnection())
                        {
                            MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                            mySqlCommand.ExecuteNonQuery();
                            CloseConnection();
                        }
                    }
                   
                    if (name == "Bear")
                    {
                        double num3 = double.Parse(this.ds.Get("StatsBear", steamID).ToString());
                        this.ds.Add("StatsBear", steamID, num3 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num4 = num3 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET bear='",
                                num4,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "Wolf")
                    {
                        double num5 = double.Parse(this.ds.Get("StatsWolf", steamID).ToString());
                        this.ds.Add("StatsWolf", steamID, num5 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num6 = num5 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET wolf='",
                                num6,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "Stag")
                    {
                        double num7 = double.Parse(this.ds.Get("StatsStag", steamID).ToString());
                        this.ds.Add("StatsStag", steamID, num7 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num8 = num7 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET stag='",
                                num8,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "Boar")
                    {
                        double num9 = double.Parse(this.ds.Get("StatsBoar", steamID).ToString());
                        this.ds.Add("StatsBoar", steamID, num9 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num10 = num9 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET boar='",
                                num10,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "Chicken")
                    {
                        double num11 = double.Parse(this.ds.Get("StatsChicken", steamID).ToString());
                        this.ds.Add("StatsChicken", steamID, num11 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num12 = num11 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET chicken='",
                                num12,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "Rabbit")
                    {
                        double num13 = double.Parse(this.ds.Get("StatsRabbit", steamID).ToString());
                        this.ds.Add("StatsRabbit", steamID, num13 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num14 = num13 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET rabbit='",
                                num14,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "MutantBear")
                    {
                        double num15 = double.Parse(this.ds.Get("StatsMutantBear", steamID).ToString());
                        this.ds.Add("StatsMutantBear", steamID, num15 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num16 = num15 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET mutantbear='",
                                num16,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    else if (name == "MutantWolf")
                    {
                        double num17 = double.Parse(this.ds.Get("StatsMutantWolf", steamID).ToString());
                        this.ds.Add("StatsMutantWolf", steamID, num17 + 1.0);
                        if (Ini.useMysql)
                        {
                            double num18 = num17 + 1.0;
                            string cmdText = string.Concat(new object[]
                            {
                                "UPDATE stats SET mutantwolf='",
                                num18,
                                "' WHERE steamid='",
                                steamID,
                                "'"
                            });
                            if (OpenConnection())
                            {
                                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, JianxianS.connection);
                                mySqlCommand.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x06000011 RID: 17 RVA: 0x000057DC File Offset: 0x000039DC
        public void deleteStats()
        {
            foreach (object key in this.ds.Keys("StatsTime"))
            {
                this.ds.Add("StatsKills", key, "0");
                this.ds.Add("StatsDeaths", key, "0");
                this.ds.Add("StatsHeadshots", key, "0");
                this.ds.Add("StatsSuicides", key, "0");
                this.ds.Add("StatsTime", key, "0");
                this.ds.Add("StatsPVE", key, "0");
                this.ds.Add("StatsBear", key, "0");
                this.ds.Add("StatsWolf", key, "0");
                this.ds.Add("StatsStag", key, "0");
                this.ds.Add("StatsBoar", key, "0");
                this.ds.Add("StatsChicken", key, "0");
                this.ds.Add("StatsRabbit", key, "0");
                this.ds.Add("StatsMutantBear", key, "0");
                this.ds.Add("StatsMutantWolf", key, "0");
                this.ds.Add("StatsGathering", key, "0");
                if (Ini.useMysql)
                {
                    this.Delete();
                }
            }
        }

        // Token: 0x06000012 RID: 18 RVA: 0x0000598C File Offset: 0x00003B8C
        public Dictionary<int, Stats.TopPlayer> calcTop(string top)
        {
            Dictionary<int, Stats.TopPlayer> dictionary = new Dictionary<int, Stats.TopPlayer>(5);
            dictionary[0] = new Stats.TopPlayer(" ", 0);
            dictionary[1] = new Stats.TopPlayer(" ", 0);
            dictionary[2] = new Stats.TopPlayer(" ", 0);
            dictionary[3] = new Stats.TopPlayer(" ", 0);
            dictionary[4] = new Stats.TopPlayer(" ", 0);
            foreach (object key in this.ds.Keys("Stats" + top))
            {
                int num = int.Parse(this.ds.Get("Stats" + top, key).ToString());
                if (num > dictionary[0].count)
                {
                    dictionary[4] = dictionary[3];
                    dictionary[3] = dictionary[2];
                    dictionary[2] = dictionary[1];
                    dictionary[1] = dictionary[0];
                    dictionary[0] = new Stats.TopPlayer(this.ds.Get("LastName", key).ToString(), num);
                }
                else if (num > dictionary[1].count)
                {
                    dictionary[4] = dictionary[3];
                    dictionary[3] = dictionary[2];
                    dictionary[2] = dictionary[1];
                    dictionary[1] = new Stats.TopPlayer(this.ds.Get("LastName", key).ToString(), num);
                }
                else if (num > dictionary[2].count)
                {
                    dictionary[4] = dictionary[3];
                    dictionary[3] = dictionary[2];
                    dictionary[2] = new Stats.TopPlayer(this.ds.Get("LastName", key).ToString(), num);
                }
                else if (num > dictionary[3].count)
                {
                    dictionary[4] = dictionary[3];
                    dictionary[3] = new Stats.TopPlayer(this.ds.Get("LastName", key).ToString(), num);
                }
                else if (num > dictionary[4].count)
                {
                    dictionary[4] = new Stats.TopPlayer(this.ds.Get("LastName", key).ToString(), num);
                }
            }
            return dictionary;
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00005C44 File Offset: 0x00003E44
        public string msToTime(double ms)
        {
            double num = Math.Round(ms / 1000.0);
            string result;
            if (num <= 60.0)
            {
                result = num + "s";
            }
            else
            {
                double num2 = (num - num % 60.0) / 60.0;
                double num3 = num % 60.0;
                if (num2 > 60.0)
                {
                    double num4 = (num2 - num2 % 60.0) / 60.0;
                    double num5 = num2 % 60.0;
                    if (num4 > 24.0)
                    {
                        result = string.Concat(new object[]
                        {
                            (num4 - num4 % 24.0) / 24.0,
                            "days ",
                            num4 % 60.0,
                            "h ",
                            num5,
                            "m ",
                            num3,
                            "s"
                        });
                    }
                    else
                    {
                        result = string.Concat(new object[]
                        {
                            num4,
                            "h ",
                            num5,
                            "m ",
                            num3,
                            "s"
                        });
                    }
                }
                else
                {
                    result = string.Concat(new object[]
                    {
                        num2,
                        "m ",
                        num3,
                        "s"
                    });
                }
            }
            return result;
        }

        // Token: 0x04000001 RID: 1
        private DataStore ds = DataStore.GetInstance();



        // Token: 0x04000004 RID: 4
        public string green = "[color #00EB7E]";

        // Token: 0x04000005 RID: 5
        public string orange = "[color #FB9A50]";

        // Token: 0x04000006 RID: 6
        public string orange2 = "[color #FFA500]";

        // Token: 0x04000007 RID: 7
        public string red = "[color #FF0000]";

        // Token: 0x04000008 RID: 8
        public string white = "[color #FFFFFF]";





        // Token: 0x02000003 RID: 3
        public class TopPlayer
        {
            // Token: 0x06000016 RID: 22 RVA: 0x00005EC5 File Offset: 0x000040C5
            public TopPlayer(string name, int count)
            {
                this.name = name;
                this.count = count;
            }

            // Token: 0x04000013 RID: 19
            public string name;

            // Token: 0x04000014 RID: 20
            public int count;
        }
    }
}
