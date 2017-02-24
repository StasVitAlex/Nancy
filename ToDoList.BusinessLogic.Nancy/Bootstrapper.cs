using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using ToDoList.BusinessLogic.Core.IServices;
using ToDoList.BusinessLogic.Core.Services;
using jwt = ToDoList.BusinessLogic.Nancy.Auth.JWT;
using ToDoList.DataAccessLayer.Repositories;
using ToDoList.DataAccessLayer.Repositories.Interfaces;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {

            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                .WithHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS")
                                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            this.RegisterDependencies(container);

            var auth = container.Resolve<jwt.Authentication>();
            auth.Enable(pipelines);

        }

        private void RegisterDependencies(TinyIoCContainer container)
        {
            container.Register<IToDoService, ToDoService>();
            container.Register<IToDoDbContext, ToDoDbContext>();
            container.Register<IUnitOfWork, UnitOfWork>();

            //jwt auth registration
            container.Register<ITokenConfiguration, jwt.TokenConfiguration>();
            container.Register<ITokenProviderOptions, jwt.TokenProviderOptions>();
            container.Register<ITokenUserMapper, jwt.TokenUserMapper>();
            container.Register<ITokenValidator, jwt.TokenValidator>();
            container.Register<ITokenEncoder, jwt.JWTTokenEncoder>();
        }
    }
}
