using System;
using UnityEngine;

namespace TNTRFDRPC.Rpc.Enso;

public class EnsoRpcHelpers {
    static DiscordRichPresence instance = DiscordRichPresence.Instance;
    public static bool isPlaying = false;
    public static EnsoRpcHelpers.EnsoType currentEnsoType;

    public enum EnsoType {
        Normal,
        Scenario,
        Training,
        DonChanBand
    }

    public static void EnsoDifficultyRpcIcon(EnsoData.EnsoLevelType ensoDifficulty) {
        switch (ensoDifficulty) {
            case EnsoData.EnsoLevelType.Easy:
                instance.richPresence.Assets.SmallImageKey = "easy";
                instance.richPresence.Assets.SmallImageText = "Easy";
                break;
            case EnsoData.EnsoLevelType.Normal:
                instance.richPresence.Assets.SmallImageKey = "normal";
                instance.richPresence.Assets.SmallImageText = "Normal";
                break;
            case EnsoData.EnsoLevelType.Hard:
                instance.richPresence.Assets.SmallImageKey = "hard";
                instance.richPresence.Assets.SmallImageText = "Hard";
                break;
            case EnsoData.EnsoLevelType.Mania:
                instance.richPresence.Assets.SmallImageKey = "extreme";
                instance.richPresence.Assets.SmallImageText = "Extreme";
                break;
            case EnsoData.EnsoLevelType.Ura:
                instance.richPresence.Assets.SmallImageKey = "ultraextreme";
                instance.richPresence.Assets.SmallImageText = "Extreme+";
                break;
            case EnsoData.EnsoLevelType.Num:
                break;
            default:
                break;
        }
    }

    public static void EnsoGenreRpc(EnsoData.SongGenre ensoSongGenre) {
        switch (ensoSongGenre) {
            case EnsoData.SongGenre.Pops:
                instance.richPresence.State = "Pop";
                break;
            case EnsoData.SongGenre.Anime:
                instance.richPresence.State = "Anime";
                break;
            case EnsoData.SongGenre.Vocalo:
                instance.richPresence.State = "VOCALOID™️ Music";
                break;
            case EnsoData.SongGenre.Variety:
                instance.richPresence.State = "Variety";
                break;
            case EnsoData.SongGenre.Classic:
                instance.richPresence.State = "Classical";
                break;
            case EnsoData.SongGenre.Game:
                instance.richPresence.State = "Game Music";
                break;
            case EnsoData.SongGenre.Namco:
                instance.richPresence.State = "NAMCO Original";
                break;
        }
    }

    public static void SetEnso(object sender, EventArgs args) {
        if (sender is SongInfoPlayer) {
            SongInfoPlayer songInfoPlayer = (SongInfoPlayer)sender;
            var ensoDataManager = GameObject.Find("CommonObjects/Datas/EnsoDataManager").GetComponent<EnsoDataManager>();
            var ensoLevelDifficulty = ensoDataManager.GetDiffCourse(0);

            EnsoRpcScreens.EnsoScreenRpc(currentEnsoType, songInfoPlayer.m_SongName, songInfoPlayer.m_Genre, ensoLevelDifficulty);
            isPlaying = true;

            Plugin.Log.LogInfo(songInfoPlayer.m_SongName);
        }
    }

}