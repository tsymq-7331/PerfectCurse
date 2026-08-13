using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PerfectCurse.Powers;

namespace PerfectCurse.Cards.Curses;

public sealed class Shortage : PerfectCurseCard
{
    public override bool HasTurnEndInHandEffect => true;

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext) =>
        await PowerCmd.Apply<ShortagePower>(choiceContext, Owner.Creature, 2m, Owner.Creature, this);
}
