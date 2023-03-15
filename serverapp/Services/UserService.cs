using Microsoft.EntityFrameworkCore;
using serverapp.Helpers;

namespace serverapp
{
    public class UserService
    {
        private static readonly AppDBContext db = new AppDBContext();
        public static async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await db.Users.ToListAsync();
        }
        internal static async Task<User> GetUserByIdAsync(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public static async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;
            return user;
        }
        internal static Task<User> GetUserByEmailAndPasswordAsync(AuthentificationModel auth)
        {
            var user =  db.Users.FirstOrDefaultAsync(u => u.Email == auth.Email && PasswordHasher.HashPassword(auth.Password) == u.Password);
            if (user == null)
                return null;
            return user;
        }
        //
        public static async Task<string> CreateUserAsync(User user)
        {
            try
            {
                if (UserVerification.EmailValidation( user.Email) == false)
                    return "Email is not valid";
                if (UserVerification.PasswordValidation( user.Password) == false)
                    return "Password is not valid";
                if (await UserVerification.CheckUserEmailExistAsync(db, user.Email))
                    return "Email already in use";
                if (await UserVerification.CheckUserCinExistAsync(db, user.Cin))
                    return "Cin already in use";
                user.Type = "user";
                user.Password = PasswordHasher.HashPassword(user.Password);
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return "Created successfully";
            }
            catch
            {
                return "Failed to create";
            }
        }
        //
        public static async Task<string> CreateAdminAsync(User user)
        {
            try
            {
                if (await UserVerification.CheckUserEmailExistAsync(db, user.Email))
                    return "Email already in use";
                if (await UserVerification.CheckUserCinExistAsync(db, user.Email))
                    return "Cin already in use";
                user.Type = "admin";
                user.Password = PasswordHasher.HashPassword(user.Password);
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return "Admin created successfully";
            }
            catch
            {
                return "Failed to create admin";
            }
        }
        internal static async Task<bool> UpdatePasswordAsync(UpdatePasswordModel pass)
        {
            //To be
            try
            {
                var requests = db.Users.Where(r => r.Id == pass.Id);
                //Check fo previous password
                
                foreach(var request in requests)
                    if (request.Password != PasswordHasher.HashPassword(pass.OldPassword))
                        return false;
                foreach (var request in requests)
                    request.Password = PasswordHasher.HashPassword(pass.NewPassword);
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var requests = db.Users.Where(r => r.Id == user.Id);
                foreach (var request in requests)
                {
                    if (request.Name != user.Name)
                    {
                        request.Name = user.Name;
                    }
                    
                    if(request.Email != user.Email)
                    {
                        // Check if email already exists
                        var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                        if (existingUser != null)
                        {
                            // Update the email of the existing user with the email of the user being updated
                            existingUser.Email = request.Email;
                        }

                        request.Email = user.Email;
                    }
                    
                    //request.Cin = user.Cin;
                }
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return false;
                }
                db.Users.Remove(user);
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
