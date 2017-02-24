using Nancy;
using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class Authentication
    {
        private ITokenValidator validator;
        private ITokenProviderOptions tokenProviderOptions;

        public Authentication(ITokenValidator tokenValidator,  ITokenConfiguration optionsProvider)
        {
            this.validator = tokenValidator;
            this.tokenProviderOptions = optionsProvider.Configure();
        }

        public void Enable(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(CheckToken);
        }

        private Response CheckToken(NancyContext context)
        {
            var path = Uri.UnescapeDataString(context.Request.Path);

            // check paths to ignore auth checking
            if (this.tokenProviderOptions != null && this.tokenProviderOptions.Path != null)
            {
                if (this.tokenProviderOptions.Path.Equals(path))
                {
                    return null;
                }
            }

            // check auth header token
            string token = context.Request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(token))
                return AuthChallengeResponse(context);

            var user = this.validator.ValidateUser(token);

            if (user == null)
                return AuthChallengeResponse(context);

            // token is valid, set it to CurrentUser
            context.CurrentUser = user;

            return null;
        }

        private Response AuthChallengeResponse(NancyContext context)
        {
            var resp = new Response();
            resp.StatusCode = HttpStatusCode.Unauthorized;
            return resp;

        }
    }
}
