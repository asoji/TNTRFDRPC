using System;

namespace TNTRFDRPC.Rpc.Enso;

public class EnsoRpcScreens {
    static DiscordRichPresence instance = DiscordRichPresence.Instance;

    public static void EnsoScreenRpc(EnsoRpcHelpers.EnsoType ensoType, String songNamePassthrough, EnsoData.SongGenre ensoSongGenre, EnsoData.EnsoLevelType ensoDifficulty) {
        switch (ensoType) {
            case EnsoRpcHelpers.EnsoType.Normal:
                instance.richPresence.Details = songNamePassthrough;
                instance.richPresence.Timestamps = Utils.startTheClock;
                instance.richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko"
                };
                EnsoRpcHelpers.EnsoGenreRpc(ensoSongGenre);
                EnsoRpcHelpers.EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoRpcHelpers.EnsoType.Scenario:
                instance.richPresence.Details = songNamePassthrough;
                instance.richPresence.Timestamps = Utils.startTheClock;
                instance.richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Progression"
                };
                EnsoRpcHelpers.EnsoGenreRpc(ensoSongGenre);
                EnsoRpcHelpers.EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoRpcHelpers.EnsoType.Training:
                instance.richPresence.Details = songNamePassthrough;
                instance.richPresence.Timestamps = Utils.startTheClock;
                instance.richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Taiko Training"
                };
                EnsoRpcHelpers.EnsoGenreRpc(ensoSongGenre);
                EnsoRpcHelpers.EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
            case EnsoRpcHelpers.EnsoType.DonChanBand:
                instance.richPresence.Details = songNamePassthrough;
                instance.richPresence.Timestamps = Utils.startTheClock;
                instance.richPresence.Assets = new DiscordRPC.Assets() {
                    LargeImageKey = "logo",
                    LargeImageText = "Don Chan Band",
                };
                EnsoRpcHelpers.EnsoGenreRpc(ensoSongGenre);
                EnsoRpcHelpers.EnsoDifficultyRpcIcon(ensoDifficulty);
                break;
        }

        instance.UpdatePresence();
    }
}