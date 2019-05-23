using Google.Apis.Auth;
using IronShop.Api.Core.Common;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IFileService _fileService;

        public UserService(IUnitOfWork unitOfWork,
                           IHttpContextAccessor context,
                           IConfiguration configuration,
                           ITokenService tokenService,
                           IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _httpContext = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _fileService = fileService;
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

            if(userBd.Email != user.Email.Trim())
                if (!(await IsEmailUnique(user.Email)))
                    throw new ValidationException("Email address is already registered");

            userBd.Modify(user.FullName, user.Email, (eRole)user.RoleId);

            _unitOfWork.Users.Update(userBd);
            await _unitOfWork.Commit();
        }

        public async Task UpdateProfile(User user)
        {
            var userBd = await _unitOfWork.Users.GetById(user.UserId);

            if (userBd.Email != user.Email.Trim())
            {
                if (userBd.GoogleAuth)
                    throw new ValidationException("You can not modify google email");

                if (!(await IsEmailUnique(user.Email)))
                    throw new ValidationException("Email address is already registered");
            }

            userBd.Modify(user.FullName, user.Email, (eRole)user.RoleId);

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

        public async Task<IronToken> Login(User user)
        {
            var userBd = await _unitOfWork.Users.GetByEmail(user.Email);

            if (userBd == null)
                throw new ValidationException("Invalid Credentials");

            if (userBd.IsGoogleUser())
                throw new ValidationException("User was created with Auth Google. Use google login");

            if (!userBd.IsMyPassword(user.Password))
                throw new ValidationException("Invalid Credentials");


            return _tokenService.Generate(userBd);
        }

        public async Task<IronToken> Login(UserGoogle user)
        {
            var googlePublicKey = _configuration["Google:PublicKey"];
            var userValidPayload = await GoogleJsonWebSignature.ValidateAsync(user.TokenGoogle, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new string[] { googlePublicKey }
            });

            var userBd = await _unitOfWork.Users.GetByEmail(userValidPayload.Email);

            if(userBd != null)
            {
                if(!userBd.GoogleAuth)
                    throw new ValidationException($"User { userValidPayload.Email } was created with default authentication. Use email and Password");
            }
            else
            {
                var profilePicture = await _fileService.DownloadAndSaveFromUrl(userValidPayload.Picture);
                userBd = new User(userValidPayload.Name, userValidPayload.Email, AuthConstants.UserGoogle_FakePassword, eRole.Employee, true);

                if (profilePicture.Success)
                    userBd.SetProfilePicture(profilePicture.FileName);

                await Register(userBd);
            }

            return _tokenService.Generate(userBd);
        }

        public async Task<User> GetCurrentUser()
        {
            var currentUser = _httpContext.HttpContext.User;
            var userId = currentUser.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var user = await GetById(Convert.ToInt32(userId));
            return user;

        }

        public async Task UploadImage(int id, IFormFile file)
        {
            var userBd = await _unitOfWork.Users.GetById(id);
            var image = await  _fileService.DownloadAndSaveFromForm(file);

            if (!image.Success)
                throw new ValidationException("Error when try to update Profile Picture");

            if(userBd.ImageFileName != null)
                 _fileService.DeleteFile(userBd.ImageFileName);

            userBd.SetProfilePicture(image.FileName);

            _unitOfWork.Users.Update(userBd);
            await _unitOfWork.Commit();
        }
    }
}
