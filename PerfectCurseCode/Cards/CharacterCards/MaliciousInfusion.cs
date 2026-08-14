using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using PerfectCurse.Powers;

namespace PerfectCurse.Cards.CharacterCards;

[Pool(typeof(NecrobinderCardPool))]
public sealed class MaliciousInfusion : PerfectCurseContentCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Summon", 5m)];

    public MaliciousInfusion() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await OstyCmd.Summon(choiceContext, Owner, DynamicVars["Summon"].BaseValue, this);

        var curses = Owner.PlayerCombatState?.Hand.Cards
            .Where(card => card != this && card.Type == CardType.Curse)
            .ToList() ?? [];
        if (curses.Count == 0) return;

        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, 1)
        {
            Cancelable = true,
            ShouldGlowGold = card => card.Type == CardType.Curse
        };
        var selected = (await CardSelectCmd.FromSimpleGrid(
            choiceContext, curses, Owner, prefs)).FirstOrDefault();
        if (selected is null) return;

        await CardPileCmd.Add(selected, PileType.Exhaust, CardPilePosition.Top, this, false);
        await PowerCmd.Apply<MaliciousInfusionPower>(
            choiceContext, Owner.Creature, DynamicVars["Summon"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars["Summon"].UpgradeValueBy(3m);
}
