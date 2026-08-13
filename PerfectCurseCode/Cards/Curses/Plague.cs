using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PerfectCurse.Cards.Curses;

public sealed class Plague : PerfectCurseCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(3m)];
    public override bool HasTurnEndInHandEffect => true;

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext) =>
        await PowerCmd.Apply<PoisonPower>(choiceContext, Owner.Creature, DynamicVars["PoisonPower"].BaseValue, Owner.Creature, this);
}
