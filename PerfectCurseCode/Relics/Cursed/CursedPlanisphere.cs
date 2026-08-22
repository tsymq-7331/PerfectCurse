using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;

namespace PerfectCurse.Relics.Cursed;

[Pool(typeof(SharedRelicPool))]
public sealed class CursedPlanisphere : CursedRelic<Planisphere>
{
    public override RelicRarity Rarity => RelicRarity.Common;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("HpLoss", 1m)];

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room.RoomType != RoomType.Event) return;
        Flash();
        await CreatureCmd.SetCurrentHp(
            Owner.Creature,
            Math.Max(1m, Owner.Creature.CurrentHp - DynamicVars["HpLoss"].BaseValue));
    }
}
