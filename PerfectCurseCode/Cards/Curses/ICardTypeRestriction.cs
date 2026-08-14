using MegaCrit.Sts2.Core.Entities.Cards;

namespace PerfectCurse.Cards.Curses;

public interface ICardTypeRestriction
{
    CardType BlockedCardType { get; }
}
