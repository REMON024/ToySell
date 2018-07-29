using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using AspWepProject.Models;

namespace AspWepProject.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                if (user.EmailConfirmed)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    using (AspWepProject.Logic.ShoppingCartActions usersShoppingCart = new AspWepProject.Logic.ShoppingCartActions())
                    {
                        String cartId = usersShoppingCart.GetCartId();
                        usersShoppingCart.MigrateCart(cartId, user.Id);
                    }
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
            
            else
            {
                ErrorMessage.Text = "An email has been sent to your account. Please view the email and confirm your account to complete the registration process.";
            }
        }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}