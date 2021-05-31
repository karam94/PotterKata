using System.Collections.Generic;

namespace PotterKata.Services
{
    public class PotterDiscountService : DiscountService
    {
        private const decimal BookPrice = 8;

        private static readonly Dictionary<int, decimal> Discounts = new()
        {
            {1, 1m},
            {2, 0.95m},
            {3, 0.9m},
            {4, 0.8m},
            {5, 0.75m}
        };

        public PotterDiscountService() : base(BookPrice, Discounts)
        {
        }
    }
}