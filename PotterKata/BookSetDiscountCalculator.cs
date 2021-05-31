using System.Collections.Generic;
using System.Linq;
using PotterKata.Services;

namespace PotterKata
{
    public class BookSetDiscountCalculator
    {
        private readonly BookSetCollection _cheapestBookSetCollection;

        public BookSetDiscountCalculator(IDiscountService discountService)
        {
            _cheapestBookSetCollection = new BookSetCollection(discountService);
        }

        public decimal CalculateCheapestBasketPermutation(int[] basket)
        {
            foreach (var bookToAdd in basket)
            {
                var bookSetsNotAlreadyContainingThisBook =
                    _cheapestBookSetCollection.GetBookSetsNotContaining(bookToAdd).ToList();

                var noExistingBookSetsNeedThisBook = !bookSetsNotAlreadyContainingThisBook.Any();

                if (noExistingBookSetsNeedThisBook)
                    _cheapestBookSetCollection.AddBookSet(new List<int> {bookToAdd});
                else
                    _cheapestBookSetCollection.AddBook(bookToAdd);
            }

            return _cheapestBookSetCollection.ReturnFinalCollectionPrice();
        }
    }
}