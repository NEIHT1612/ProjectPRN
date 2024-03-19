using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookByID(int BookID);
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int BookID);
        List<Book> GetBooksByName(string searchBook);
        List<Book> GetBooksByPrice(decimal minPrice, decimal maxPrice);
        List<Book> GetBooksByNameAndPrice(string searchBook, decimal minPrice, decimal maxPrice);
        List<Book> GetBooksByCategory(int categoryId);
    }
}
