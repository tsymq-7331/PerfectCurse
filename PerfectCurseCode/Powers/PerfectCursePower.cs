using BaseLib.Abstracts;
using BaseLib.Extensions;
using PerfectCurse.Extensions;

namespace PerfectCurse.Powers;

public abstract class PerfectCursePower : CustomPowerModel
{
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerIcon();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerIcon(big: true);
}
