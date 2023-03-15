using Microsoft.EntityFrameworkCore;

namespace serverapp.Helpers
{
    internal class UserVerification
    {
        internal static Task<bool> CheckUserEmailExistAsync(AppDBContext db,string email)
        {
            return db.Users.AnyAsync(u => u.Email == email);
        }
        internal static Task<bool> CheckUserCinExistAsync(AppDBContext db, string cin)
        {
            return db.Users.AnyAsync(u => u.Cin == cin);
        }
        internal static bool EmailValidation(string email)
        {
            if (email == null)
                return false;
            if (email.Length < 5)
                return false;
            if (email.Length > 50)
                return false;
            if (!email.Contains("@"))
                return false;
            if (!email.Contains("."))
                return false;
            if (email.Contains(" "))
                return false;
            if ('0' <= email[0] && email[0] <= '9')
                return false;
            return true;
        }
        internal static bool PasswordValidation(string password)
        {
            if (password == null)
                return false;
            if (password.Length < 8)
                return false;
            if (password.Length > 50)
                return false;
            //Don't contains number
            if (!password.Any(char.IsDigit))
                return false;
            //Dont contains letter
            if (!password.Any(char.IsLetter))
                return false;
            //Dont contains special character
            /*if (!password.Any(char.IsPunctuation))
                return false;*/
            return true;
        }
        internal static bool CinValidation(string cin)
        {
            if (cin == null)
                return false;
            if (cin.Length != 8)
                return false;
            //Don't contains number
            if (!cin.All(char.IsDigit))
                return false;
            return true;
        }
    }
}
