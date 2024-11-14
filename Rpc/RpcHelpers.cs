#nullable enable

using TNTRFDRPC.Rpc.Enso;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TNTRFDRPC.Rpc;

public class RpcHelpers {
    static DiscordRichPresence instance = DiscordRichPresence.Instance;

    public static bool TryFindDifficultySettingsObject(string objName, ref GameObject? diffGameObject) {
        if (diffGameObject) return true;
        try {
            Plugin.Log.LogDebug($"Finding Difficulty Setting Object");
            var spawnRoot = GameObject.Find("Canvas/CanvasMid/SpawnRoot");
            if (spawnRoot) {
                diffGameObject = spawnRoot.transform.Find(objName).gameObject;
            }

            return true;
        }
        catch {
            Plugin.Log.LogError($"Could not find {objName}");
            return false;
        }
    }

    public static void SceneChange(Scene scene, LoadSceneMode mode) {
        instance.currentScene = scene.name;
        instance.difficultySettingObject = null;
        instance.isOnDifficultyScreen = false;
        EnsoRpcHelpers.isPlaying = false;

        switch (scene.name) {
            case "Boot":
                Plugin.Log.LogInfo("Currently on Boot screen");
                RpcScreens.BootAndLoadingScreenRpc();
                break;
            case "Relay":
                Plugin.Log.LogInfo("Currently on Relay [loading] screen");
                RpcScreens.BootAndLoadingScreenRpc();
                break;
            case "Title":
                Plugin.Log.LogInfo("Currently on Title screen");
                RpcScreens.TitleScreenRpc();
                break;
            case "MainMenu":
                Plugin.Log.LogInfo("Currently on Main Menu screen");
                RpcScreens.MainMenuScreenRpc();
                break;
            case "ThunderShrine":
                Plugin.Log.LogInfo("Currently on Taiko Mode screen");
                RpcScreens.TaikoModeSelectScreenRpc();
                break;
            case "SongSelect":
                Plugin.Log.LogInfo("Currently on Song Selection screen");
                // difficultySettingObject = GameObject.Find("DifficutySetting1P(Clone)");
                RpcScreens.SongSelectScreenRpc(false);
                break;
            case "SongSelectTraining":
                Plugin.Log.LogInfo("Currently on Song Training Selection screen");
                // difficultySettingObject = GameObject.Find("DifficutySettingTraining(Clone)");
                RpcScreens.SongSelectScreenRpc(true);
                break;
            case "Enso":
                Plugin.Log.LogInfo("Currently banging a drum as hard as humanly possible");
                EnsoRpcHelpers.currentEnsoType = EnsoRpcHelpers.EnsoType.Normal;
                // EnsoScreenRpc(EnsoType.Normal, PlaceholderText.ENSO_SONG_NAME, ensoLevelDifficulty);
                break;
            case "EnsoTrainingFull":
                Plugin.Log.LogInfo("Currently practicing banging the drums as hard as humanly possible");
                // EnsoScreenRpc(EnsoType.Training);
                EnsoRpcHelpers.currentEnsoType = EnsoRpcHelpers.EnsoType.Training;
                break;
            case "OnlineRoom":
                Plugin.Log.LogInfo("Currently on Online Room screen");
                break;
            case "Store":
                Plugin.Log.LogInfo("Lets go shopping! Aw man! Aw man! Aw man!");
                break;
            case "StoreDress":
                Plugin.Log.LogInfo("Currently in the Outfit Shop screen");
                break;
            case "StoreNeiro":
                Plugin.Log.LogInfo("Currently in the Instruments Shop screen");
                break;
            case "StoreVariety":
                Plugin.Log.LogInfo("Currently in the General Shop screen");
                break;
            case "OmochaTaisen":
                Plugin.Log.LogInfo("Currently on the Great Drum Toy War screen");
                break;
            case "DonChanBand":
                Plugin.Log.LogInfo("Currently on the Don Chan Band screen");
                break;
            case "SongSelectDonChanBand":
                Plugin.Log.LogInfo("Currently on the Don Chan Song Selection screen");
                break;
            case "EnsoDonChanBand":
                Plugin.Log.LogInfo("Currently playing for the Don Chan Band");
                // EnsoScreenRpc(EnsoType.DonChanBand);
                EnsoRpcHelpers.currentEnsoType = EnsoRpcHelpers.EnsoType.DonChanBand;
                break;
            case "MyRoom":
                Plugin.Log.LogInfo("GET THE FUCK OUT OF MY ROOM IM PLAYING MINECRAFT");
                break;
            case "DressUp":
                Plugin.Log.LogInfo("Currently on Dress Up screen");
                break;
            case "CustomizeGreeting":
                Plugin.Log.LogInfo("Currently on Customize Greeting screen");
                break;
            case "CustomizeTitleNamePlate":
                Plugin.Log.LogInfo("Currently on Customize Title Name Plate screen");
                break;
            case "Trophy":
                Plugin.Log.LogInfo("Currently on Trophy screen");
                break;
            case "Memories":
                Plugin.Log.LogInfo("Currently on Memories screen");
                break;
            case "GameSetting":
                Plugin.Log.LogInfo("Currently on Game Settings screen");
                break;
            case "AutoNotesAdjustSetting":
                Plugin.Log.LogInfo("Currently on Auto Notes Adjust Setting screen");
                break;
            case "EnsoScenario":
                Plugin.Log.LogInfo("Currently progressing");
                EnsoRpcHelpers.currentEnsoType = EnsoRpcHelpers.EnsoType.Scenario;
                // EnsoScreenRpc(EnsoType.Scenario);
                break;
            default:
                Plugin.Log.LogError(
                    "What. What happened? Is there an unknown scene not added to the plugin? Or did you fall into Taiko Void? Please report the missing scene above to https://github.com/asoji/TNTRFDRPC/issues");
                break;
        }
    }
}