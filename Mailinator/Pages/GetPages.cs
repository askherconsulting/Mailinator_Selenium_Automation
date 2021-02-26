using System;
using Framework.Selenium;

namespace Mailinator.Pages 
{

    public class GetPages
    {
        [ThreadStatic]
        public static HeaderNav Header;

        [ThreadStatic]
        public static CreateAccountPage CreateAccount;

        [ThreadStatic]
        public static HomePage Home;

        [ThreadStatic]
        public static InboxPage Inbox;

        [ThreadStatic]
        public static LinksPage Links;

        [ThreadStatic]
        public static LoginPage Login;

        [ThreadStatic]
        public static MessagePage Message;

        [ThreadStatic]
        public static PasswordPage Password;

        [ThreadStatic]
        public static RegisterPage Register;

        [ThreadStatic]
        public static ResetPasswordPage ResetPassword;

        [ThreadStatic]
        public static TextPage Text;

        [ThreadStatic]
        public static WordPressAdminPage WordPressAdmin;

        [ThreadStatic]
        public static WordPressPage WordPress;

        public static void Init()
        {
            Header = new HeaderNav();
            CreateAccount = new CreateAccountPage();
            Home = new HomePage();
            Inbox = new InboxPage();
            Links = new LinksPage();
            Login = new LoginPage();
            Message = new MessagePage();
            Password = new PasswordPage();
            Register = new RegisterPage();
            ResetPassword = new ResetPasswordPage();
            Text = new TextPage();
            WordPressAdmin = new WordPressAdminPage();
            WordPress = new WordPressPage();
        }


    }
}