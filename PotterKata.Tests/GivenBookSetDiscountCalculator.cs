using PotterKata.Services;
using Shouldly;
using Xunit;

namespace PotterKata.Tests
{
    public class GivenBookSetDiscountCalculator
    {
        [Theory]
        [InlineData(new int[0], 0)] // (0 * 8)
        public void ItReturnsZero_WhenBasketIsEmpty(int[] basket,
            decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1}, 8)] // (1 * 8)
        [InlineData(new[] {2}, 8)] // (1 * 8)
        [InlineData(new[] {3}, 8)] // (1 * 8)
        [InlineData(new[] {4}, 8)] // (1 * 8)
        [InlineData(new[] {5}, 8)] // (1 * 8)
        public void ItCalculates_IndividualBookPrices(int[] basket, decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1, 1}, 16)] // (2 * 8)
        [InlineData(new[] {1, 1, 1}, 24)] // (3 * 8)
        [InlineData(new[] {2, 2, 2, 2}, 32)] // (4 * 8)
        [InlineData(new[] {3, 3}, 16)] // (2 * 8) 
        [InlineData(new[] {4, 4, 4}, 24)] // (3 * 8)
        [InlineData(new[] {5, 5, 5, 5, 5}, 40)] // (5 * 8)
        [InlineData(new[] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5}, 80)] // (5 * 8) + (5 * 8)
        public void ItCalculatesDiscounts_WhenCollectionsContainASingleBook(int[] basket, decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1, 2}, 15.20)] // ((2 * 8) * 0.95)
        [InlineData(new[] {2, 5}, 15.20)] // ((3 * 8) * 0.9)
        [InlineData(new[] {1, 2, 3}, 21.60)] // ((3 * 8) * 0.9)
        [InlineData(new[] {1, 2, 4}, 21.60)] // ((3 * 8) * 0.9)
        [InlineData(new[] {1, 2, 3, 4}, 25.60)] // ((4 * 8) * 0.8)
        [InlineData(new[] {1, 2, 3, 4, 5}, 30)] // ((5 * 8) * 0.75)
        public void ItCalculatesDiscounts_WhenCollectionsContainNoDuplicateBooks(int[] basket,
            decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1, 1, 2}, 23.20)] // [1, 2], [1] = ((2 * 8) * 0.95) + 8)
        [InlineData(new[] {2, 2, 2, 3}, 31.20)] // [2, 3], [2], [2] = ((2 * 8) * 0.95) + 8 + 8)
        [InlineData(new[] {1, 1, 2, 2, 3, 3}, 43.20)] // [1, 2, 3], [1, 2, 3] = ((3 * 8) * 0.9) + (3 * 8) * 0.9))
        [InlineData(new[] {1, 1, 2, 2, 3, 3, 4, 4, 5, 5},
            60)] // [1, 2, 3, 4, 5], [1, 2, 3, 4, 5] = ((5 * 8) * 0.75) + (5 * 8) * 0.75))
        public void ItCalculatesDiscounts_WhenCollectionsContainDuplicateBooks(int[] basket, decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1, 1, 2, 3}, 29.60)] // [1, 2, 3], [1] = ((3 * 8 * 0.90) + 8)
        [InlineData(new[] {1, 2, 2, 3}, 29.60)] // [1, 2, 3], [2] = ((3 * 8 * 0.90) + 8)
        [InlineData(new[] {4, 1, 4, 2}, 29.60)] // [1, 2, 4], [4] = ((3 * 8 * 0.90) + 8)
        [InlineData(new[] {1, 1, 2, 2, 3, 3, 4, 5},
            51.20)] // [1, 2, 3, 4], [1, 2, 3, 5] = (4 * 8 * 0.8) + (4 * 8 * 0.8)
        [InlineData(new[] {5, 3, 3, 2, 1, 2, 1, 4},
            51.20)] // [1, 2, 3, 4], [1, 2, 3, 5] = (4 * 8 * 0.8) + (4 * 8 * 0.8)
        [InlineData(new[] {1, 1, 2, 2, 3, 3, 4, 5, 1, 2, 3},
            72.80)] // [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 3] = (4 * 8 * 0.8) + (4 * 8 * 0.8) + (3 * 8 * 0.90)
        [InlineData(new[] {1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 5},
            72.80)] // [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 3] = (4 * 8 * 0.8) + (4 * 8 * 0.8) + (3 * 8 * 0.90)
        [InlineData(new[] {1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 5},
            80.80)] // [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 3], [1] = (4 * 8 * 0.8) + (4 * 8 * 0.8) + (3 * 8 * 0.90) + 8
        [InlineData(new[] {1, 1, 2, 2, 3, 3, 4, 5, 1, 2, 3, 1},
            80.80)] // [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 3] = (4 * 8 * 0.8) + (4 * 8 * 0.8) + (3 * 8 * 0.90)
        public void ItCalculatesCheapestDiscounts_WhenCollectionsCanBeSortedWithNumerousPermutations(int[] basket,
            decimal expectedCost)
        {
            var discountCalculator = new BookSetDiscountCalculator(new PotterDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1}, 6)] // (0.5 * 12)
        [InlineData(new[] {1, 2}, 10.8)] // (0.45 * 24)
        [InlineData(new[] {1, 2, 3, 4, 5}, 18)] // (0.3 * 60)
        [InlineData(new[] {1, 2, 3, 3, 4, 5}, 24)] // (0.3 * 60) + (0.5 * 12)
        [InlineData(new[] {1, 2, 3, 3, 4, 5, 5}, 28.80)] // (0.3 * 60) + (0.45 * 24)
        public void ItCalculates_WithCustomDiscounts(int[] basket, decimal expectedCost)
        {
            // LOTR books cost £12 each, unlike Potter books which are £8 each
            // LOTR books start at 50% off with an extra 5% for every extra book that forms a set

            var discountCalculator = new BookSetDiscountCalculator(new LotrDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }

        [Theory]
        [InlineData(new[] {1, 2, 3}, 18)] // (0.4 * 20) + 10
        [InlineData(new[] {1, 200, 300}, 18)] // (0.4 * 20) + 10
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 48)] // (0.4 * 20) + 40
        public void ItCalculatesDiscounts_WhenBookSetCanBeLargerThanAvailableDiscounts(int[] basket, decimal expectedCost)
        {
            // The O'Reilly discount service provides a discount of up to 2 books per unique book set.
            // These scenarios result in unique sets containing > 2 books.
            // It should apply the max possible discount at all times to only the first 2 books then charge normal for the rest.

            var discountCalculator = new BookSetDiscountCalculator(new OreillyDiscountService());
            var cost = discountCalculator.CalculateCheapestBasketPermutation(basket);

            cost.ShouldBe(expectedCost);
        }
    }
}