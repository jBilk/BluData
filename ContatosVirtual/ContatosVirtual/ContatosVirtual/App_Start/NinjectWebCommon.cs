using ContatosVirtual.Controllers;
using ContatosVirtual.DAO;
using ContatosVirtual.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ContatosVirtual.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(ContatosVirtual.App_Start.NinjectWebCommon), "Stop")]

namespace ContatosVirtual.App_Start
{
    public class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);


        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

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

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICodificadorSenhas>().To<CodificadorSenhasController>();
            kernel.Bind<IEmail>().To<EmailController>();
            kernel.Bind<IGerarSenhaCriptografada>().To<SenhasCriptogafadasController>();

            kernel.Bind<IEmpresas>().To<EmpresasDAO>();
            kernel.Bind<IEstados>().To<EstadosDAO>();
            kernel.Bind<IFornecedores>().To<FornecedoresDAO>();
            kernel.Bind<ITelefones>().To<TelefonesDAO>();
            kernel.Bind<IUsuarios>().To<UsuariosDAO>();
        }
    }
}