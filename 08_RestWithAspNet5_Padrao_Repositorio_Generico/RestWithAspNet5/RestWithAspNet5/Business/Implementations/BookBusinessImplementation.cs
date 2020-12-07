using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RestWithAspNet5.Repository;

namespace RestWithAspNet5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;

        }

        public List<Book> FindAll()
        {

            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book book)
        {
            try
            {
                _repository.Create(book);
            }
            catch (Exception)
            {

                throw;
            }


            return book;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);



        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
                
    }
}
