using DddInPractice.Logic;
using FluentAssertions;
using System;
using Xunit;

namespace DddInPractice.Tests
{
    public class Money_Tests
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result() {
            //Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            //Act
            Money sum = money1 + money2;

            //Assert.Equal(2, sum.OneCentCount);
            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_moneys_instance_equal_if_contain_the_same_money_amount() {
            //Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instance_not_equal_if_contain_different_amount() {
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -1, 0, 0, 0, 0)]
        [InlineData(0, 0, -1, 0, 0, 0)]
        [InlineData(0, 0, 0, -1, 0, 0)]
        [InlineData(0, 0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0, -1)]
        public void Can_not_create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quaterCount,
            int oneDollarCount,
            int fiveDollarCOunt,
            int twentyDollarCount)
        {
            Action action = () =>
            {
                new Money(
                    oneCentCount,
                    tenCentCount,
                    quaterCount,
                    oneDollarCount,
                    fiveDollarCOunt,
                    twentyDollarCount);
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount)
        {
           Money money = new Money(
                    oneCentCount,
                    tenCentCount,
                    quarterCount,
                    oneDollarCount,
                    fiveDollarCount,
                    twentyDollarCount);
            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Substraction_of_two_moneys_produce_correct_result() {
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Can_not_substract_more_than_excist() {
            Money money1 = new Money(0, 0, 1, 0, 0, 0);
            Money money2 = new Money(1, 0, 0, 0, 0, 0);

            Action action = () => {
                Money result = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
