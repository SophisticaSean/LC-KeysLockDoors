using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace KeysLockDoors.MonoBehaviours
{
    public class KeyItem : NetworkBehaviour
    {
        public void LockDoor(ulong doorID)
        {
            LockDoorServerRpc(doorID);
        }

        [ServerRpc(RequireOwnership = false)]
        public void LockDoorServerRpc(ulong doorID)
        {
            LockDoorClientRpc(doorID);
        }

        [ClientRpc]
        public void LockDoorClientRpc(ulong doorID)
        {
            foreach (var doorLock in UnityEngine.Object.FindObjectsOfType<DoorLock>())
            {
                if (doorLock.NetworkObjectId == doorID)
                {
                    Plugin.logger.LogInfo("found door to lock, locking door");
                    doorLock.LockDoor();
                }
            }
        }
    }
}
