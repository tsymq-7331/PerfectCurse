using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;

namespace PerfectCurse.Relics.Cursed;

public sealed class CursedRedMask : CursedRelic<RedMask>
{
    public override RelicRarity Rarity => RelicRarity.Common;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(1m)];

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> creatures, ICombatState combatState)
    {
        if (side != Owner.Creature.Side || combatState.RoundNumber != 1) return;
        Flash([Owner.Creature]);
        await PowerCmd.Apply<WeakPower>(choiceContext, Owner.Creature, DynamicVars["WeakPower"].BaseValue, Owner.Creature, null);
    }
}
