using PotterKata.Services;
using Shouldly;
using Xunit;

namespace PotterKata.Tests.Services
{
    public class GivenPotterDiscountService
    {
        private readonly PotterDiscountService _potterDiscountService = new();

        [Fact]
        public void ItCharges_Eight_PerBook()
        {
            var numberOfDiscounts = _potterDiscountService.GetIndividualBookPrice();
            numberOfDiscounts.ShouldBe(8);
        }

        [Fact]
        public void ItContains_Five_Discounts()
        {
            var numberOfDiscounts = _potterDiscountService.GetNumberOfDiscountableBooks();
            numberOfDiscounts.ShouldBe(5);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0.95)]
        [InlineData(3, 0.9)]
        [InlineData(4, 0.8)]
        [InlineData(5, 0.75)]
        public void ItProvidesExpectedDiscounts(int numberOfUniqueBooks, decimal expectedDiscount)
        {
            var numberOfDiscounts = _potterDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(6, 0.75)]
        [InlineData(600, 0.75)]
        public void ItReturnsTheMaximumDiscount_WhenGivenTooLargeNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _potterDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100, 0)]
        public void ItReturnsTheMinimumDiscount_WhenGivenTooSmallNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _potterDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }
    }
}