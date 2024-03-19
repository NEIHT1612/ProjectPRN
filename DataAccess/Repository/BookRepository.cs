using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        public void DeleteBook(int BookID) => BookDAO.Instance.Delete(BookID);

        public Book GetBookByID(int BookID) => BookDAO.Instance.GetBookByID(BookID);

        public IEnumerable<Book> GetBooks() => BookDAO.Instance.GetBookList();

        public List<Book> GetBooksByCategory(int categoryId) => BookDAO.Instance.GetBooksByCategory(categoryId);

        public List<Book> GetBooksByName(string searchBook) => BookDAO.Instance.GetBooksByName(searchBook);

        public List<Book> GetBooksByNameAndPrice(string searchBook, decimal minPrice, decimal maxPrice) => BookDAO.Instance.GetBooksByNameAndPrice(searchBook, minPrice, maxPrice);

        public List<Book> GetBooksByPrice(decimal minPrice, decimal maxPrice) => BookDAO.Instance.GetBooksByPrice(minPrice, maxPrice);

        public void InsertBook(Book book) => BookDAO.Instance.AddNew(book);

        public void UpdateBook(Book book) => BookDAO.Instance.Update(book);
    }
}
