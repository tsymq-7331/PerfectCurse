using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using PerfectCurse.Powers;

namespace PerfectCurse.Cards.CharacterCards;

[Pool(typeof(IroncladCardPool))]
public sealed class CurseBlood : PerfectCurseContentCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("StrengthGain", 3m)];

    public CurseBlood() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<CurseBloodPower>(choiceContext, Owner.Creature,
            DynamicVars["StrengthGain"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}
