namespace TNTRFDRPC.Rpc;

public class RpcScreens {
    static DiscordRichPresence instance = DiscordRichPresence.Instance;

    public static void BootAndLoadingScreenRpc() {
        instance.richPresence.Details = "Loading game...";
        instance.richPresence.State = "";
        instance.richPresence.Timestamps = Utils.startTheClock;
        instance.richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        instance.UpdatePresence();
    }

    public static void TitleScreenRpc() {
        instance.richPresence.Details = "Title Screen";
        instance.richPresence.State = "";
        instance.richPresence.Timestamps = Utils.startTheClock;
        instance.richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        instance.UpdatePresence();
    }

    public static void MainMenuScreenRpc() {
        instance.richPresence.Details = "Main Menu";
        instance.richPresence.State = "";
        instance.richPresence.Timestamps = Utils.startTheClock;
        instance.richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        instance.UpdatePresence();
    }

    public static void TaikoModeSelectScreenRpc() {
        instance.richPresence.Details = "Taiko Mode";
        instance.richPresence.State = "Selecting Mode...";
        instance.richPresence.Timestamps = Utils.startTheClock;
        instance.richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        instance.UpdatePresence();
    }

    public static void SongSelectScreenRpc(bool trainingMode) {
        // make this an enum switch like below to support several different modes
        // instance.richPresence.Details = "Song Select";

        if (trainingMode)
            instance.richPresence.Details = "Song Select - Training Mode";
        else
            instance.richPresence.Details = "Song Select - Normal Mode";

        instance.richPresence.Timestamps = Utils.startTheClock;
        instance.richPresence.Assets = new DiscordRPC.Assets() {
            LargeImageKey = "logo"
        };

        instance.UpdatePresence();
    }
}