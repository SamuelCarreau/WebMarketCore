using NUnit.Framework;
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

        [SetUp]
        public void Initialize()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _roleRepository = Substitute.For<IRoleRepository>();
            _sut = new SecurityService(_userRepository, _roleRepository);
        }

        #region UserTest    

        #region GetUser
        [Test]
        public void GetUser_WithData_valid()
        {
            // Mock

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "John_Doe",
                Email = "john.doe@gmail.com",
                Roles = new List<Role>
                {
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        Name = "admin"
                    }
                }
            };

            _userRepository.GetUser(Arg.Any<Guid>()).Returns(user);

            // Execute

            var result = _sut.GetUser(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Roles);
            Assert.AreSame(user.Email, "john.doe@gmail.com");
            Assert.AreNotEqual(0, result.Roles.Count);
            Assert.AreSame("admin", result.Roles.First().Name);
        }

        [Test]
        public void GetUser_WithoutData_valid()
        {
            // Mock
            _userRepository.GetUser(Arg.Any<Guid>()).Returns(x => null);

            // Execute
            var result = _sut.GetUser(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetUser_WithRolesNull_ThrowException()
        {
            // Mock
            var user = new User()
            {
                Id = Guid.NewGuid()
            };

            _userRepository.GetUser(Arg.Any<Guid>()).Returns(user);

            // Execute && Assert
            Assert.That(() => _sut.GetUser(Guid.NewGuid()),
                Throws.TypeOf<Exception>());

        }

        [Test]
        public void GetUser_WithRolesEmpty_ThrowException()
        {
            // Mock
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Roles = new List<Role>()
            };

            _userRepository.GetUser(Arg.Any<Guid>()).Returns(user);

            // Execute && Assert
            Assert.That(() => _sut.GetUser(Guid.NewGuid()),
                Throws.TypeOf<Exception>());

        }

        #endregion

        #region GetUsers

        [Test]
        public void GetUsers_WithData_valid()
        {
            // Mock
            var user1 = new User()
            {
                Id = Guid.NewGuid()
            };

            var user2 = new User()
            {
                Id = Guid.NewGuid()
            };

            var user3 = new User()
            {
                Id = Guid.NewGuid()
            };

            bool isActive = true;

            _userRepository.GetUsers(isActive).Returns(new List<User> { user1, user2, user3 });

            // execute

            var result = _sut.GetUsers(isActive);

            //
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

        }


        #endregion

        #region CreateUser
        [Test]
        public void CreateUser_EveryTime_valid()
        {
            // Mock
            var userToCreate = new User()
            {
                UserName = "John_Doe",
                Email = "john.doe@gmail.com",
                Password = "soleil123",
                Roles = new List<Role>{
                    new Role(){
                        Id = Guid.NewGuid(),
                        Name = "admin"
                    }
                }
            };

            // Execute
            _sut.CreateUser(userToCreate);

            // Assert

            Assert.NotNull(userToCreate);
            Assert.AreEqual(Guid.Empty, userToCreate.Id);
            Assert.NotNull(userToCreate.Roles);
            Assert.AreEqual(1, userToCreate.Roles.Count);
            Assert.AreNotEqual("soleil123", userToCreate.Password);
            Assert.IsFalse(string.IsNullOrEmpty(userToCreate.PasswordSalt));
            Assert.IsTrue(userToCreate.IsActive);
            Assert.IsNull(userToCreate.UpdateTime);
        }

        [Test]
        public void CreateUser_UserAlreadyExistsByUserName_ThrowException()
        {
            // Mock
            var userToCreate = new User()
            {
                UserName = "John_Doe"
            };

            var userDB = new User()
            {
            };

            _userRepository.GetUser(null, userToCreate.UserName).Returns(x => userDB);

            // Execute
            Assert.That(() => _sut.CreateUser(userToCreate),
                Throws.TypeOf<Exception>());
        }

        [Test]
        public void CreateUser_UserAlreadyExistsByEmail_ThrowException()
        {
            // Mock
            var userToCreate = new User()
            {
                UserName = "BOB",
                Email = "johnDoe@gmail.com"
            };

            var userDB = new User()
            {
            };

            _userRepository.GetUser(null, userToCreate.UserName, null).Returns(x => null);
            _userRepository.GetUser(null, null, userToCreate.Email).Returns(x => userDB);

            // Execute

            Assert.That(() => _sut.CreateUser(userToCreate),
                Throws.TypeOf<Exception>());
        }

        [Test]
        public void CreateUser_UserHaveNoRole_ThrowException()
        {
            // Mock
            var userToCreate = new User()
            {

            };

            // Execute && Assert
            Assert.That(() => _sut.CreateUser(userToCreate),
                Throws.TypeOf<Exception>());
        }

        #endregion

        #region UpdateUser

        [Test]
        public void UpdateUser_EveryTime_valid()
        {
            // Mock

            var userModel = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "John",
                Email = "john@gmail.com",
                Roles = new List<Role>{
                    new Role(){
                        Id = Guid.NewGuid(),
                        Name = "manager"
                    }
                }
            };

            _userRepository.Update(Arg.Any<User>());

            // Execute
            _sut.UpdateUser(userModel);

            // Assert
            _userRepository.Received(1).Update(Arg.Any<User>());

        }

        #endregion

        #region DeleteUser
        [Test]
        public void DeleteUser_EveryTime_valid()
        {
            // mock
            var userModel = new User()
            {
                Id = Guid.NewGuid(),
            };

            _userRepository.Delete(Arg.Any<Guid>());

            // Execute
            _sut.DeleteUser(userModel.Id);

            // Assert
            _userRepository.Received(1).Delete(Arg.Any<Guid>());
        }

        #endregion

        #endregion

        #region RoleTest  

        #region GetRole

        [Test]
        public void GetRole_WithData_valid()
        {
            // Mock
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "admin"
            };

            _roleRepository.GetRole(Arg.Any<Guid>()).Returns(x => role);

            // Execute

            var result = _sut.GetRole(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
            Assert.AreEqual("admin", result.Name);
        }

        [Test]
        public void GetRole_WithoutData_valid()
        {
            // Mock
            _roleRepository.GetRole(Arg.Any<Guid>()).Returns(x => null);
            // Execute
            var result = _sut.GetRole(Arg.Any<Guid>());
            // Assert
            Assert.Null(result);
        }


        #endregion

        #region GetRoles

        [Test]
        public void GetRoles_WithData_valid()
        {
            // Mock
            var role1 = new Role()
            {
                Id = Guid.NewGuid()
            };

            var role2 = new Role()
            {
                Id = Guid.NewGuid()
            };

            var role3 = new Role()
            {
                Id = Guid.NewGuid()
            };

            bool isActive = true;

            _roleRepository.GetRoles(isActive).Returns(new List<Role> { role1, role2, role3 });

            // execute

            var result = _sut.GetRoles(isActive);

            //
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());

        }

        #endregion

        #region CreateRole
        [Test]
        public void CreateRoles_Always_valid()
        {
            // Mock
            var roleModel = new Role()
            {
                Name = "admin",
            };

            // execute

            _sut.CreateRole(roleModel);

            // Assert
            Assert.IsNotNull(roleModel);
            Assert.AreNotEqual(Guid.Empty, roleModel.Id);
            Assert.IsTrue(roleModel.IsActive);
            Assert.AreEqual("admin", roleModel.Name);
            Assert.IsNull(roleModel.UpdateTime);
        }

        [Test]
        public void CreateRoles_withSameName_ThrowException()
        {
            // Mock
            var roleModel = new Role()
            {
                Name = "admin",
            };

            var roleDb = new Role();

            _roleRepository.GetRole(null, roleModel.Name).Returns(x => roleDb);

            // execute & Assert

            Assert.That(() => _sut.CreateRole(roleModel),
                Throws.TypeOf<Exception>());
        }


        #endregion

        #region UpdateRole

        [Test]
        public void UpdateRoles_Alway_valid()
        {
            var roleModel = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
            };

            _roleRepository.Update(Arg.Any<Role>());

            // Execute
            _sut.UpdateRole(roleModel);

            // Assert
            _roleRepository.Received(1).Update(Arg.Any<Role>());
        }


        #endregion

        #region DeleteRole

        [Test]
        public void DeleteRoles_Alway_valid()
        {
            _roleRepository.Delete(Arg.Any<Guid>());

            // Execute
            _sut.DeleteRole(Guid.NewGuid());

            // Assert
            _roleRepository.Received(1).Delete(Arg.Any<Guid>());
        }

        #endregion

        #endregion

    }
}
