using MegaCrit.Sts2.Core.Entities.Cards;

namespace PerfectCurse.Cards.Curses;

public sealed class Prejudice : PerfectCurseCard, ICardTypeRestriction
{
    public CardType BlockedCardType => CardType.Skill;
}
