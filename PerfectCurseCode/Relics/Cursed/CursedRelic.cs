using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace PerfectCurse.Relics.Cursed;

public interface ICursedRelic
{
    bool IsRevealed { get; }
    LocString DisguiseDescription { get; }
    LocString DisguiseFlavor { get; }
}

public abstract class CursedRelic<TDisguise> : PerfectCurseRelic, ICursedRelic
    where TDisguise : RelicModel, new()
{
    [SavedProperty]
    public bool IsRevealed { get; private set; }

    private static TDisguise Disguise => ModelDb.Relic<TDisguise>();
    public LocString DisguiseDescription => new("relics", $"{Disguise.Id.Entry}.description");
    public LocString DisguiseFlavor => new("relics", $"{Disguise.Id.Entry}.flavor");

    public override bool HasUponPickupEffect => true;
    public override LocString Title => IsRevealed ? base.Title : Disguise.Title;
    public override string PackedIconPath => IsRevealed ? base.PackedIconPath : Disguise.PackedIconPath;
    protected override string PackedIconOutlinePath => IsRevealed
        ? base.PackedIconOutlinePath
        : $"res://images/atlases/relic_outline_atlas.sprites/{Disguise.Id.Entry.ToLowerInvariant()}.tres";
    protected override string BigIconPath => IsRevealed
        ? base.BigIconPath
        : $"res://images/relics/{Disguise.Id.Entry.ToLowerInvariant()}.png";

    public override async Task AfterObtained()
    {
        await base.AfterObtained();
        IsRevealed = true;
        RelicIconChanged();
        InvokeDisplayAmountChanged();
        Flash();
    }
}
