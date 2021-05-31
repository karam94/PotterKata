using PotterKata.Services;
using Shouldly;
using Xunit;

namespace PotterKata.Tests.Services
{
    public class GivenLotrDiscountService
    {
        private readonly LotrDiscountService _lotrDiscountService = new();

        [Fact]
        public void ItCharges_Twelve_PerBook()
        {
            var numberOfDiscounts = _lotrDiscountService.GetIndividualBookPrice();
            numberOfDiscounts.ShouldBe(12);
        }

        [Fact]
        public void ItContains_Five_Discounts()
        {
            var numberOfDiscounts = _lotrDiscountService.GetNumberOfDiscountableBooks();
            numberOfDiscounts.ShouldBe(5);
        }

        [Theory]
        [InlineData(1, 0.5)]
        [InlineData(2, 0.45)]
        [InlineData(3, 0.4)]
        [InlineData(4, 0.35)]
        [InlineData(5, 0.3)]
        public void ItProvidesExpectedDiscounts(int numberOfUniqueBooks, decimal expectedDiscount)
        {
            var numberOfDiscounts = _lotrDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(6, 0.3)]
        [InlineData(600, 0.3)]
        public void ItReturnsTheMaximumDiscount_WhenGivenTooLargeNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _lotrDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100, 0)]
        public void ItReturnsTheMinimumDiscount_WhenGivenTooSmallNumberOfBooks(int numberOfUniqueBooks,
            decimal expectedDiscount)
        {
            var numberOfDiscounts = _lotrDiscountService.ReturnDiscountPercentage(numberOfUniqueBooks);
            numberOfDiscounts.ShouldBe(expectedDiscount);
        }
    }
}