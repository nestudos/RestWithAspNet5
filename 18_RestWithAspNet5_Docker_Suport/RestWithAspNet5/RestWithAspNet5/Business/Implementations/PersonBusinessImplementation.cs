using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Data.Converter.Implementations;
using RestWithAspNet5.Hypermedia.Utils;

namespace RestWithAspNet5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
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

        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disable(id);

            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {

            var sort = (!string.IsNullOrEmpty(sortDirection) && sortDirection != "desc") ? "asc" : "desc";

            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * pageSize : 0;

            string query = @"select * from person p where 1 = 1";

            if (!string.IsNullOrEmpty(name)) query += $" and p.first_name like '%{name}%' ";

            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            var countQuery = $"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery += $" and p.first_name like '%{name}%' ";

            var persons = _repository.FindWithPagedSearched(query);
            int totalResults = _repository.GetCount(countQuery);


            return new PagedSearchVO<PersonVO>() 
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
