using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Delete(User user)
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _unitOfWork.Users.GetByEmail(email);
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task Insert(User user)
        {
            _unitOfWork.Users.Add(user);
             await _unitOfWork.Commit();
        }

        public async Task Update(User user)
        {
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
