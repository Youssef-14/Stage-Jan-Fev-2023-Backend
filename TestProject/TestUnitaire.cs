using System.Collections.Generic;
using System.Threading.Tasks;
using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;
using serverapp.Data;
using serverapp.Services;
using Xunit;

namespace AspWebApp.Tests
{
    public class UserServiceTests
    {
        private readonly AppDBContext _context;
        private readonly UserService _service;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDBContext(options);
            _service = new UserService(_context);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Cin = "123456", Email = "user1@example.com", Type = "admin" },
                new User { Id = 2, Cin = "654321", Email = "user2@example.com", Type = "user" },
                new User { Id = 3, Cin = "987654", Email = "user3@example.com", Type = "user" }
            };
            

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act

            var result = await _service.GetUsersAsync();

            // Assert
            Assert.Equal(users, result);
        }
    }
}
