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
using WebMarket.WebUI.Models.Security;

namespace WebMarket.WebApp.Controllers.Security
{
    [Route("Security")]
    public class SecurityController : Controller
    {
        //[Dependency]
        public ISecurityService _securityService { get; set; }


        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        #region User
        // GET: Security/User
        [HttpGet("userindex")]
        public ActionResult UserIndex()
        {
            List<User> users = _securityService.GetUsers(true).ToList();

            return View(users);
        }

        // GET: Security/User/Details/3
        [Route("{id}")]
        public ActionResult UserDetails(Guid id)
        {
            var user = _securityService.GetUser(id);

            return View(user);
        }

        // GET: Security/Create
        public ActionResult UserCreate()
        {
            var roles = _securityService.GetRoles(true).ToList();
            ViewBag.RoleList = roles;
            return View();
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
        [HttpGet("UserEdit/{id}")]
        public ActionResult UserEdit(Guid id)
        {
            var user = _securityService.GetUser(id);

            return View(user);
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
        [HttpGet("roleindex")]
        public ActionResult RoleIndex()
        {
            List<Role> roles = _securityService.GetRoles(true).ToList();

            return View(roles);
        }

        // GET: Security/Create
        [HttpGet("RoleCreate")]
        public ActionResult RoleCreate()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate([Bind]RoleViewModel model)
        {

            var role = new Role { Name = model.Name };

            _securityService.CreateRole(role);

            return RedirectToAction("RoleIndex");
        }


        [HttpGet("RoleDetails/{id}")]
        public ActionResult RoleDetails(Guid id)
        {
            var role = _securityService.GetRole(id);

            return View(role);
        }

        [HttpGet("RoleUpdate/{id}")]
        public ActionResult RoleUpdate(Guid id)
        {
            var role = _securityService.GetRole(id);
            return View(role);
        }

        [HttpPost("RoleUpdate/{role}")]
        [ValidateAntiForgeryToken]
        public void RoleUpdate([Bind]RoleViewModel model)
        {
            var role = _securityService.GetRole(model.Id);

            role.Name = model.Name;

            _securityService.UpdateRole(role);

            RedirectToAction("RoleIndex");
        }

        [HttpDelete("RoleDelete/{id}")]
        public ViewResult RoleDelete(Guid id)
        {
            var role = _securityService.GetRole(id);
            return View(role);
        }




        #endregion
    }
}