using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using WebMarket.Services;
using Unity;
using System.Threading.Tasks;
using WebMarket.Models.Security;

namespace WebMarket.WebApp.Controllers.Security
{
    public class SecurityController : Controller
    {
        //[Dependency]
        public ISecurityService _securityService { get; set; }


        public SecurityController()
        {
            _securityService = BootStrap.Container.Resolve<ISecurityService>();
        }

        #region User
        // GET: Security/User
        [Route("Security/User/index")]
        public ActionResult UserIndex()
        {
            List<User> users = _securityService.GetUsers(true).ToList();

            return View("User/UserIndex", users);
        }

        // GET: Security/User/Details/3
        [Route("Security/User/Details/{id}")]
        public ActionResult UserDetails(Guid id)
        {
            var user = _securityService.GetUser(id);

            return View(user);
        }

        // GET: Security/Create
        [Route("Security/User/Create")]
        public ActionResult UserCreate()
        {
            var roles = _securityService.GetRoles(true).ToList();
            ViewBag.RoleList = roles;
            return View("User/UserCreate");
        }

        // POST: Security/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind]WebUI.Models.UserViewModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Roles = model.Roles
            };

             _securityService.CreateUser(user);

            return RedirectToAction("UserIndex");
        }

        // GET: Security/Create
        public ActionResult UserEdit(Guid id)
        {
            var user = _securityService.GetUser(id);

            return View("User/UserCreate", user);
        }


        // POST: Seciroty/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserUpdate(Guid id,[Bind("Id,Username,Email")] User user)
        {
            if(id == user.Id)
                _securityService.UpdateUser(user);

            return RedirectToAction("UserIndex");
        }

        #endregion

        #region Role
        // GET: Security

        public ActionResult RoleIndex()
        {
            List<Role> roles = _securityService.GetRoles(true).ToList();

            return View("Role/RoleIndex", roles);
        }

        // GET: Security/Create
        public ActionResult RoleCreate()
        {
            return View("Role/RoleCreate");
        }

        // POST: Security/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate([Bind]Role role)
        {

            _securityService.CreateRole(role);

            return RedirectToAction("RoleIndex");
        }

        #endregion
    }
}