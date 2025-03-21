﻿using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        string Register(UserModel user); // ✅ Return string (JWT token or success message)
        string Login(string email, string password);

        UserModel GetUserByEmail(string email);
        string GeneratePasswordResetToken(string email);
        string ValidateResetToken(string token);
        bool ResetPassword(string email, string newPassword);


    }
}
