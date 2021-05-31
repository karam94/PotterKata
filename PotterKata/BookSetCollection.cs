using System.Collections.Generic;
using System.Linq;
using PotterKata.Services;

namespace PotterKata
{
    public class BookSetCollection
    {
        private List<BookSet> _bookSets = new();
        private readonly IDiscountService _discountService;

        public BookSetCollection(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IEnumerable<BookSet> GetBookSetsNotContaining(int book) => _bookSets.Where(x => !x.Books.Contains(book));

        public void AddBookSet(List<int> booksToAdd) => _bookSets.Add(new BookSet(booksToAdd));

        public decimal ReturnFinalCollectionPrice() => _bookSets.Sum(CalculateBookSetPrice);

        // This isn't used but I've just left it here for now as reference for myself for the future
        // private IEnumerable<BookSet> GetBookSetsWhere(Func<BookSet, bool> condition) => BookSets.Where(condition);

        public void AddBook(int bookToAdd)
        {
            var bookSetCollectionPermutations = new List<BookSetCollection>();
            var bookSetsNotContainingThisBook = GetBookSetsNotContaining(bookToAdd).ToList();

            foreach (var availableBookSet in bookSetsNotContainingThisBook)
            {
                var availableBookSetWithBookAdded = availableBookSet.DeepClone();
                availableBookSetWithBookAdded.Books.Add(bookToAdd);

                var otherBookSetsNotContainingThisBook = bookSetsNotContainingThisBook.ToList();
                otherBookSetsNotContainingThisBook.Remove(availableBookSet);

                var bookSetPermutation =
                    new List<BookSet> {availableBookSetWithBookAdded};
                bookSetPermutation.AddRange(otherBookSetsNotContainingThisBook);

                // Create the final bookset permutation by adding the existing booksets
                // which already have an instance of the book we're adding
                var bookSetsThatAlreadyContainThisBook = _bookSets.Where(x => x.Books.Contains(bookToAdd));
                bookSetPermutation.AddRange(bookSetsThatAlreadyContainThisBook);

                bookSetCollectionPermutations.Add(new BookSetCollection(_discountService)
                {
                    _bookSets = bookSetPermutation
                });
            }

            if (bookSetCollectionPermutations.Count > 1)
                _bookSets = GetCheapestBookSetPermutation(bookSetCollectionPermutations);
            else
                bookSetsNotContainingThisBook.First().Books.Add(bookToAdd);
        }

        private List<BookSet> GetCheapestBookSetPermutation(
            IReadOnlyCollection<BookSetCollection> permutations)
        {
            var cheapestPermutation = permutations.First()._bookSets;
            var cheapestPermutationPrice = decimal.MaxValue;

            foreach (var permutation in permutations)
            {
                var price = permutation._bookSets.Sum(CalculateBookSetPrice);

                if (price < cheapestPermutationPrice)
                {
                    cheapestPermutationPrice = price;
                    cheapestPermutation = permutation._bookSets;
                }
            }

            return cheapestPermutation;
        }

        private decimal CalculateBookSetPrice(BookSet bookSet)
        {
            var numberOfBooksInBookSet = bookSet.Books.Count;
            var numberOfDiscountableBooks = _discountService.GetNumberOfDiscountableBooks();
            var individualBookPrice = _discountService.GetIndividualBookPrice();

            // Handles scenarios where a Book Set can contain more unique items, than max. number of discountable items.
            // e.g. Discount Service might provide x% off up to 2 items. However a book set could contain > 2 items.
            if (numberOfBooksInBookSet > numberOfDiscountableBooks)
            {
                var discountPercentage = _discountService.ReturnDiscountPercentage(numberOfDiscountableBooks);
                var priceForNumberOfBooksWeCanDiscount =
                    individualBookPrice * numberOfDiscountableBooks * discountPercentage;
                var priceForRemainingBooks = individualBookPrice * (numberOfBooksInBookSet - numberOfDiscountableBooks);

                return priceForNumberOfBooksWeCanDiscount + priceForRemainingBooks;
            }
            else
            {
                var discountPercentage = _discountService.ReturnDiscountPercentage(numberOfBooksInBookSet);

                return individualBookPrice * numberOfBooksInBookSet * discountPercentage;
            }
        }
    }
}