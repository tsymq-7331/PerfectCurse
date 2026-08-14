using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PerfectCurse.Cards.Curses;

// RotUpgradeSuppressionPatch temporarily presents cards in this hand as unupgraded.
// The underlying deck cards are never mutated.
public sealed class Rot : PerfectCurseCard, IImmediateCurseEffect
{
    public Task TriggerImmediateCurseEffect(PlayerChoiceContext choiceContext) => Task.CompletedTask;
}
