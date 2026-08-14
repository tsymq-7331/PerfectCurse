using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace PerfectCurse.Cards.CharacterCards;

[Pool(typeof(DefectCardPool))]
public sealed class ReinforcedSmelting : PerfectCurseContentCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(8m, ValueProp.Move),
        new DynamicVar("Draw", 1m)
    ];

    public ReinforcedSmelting() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await AttackAndDraw(choiceContext, cardPlay);

        var combatState = Owner.PlayerCombatState
            ?? throw new InvalidOperationException("Reinforced Smelting requires an active combat.");
        var selectableCurses = new[]
            {
                combatState.Hand,
                combatState.DrawPile,
                combatState.DiscardPile
            }
            .SelectMany(pile => pile.Cards)
            .Where(card => card.Type == CardType.Curse)
            .ToList();

        if (selectableCurses.Count == 0) return;

        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, 1)
        {
            Cancelable = true,
            ShouldGlowGold = card => card.Type == CardType.Curse
        };
        var selected = (await CardSelectCmd.FromSimpleGrid(
            choiceContext, selectableCurses, Owner, prefs)).FirstOrDefault();

        if (selected is null) return;

        await CardPileCmd.Add(selected, PileType.Exhaust, CardPilePosition.Top, this, false);
        await AttackAndDraw(choiceContext, cardPlay);
    }

    private async Task AttackAndDraw(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target
            ?? throw new InvalidOperationException("Reinforced Smelting requires an enemy target.");
        await CreatureCmd.Damage(choiceContext, target, DynamicVars.Damage, Owner.Creature);
        await CardPileCmd.Draw(choiceContext, DynamicVars["Draw"].BaseValue, Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars["Draw"].UpgradeValueBy(1m);
    }
}
