using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using PerfectCurse.Cards.Curses;

namespace PerfectCurse.Patches;

[HarmonyPatch]
internal static class CurseCardTypeRestrictionPatch
{
    private static MethodBase TargetMethod() => AccessTools.Method(
        typeof(CardModel),
        nameof(CardModel.CanPlay),
        [typeof(UnplayableReason).MakeByRefType(), typeof(AbstractModel).MakeByRefType()]);

    private static void Postfix(
        CardModel __instance,
        ref bool __result,
        ref UnplayableReason reason,
        ref AbstractModel? preventer)
    {
        if (!__result || !__instance.IsInCombat) return;

        var hand = __instance.Owner.PlayerCombatState?.Hand;
        if (hand is null || __instance.Pile != hand) return;

        var restrictingCurse = hand.Cards.FirstOrDefault(card =>
            card is ICardTypeRestriction restriction &&
            restriction.BlockedCardType == __instance.Type);
        if (restrictingCurse is null) return;

        __result = false;
        reason = UnplayableReason.BlockedByCardLogic;
        preventer = restrictingCurse;
    }
}
