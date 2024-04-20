using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Csg;

namespace RicardoBracken
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class RicardoBracken : BaseUnityPlugin
    {
        public static RicardoBracken Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        internal static Dictionary<string, AudioClip> Sfx { get; private set; } = [];
        internal static Dictionary<string, GameObject> Models { get; private set; } = [];

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            Patch();

            if (LoadBundles()) Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
            else Unpatch();
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }

        internal static bool LoadBundles()
        {
            bool ok = true;

            string location = Instance.Info.Location;
            location = location.TrimEnd((MyPluginInfo.PLUGIN_GUID + ".dll").ToCharArray());

            AssetBundle sfxBundle = AssetBundle.LoadFromFile(location + "audios");
            AssetBundle modelsBundle = AssetBundle.LoadFromFile(location + "models");

            if (sfxBundle != null)
            {
                List<AudioClip> sounds = [.. sfxBundle.LoadAllAssets<AudioClip>()];

                sounds.ForEach(sound => {
                    Sfx.Add(sound.name, sound);
                    Logger.LogInfo("Loaded sound " + sound.name);
                });

                Logger.LogInfo("Successfully loaded the audio files");
            }
            else
            {
                ok = false;
                Logger.LogError("Failed to load audio files");
            }

            if (modelsBundle != null)
            {
                List<GameObject> models = [.. modelsBundle.LoadAllAssets<GameObject>()];

                models.ForEach(model => {
                    Models.Add(model.name, model);
                    Logger.LogInfo("Loaded model " + model.name);
                });

                Logger.LogInfo("Successfully loaded the model files");
            }
            else
            {
                ok = false;
                Logger.LogError("Failed to load model files");
            }

            return ok;
        }
    }
}
