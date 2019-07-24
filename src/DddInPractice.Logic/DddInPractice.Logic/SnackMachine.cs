using System;
using System.Linq;

namespace DddInPractice.Logic
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }

        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = Money.None;
        }

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };

            if (!coinsAndNotes.Contains(money)) {
                throw new InvalidOperationException();
            }
            MoneyInTransaction = MoneyInTransaction + money;
        }

        public void ReturnMoney() {
            //we don't manipulate money in here, keep it immutable
            MoneyInTransaction = Money.None;
        }

        public void BuySnack() {
            MoneyInside = MoneyInside + MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }
    }
}
