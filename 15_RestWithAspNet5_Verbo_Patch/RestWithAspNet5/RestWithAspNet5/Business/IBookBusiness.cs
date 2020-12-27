
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindById(long id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        void Delete(long id);


    }
}
