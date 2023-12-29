using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using System.Collections;
using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;
using System.ComponentModel;
using GameNetcodeStuff;


namespace KeysLockDoors.Patches
{
    public class KeyPatch
    {
        public static void Load()
        {
            // On.KeyItem.ItemActivate += KeyItem_ItemActivate;
            // Plugin.logger.LogInfo("Trying load keypatch on gamenetworkmanager");

            On.GameNetworkManager.Start += GameNetworkManager_Start;
            On.KeyItem.ItemActivate += KeyItem_ItemActivate;
        }

        private static void GameNetworkManager_Start(On.GameNetworkManager.orig_Start orig, GameNetworkManager self)
        {
            orig(self);

            foreach (var prefab in self.GetComponent<NetworkManager>().NetworkConfig.Prefabs.m_Prefabs)
            {
                if (prefab.Prefab.GetComponent<GrabbableObject>() != null)
                {
                    Plugin.logger.LogInfo(prefab.Prefab.GetComponent<GrabbableObject>().__getTypeName());
                    if (prefab.Prefab.GetComponent<GrabbableObject>().__getTypeName() == "KeyItem")
                    {
                        // Plugin.logger.LogInfo("Adding component to KeyItem");
                        var comp = prefab.Prefab.AddComponent<KeysLockDoors.MonoBehaviours.KeyItem>();
                    }
                }
            }
        }

        private static void KeyItem_ItemActivate(On.KeyItem.orig_ItemActivate orig, KeyItem self, bool used, bool buttonDown = true)
        {

            var isHoldingKey = (((object)GameNetworkManager.Instance.localPlayerController.ItemSlots[GameNetworkManager.Instance.localPlayerController.currentItemSlot]).GetType() == typeof(KeyItem));
            var playerHeldBy = GameNetworkManager.Instance.localPlayerController;
            if (!(playerHeldBy == null) && Physics.Raycast(new Ray(playerHeldBy.gameplayCamera.transform.position, playerHeldBy.gameplayCamera.transform.forward), out var hitInfo, 3f, 2816))
            {
                DoorLock component = hitInfo.transform.GetComponent<DoorLock>();
                if (component != null && !component.isLocked)
                {
                    var id = component.NetworkObjectId;
                    KeyItem currentlyHeldObjectServer = (KeyItem)GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer;
                    currentlyHeldObjectServer.GetComponent<KeysLockDoors.MonoBehaviours.KeyItem>().LockDoor(id);
                    component.LockDoor(); // client side locking
                    GameNetworkManager.Instance.localPlayerController.DespawnHeldObject(); 
                } else
                {
                    orig(self, used, buttonDown);
                }
            }
        }

    }
}
