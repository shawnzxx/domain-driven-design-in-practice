using DddInPractice.Logic;
using FluentAssertions;
using System;
using Xunit;

namespace DddInPractice.Tests
{
    public class SnackMachine_Tests
    {
        [Fact]
        public void Return_money_should_empty_in_transaction() {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void Add_money_go_into_money_in_transaction() {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.TwentyDollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Quarter);
            snackMachine.InsertMoney(Money.Cent);

            snackMachine.MoneyInTransaction.Amount.Should().Be(21.26m);
        }

        [Fact]
        public void Can_not_insert_more_than_one_coin_or_note_at_same_time() {
            SnackMachine snackMachine = new SnackMachine();
            Money twoDollar = Money.Dollar + Money.Dollar;

            Action action = () => snackMachine.InsertMoney(twoDollar);
            action.Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void Money_in_transaction_goes_to_money_inside_after_purchase() {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.MoneyInTransaction.Amount.Should().Be(3m);
            snackMachine.BuySnack();
            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
            snackMachine.MoneyInTransaction.Should().Be(Money.None);

            snackMachine.MoneyInside.Amount.Should().Be(3m);
        }
    }
}
