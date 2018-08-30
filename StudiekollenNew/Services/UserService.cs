using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;

namespace StudiekollenNew.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(RepositoryFactory repoFactory)
        {
            _userRepository = repoFactory.GetUserRepository();
        }

        public IEnumerable<User> All()
        {
            return _userRepository.All();
        }
    }
}