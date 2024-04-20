using HarmonyLib;
using UnityEngine;

namespace RicardoBracken.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class FlowermanAIPatch
    {

        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        static void CrackNeckSoundPatch(FlowermanAI __instance)
        {
            if (RicardoBracken.Sfx.TryGetValue("crackneck", out AudioClip newCrackNeckSFX))
            {
                __instance.crackNeckSFX = newCrackNeckSFX;
                __instance.crackNeckAudio.clip = newCrackNeckSFX;
            }
        }

        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        static void AngerSoundPatch(FlowermanAI __instance)
        {
            if (RicardoBracken.Sfx.TryGetValue("anger", out AudioClip newCreatureAngerVoice))
                __instance.creatureAngerVoice.clip = newCreatureAngerVoice;
        }

        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        static void DeathSoundPatch(FlowermanAI __instance)
        {
            if (RicardoBracken.Sfx.TryGetValue("death", out AudioClip newDieSFX))
                __instance.dieSFX = newDieSFX;
        }

        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        static void ScanTagPatch(FlowermanAI __instance)
        {
            __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Ricardo Milos";
        }

    }
}
