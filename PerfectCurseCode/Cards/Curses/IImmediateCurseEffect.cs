using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PerfectCurse.Cards.Curses;

public interface IImmediateCurseEffect
{
    Task TriggerImmediateCurseEffect(PlayerChoiceContext choiceContext);
}
