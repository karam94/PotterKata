namespace PotterKata.Services
{
    public interface IDiscountService
    {
        public int GetNumberOfDiscountableBooks();
        public decimal GetIndividualBookPrice();
        public decimal ReturnDiscountPercentage(int numberOfBooks);
    }
}