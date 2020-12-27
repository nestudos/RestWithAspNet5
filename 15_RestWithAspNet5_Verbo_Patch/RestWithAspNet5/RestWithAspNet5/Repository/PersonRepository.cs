﻿using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Repository.Generic;
using System;
using System.Linq;

namespace RestWithAspNet5.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base(context)
        {

        }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id == id))
            {
                return null;
            }

            var user = _context.Persons.SingleOrDefault(p => p.Id == id);

            user.Enabled = false;

            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
