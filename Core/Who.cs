using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JianxianS.Core
{
    public class Who
    {
        public static void opbjectWho(Fougerite.Player player)
        {
            string text = "耐久度还有 %OBJECT.HEALTH% / %OBJECT.MAXHEALTH%";
            NetUser Sender=NetUser.FindByUserID(player.UID);
            float distance = player.Admin ? 1000f : 10f;
            GameObject lookObject = Helper.GetLookObject(Helper.GetLookRay(Sender), distance, -1);
            if (lookObject == null)
            {
                player.Message("您面前没有物品");
                return;
            }
            string newValue = Helper.NiceName(lookObject.name);
            StructureComponent component = lookObject.GetComponent<StructureComponent>();
            DeployableObject component2 = lookObject.GetComponent<DeployableObject>();
            TakeDamage component3 = lookObject.GetComponent<TakeDamage>();
            Fougerite.Player bySteamID;
            if (component != null)
            {
                
                bySteamID = Fougerite.Player.FindBySteamID(component._master.ownerID.ToString());
            }
            else
            {
                if (!(component2 != null))
                {

                    player.Message("没有找到所属");
                    return;
                }
                bySteamID = Fougerite.Player.FindBySteamID(component2.ownerID.ToString());
            }
            
            if (component3 == null)
            {
                text = "";
            }
            else
            {
                text = text.Replace("%OBJECT.HEALTH%", component3.health.ToString());
                text = text.Replace("%OBJECT.MAXHEALTH%", component3.maxHealth.ToString());
            }
            if (bySteamID != null)
            {
                string text2 = "建筑名称 %OBJECT.NAME% 建造主人 %OBJECT.OWNERNAME%. %OBJECT.CONDITION%".Replace("%OBJECT.CONDITION%", text);
                text2 = text2.Replace("%OBJECT.NAME%", newValue).Replace("%OBJECT.OWNERNAME%", bySteamID.Name);
                player.Message(text2);
                bool online = bySteamID.IsOnline;
                
                if (Sender.admin)
                {

                    if(online) player.Message("状态:在线");else player.Message("状态:离线");
                    player.Message( string.Concat(new object[]
                    {
                        "玩家最后位置: ",
                        bySteamID.X,
                        ",",
                        bySteamID.Y,
                        ",",
                        bySteamID.Z
                    }));
                }
            }
            else
            {
                player.Message("这个 %OBJECT.NAME% 不拥有名字对象. %OBJECT.CONDITION%".Replace("%OBJECT.NAME%", newValue).Replace("%OBJECT.CONDITION%",text));
            }
        }
    }
}