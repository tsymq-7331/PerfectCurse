using MegaCrit.Sts2.Core.Entities.Cards;

namespace PerfectCurse.Cards.Curses;

public sealed class Lust : PerfectCurseCard, ICardTypeRestriction
{
    public CardType BlockedCardType => CardType.Attack;
}
