using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using WebMarket.Data;
using WebMarket.Data.Repositories.Security;

namespace WebMarket.Services
{
    public static class BootStrap
    {

        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();

                    // Repo
                    _container.RegisterType<IUserRepository, UserRepository>();
                    _container.RegisterType<IRoleRepository, RoleRepository>();


                    // Services
                    _container.RegisterType<ISecurityService, SecurityService>();

                    // Misc
                    _container.RegisterSingleton<DataContext>();
                }
                return _container;
            }
        }
    }
}
