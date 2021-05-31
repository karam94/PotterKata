using System.Collections.Generic;

namespace PotterKata.Services
{
    public class OreillyDiscountService : DiscountService
    {
        private const decimal BookPrice = 10;

        private static readonly Dictionary<int, decimal> Discounts = new()
        {
            {1, 0.5m},
            {2, 0.40m}
        };

        public OreillyDiscountService() : base(BookPrice, Discounts)
        {
        }
    }
}