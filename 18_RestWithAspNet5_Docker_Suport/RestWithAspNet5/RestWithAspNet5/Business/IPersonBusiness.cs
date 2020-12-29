
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        PersonVO Update(PersonVO person);

        void Delete(long id);

        PersonVO Disabled(long id);

        List<PersonVO> FindByName(string firstName, string secondName);

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

    }
}
