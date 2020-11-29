
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Text;
using Facepunch.MeshBatch;
using System.Text.RegularExpressions;

namespace JianxianS.Core
{
    public class Helper
    {
        private static long seed = 0L;
        public static uint NewSerial
        {
            get
            {
                return (uint)Helper.NewSerial64;
            }
        }

        public static ulong NewSerial64
        {
            get
            {
                return (ulong)(DateTime.Now.Ticks ^ (Helper.seed += 1L));
            }
        }
        public static string NiceName(string input)
        {
            input = input.Replace("_A", "").Replace("A(Clone)", "").Replace("(Clone)", "");
            MatchCollection matchCollection = new Regex("([A-Z]*[^A-Z_]+)", RegexOptions.Compiled).Matches(input);
            string[] array = new string[matchCollection.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = matchCollection[i].Groups[0].Value.Trim();
            }
            return string.Join(" ", array);
        }
        public static Ray GetLookRay(NetUser player)
        {
            if (player == null)
            {
                return default(Ray);
            }
            return Helper.GetLookRay(player.playerClient);
        }

        public static Ray GetLookRay(PlayerClient player)
        {
            if (player == null)
            {
                return default(Ray);
            }
            return Helper.GetLookRay(player.controllable);
        }

        public static Ray GetLookRay(Controllable controllable)
        {
            if (controllable == null)
            {
                return default(Ray);
            }
            return Helper.GetLookRay(controllable.character);
        }

        public static Ray GetLookRay(Character character)
        {
            if (character == null)
            {
                return default(Ray);
            }
            Vector3 position = character.transform.position;
            Vector3 direction = character.eyesRay.direction;
            position.y += (character.stateFlags.crouch ? 0.85f : 1.65f);
            return new Ray(position, direction);
        }

        // Token: 0x06000638 RID: 1592 RVA: 0x0004678C File Offset: 0x0004498C
        public static GameObject GetLookObject(NetUser player, int layerMask = -1)
        {
            bool flag = player == null;
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = Helper.GetLookObject(player.playerClient, -1);
            }
            return result;
        }

        // Token: 0x06000639 RID: 1593 RVA: 0x000467B8 File Offset: 0x000449B8
        public static GameObject GetLookObject(PlayerClient player, int layerMask = -1)
        {
            bool flag = player == null;
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = Helper.GetLookObject(player.controllable, -1);
            }
            return result;
        }

        // Token: 0x0600063A RID: 1594 RVA: 0x000467E8 File Offset: 0x000449E8
        public static GameObject GetLookObject(Controllable controllable, int layerMask = -1)
        {
            bool flag = controllable == null;
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = Helper.GetLookObject(controllable.character, -1);
            }
            return result;
        }

        // Token: 0x0600063B RID: 1595 RVA: 0x00046818 File Offset: 0x00044A18
        public static GameObject GetLookObject(Character character, int layerMask = -1)
        {
            bool flag = character == null;
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                Vector3 position = character.transform.position;
                Vector3 direction = character.eyesRay.direction;
                position.y += (character.stateFlags.crouch ? 1f : 1.85f);
                result = Helper.GetLookObject(new Ray(position, direction), 300f, -1);
            }
            return result;
        }

        // Token: 0x0600063C RID: 1596 RVA: 0x0004688C File Offset: 0x00044A8C
        public static GameObject GetLookObject(Ray ray, float distance = 300f, int layerMask = -1)
        {
            Vector3 zero = Vector3.zero;
            return Helper.GetLookObject(ray, out zero, distance, layerMask);
        }

        // Token: 0x0600063D RID: 1597 RVA: 0x000468B0 File Offset: 0x00044AB0
        public static GameObject GetLookObject(Ray ray, out Vector3 point, float distance = 300f, int layerMask = -1)
        {
            point = Vector3.zero;
            RaycastHit raycastHit;
            bool flag2;
            MeshBatchInstance meshBatchInstance;
            bool flag = !Facepunch.MeshBatch.MeshBatchPhysics.Raycast(ray, out raycastHit, distance, layerMask, out flag2, out meshBatchInstance);
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IDMain idmain = flag2 ? meshBatchInstance.idMain : IDBase.GetMain(raycastHit.collider);
                point = raycastHit.point;
                bool flag3 = !(idmain != null);
                if (flag3)
                {
                    result = raycastHit.collider.gameObject;
                }
                else
                {
                    result = idmain.gameObject;
                }
            }
            return result;
        }

        // Token: 0x0600063E RID: 1598 RVA: 0x00046938 File Offset: 0x00044B38
        public static GameObject GetLineObject(Vector3 start, Vector3 end, out Vector3 point, int layerMask = -1)
        {
            point = Vector3.zero;
            RaycastHit raycastHit;
            bool flag2;
            MeshBatchInstance meshBatchInstance;
            bool flag = !Facepunch.MeshBatch.MeshBatchPhysics.Linecast(start, end, out raycastHit, layerMask, out flag2, out meshBatchInstance);
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IDMain idmain = flag2 ? meshBatchInstance.idMain : IDBase.GetMain(raycastHit.collider);
                point = raycastHit.point;
                bool flag3 = !(idmain != null);
                if (flag3)
                {
                    result = raycastHit.collider.gameObject;
                }
                else
                {
                    result = idmain.gameObject;
                }
            }
            return result;
        }
    }
}