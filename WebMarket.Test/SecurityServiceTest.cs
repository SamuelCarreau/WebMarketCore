using Xunit;
using NSubstitute;
using WebMarket.Data.Repositories.Security;
using WebMarket.Services;
using WebMarket.Models.Security;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WebMarket.Test
{
    public class SecurityServiceTest
    {
        private Services.SecurityService _sut;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public SecurityServiceTest()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _roleRepository = Substitute.For<IRoleRepository>();
            _sut = new SecurityService(_userRepository, _roleRepository);
        }

        [Fact]
        public void GetUser_Everytime_valid()
        {
            // Mock

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "John_Doe",
                Email = "john.doe@gmail.com"
            };

            user.Roles = new List<Role>();
            user.Roles.Add(new Role()
            {
                Id = Guid.NewGuid(),
                Name = "admin"
            });

            _userRepository.GetUser(Arg.Any<Guid>()).Returns(user);

            // Execute

            var result = _sut.GetUser(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Roles);
            Assert.Same(user.Email, "john.doe@gmail.com");
            Assert.NotEqual(0, result.Roles.Count);
            Assert.Same("admin", result.Roles.First().Name);

        }

    }
}
