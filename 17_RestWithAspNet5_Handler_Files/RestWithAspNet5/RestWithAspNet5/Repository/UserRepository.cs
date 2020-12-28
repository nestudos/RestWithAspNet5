using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNet5.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;


        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public User RefreshUserInfo(User user)
        {

            if (!_context.Users.Any(u => u.Id == user.Id)) return null;
            

            var result = _context.Users.SingleOrDefault(p => p.Id == user.Id);

            if (result == null)
            {
                return null;
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);

            if (user == null) return false;

            user.RefreshToken = null;

            _context.SaveChanges();

            return true;
        }
    }
}
