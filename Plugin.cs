using BepInEx;
using HarmonyLib;
using Mirror;
using System;
using System.Reflection;
using UnityEngine;

namespace BiggerLobbyMod
{
    [BepInPlugin("com.DDDrag0.stonewards.biggerlobby", "Bigger Lobby", "1.0.0")]
    public class BiggerLobbyMod : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony harmony = new Harmony("com.DDDrag0.stonewards.biggerlobby");
            harmony.PatchAll();
            Debug.Log("[BiggerLobbyMod] Mod loaded and patches applied!");
        }
    }

    // Patch to force maxConnections to 20 using NetworkManager (base class)
    [HarmonyPatch(typeof(NetworkManager), "Awake")]
    public static class PatchMaxConnections
    {
        static void Postfix(NetworkManager __instance)
        {
            __instance.maxConnections = 20;
            Debug.Log($"[BiggerLobbyMod] maxConnections set to {__instance.maxConnections}");
        }
    }

    // Patch to increase the number of lobby slots
    [HarmonyPatch(typeof(LobbyPlayerSlotsManager), "Awake")]
    public static class PatchSlotManager
    {
        static void Postfix(LobbyPlayerSlotsManager __instance)
        {
            FieldInfo slotsField = typeof(LobbyPlayerSlotsManager).GetField("playerSlots", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo playersField = typeof(LobbyPlayerSlotsManager).GetField("playersInSlots", BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (slotsField == null || playersField == null)
            {
                Debug.LogError("[BiggerLobbyMod] No private fields found!");
                return;
            }

            Transform[] currentSlots = (Transform[])slotsField.GetValue(__instance);
            int oldCount = currentSlots.Length;
            int newCount = 20;

            if (oldCount >= newCount) return;
            
            Transform[] newSlots = new Transform[newCount];
            Array.Copy(currentSlots, newSlots, oldCount);
            
            Transform template = currentSlots[0];
            Transform parent = template.parent;
            
            int cols = 4;
            for (int i = oldCount; i < newCount; i++)
            {
                Transform newSlot = UnityEngine.Object.Instantiate(template, parent);
                int row = i / cols;
                int col = i % cols;
                newSlot.localPosition = new Vector3(col * 200f, -row * 150f, 0f);
                newSlots[i] = newSlot;
            }
            slotsField.SetValue(__instance, newSlots);
            
            LobbyPlayer[] newPlayers = new LobbyPlayer[newCount];
            LobbyPlayer[] oldPlayers = (LobbyPlayer[])playersField.GetValue(__instance);
            if (oldPlayers != null)
            {
                Array.Copy(oldPlayers, newPlayers, oldPlayers.Length);
            }
            playersField.SetValue(__instance, newPlayers);
            
            Debug.Log($"[BiggerLobbyMod] Slots increased from {oldCount} to {newCount}");
        }
    }
}
