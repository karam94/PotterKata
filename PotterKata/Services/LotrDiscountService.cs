using System.Collections.Generic;

namespace PotterKata.Services
{
    public class LotrDiscountService : DiscountService
    {
        private const decimal BookPrice = 12;

        private static readonly Dictionary<int, decimal> Discounts = new()
        {
            {1, 0.5m},
            {2, 0.45m},
            {3, 0.4m},
            {4, 0.35m},
            {5, 0.3m}
        };

        public LotrDiscountService() : base(BookPrice, Discounts)
        {
        }
    }
}