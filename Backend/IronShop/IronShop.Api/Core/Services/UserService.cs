using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUnitOfWork unitOfWork,
                           IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _httpContext = context;
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

        public async Task<PaginableList<User>> GetAll(PageParameters<User> parameters)
        {
            return await _unitOfWork.Users.GetPagedList(parameters);

        }

        public virtual async Task<IList<T>> GetList<T>(string storedProcedure, KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            return await _unitOfWork.Users.GetList<T>(storedProcedure, parameters);
        }


        public async Task<User> GetByEmail(string email)
        {
            return await _unitOfWork.Users.GetByEmail(email);
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task Register(User user)
        {
            user.EncryptPassword();

            // the User entity is only responsible for its own validity, not the validity of the set of others users.
            if (!(await IsEmailUnique(user.Email)))
                throw new ValidationException("Email address is already registered");

            _unitOfWork.Users.Add(user);
            await _unitOfWork.Commit();
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            var isUnique = true;
            var user = await _unitOfWork.Users.GetByEmail(email);

            if (user != null)
                isUnique = false;

            return isUnique;
        }

        public async Task Update(User user)
        {
            var userBd = await _unitOfWork.Users.GetById(user.UserId);

            userBd.ValidateChangeEmail(user.Email);
            userBd.Modify(user.FullName, user.Email, user.Role);

            _unitOfWork.Users.Update(userBd);
            await _unitOfWork.Commit();
        }

        public async Task ChangePassword(User user)
        {
            var userBd = await _unitOfWork.Users.GetById(user.UserId);
            userBd.ChangePassword(user.Password);

            _unitOfWork.Users.Update(userBd);
            await _unitOfWork.Commit();
        }

        public async Task<User> Login(User user)
        {
            var userBd = await _unitOfWork.Users.GetByEmail(user.Email);

            if (userBd == null)
                throw new ValidationException("Invalid Credentials");

            if(!userBd.IsMyPassword(user.Password))
                throw new ValidationException("Invalid Credentials");

            return userBd;
        }

        public async Task<User> GetCurrentUser()
        {
            var currentUser = _httpContext.HttpContext.User;
            var userId = currentUser.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var user = await GetById(Convert.ToInt32(userId));
            return user;

        }

        public async Task UploadImage(int id, string image)
        {
            var userBd = await _unitOfWork.Users.GetById(id);
            userBd.SetImage(image);

            _unitOfWork.Users.Update(userBd);
            await _unitOfWork.Commit();
        }
    }
}
