using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;

namespace TNTRFDRPC;

public class SongInfoPlayerPatcher {

    public EventHandler OnSongInfoPlayerFinished;

    public static SongInfoPlayerPatcher Instance { get; private set; } = new SongInfoPlayerPatcher();


    [HarmonyPatch(typeof(SongInfoPlayer), nameof(SongInfoPlayer.Start))]
    [HarmonyPostfix]
    public static void PatchStartObject(SongInfoPlayer __instance) {
        SongInfoPlayerPatcher.Instance.OnSongInfoPlayerFinished?.Invoke(__instance, EventArgs.Empty);
    }
}