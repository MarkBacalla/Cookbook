using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cookbook.Data.Models;

namespace Cookbook.Data.Repository
{
    public class UserRepository : IDataRepository<User, User>
    {
        private readonly CookbookContext _cookbookContext;

        public UserRepository(CookbookContext cookbookContext)
        {
            _cookbookContext = cookbookContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _cookbookContext.User.ToList();
        }

        public User Get(long id)
        {
            return _cookbookContext.User.SingleOrDefault(u => u.Id == id);
        }

//        public User Get(string username, string password)
//        {
//            return _cookbookContext.User.SingleOrDefault(u => u.UserName == username && u.Password == password);
//        }

        public User GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public void Add(User entity)
        {
            _cookbookContext.User.Add(entity);
            _cookbookContext.SaveChanges();
        }

        public void Update(User entityToUpdate, User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            _cookbookContext.Remove(entity);
            _cookbookContext.SaveChanges();
        }
    }
}
