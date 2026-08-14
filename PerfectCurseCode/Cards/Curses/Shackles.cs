using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace PerfectCurse.Cards.Curses;

public sealed class Shackles : PerfectCurseCard, IImmediateCurseEffect
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3m, ValueProp.Move)];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Pile?.Type != PileType.Hand || cardPlay.Card == this) return;
        await CreatureCmd.Damage(choiceContext, Owner.Creature, DynamicVars.Damage, Owner.Creature);
    }

    public Task TriggerImmediateCurseEffect(PlayerChoiceContext choiceContext) =>
        CreatureCmd.Damage(choiceContext, Owner.Creature, DynamicVars.Damage, Owner.Creature);
}
