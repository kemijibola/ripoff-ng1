using ripoffnigeria.DTO;
using ripoffnigeriaonline.Photo;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Implementations;
using ripoffnigeria.Repository.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ripoffnigeriaonline.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ripoffnigeriaonline.App_Start.NinjectWebCommon), "Stop")]

namespace ripoffnigeriaonline.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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

            kernel.Bind<RipOffContext>().To<RipOffContext>().InRequestScope();
            kernel.Bind<ICategory>().To<CategoryRepository>().InRequestScope();
            kernel.Bind<ICity>().To<CityRepository>().InRequestScope();
            kernel.Bind<ICountry>().To<CountryRepository>().InRequestScope();
            kernel.Bind<ILocationType>().To<LocationTypeRepository>().InRequestScope();
            kernel.Bind<IRebuttalImage>().To<RebuttalImageRepository>().InRequestScope();
            kernel.Bind<IRebuttal>().To<RebuttalRepository>().InRequestScope();
            kernel.Bind<IPhotoManager>().To<LocalPhotoManager>().InRequestScope();
            kernel.Bind<IReport>().To<ReportRepository>().InRequestScope();
            kernel.Bind<IReportImage>().To<ReportImageRepository>().InRequestScope();
            kernel.Bind<IRipOffLawyer>().To<RipOffLawyerRepository>().InRequestScope();
            kernel.Bind<IRipOffFirm>().To<RipOffFirmRepository>().InRequestScope();
            kernel.Bind<IState>().To<StateRepository>().InRequestScope();
            kernel.Bind<ITopic>().To<TopicRepository>().InRequestScope();
            kernel.Bind<ILawTypeCategory>().To<LawTypeCategoryRepository>().InRequestScope();
            kernel.Bind<ILawCategory>().To<LawCategoryRepository>().InRequestScope();
            kernel.Bind<IFirmCategory>().To<FirmCategoryRepository>().InRequestScope();
            kernel.Bind<IFirmImage>().To<FirmImageRepository>().InRequestScope();
            kernel.Bind<ItrackUser>().To<trackUserRepository>().InRequestScope();
            kernel.Bind<IFirmComment>().To<FirmCommentRepository>().InRequestScope();
            kernel.Bind<ITransaction>().To<TransactionRepository>().InRequestScope();
            kernel.Bind<IBank>().To<BankRepository>().InRequestScope();
            kernel.Bind<IClientMeetingRequest>().To<ClientMeetingRequestRepository>().InRequestScope();
        }        
    }
}
