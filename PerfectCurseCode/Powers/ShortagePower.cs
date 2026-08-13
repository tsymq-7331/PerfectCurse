using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace PerfectCurse.Powers;

public sealed class ShortagePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string CustomPackedIconPath => "res://images/powers/32/draw_reduction.png";
    public override string CustomBigIconPath => "res://images/powers/128/draw_reduction.png";

    public override decimal ModifyHandDraw(Player player, decimal amount) =>
        player == Owner.Player ? Math.Max(0m, amount - Amount) : amount;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<MegaCrit.Sts2.Core.Entities.Creatures.Creature> creatures, ICombatState combatState)
    {
        if (side == Owner.Side) await MegaCrit.Sts2.Core.Commands.PowerCmd.Remove(this);
    }
}
