using Ardalis.GuardClauses;
using IronShop.Api.Core.Common;
using IronShop.Api.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class User
    {
        public int UserId { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Role { get; private set; }

        public string ImageFileName { get; private set; }

        public bool GoogleAuth { get; private set; }

        public User()
        {

        }

        //Automapper call this method
        public User(string fullName, string email, string password, string role = null, bool googleUser = false)
        {
            Guard.Against.NullOrEmpty(fullName, nameof(fullName));
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(password, nameof(password));

            FullName = fullName;
            Email = email;
            Password = password;
            Role = role?? AuthConstants.Role_User;
            GoogleAuth = googleUser;
        }

        public User(int userId, string fullName, string email, string role= null)
        {
            Guard.Against.NullOrEmpty(fullName, nameof(fullName));
            Guard.Against.NullOrEmpty(email, nameof(email));

            UserId = userId;
            FullName = fullName;
            Email = email;
            Role = role ?? AuthConstants.Role_User;
        }

        public User(int userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void ChangeRole(string role)
        {
            Role = role;
        }

        public void EncryptPassword()
        {
            Password = HashHelper.Create(Password);
        }

        public void ChangePassword(string newPassword)
        {
            var hashPassword = HashHelper.Create(newPassword);

            if (Password == hashPassword)
                throw new ValidationException("New password has already been used");

            Password = hashPassword;
        }

        public void Modify(string fullName, string email, string role)
        {
            FullName = fullName;
            Email = email;
            Role = role;
        }

        internal bool IsMyPassword(string password)
        {
            return Password == HashHelper.Create(password);
        }

        internal void SetProfilePicture(string fileName)
        {
            ImageFileName = fileName;
        }

        internal  bool IsGoogleUser()
        {
            return GoogleAuth;
        }
    }
}
