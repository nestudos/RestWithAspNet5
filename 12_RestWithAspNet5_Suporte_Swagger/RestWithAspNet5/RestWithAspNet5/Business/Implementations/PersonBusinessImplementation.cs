using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Data.Converter.Implementations;

namespace RestWithAspNet5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();

        }

        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            try
            {
                var personEntity = _converter.Parse(person);

                personEntity =  _repository.Create(personEntity);
                return _converter.Parse(personEntity);

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

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);

            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
                
    }
}
