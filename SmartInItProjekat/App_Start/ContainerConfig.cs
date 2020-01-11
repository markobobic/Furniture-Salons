using Autofac;
using Autofac.Integration.Mvc;
using SmartInItProjekat.Repository;
using System.Web.Mvc;
using SmartInItProjekat.Models;


namespace SmartInItProjekat.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<FurnitureSalonRepo>().As<IFurnitureSalonRepo>().InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<FurnitureRepo>().As<IFurnitureRepo>().InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<CategoriesRepo>().As<ICategoriesRepo>().InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}