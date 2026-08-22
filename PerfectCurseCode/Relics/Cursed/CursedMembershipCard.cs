using BaseLib.Utils;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace PerfectCurse.Relics.Cursed;

[Pool(typeof(SharedRelicPool))]
public sealed class CursedMembershipCard : CursedRelic<MembershipCard>
{
    public override RelicRarity Rarity => RelicRarity.Common;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("PriceIncrease", 100m)];

    public override decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal amount)
    {
        if (player != Owner || !LocalContext.IsMe(Owner)) return amount;
        return amount * (1m + DynamicVars["PriceIncrease"].BaseValue / 100m);
    }
}
