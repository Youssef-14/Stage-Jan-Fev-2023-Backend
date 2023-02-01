using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;


namespace serverapp.Services
{
    public class UserService
    {
        private  readonly AppDBContext db;
        public UserService(AppDBContext db)
        {
            this.db = db;
        }
        internal async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await db.Users.ToListAsync();
        }
        internal async Task<User> GetUserByIdAsync(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        internal async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        internal async Task<User> GetUserByEmailAndPasswordAsync(string email,string password)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email && PasswordHasher.HashPassword(password) == u.Password);
            if (user == null)
            {
                return null;
            }
            return user;

        }
        public async Task<bool> CreateUserAsync(User user)
        {
            user.Type = "user";
            user.Password = PasswordHasher.HashPassword(user.Password);
            try
            {
                await db.Users.AddAsync(user);
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal async Task<bool> CreateAdminAsync(User user)
        {
            user.Type = "admin";
            if (await CheckUserEmailExistAsync(user.Email))
            {
                return false;
            }
            await db.Users.AddAsync(user);
            return await db.SaveChangesAsync() >= 1;

        }
        internal async Task<bool> UpdateUserAsync(User user)
        {
            user.Type = "user";
            try
            {
                db.Users.Update(user);
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal async Task<bool> UpdateAdminAsync(User user)
        {
            user.Type = "admin";
            try
            {
                db.Users.Update(user);
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal async Task<bool> DeleteUserAsync(int id)
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
        //Service of service
        private Task<bool> CheckUserEmailExistAsync(string email)
        {
            return db.Users.AnyAsync(u => u.Email == email);
        }
        private Task<bool> CheckUserCinExistAsync(string cin)
        {
            return db.Users.AnyAsync(u => u.Cin == cin);
        }
    }
}
