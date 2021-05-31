using System.Collections.Generic;

namespace PotterKata.Services
{
    public abstract class DiscountService : IDiscountService
    {
        private readonly decimal _bookPrice;

        private readonly Dictionary<int, decimal> _discounts;
        
        protected DiscountService(decimal bookPrice, Dictionary<int, decimal> discounts)
        {
            _bookPrice = bookPrice;
            _discounts = discounts;
        }

        public int GetNumberOfDiscountableBooks() => _discounts.Count;
        
        public decimal GetIndividualBookPrice() => _bookPrice;

        public decimal ReturnDiscountPercentage(int numberOfBooks)
        {
            if (numberOfBooks < 1) return 0;
            if (numberOfBooks > _discounts.Count) return _discounts[_discounts.Count];
            
            return _discounts[numberOfBooks];
        }
    }
}