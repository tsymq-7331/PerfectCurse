using BaseLib.Abstracts;
using BaseLib.Extensions;
using PerfectCurse.Extensions;

namespace PerfectCurse.Relics;

public abstract class PerfectCurseRelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicIcon();
    protected override string PackedIconOutlinePath => "relic_outline.png".RelicIcon();
    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicIcon(big: true);
}
