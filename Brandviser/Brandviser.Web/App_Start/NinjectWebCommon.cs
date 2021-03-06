[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Brandviser.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Brandviser.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Brandviser.Web.App_Start
{
    using System;
    using System.Web;
    using Services.Contracts;
    using Common;
    using Common.Contracts;
    using Data;
    using Data.Contracts;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Services;
    using Factories;
    using Ninject.Extensions.Factory;
    using Helpers.Contracts;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IWhois>().To<Whois>().InRequestScope();
            kernel.Bind<ITxtRecordsChecker>().To<TxtRecordsChecker>().InRequestScope();
            kernel.Bind<ISocket>().To<JustTcpStreamSocket>().InRequestScope();
            kernel.Bind<IBrandviserData>().To<BrandviserData>().InRequestScope();
            kernel.Bind<IBrandviserDbContext>().To<BrandviserDbContext>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IDomainService>().To<DomainService>().InRequestScope();
            kernel.Bind<IDateTimeProvider>().To<DateTimeProvider>().InRequestScope();

            kernel.Bind<IDomainFactory>().ToFactory().InRequestScope();

            kernel.Bind<ILoggedInUser>().To<LoggedInUser>();
        }
    }
}
