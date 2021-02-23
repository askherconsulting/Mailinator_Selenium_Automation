using System.Security.Cryptography;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Mailinator.Pages;
using OpenQA.Selenium.Support.UI;
using Tests.Base;
using Framework.Selenium;

namespace Mailinator.Tests
{
    public class MailinatorTests : TestBase

    {

        [Test, Category("e2e")]
        public void private_inbox_Click_Email_Link()
        {         
            //Login to mailinator and open private inbox  
            Driver.Current.Url = "https://www.mailinator.com/v4/private/inboxes.jsp?to=beth";
            var homePage = new HomePage(Driver.Current);
            homePage.clickLoginButton(Driver.Current);
            var loginPage = new LoginPage(Driver.Current);
            loginPage.Login(username, password);
            //Go to private inbox
            Driver.Current.Url = "https://www.mailinator.com/v4/private/inboxes.jsp?to=beth";
            var inboxPage = new InboxPage(Driver.Current);
            var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
      
            inboxPage.selectInbox("beth");
            //give the email time to land in the inbox
      ////      wait.Until(Driver.Current => inboxPage.Map.emailSW.Displayed);
            // Click on the email
            inboxPage.openEmail(inboxPage.Map.emailSW);
            var messagePage = new MessagePage(Driver.Current);
            // Now switch to the email body iframe
             //note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
             try {
             Driver.Current.SwitchTo().Frame("html_msg_body");}
        ////     wait.Until(Driver.Current => messagePage.Map.viewEventButton.Displayed);}
             catch (WebDriverException ) {
                 Driver.Current.SwitchTo().DefaultContent();
                 Driver.Current.SwitchTo().Frame("texthtml_msg_body");
       ////          wait.Until(Driver.Current => messagePage.Map.viewEventButton.Displayed);
             }
            // Click on the email link 
            messagePage.clickViewEvent(Driver.Current);
            // If you need to go back to the menu, don't forget to switch back:
            Driver.Current.SwitchTo().DefaultContent();
            //check a new tab has been opened
       ////     wait.Until(driver => Driver.Current.WindowHandles.Count == 2);
            Assert.AreEqual(2, Driver.Current.WindowHandles.Count);
        }


[Test, Category("e2e")]
        public void public_inbox_switch_to_and_from_mailinator_tab()
        {         
            //go to public inbox
            Driver.Current.Url = ("https://www.mailinator.com/v4/public/inboxes.jsp?to=beth123");
            //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //open new URL
            Driver.Current.Url = ("https://testproject.io/platform/");
            //go back to first tab
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[0]);
            //open email
            var inboxPage = new InboxPage(Driver.Current);
      ////      var wait = new WebDriver.CurrentWait(Driver.Current, TimeSpan.FromSeconds(10));
      ////      wait.Until(Driver.Current => inboxPage.Map.emailSW.Displayed);
            // Click on the email
            inboxPage.openEmail(inboxPage.Map.emailSW);
            var messagePage = new MessagePage(Driver.Current);
            // Now switch to the email body iframe:
            Driver.Current.SwitchTo().Frame("html_msg_body");
      ////      wait.Until(Driver.Current => messagePage.Map.viewEventButton.Displayed);
            // Click on the email link 
            messagePage.clickViewEvent(Driver.Current);
            // If you need to go back to the menu, don't forget to switch back:
            Driver.Current.SwitchTo().DefaultContent();
            //check a new tab has been opened
      ////      wait.Until(Driver.Current => Driver.Current.WindowHandles.Count == 3);
            Assert.AreEqual(3, Driver.Current.WindowHandles.Count);
        }

        [Test, Category("e2e")]
        public void e2e_public_mailbox_signup_test()
        {   //pre-requisites - generate pages
            var wordPressPage = new WordPressPage(Driver.Current);
            var registerPage = new RegisterPage(Driver.Current); 
            var inboxPage = new InboxPage(Driver.Current);
            var messagePage = new MessagePage(Driver.Current);
            var passwordPage = new PasswordPage(Driver.Current);
            var resetPasswordPage = new ResetPasswordPage(Driver.Current);
            var wordPressAdminPage = new WordPressAdminPage(Driver.Current);
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string Email = generateUniquePublicMailinatorEmail(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //set wait
            var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
            //go to sign in
            Driver.Current.Url = ("https://timelesstales.in/");
            wordPressPage.ScrollToBottom(Driver.Current);
            wordPressPage.ClickRegister(Driver.Current);
            //click sign in button         
            registerPage.CreateAccount(Username, Email);      
             //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to public Mailinator inbox
            Driver.Current.Url = ("https://www.mailinator.com/v4/public/inboxes.jsp?to=" + Email);
            //open email
            wait.Until(driver => inboxPage.Map.emailWP.Displayed);
            inboxPage.openEmail(inboxPage.Map.emailWP);
            //Now switch to the email body iframe and move back afterwards                
            //note - the frame will either be html_msg_body or texthtml_msg_body 
            //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("texthtml_msg_body");}
    ////        wait.Until(Driver.Current => messagePage.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("html_msg_body");
    ////            wait.Until(Driver.Current => messagePage.Map.textLink.Displayed);
            }
            //Click on the email link 
            messagePage.clickTextLink(Driver.Current);
             //user auto-navigates to tab 3  
             //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //wait for auto-generated password to appear
    ////        wait.Until(Driver.Current => passwordPage.Map.passStrengthResult.Displayed);
            //clear field and enter password
            passwordPage.enterPassword(Password);
            passwordPage.clickResetPasswordButton(Driver.Current);
            //login
            resetPasswordPage.clickLogin(Driver.Current);
            registerPage.Login(Email, Password);
            //assert landed on the correct page
            String title = Driver.Current.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }





        

        [Test, Category("e2e")]
        public void e2e_private_mailbox_signup_test()
        {         
            //pre-requisites - generate pages
            var wordPressPage = new WordPressPage(Driver.Current);
            var registerPage = new RegisterPage(Driver.Current); 
            var homePage = new HomePage(Driver.Current);
            var loginPage = new LoginPage(Driver.Current);
            var inboxPage = new InboxPage(Driver.Current);
            var messagePage = new MessagePage(Driver.Current);
            var passwordPage = new PasswordPage(Driver.Current);
            var resetPasswordPage = new ResetPasswordPage(Driver.Current);
            var wordPressAdminPage = new WordPressAdminPage(Driver.Current);
            //generate explicit wait condition
            var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string EmailPrefix = generateUniquePrivateMailinatorEmailPrefix(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //go to sign in
            Driver.Current.Url = ("https://timelesstales.in/wp-login.php?action=register");
            //click sign in button
            registerPage.CreateAccount(Username, EmailPrefix);
            //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to inbox
            Driver.Current.Url = ("https://www.mailinator.com/");   
            homePage.clickLoginButton(Driver.Current);
            loginPage.Login(username, password);  
            //go to Mailinator
            Driver.Current.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix);   
             //open email
            Driver.Current.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix); 
       ////    wait.Until(Driver.Current => inboxPage.Map.emailWP.Displayed);
            inboxPage.openEmail(inboxPage.Map.emailWP);
             //Now switch to the email body iframe  
             // AND REMEMBER TO SWITCH BACK AFTERWARDS          
             //Note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("texthtml_msg_body");}
     ////       wait.Until(Driver.Current => messagePage.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("html_msg_body");
      ////          wait.Until(Driver.Current => messagePage.Map.textLink.Displayed);
            }
            //Click on the email link 
            messagePage.clickTextLink(Driver.Current);
            //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //wait for auto-generated password to appear
      ////      wait.Until(Driver.Current => passwordPage.Map.passStrengthResult.Displayed);
            //clear field and enter password
            passwordPage.enterPassword(Password);
            passwordPage.clickResetPasswordButton(Driver.Current);
            //login
            resetPasswordPage.clickLogin(Driver.Current);
            registerPage.Login(EmailPrefix, Password);
            //assert landed on the correct page
            String title = Driver.Current.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }

    }
}