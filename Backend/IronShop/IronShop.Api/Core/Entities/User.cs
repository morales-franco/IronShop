﻿using Ardalis.GuardClauses;
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

        public User()
        {

        }

        //Automapper call this method
        public User(string fullName, string email, string password, string role)
        {
            Guard.Against.NullOrEmpty(fullName, nameof(fullName));
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(password, nameof(password));
            Guard.Against.NullOrEmpty(role, nameof(role));

            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
        }

        public User(int userId, string fullName, string email, string role)
        {
            Guard.Against.NullOrEmpty(fullName, nameof(fullName));
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(role, nameof(role));

            UserId = userId;
            FullName = fullName;
            Email = email;
            Role = role;
        }

        public User(int userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public void ChangeRole(string role)
        {
            Role = role;
        }

        public void ChangePassword(string newPassword)
        {
            if (Password == newPassword)
                throw new ValidationException("New password has already been used");

            Password = newPassword;
        }

        public void Modify(string fullName, string email, string role)
        {
            FullName = fullName;
            Email = email;
            Role = role;
        }

        public void ValidateChangeEmail(string email)
        {
            if (Email.ToLower() != email.ToLower())
                throw new ValidationException("You can't change email address");
        }
    }
}
