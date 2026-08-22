using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace PerfectCurse.Relics.Cursed;

[Pool(typeof(SharedRelicPool))]
public sealed class CursedLavaLamp : CursedRelic<LavaLamp>
{
    private bool _tookDamageThisCombat;

    public override RelicRarity Rarity => RelicRarity.Common;

    public override Task AfterRoomEntered(AbstractRoom room)
    {
        _tookDamageThisCombat = false;
        return Task.CompletedTask;
    }

    public override Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? source,
        CardModel? cardSource)
    {
        if (Owner.RunState.CurrentRoom is CombatRoom &&
            target == Owner.Creature &&
            result.UnblockedDamage > 0 &&
            !props.HasFlag(ValueProp.Unblockable))
        {
            _tookDamageThisCombat = true;
        }

        return Task.CompletedTask;
    }

    public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
    {
        if (player != Owner || room is not CombatRoom || !_tookDamageThisCombat) return false;
        var removed = rewards.RemoveAll(reward => reward is CardReward);
        if (removed > 0) Flash();
        return removed > 0;
    }
}
