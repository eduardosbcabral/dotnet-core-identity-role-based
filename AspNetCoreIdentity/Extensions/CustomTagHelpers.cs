using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace AspNetCoreIdentity.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-role-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-role-value")]
    public class ApagaElementoByRoleTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApagaElementoByRoleTagHelper(
            IHttpContextAccessor contextAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("supress-by-role-name")]
        public string IdentityRoleClaimName { get; set; }

        [HtmlAttributeName("supress-by-role-value")]
        public string IdentityRoleClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarRolesUsuario(_contextAccessor.HttpContext, _roleManager, IdentityRoleClaimName, IdentityRoleClaimValue);

            if (temAcesso) return;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("a", Attributes = "disable-by-role-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-role-value")]
    public class DesabilitaLinkByRoleTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DesabilitaLinkByRoleTagHelper(IHttpContextAccessor contextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("disable-by-role-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-role-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarRolesUsuario(_contextAccessor.HttpContext, _roleManager, IdentityClaimName, IdentityClaimValue);

            if (temAcesso) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
        }
    }

    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}