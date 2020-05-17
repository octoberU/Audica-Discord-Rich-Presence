using System;
using MelonLoader;
using NET_SDK;
using NET_SDK.Harmony;
using DiscordRichPresense;

namespace AudicaModding
{
    public class AudicaRPC : MelonMod
    {
        private const string DiscordAppID = "702671243895111850";
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();
        public static Patch SongSelectItem_OnSelect;
        public static Patch SongSelect_OnEnable;
        public static class BuildInfo
        {
            public const string Name = "Audica Discord RPC"; // Name of the Mod.  (MUST BE SET)
            public const string Author = "octo"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }
        public override void OnApplicationStart()
        {
            Instance instance = Manager.CreateInstance("AudicaRPC");
            AudicaRPC.SongSelectItem_OnSelect = instance.Patch(SDK.GetClass("SongSelectItem").GetMethod("OnSelect"), typeof(AudicaRPC).GetMethod("OnSelect"));
            AudicaRPC.SongSelect_OnEnable = instance.Patch(SDK.GetClass("SongSelect").GetMethod("OnEnablle"), typeof(AudicaRPC).GetMethod("SongSelectOnEnable"));
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(DiscordAppID, ref handlers, false, string.Empty);
            Presence.state = string.Empty;
            Presence.details = "In Menu";
            Presence.startTimestamp = default(long);
            Presence.largeImageKey = "audica_main";
            Presence.largeImageText = "AUDICA";
            Presence.smallImageKey = "expert";
            Presence.smallImageText = "Expert difficulty";
            DiscordRpc.UpdatePresence(Presence);
        }
        public override void OnUpdate()
        {
            DiscordRpc.RunCallbacks();
        }

        public static void OnSelect(IntPtr @this)
        {
            AudicaRPC.SongSelectItem_OnSelect.InvokeOriginal(@this);
            MelonModLogger.Log("Song Selected!");

            SongList.SongData currentSong = SongDataHolder.I.songData;

            Presence.state = currentSong.artist;
            Presence.details = currentSong.title;
            Presence.startTimestamp = default(long);
            Presence.largeImageKey = "audica_main";
            Presence.largeImageText = "Audica";
            Presence.smallImageKey = "expert";
            Presence.smallImageText = "Expert difficulty";
            DiscordRpc.UpdatePresence(Presence);
        }

        public override void OnApplicationQuit()
        {
            DiscordRpc.Shutdown();
        }

    }
}
