using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookDAO
    {
        private static BookDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Book> GetBookList()
        {
            var books = new List<Book>();
            try
            {
                using var context = new BookStorePRNContext();
                books = context.Books.Include(x => x.Category).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }

        public Book GetBookByID(int BookID)
        {
            Book book = null;
            try
            {
                using var context = new BookStorePRNContext();
                book = context.Books.Include(x => x.Category).SingleOrDefault(b => b.BookId == BookID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return book;
        }

        public void AddNew(Book book)
        {
            try
            {
                Book _book = GetBookByID(book.BookId);
                if( _book == null)
                {
                    using var context = new BookStorePRNContext();
                    context.Books.Add(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The book is already exist");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Book book)
        {
            try
            {
                Book _book = GetBookByID(book.BookId);
                if( _book != null)
                {
                    using var context = new BookStorePRNContext();
                    context.Books.Update(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The book does not already exist");
                }
            }
            catch( Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int BookID)
        {
            try
            {
                Book book = GetBookByID(BookID);
                if( book != null )
                {
                    using var context = new BookStorePRNContext();
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The book does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Book> GetBooksByName(string searchBook)
        {
            var books = new List<Book>();
            try
            {
                using var context = new BookStorePRNContext();
                books = context.Books.Where(p => p.Title.Contains(searchBook)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }

        public List<Book> GetBooksByNameAndPrice(string searchBook, decimal minPrice, decimal maxPrice)
        {
            var books = new List<Book>();
            try
            {
                using var context = new BookStorePRNContext();
                books = context.Books.Where(p => (p.Price >= minPrice && p.Price <= maxPrice) && (p.Title.Contains(searchBook))).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }

        public List<Book> GetBooksByPrice(decimal minPrice, decimal maxPrice)
        {
            var books = new List<Book>();
            try
            {
                using var context = new BookStorePRNContext();
                books = context.Books.Where(p => (p.Price >= minPrice && p.Price <= maxPrice)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }

        public List<Book> GetBooksByCategory(int categoryId)
        {
            var books = new List<Book>();
            try
            {
                using var context = new BookStorePRNContext();
                books = context.Books.Where(p => p.CategoryId == categoryId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }
    }
}
