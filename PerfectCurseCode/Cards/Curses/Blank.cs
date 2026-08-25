using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PerfectCurse.Cards.Curses;

public sealed class Blank : PerfectCurseCard, IImmediateCurseEffect
{
    public override async Task AfterCardChangedPiles(
        CardModel card,
        PileType oldPileType,
        AbstractModel? clonedBy)
    {
        if (card != this || Pile?.Type != PileType.Discard) return;

        await CardPileCmd.Add(this, PileType.Draw, CardPilePosition.Top, this, false);
    }

    public Task TriggerImmediateCurseEffect(PlayerChoiceContext choiceContext) => Task.CompletedTask;
}
