using MelonLoader;
using DiscordRichPresense;
using Harmony;

namespace AudicaModding
{
    public class AudicaRPC : MelonMod
    {
        private const string DiscordAppID = "702671243895111850";
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();
        public static class BuildInfo
        {
            public const string Name = "Audica Discord RPC"; // Name of the Mod.  (MUST BE SET)
            public const string Author = "octo"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "1.0.1"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }
        public override void OnApplicationStart()
        {
            var i = HarmonyInstance.Create("audica-rpc");
            Hooks.ApplyHooks(i);
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

        public override void OnApplicationQuit()
        {
            DiscordRpc.Shutdown();
        }

    }
}
