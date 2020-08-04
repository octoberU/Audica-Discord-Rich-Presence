using Harmony;
using System.Reflection;
using System;
using DiscordRichPresense;

namespace AudicaModding
{
    internal static class Hooks
    {
        public static void ApplyHooks(HarmonyInstance instance)
        {
            instance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(SongSelectItem), "OnSelect", new Type[0])]
        private static class ChangeSelectedSong
        {
            private static void Postfix(SongSelectItem __instance)
            {
                SongList.SongData currentSong = SongDataHolder.I.songData;
                AudicaRPC.Presence.state = currentSong.artist;
                AudicaRPC.Presence.details = currentSong.title;
                AudicaRPC.Presence.startTimestamp = default(long);
                AudicaRPC.Presence.largeImageKey = "audica_main";
                AudicaRPC.Presence.largeImageText = "Audica";
                AudicaRPC.Presence.smallImageKey = "expert";
                AudicaRPC.Presence.smallImageText = "Expert difficulty";
                DiscordRpc.UpdatePresence(AudicaRPC.Presence);
            }
        }

    }
}
