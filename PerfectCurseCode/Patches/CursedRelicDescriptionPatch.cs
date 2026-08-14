using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using PerfectCurse.Relics.Cursed;

namespace PerfectCurse.Patches;

public static class CursedRelicDescriptionPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(RelicModel), "get_Description")]
    private static bool DescriptionPrefix(RelicModel __instance, ref LocString __result) =>
        ApplyDisguise(__instance, ref __result);

    [HarmonyPrefix]
    [HarmonyPatch(typeof(RelicModel), "get_EventDescription")]
    private static bool EventDescriptionPrefix(RelicModel __instance, ref LocString __result) =>
        ApplyDisguise(__instance, ref __result);

    [HarmonyPrefix]
    [HarmonyPatch(typeof(RelicModel), "get_Flavor")]
    private static bool FlavorPrefix(RelicModel __instance, ref LocString __result)
    {
        if (__instance is not ICursedRelic { IsRevealed: false } cursedRelic)
            return true;

        __result = cursedRelic.DisguiseFlavor;
        return false;
    }

    private static bool ApplyDisguise(RelicModel instance, ref LocString result)
    {
        if (instance is not ICursedRelic { IsRevealed: false } cursedRelic)
            return true;

        result = cursedRelic.DisguiseDescription;
        return false;
    }
}
