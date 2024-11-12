using System;
using DiscordRPC;
using DiscordRPC.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using LogLevel = DiscordRPC.Logging.LogLevel;

namespace TNTRFDRPC;

public class DiscordRichPresence {
    private DiscordRichPresence() { }

    public static DiscordRichPresence Instance => instance;
    public static DiscordRichPresence instance = new();
    public DiscordRpcClient rpc;

    public bool isInitialized = false;
    private bool isOnDifficultyScreen = false;
    public bool isPlaying = false;
    public EnsoType currentEnsoType;
    private GameObject? difficultySettingObject { get; set; }
    public static Timestamps startTheClock = Timestamps.Now;
    public string currentScene = "";

    public enum EnsoType {
        Normal,
        Scenario,
        Training,
        DonChanBand
    }

    public RichPresence richPresence = new() {
        Details = "Initializing...",
        State = "*whistles*",
        Timestamps = startTheClock,
        Assets = new DiscordRPC.Assets {
            LargeImageKey = "logo"
        }
    };

    public void Initialize() {
        if (isInitialized) return;

        SongInfoPlayerPatcher.Instance.OnSongInfoPlayerFinished += SetEnso;

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
        richPresence.Timestamps = startTheClock;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void TitleScreenRpc() {
        richPresence.Details = "Title Screen";
        richPresence.State = "";
        richPresence.Timestamps = startTheClock;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void MainMenuScreenRpc() {
        richPresence.Details = "Main Menu";
        richPresence.State = "";
        richPresence.Timestamps = startTheClock;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void TaikoModeSelectScreenRpc() {
        richPresence.Details = "Taiko Mode";
        richPresence.State = "Selecting Mode...";
        richPresence.Timestamps = startTheClock;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void SongSelectScreenRpc(bool trainingMode) {
        // make this an enum switch like below to support several different modes
        richPresence.Details = "Song Select";

        if (trainingMode)
            richPresence.State = "Training Mode";
        else
            richPresence.State = "";

        richPresence.Timestamps = startTheClock;
        richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        UpdatePresence();
    }

    public void EnsoScreenRpc(EnsoType ensoType, String songNamePassthrough, EnsoData.SongGenre ensoSongGenre, EnsoData.EnsoLevelType ensoDifficulty) {
        switch (ensoType) {
            case EnsoType.Normal:
                richPresence.Details = songNamePassthrough;
                richPresence.Timestamps = startTheClock;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko"
                };
                EnsoGenreRpc(ensoSongGenre);
                EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoType.Scenario:
                richPresence.Details = songNamePassthrough;
                richPresence.Timestamps = startTheClock;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Progression"
                };
                EnsoGenreRpc(ensoSongGenre);
                EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoType.Training:
                richPresence.Details = songNamePassthrough;
                richPresence.Timestamps = startTheClock;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Training"
                };
                EnsoGenreRpc(ensoSongGenre);
                EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoType.DonChanBand:
                richPresence.Details = songNamePassthrough;
                richPresence.Timestamps = startTheClock;
                richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Don Chan Band",
                };
                EnsoGenreRpc(ensoSongGenre);
                EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
        }

        UpdatePresence();
    }

    public void EnsoDifficultyRpcIcon(EnsoData.EnsoLevelType ensoDifficulty) {
        switch (ensoDifficulty) {
            case EnsoData.EnsoLevelType.Easy:
                richPresence.Assets.SmallImageKey = "easy";
                richPresence.Assets.SmallImageText = "Easy";
                break;
            case EnsoData.EnsoLevelType.Normal:
                richPresence.Assets.SmallImageKey = "normal";
                richPresence.Assets.SmallImageText = "Normal";
                break;
            case EnsoData.EnsoLevelType.Hard:
                richPresence.Assets.SmallImageKey = "hard";
                richPresence.Assets.SmallImageText = "Hard";
                break;
            case EnsoData.EnsoLevelType.Mania:
                richPresence.Assets.SmallImageKey = "extreme";
                richPresence.Assets.SmallImageText = "Extreme";
                break;
            case EnsoData.EnsoLevelType.Ura:
                richPresence.Assets.SmallImageKey = "ultraextreme";
                richPresence.Assets.SmallImageText = "Extreme+";
                break;
            case EnsoData.EnsoLevelType.Num:
                break;
            default:
                break;
        }
    }

    public void EnsoGenreRpc(EnsoData.SongGenre ensoSongGenre) {
        switch (ensoSongGenre) {
            case EnsoData.SongGenre.Pops:
                richPresence.State = "Pop";
                break;
            case EnsoData.SongGenre.Anime:
                richPresence.State = "Anime";
                break;
            case EnsoData.SongGenre.Vocalo:
                richPresence.State = "VOCALOID™️ Music";
                break;
            case EnsoData.SongGenre.Variety:
                richPresence.State = "Variety";
                break;
            case EnsoData.SongGenre.Classic:
                richPresence.State = "Classical";
                break;
            case EnsoData.SongGenre.Game:
                richPresence.State = "Game Music";
                break;
            case EnsoData.SongGenre.Namco:
                richPresence.State = "NAMCO Original";
                break;
        }
    }

    public void HandleFixedUpdate() {
        // Plugin.Log.LogInfo($"FixedUpdate received! {currentScene}");
        switch (currentScene) {
            case "SongSelect":
                // if (!DifficultySettingObject) {
                //     try {
                //         Plugin.Log.LogDebug($"Finding Difficulty Setting Object");
                //         DifficultySettingObject = GameObject.Find("DifficutySetting1P(Clone)");
                //     }
                //     catch {
                //         Plugin.Log.LogError($"Could not find DifficutySetting1P(Clone)");
                //     }
                // }
                if (!isOnDifficultyScreen && difficultySettingObject && difficultySettingObject.active) {
                    isOnDifficultyScreen = true;
                    Plugin.Log.LogInfo("Difficulty screen active.");
                } else if (isOnDifficultyScreen && !difficultySettingObject.active) {
                    isOnDifficultyScreen = false;
                    Plugin.Log.LogInfo("Difficulty screen inactive.");
                }
                break;
            case "SongSelectTraining":
                // if (!DifficultySettingObject) {
                //     try {
                //         Plugin.Log.LogInfo($"Find Difficulty Setting Object");
                //         DifficultySettingObject = GameObject.Find("DifficutySettingTraining(Clone)");
                //     }
                //     catch {
                //         Plugin.Log.LogError($"Could not find DifficutySettingTraining(Clone)");
                //     }
                // }
                if (!isOnDifficultyScreen&& difficultySettingObject&& difficultySettingObject.active) {
                    isOnDifficultyScreen = true;
                    Plugin.Log.LogInfo("Training Difficulty screen active.");
                }
                break;
        }
    }


    public void SetEnso(object sender, EventArgs args) {
        if (sender is SongInfoPlayer) {
            SongInfoPlayer songInfoPlayer = (SongInfoPlayer)sender;
            var ensoDataManager = GameObject.Find("CommonObjects/Datas/EnsoDataManager").GetComponent<EnsoDataManager>();
            var ensoLevelDifficulty = ensoDataManager.GetDiffCourse(0);

            EnsoScreenRpc(currentEnsoType, songInfoPlayer.m_SongName, songInfoPlayer.m_Genre, ensoLevelDifficulty);
            isPlaying = true;

            Plugin.Log.LogInfo(songInfoPlayer.m_SongName);
        }
    }


    public void SceneChange(Scene scene, LoadSceneMode mode) {
        currentScene = scene.name;
        difficultySettingObject = null;
        isOnDifficultyScreen = false;
        isPlaying = false;

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
                //DifficultySettingObject = GameObject.Find("DifficutySetting1P(Clone)");
                SongSelectScreenRpc(false);
                break;
            case "SongSelectTraining":
                Plugin.Log.LogInfo("Currently on Song Training Selection screen");
                //DifficultySettingObject = GameObject.Find("DifficutySettingTraining(Clone)");
                SongSelectScreenRpc(true);
                break;
            case "Enso":
                Plugin.Log.LogInfo("Currently banging a drum as hard as humanly possible");
                currentEnsoType = EnsoType.Normal;
                // EnsoScreenRpc(EnsoType.Normal, PlaceholderText.ENSO_SONG_NAME, ensoLevelDifficulty);
                break;
            case "EnsoTrainingFull":
                Plugin.Log.LogInfo("Currently practicing banging the drums as hard as humanly possible");
                // EnsoScreenRpc(EnsoType.Training);
                currentEnsoType = EnsoType.Training;
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
                currentEnsoType = EnsoType.DonChanBand;
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
                currentEnsoType = EnsoType.Scenario;
                // EnsoScreenRpc(EnsoType.Scenario);
                break;
            default:
                Plugin.Log.LogError(
                    "What. What happened? Is there an unknown scene not added to the plugin? Or did you fall into Taiko Void? Please report the missing scene above to https://github.com/asoji/TNTRFDRPC/issues");
                break;
        }
    }

    public void UpdatePresence() => rpc.SetPresence(richPresence);
}