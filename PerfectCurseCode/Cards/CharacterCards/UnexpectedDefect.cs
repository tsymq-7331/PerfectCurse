using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using PerfectCurse.Cards.Curses;

namespace PerfectCurse.Cards.CharacterCards;

[Pool(typeof(DefectCardPool))]
public sealed class UnexpectedDefect : PerfectCurseContentCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(12m, ValueProp.Move),
        new DynamicVar("EnergyGain", 1m),
        new DynamicVar("Draw", 1m),
        new DynamicVar("CurseCount", 2m)
    ];

    public UnexpectedDefect() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block.BaseValue, ValueProp.Move, cardPlay);
        await PlayerCmd.GainEnergy(DynamicVars["EnergyGain"].BaseValue, Owner);
        await CardPileCmd.Draw(choiceContext, DynamicVars["Draw"].BaseValue, Owner);

        for (var i = 0; i < (int)DynamicVars["CurseCount"].BaseValue; i++)
        {
            var curse = CreateRandomCurse();
            await CardPileCmd.AddGeneratedCardToCombat(
                curse, PileType.Hand, Owner, CardPilePosition.Random);
        }
    }

    private CardModel CreateRandomCurse()
    {
        var runState = RunState ?? throw new InvalidOperationException("Unexpected Defect requires an active run.");
        var combatState = CombatState ?? throw new InvalidOperationException("Unexpected Defect requires an active combat.");
        var index = runState.Rng.CombatCardGeneration.NextInt(5);
        return index switch
        {
            0 => combatState.CreateCard<Plague>(Owner),
            1 => combatState.CreateCard<Rot>(Owner),
            2 => combatState.CreateCard<OldWound>(Owner),
            3 => combatState.CreateCard<Shackles>(Owner),
            _ => combatState.CreateCard<Shortage>(Owner)
        };
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
        DynamicVars["EnergyGain"].UpgradeValueBy(1m);
        DynamicVars["Draw"].UpgradeValueBy(1m);
        DynamicVars["CurseCount"].UpgradeValueBy(1m);
    }
}
