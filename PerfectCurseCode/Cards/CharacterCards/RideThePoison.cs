using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using PerfectCurse.Powers;

namespace PerfectCurse.Cards.CharacterCards;

[Pool(typeof(SilentCardPool))]
public sealed class RideThePoison : PerfectCurseContentCard
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Poison", 10m)];

    public RideThePoison() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<RideThePoisonPower>(
            choiceContext, Owner.Creature, DynamicVars["Poison"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars["Poison"].UpgradeValueBy(5m);
}
