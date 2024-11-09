using DiscordRPC;
using DiscordRPC.Logging;
using UnityEngine.SceneManagement;
using LogLevel = DiscordRPC.Logging.LogLevel;

namespace TNTRFDRPC;

public class DiscordRichPresence {
    private DiscordRichPresence() { }

    public static DiscordRichPresence Instance => instance;

    public static DiscordRichPresence instance = new();

    public DiscordRpcClient rpc;
    public bool isInitialized = false;

    public RichPresence richPresence = new() {
        Details = "Initializing...",
        State = "*whistles*",
        Timestamps = Timestamps.Now,
        Assets = new DiscordRPC.Assets {
            LargeImageKey = "logo"
        }
    };

    public void Initialize() {
        if (isInitialized) return;
        rpc = new DiscordRpcClient("1304558957813174373");

        rpc.Logger = new ConsoleLogger() {
            Level = LogLevel.Warning,
            Coloured = true
        };

        rpc.OnReady += (sender, e) => {
            Plugin.Log.LogInfo($"Ready has been received from user {e.User.Username}");
            UpdatePresence();
        };

        rpc.OnPresenceUpdate += (sender, e) => {
            Plugin.Log.LogInfo($"Update received! {e.Presence.Details}, {e.Presence.State}");
        };

        rpc.Initialize();
    }

    public void Dispose() {
        rpc.Dispose();
    }

    public void BootAndLoadingScreenRpc() {
        richPresence.Details = "Loading game...";
        richPresence.State = "";
        richPresence.Timestamps = Timestamps.Now;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void TitleScreenRpc() {
        richPresence.Details = "Title Screen";
        richPresence.State = "";
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void MainMenuScreenRpc() {
        richPresence.Details = "Main Menu";
        richPresence.State = "";
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void TaikoModeSelectScreenRpc() {
        richPresence.Details = "Taiko Mode";
        richPresence.State = "Selecting Mode...";
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void SongSelectScreenRpc(bool trainingMode) { // make this an enum switch like below to support several different modes
        richPresence.Details = "Song Select";

        if (trainingMode)
            richPresence.State = "Training Mode";
        else
            richPresence.State = "";

        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public enum EnsoType {
        Normal,
        Scenario,
        Training,
        DonChanBand
    }

    public void EnsoScreenRpc(EnsoType ensoType) {
        switch (ensoType) {
            case EnsoType.Normal:
                richPresence.Details = PlaceholderText.ENSO_SONG_NAME;
                richPresence.State = PlaceholderText.ENSO_DIFFICULTY;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko"
                };
                break;
            case EnsoType.Scenario:
                richPresence.Details = PlaceholderText.ENSO_SONG_NAME;
                richPresence.State = PlaceholderText.ENSO_DIFFICULTY;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Progression"
                };
                break;
            case EnsoType.Training:
                richPresence.Details = PlaceholderText.ENSO_SONG_NAME;
                richPresence.State = PlaceholderText.ENSO_DIFFICULTY;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Training"
                };
                break;
            case EnsoType.DonChanBand:
                richPresence.Details = PlaceholderText.ENSO_SONG_NAME;
                richPresence.State = PlaceholderText.ENSO_DIFFICULTY;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Don Chan Band"
                };
                break;
        }

        UpdatePresence();
    }

    public void SceneChange(Scene scene, LoadSceneMode mode) {
        switch (scene.name) {
            case "Boot":
                Plugin.Log.LogInfo("Currently on Boot screen");
                BootAndLoadingScreenRpc();
                break;
            case "Relay":
                Plugin.Log.LogInfo("Currently on Relay [loading] screen");
                BootAndLoadingScreenRpc();
                break;
            case "Title":
                Plugin.Log.LogInfo("Currently on Title screen");
                TitleScreenRpc();
                break;
            case "MainMenu":
                Plugin.Log.LogInfo("Currently on Main Menu screen");
                MainMenuScreenRpc();
                break;
            case "ThunderShrine":
                Plugin.Log.LogInfo("Currently on Taiko Mode screen");
                TaikoModeSelectScreenRpc();
                break;
            case "SongSelect":
                Plugin.Log.LogInfo("Currently on Song Selection screen");
                SongSelectScreenRpc(false);
                break;
            case "SongSelectTraining":
                Plugin.Log.LogInfo("Currently on Song Training Selection screen");
                SongSelectScreenRpc(true);
                break;
            case "Enso":
                Plugin.Log.LogInfo("Currently banging a drum as hard as humanly possible");
                EnsoScreenRpc(EnsoType.Normal);
                break;
            case "EnsoTrainingFull":
                Plugin.Log.LogInfo("Currently practicing banging the drums as hard as humanly possible");
                EnsoScreenRpc(EnsoType.Training);
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
                EnsoScreenRpc(EnsoType.DonChanBand);
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
                EnsoScreenRpc(EnsoType.Scenario);
                break;
            default:
                Plugin.Log.LogError(
                    "What. What happened? Is there an unknown scene not added to the plugin? Or did you fall into Taiko Void? Please report the missing screen above to https://github.com/asoji/TNTRFDRPC/issues");
                break;
        }
    }

    public void UpdatePresence() => rpc.SetPresence(richPresence);
}