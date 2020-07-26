using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Service.Common;
using Stories.Repository.Common;
using Stories.Model;

namespace Stories.Service
{
    public class UserService : IUserService
    {
        protected IUserRepository UserRepository { get; private set; }

        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<bool> PostUserAsync(UserModel userModel)
        {
            return await UserRepository.PostUserAsync(userModel);
        }
    }
}
