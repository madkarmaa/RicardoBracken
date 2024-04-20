using HarmonyLib;
using UnityEngine;

namespace RicardoBracken.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class FlowermanAIModelReplacement
    {

        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        static void ReplaceFlowermanModelPatch(FlowermanAI __instance)
        {
            // Hide default model
            Renderer[] renderers = __instance.transform.Find("FlowermanModel").GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh in renderers) mesh.enabled = false;
            RicardoBracken.Logger.LogInfo("Default bracken model hidden");

            if (RicardoBracken.Models.TryGetValue("ricardo", out GameObject ricardoModel))
            {
                GameObject ricardoClone = Object.Instantiate(ricardoModel, __instance.gameObject.transform);
                ricardoClone.name = "Ricardo(Clone)";
                ricardoClone.transform.localPosition = Vector3.zero;
                ricardoClone.transform.localScale += new Vector3(0.6f, 0.6f, 0.6f);
                ricardoClone.SetActive(true);
            }
        }

        [HarmonyPatch(nameof(FlowermanAI.KillEnemy))]
        [HarmonyPostfix]
        static void DeleteFlowermanModelOnKilledPatch(FlowermanAI __instance)
        {
            __instance.transform.Find("Ricardo(Clone)").gameObject.SetActive(false);
        }

    }
}
