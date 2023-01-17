using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;


namespace serverapp.Services
{
    internal static class UsersRepository
    {
        internal async static Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Users.ToListAsync();
            }
        }
        internal async static Task<User> GetUserByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
        }
        internal async static Task<User> GetUserByEmailAsync(string email)
        {
            using (var db = new AppDBContext())
            {
                return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
        }
        internal async static Task<bool> CreateUserAsync(User user)
        {
            user.Type = "user";
            using (var db = new AppDBContext())
            {
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
        }
        internal async static Task<bool> CreateAdminAsync(User user)
        {
            using (var db = new AppDBContext())
            {
                user.Type = "admin";
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
        }
        internal async static Task<bool> UpdateUserAsync(User user)
        {
            using (var db = new AppDBContext())
            {
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
        }
        internal async static Task<bool> DeleteUserAsync(int id)
        {
            using (var db = new AppDBContext())
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
}
