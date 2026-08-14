using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using PerfectCurse.Cards.Curses;

namespace PerfectCurse.Patches;

/// <summary>
/// Implements Rot without downgrading or replacing permanent deck cards. While a combat
/// card is in a hand that also contains Rot, all reads of IsUpgraded return false.
/// Leaving the hand immediately restores the card's real upgrade state.
/// </summary>
[HarmonyPatch(typeof(CardModel), nameof(CardModel.IsUpgraded), MethodType.Getter)]
internal static class RotUpgradeSuppressionPatch
{
    private static void Postfix(CardModel __instance, ref bool __result)
    {
        if (!__result || !__instance.IsInCombat) return;

        CardPile? pile = __instance.Pile;
        if (pile?.Type != PileType.Hand) return;

        if (pile.Cards.Any(card => card is Rot))
            __result = false;
    }
}
