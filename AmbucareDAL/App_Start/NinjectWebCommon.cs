

using AmbucareDAL.Repository;
using AmbucareDAL.Repository.Hospital;
using AmbucareDAL.Repository.Hospital.Implementation.Initialization;
using AmbucareDAL.Repository.Hospital.Implementation.Menu;
using AmbucareDAL.Repository.Hospital.Interface;
using AmbucareDAL.Repository.Hospital.Interface.Initialization;
using AmbucareDAL.Repository.Hospital.Interface.Menu;
using AmbucareDAL.Repository.Implementation.Menu;
using AmbucareDAL.Repository.Implementation.Queries;
using AmbucareDAL.Repository.Implementation.Report;
using AmbucareDAL.Repository.Implementation.Setup;
using AmbucareDAL.Repository.Implementation.Transaction;
using AmbucareDAL.Repository.Interface;
using AmbucareDAL.Repository.Interface.Menu;
using AmbucareDAL.Repository.Interface.Queries;
using AmbucareDAL.Repository.Interface.Report;
using AmbucareDAL.Repository.Interface.Setup;
using AmbucareDAL.Repository.Interface.Transaction;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AmbucareDAL.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AmbucareDAL.App_Start.NinjectWebCommon), "Stop")]

namespace AmbucareDAL.App_Start
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
                kernel.Bind<IT74014>().To<T74014Repository>();
                kernel.Bind<IMenu>().To<MenuRepository>();
                kernel.Bind<IT74052>().To<T74052Repository>();
                kernel.Bind<IT74053>().To<T74053Repository>();
                kernel.Bind<ILogin>().To<LoginRepository>();
                kernel.Bind<IT74003>().To<T74003Repository>();
                kernel.Bind<IT74004>().To<T74004Repository>();
                kernel.Bind<IT74001>().To<T74001Repository>();
                kernel.Bind<IT74002>().To<T74002Repository>();
                kernel.Bind<IT74000>().To<T74000Reposatory>();
                kernel.Bind<IT74005>().To<T74005Repository>();
                kernel.Bind<IT74006>().To<T74006Repository>();
                kernel.Bind<IT74008>().To<T74008Repository>();
                kernel.Bind<IT74009>().To<T74009Repository>();
                kernel.Bind<IT74010>().To<T74010Repository>();
                kernel.Bind<IT74011>().To<T74011Repository>();
                kernel.Bind<IT74012>().To<T74012Repository>();
                kernel.Bind<IT74013>().To<T74013Repository>();
                kernel.Bind<IT74015>().To<T74015Repository>();
                kernel.Bind<IT74016>().To<T74016Repository>();
                kernel.Bind<IT74017>().To<T74017Repository>();
                kernel.Bind<IT74018>().To<T74018Repository>();
                kernel.Bind<IT74019>().To<T74019Repository>();
                kernel.Bind<IT74020>().To<T74020Repository>();
                kernel.Bind<IT74021>().To<T74021Repository>();
                kernel.Bind<IT74022>().To<T74022Repository>();
                kernel.Bind<IT74023>().To<T74023Repository>();
                kernel.Bind<IT74024>().To<T74024Repository>();
                kernel.Bind<IT74025>().To<T74025Repository>();
                kernel.Bind<IT74028>().To<T74028Repository>();
                kernel.Bind<IT74069>().To<T74069Repository>();
                //kernel.Bind<IT74024>().To<T74024Repository>();
                kernel.Bind<IT74066>().To<T74066Repository>();
                kernel.Bind<IT74037>().To<T74037Repository>();
                kernel.Bind<IT74038>().To<T74038Repository>();
                kernel.Bind<IT74046>().To<T74046Repository>();
                kernel.Bind<IT74044>().To<T74044Repository>();
                kernel.Bind<IT74068>().To<T74068Repository>();
                kernel.Bind<IT74071>().To<T74071Repository>();
                kernel.Bind<IT74073>().To<T74073Repository>();
                kernel.Bind<IT74112>().To<T74112Repository>();
                kernel.Bind<IT74054>().To<T74054Repository>();
                kernel.Bind<IT74055>().To<T74055Repository>();
                kernel.Bind<IT74056>().To<T74056Repository>();
                kernel.Bind<IT74047>().To<T74047Repository>();
                kernel.Bind<IT74035>().To<T74035Repository>();
                kernel.Bind<IT74041>().To<T74041Repository>();
                kernel.Bind<IT74042>().To<T74042Repository>();
                kernel.Bind<IT74111>().To<T74111Repository>();
                kernel.Bind<IT74027>().To<T74027Repository>();
                kernel.Bind<IT74113>().To<T74113Repository>();
                kernel.Bind<IT74075>().To<T74075Repository>();
                kernel.Bind<IT74045>().To<T74045Repository>();
                kernel.Bind<IT74114>().To<T74114Repository>();
                kernel.Bind<IT74115>().To<T74115Repository>();
                kernel.Bind<IT74116>().To<T74116Repository>();
                kernel.Bind<IT74117>().To<T74117Repository>();
                kernel.Bind<IT74118>().To<T74118Repository>();
                kernel.Bind<IT74119>().To<T74119Repository>();
                kernel.Bind<IT74120>().To<T74120Repository>();
                kernel.Bind<IT74121>().To<T74121Repository>();
                kernel.Bind<IT74122>().To<T74122Repository>();
                kernel.Bind<IT74123>().To<T74123Repository>();
                kernel.Bind<IT74124>().To<T74124Repository>();
                kernel.Bind<IT74125>().To<T74125Repository>();
                kernel.Bind<IT74126>().To<T74126Repository>();
                kernel.Bind<IT74127>().To<T74127Repository>();
                kernel.Bind<IT74128>().To<T74128Repository>();
                kernel.Bind<IT74129>().To<T74129Repository>();
                kernel.Bind<IT74096>().To<T74096Repository>();
                kernel.Bind<IT74130>().To<T74130Repository>();
                kernel.Bind<IT74131>().To<T74131Repository>();
                kernel.Bind<IT74132>().To<T74132Repository>();
                kernel.Bind<IT74133>().To<T74133Reposatory>();
                kernel.Bind<IT74134>().To<T74134Repository>();
                kernel.Bind<IT74135>().To<T74135Repository>();
                kernel.Bind<IT74136>().To<T74136Repository>();
                kernel.Bind<IT74138>().To<T74138Repository>();
                kernel.Bind<IQ74001>().To<Q74001Repository>();
                kernel.Bind<IQ74136>().To<Q74136Repository>();
                kernel.Bind<IQ74138>().To<Q74138Repository>();
                kernel.Bind<IT74143>().To<T74143Repository>();
                kernel.Bind<IT74144>().To<T74144Repository>();
                kernel.Bind<IT74145>().To<T74145Repository>();
                kernel.Bind<IT74146>().To<T74146Repository>();
                kernel.Bind<IT74148>().To<T74148Repository>();
                kernel.Bind<IT74149>().To<T74149Repository>();
                kernel.Bind<IT74150>().To<T74150Repository>();
                kernel.Bind<IT74151>().To<T74151Repository>();


                kernel.Bind<IQ74143>().To<Q74143Repository>();
                kernel.Bind<IQ74144>().To<Q74144Repository>();
                kernel.Bind<IQ74145>().To<Q74145Repository>();
                kernel.Bind<IQ74146>().To<Q74146Repository>();

                kernel.Bind<IT74015Report>().To<T74015ReportRepository>();
                kernel.Bind<IR74027Report>().To<R74027Repository>();
                kernel.Bind<IT74046Report>().To<T74046ReportRepository>();
                kernel.Bind<IT74111Report>().To<T74111ReportRepository>();
                kernel.Bind<IR74120Report>().To<R74120Repository>();
                kernel.Bind<IR74046Report>().To<R74046Repository>();
                kernel.Bind<IR74123Report>().To<R74123Repository>();
                kernel.Bind<IR74125Report>().To<R74125Repository>();

                kernel.Bind<IError>().To<ErrorRepository>();
               


                //----------------------Hospital-----------------------------
                //kernel.Bind<IHMenu>().To<HMenuRepository>();
                //kernel.Bind<HILogin>().To<HLoginRepository>();
                //kernel.Bind<IT04225>().To<T04225Repository>();
                //kernel.Bind<IT04230>().To<T04230Repository>();

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
        }
    }
}
