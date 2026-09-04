using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PerfectCurse.Powers;

public sealed class WeightlessnessPower : PerfectCursePower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner) return;

        Flash();
        var strength = Owner.GetPower<StrengthPower>();
        if (strength is not null && strength.Amount != 0m)
            await PowerCmd.ModifyAmount(choiceContext, strength, -2m * strength.Amount, Owner, null);

        var dexterity = Owner.GetPower<DexterityPower>();
        if (dexterity is not null && dexterity.Amount != 0m)
            await PowerCmd.ModifyAmount(choiceContext, dexterity, -2m * dexterity.Amount, Owner, null);

        await PowerCmd.Remove(this);
    }
}
