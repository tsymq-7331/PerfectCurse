using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;

namespace PerfectCurse.Relics.Cursed;

[Pool(typeof(SharedRelicPool))]
public sealed class UndyingSeed : CursedRelic<GhostSeed>
{
    public override RelicRarity Rarity => RelicRarity.Common;

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        ApplyEternal(card);
        return Task.CompletedTask;
    }

    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom || Owner.PlayerCombatState is null) return Task.CompletedTask;
        foreach (var card in Owner.PlayerCombatState.AllCards) ApplyEternal(card);
        return Task.CompletedTask;
    }

    private void ApplyEternal(CardModel card)
    {
        if (card.Owner != Owner || card.Rarity != CardRarity.Basic) return;
        if (!card.Tags.Contains(CardTag.Strike) && !card.Tags.Contains(CardTag.Defend)) return;
        if (card.Keywords.Contains(CardKeyword.Eternal)) return;
        CardCmd.ApplyKeyword(card, [CardKeyword.Eternal]);
    }
}
