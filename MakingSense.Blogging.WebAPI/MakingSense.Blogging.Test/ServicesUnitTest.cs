using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MakingSense.Blogging.WebAPI.Services;

using Xunit;
using MakingSense.Blogging.WebAPI.Entities;
using System.Linq;

namespace MakingSense.Blogging.Test
{
    public class ServicesUnitTest
    {
        [Fact]
        public void TestLoginOK()
        {
            // Arrange
            var dataContext = DbContextMocker.GetDataContext(nameof(TestLoginOK));
            var service = new UserService(dataContext);

            // Act
            var user = service.Authenticate("frossi", "123456");

            dataContext.Dispose();

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void TestLoginWrongPassword()
        {
            // Arrange
            var dataContext = DbContextMocker.GetDataContext(nameof(TestLoginWrongPassword));
            var service = new UserService(dataContext);

            // Act
            var user = service.Authenticate("frossi", "123");

            dataContext.Dispose();

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void TestFilterPostAnonymouns()
        {
            // Arrange
            var dataContext = DbContextMocker.GetDataContext(nameof(TestFilterPostAnonymouns));
            var service = new PostService(dataContext);


            // Act
            var posts = service.Filter("publico").ToList();

            dataContext.Dispose();

            // Assert
            Assert.Equal(2, posts.Count);

        }

    }
}
