using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TNTRFDRPC;

public class CustomTNTRFDRPCMonoBehavior(IntPtr handle): MonoBehaviour(handle) {
    private void Update() {
        if (DiscordRichPresence.Instance.isInitialized) DiscordRichPresence.Instance.rpc.Invoke();
    }

    private void OnApplicationQuit() {
        DiscordRichPresence.Instance.Dispose();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += (UnityAction<Scene, LoadSceneMode>) OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Plugin.Log.LogInfo($"OnSceneLoaded {scene.name}");

        DiscordRichPresence.Instance.SceneChange(scene, mode);
    }
}