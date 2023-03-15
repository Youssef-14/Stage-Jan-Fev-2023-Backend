using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serverapp;
using Xunit;

namespace AspWebApp.Tests
{
    public class UserServiceTests
    {
        private readonly MockedAppDBContext _context;

        public UserServiceTests()
        {
            _context = new MockedAppDBContext();
        }

        [Fact]
        public async Task Add_Get_User_Test()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Cin = "12345227", Email = "user4@example.com", Type = "admin" },
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act

            var result = await UserService.GetUserByEmailAsync(users[0].Email);

            // Assert
            Assert.Equal(users[0].Email, result.Email);
        }
        [Fact]
        public async Task Add_Get_Demande_Test()
        {
            // Arrange
            var demandes = new List<Demande>
            {
                new Demande { Status = "accepté", UserId = 1 },
            };

            await _context.Demandes.AddRangeAsync(demandes);
            int id = await _context.SaveChangesAsync();

            // Act

            var result = await DemandeService.GetDemandeByIdAsync(demandes[0].Id);

            // Assert
            Assert.Equal(id, result.Id);
        }
    }
}
