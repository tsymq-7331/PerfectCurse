using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PerfectCurse.Powers;

namespace PerfectCurse.Cards.Curses;

[Pool(typeof(CurseCardPool))]
public sealed class Weightlessness : PerfectCurseCard, IImmediateCurseEffect
{
    public override bool HasTurnEndInHandEffect => true;

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext) =>
        await PowerCmd.Apply<WeightlessnessPower>(choiceContext, Owner.Creature, 1m, Owner.Creature, this);

    public Task TriggerImmediateCurseEffect(PlayerChoiceContext choiceContext) => OnTurnEndInHand(choiceContext);
}
