using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;

namespace RestWithAspNet5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string userName);


        User RefreshUserInfo(User user);

        bool RevokeToken(string userName);
    }
}
