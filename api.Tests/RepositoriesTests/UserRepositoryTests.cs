using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Xunit;
using System.Threading.Tasks;
using api.Repositories;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Data;

namespace api.Tests.RepositoriesTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task FindById_ReturnsUser_WhenUserExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDBContext(options);
            context.Users.Add(new User { Id = 1, Name = "Alice", Surname= "Nováková" });
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            var user = await repository.FindById(1);

            Assert.NotNull(user);
            Assert.Equal("Alice", user.Name);
        }


    }
}
