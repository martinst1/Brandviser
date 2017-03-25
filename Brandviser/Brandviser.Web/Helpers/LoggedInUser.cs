using Microsoft.AspNet.Identity;
using System.Web;
using Brandviser.Services.Contracts;
using Brandviser.Web.Helpers.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Services
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly HttpContextBase context;

        public LoggedInUser(HttpContextBase context)
        {
            Guard.WhenArgument(context, "HttpContextBase").IsNull().Throw();

            this.context = context;
        }
        public string GetUserId()
        {
            return this.context.User.Identity.GetUserId();
        }
    }
}