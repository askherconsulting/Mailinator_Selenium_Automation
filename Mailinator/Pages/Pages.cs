using System.Threading;
using System.Runtime.Versioning;
using System;
using Framework.Selenium;

namespace Mailinator.Pages {

    public class Pages
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
            Header = new HeaderNav(Driver.Current);
            CreateAccount = new CreateAccountPage(Driver.Current);
            Home = new HomePage(Driver.Current);
            Inbox = new InboxPage(Driver.Current);
            Links = new LinksPage(Driver.Current);
            Login = new LoginPage(Driver.Current);
            Message = new MessagePage(Driver.Current);
            Password = new PasswordPage(Driver.Current);
            Register = new RegisterPage(Driver.Current);
            ResetPassword = new ResetPasswordPage(Driver.Current);
            Text = new TextPage(Driver.Current);
            WordPressAdmin = new WordPressAdminPage(Driver.Current);
            WordPress = new WordPressPage(Driver.Current);
        }


    }
}