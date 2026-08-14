using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using PerfectCurse.Cards.Curses;

namespace PerfectCurse.Powers;

public sealed class RideThePoisonPower : PerfectCursePower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner.Creature != Owner || card.Type != CardType.Curse) return;

        Flash();
        await TriggerCurseImmediately(choiceContext, card);

        if (card.Pile?.Type == PileType.Hand)
            await CardPileCmd.Add(card, PileType.Exhaust, CardPilePosition.Top, this, false);

        var combatState = Owner.CombatState;
        if (combatState is null) return;

        foreach (var enemy in combatState.GetOpponentsOf(Owner).Where(enemy => enemy.IsAlive))
            await PowerCmd.Apply<PoisonPower>(choiceContext, enemy, Amount, Owner, null);
    }

    private static async Task TriggerCurseImmediately(PlayerChoiceContext choiceContext, CardModel card)
    {
        if (card is IImmediateCurseEffect immediateEffect)
        {
            await immediateEffect.TriggerImmediateCurseEffect(choiceContext);
            return;
        }

        if (card.HasTurnEndInHandEffect)
            await card.OnTurnEndInHandWrapper(choiceContext);
    }
}
