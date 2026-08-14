using MegaCrit.Sts2.Core.Entities.Cards;

namespace PerfectCurse.Cards.Curses;

public sealed class Amnesia : PerfectCurseCard, ICardTypeRestriction
{
    public CardType BlockedCardType => CardType.Power;
}
