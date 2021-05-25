using DocumentRepoAPI.Services.Implementations;
using DocumentRepoAPI.Services.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace DocumentRepoAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IDocumentService, DocumentService>();
            container.RegisterType<IEncryptionService, EncryptionService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}