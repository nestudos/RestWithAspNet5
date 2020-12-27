using RestWithAspNet5.Model;

namespace RestWithAspNet5.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
