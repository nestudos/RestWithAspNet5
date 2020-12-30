using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Data.Converter.Implementations;
using RestWithAspNet5.Hypermedia.Utils;

namespace RestWithAspNet5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            try
            {
                var bookEntity = _converter.Parse(book);

                bookEntity = _repository.Create(bookEntity);
                return _converter.Parse(bookEntity);

            }
            catch (Exception)
            {
                throw;
            }


        }

        public void Delete(long id)
        {
            _repository.Delete(id);

        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);

            bookEntity = _repository.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrEmpty(sortDirection) && sortDirection != "desc") ? "asc" : "desc";

            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * pageSize : 0;

            string query = @"select * from book b where 1 = 1";

            if (!string.IsNullOrEmpty(title)) query += $" and b.title like '%{title}%' ";

            query += $" order by b.title {sort} limit {size} offset {offset}";

            var countQuery = $"select count(*) from book b where 1 = 1 ";
            if (!string.IsNullOrEmpty(title)) countQuery += $" and b.title like '%{title}%' ";

            var books = _repository.FindWithPagedSearched(query);
            int totalResults = _repository.GetCount(countQuery);


            return new PagedSearchVO<BookVO>()
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
