using System.Collections.Generic;
using System.Linq;

namespace PotterKata
{
    public class BookSet
    {
        public List<int> Books { get; private set; }

        public BookSet(List<int> books)
        {
            Books = books;
        }
        
        public BookSet DeepClone()
        {
            var clonedBookSet = (BookSet) MemberwiseClone();
            clonedBookSet.Books = Books.ToArray().ToList();

            return clonedBookSet;
        }
    }
}