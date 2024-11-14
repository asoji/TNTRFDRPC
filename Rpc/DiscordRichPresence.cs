#nullable enable

using System;
using DiscordRPC;
using DiscordRPC.Logging;
using TNTRFDRPC.Rpc;
using TNTRFDRPC.Rpc.Enso;
using UnityEngine;
using UnityEngine.SceneManagement;
using LogLevel = DiscordRPC.Logging.LogLevel;

namespace TNTRFDRPC;

public class DiscordRichPresence {
    private DiscordRichPresence() { }

    public static DiscordRichPresence Instance => instance;
    static DiscordRichPresence instance = new();
    public DiscordRpcClient rpc;

    public bool isInitialized = false;
    public bool isOnDifficultyScreen = false;

    public GameObject? difficultySettingObject;

    public string currentScene = "";

    public RichPresence richPresence = new() {
        Details = "Initializing...",
        State = "*whistles*",
        Timestamps = Utils.startTheClock,
        Assets = new DiscordRPC.Assets {
            LargeImageKey = "logo"
        }
    };

    public void Initialize() {
        if (isInitialized) return;

        SongInfoPlayerPatcher.Instance.OnSongInfoPlayerFinished += EnsoRpcHelpers.SetEnso;

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

    public void HandleFixedUpdate() {
        // Plugin.Log.LogInfo($"FixedUpdate received! {currentScene}");
        switch (currentScene) {
            case "SongSelect":
                if (!difficultySettingObject) {
                    RpcHelpers.TryFindDifficultySettingsObject("DifficutySetting1P(Clone)", ref difficultySettingObject);
                }
                if (!isOnDifficultyScreen && difficultySettingObject && difficultySettingObject!.active) {
                    isOnDifficultyScreen = true;
                    instance.richPresence.State = "Selecting Difficulty...";
                    UpdatePresence();
                    Plugin.Log.LogInfo("Difficulty screen active.");
                } else if (isOnDifficultyScreen && difficultySettingObject && !difficultySettingObject!.active) {
                    isOnDifficultyScreen = false;
                    instance.richPresence.State = "";
                    UpdatePresence();
                    Plugin.Log.LogInfo("Difficulty screen inactive.");
                }
                break;
            case "SongSelectTraining":
                if (!difficultySettingObject) {
                    RpcHelpers.TryFindDifficultySettingsObject("DifficutySettingTraining(Clone)",
                        ref difficultySettingObject);
                }

                if (!isOnDifficultyScreen && difficultySettingObject && difficultySettingObject!.active) {
                    isOnDifficultyScreen = true;
                    Plugin.Log.LogInfo("Training Difficulty screen active.");
                }
                else if (isOnDifficultyScreen && difficultySettingObject &&!difficultySettingObject!.active) {
                    isOnDifficultyScreen = false;
                    Plugin.Log.LogInfo("Training Difficulty screen inactive.");
                }
                break;
        }
    }

    public void UpdatePresence() => rpc.SetPresence(richPresence);
}