using PotterKata.Services;
using Shouldly;
using Xunit;

namespace PotterKata.Tests.Services
{
    public class GivenOreillyDiscountService
    {
        private readonly OreillyDiscountService _oreillyDiscountService = new();

        [Fact]
        public void ItCharges_Ten_PerBook()
        {
            var numberOfDiscounts = _oreillyDiscountService.GetIndividualBookPrice();
            numberOfDiscounts.ShouldBe(10);
        }

        [Fact]
        public void ItContains_Two_Discounts()
        {
            var numberOfDiscounts = _oreillyDiscountService.GetNumberOfDiscountableBooks();
            numberOfDiscounts.ShouldBe(2);
        }

        [Theory]
        [InlineData(1, 0.5)]
        [InlineData(2, 0.40)]
        public void ItProvidesExpectedDiscounts(int numberOfUniqueBooks, decimal expectedDiscount)
        {
            var numberOfDiscounts = _oreillyDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(3, 0.40)]
        [InlineData(600, 0.40)]
        public void ItReturnsTheMaximumDiscount_WhenGivenTooLargeNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _oreillyDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100, 0)]
        public void ItReturnsTheMinimumDiscount_WhenGivenTooSmallNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _oreillyDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }
    }
}