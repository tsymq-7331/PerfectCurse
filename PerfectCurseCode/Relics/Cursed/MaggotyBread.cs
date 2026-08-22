using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace PerfectCurse.Relics.Cursed;

[Pool(typeof(SharedRelicPool))]
public sealed class MaggotyBread : CursedRelic<Bread>
{
    public override RelicRarity Rarity => RelicRarity.Common;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DynamicVar("FirstTurnEnergy", 2m),
        new DynamicVar("LaterTurnEnergyLoss", 1m)
    ];

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player != Owner || player.PlayerCombatState is null) return amount;
        if (player.PlayerCombatState.TurnNumber == 1)
            return amount + DynamicVars["FirstTurnEnergy"].BaseValue;

        return Math.Max(0m, amount - DynamicVars["LaterTurnEnergyLoss"].BaseValue);
    }
}
