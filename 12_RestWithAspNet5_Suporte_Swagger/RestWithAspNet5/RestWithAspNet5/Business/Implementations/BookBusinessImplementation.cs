using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Data.Converter.Implementations;

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

    }
}
