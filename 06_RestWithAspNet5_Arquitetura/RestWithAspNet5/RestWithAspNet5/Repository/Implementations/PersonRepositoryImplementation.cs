using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _context;

        public PersonRepositoryImplementation(MySqlContext context)
        {
            _context = context;

        }

        public List<Person> FindAll()
        {

            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (result == null)
            {
                return;
            }

            try
            {
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }



        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id))
            {
                return new Person();
            }

            var result = _context.Persons.SingleOrDefault(p => p.Id == person.Id);

            if (result == null)
            {
                return person;
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


            return person;
        }

        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }
    }
}
