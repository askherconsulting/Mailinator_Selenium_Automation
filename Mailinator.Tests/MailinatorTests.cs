using System.Security.Cryptography;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Mailinator.Pages;
using OpenQA.Selenium.Support.UI;
using Tests.Base;




namespace Mailinator.Tests
{
    public class MailinatorTests : TestBase

    {

        [Test, Category("e2e")]
        public void private_inbox_Click_Email_Link()
        {         
            //Login to mailinator and open private inbox  
            driver.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth");
            var homePage = new HomePage(driver);
            homePage.clickLoginButton(driver);
            var loginPage = new LoginPage(driver);
            loginPage.Login(username, password);
            //Go to private inbox
            driver.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth");
            var inboxPage = new InboxPage(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            inboxPage.selectInbox("beth");
            //give the email time to land in the inbox
            wait.Until(driver => inboxPage.Map.emailSW.Displayed);
            // Click on the email
            inboxPage.openEmail(inboxPage.Map.emailSW);
            var messagePage = new MessagePage(driver);
            // Now switch to the email body iframe
             //note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
             try {
             driver.SwitchTo().Frame("html_msg_body");
             wait.Until(driver => messagePage.Map.viewEventButton.Displayed);}
             catch (WebDriverException ) {
                 driver.SwitchTo().DefaultContent();
                 driver.SwitchTo().Frame("texthtml_msg_body");
                 wait.Until(driver => messagePage.Map.viewEventButton.Displayed);
             }
            // Click on the email link 
            messagePage.clickViewEvent(driver);
            // If you need to go back to the menu, don't forget to switch back:
            driver.SwitchTo().DefaultContent();
            //check a new tab has been opened
            wait.Until(driver => driver.WindowHandles.Count == 2);
            Assert.AreEqual(2, driver.WindowHandles.Count);
        }


[Test, Category("e2e")]
        public void public_inbox_switch_to_and_from_mailinator_tab()
        {         
            //go to public inbox
            driver.Url = ("https://www.mailinator.com/v4/public/inboxes.jsp?to=beth123");
            //switch to new tab
            driver = driver.SwitchTo().NewWindow(WindowType.Tab);
            //open new URL
            driver.Url = ("https://testproject.io/platform/");
            //go back to first tab
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            //open email
            var inboxPage = new InboxPage(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => inboxPage.Map.emailSW.Displayed);
            // Click on the email
            inboxPage.openEmail(inboxPage.Map.emailSW);
            var messagePage = new MessagePage(driver);
            // Now switch to the email body iframe:
            driver.SwitchTo().Frame("html_msg_body");
            wait.Until(driver => messagePage.Map.viewEventButton.Displayed);
            // Click on the email link 
            messagePage.clickViewEvent(driver);
            // If you need to go back to the menu, don't forget to switch back:
            driver.SwitchTo().DefaultContent();
            //check a new tab has been opened
            wait.Until(driver => driver.WindowHandles.Count == 3);
            Assert.AreEqual(3, driver.WindowHandles.Count);
        }

        [Test, Category("e2e")]
        public void e2e_public_mailbox_signup_test()
        {   //pre-requisites - generate pages
            var wordPressPage = new WordPressPage(driver);
            var registerPage = new RegisterPage(driver); 
            var inboxPage = new InboxPage(driver);
            var messagePage = new MessagePage(driver);
            var passwordPage = new PasswordPage(driver);
            var resetPasswordPage = new ResetPasswordPage(driver);
            var wordPressAdminPage = new WordPressAdminPage(driver);
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(driver);
            string Email = generateUniquePublicMailinatorEmail(driver);
            string Password = generateUniquePassword(driver);
            //set wait
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //go to sign in
            driver.Url = ("https://timelesstales.in/");
            wordPressPage.ScrollToBottom(driver);
            wordPressPage.ClickRegister(driver);
            //click sign in button         
            registerPage.CreateAccount(Username, Email);      
             //switch to new tab
            driver = driver.SwitchTo().NewWindow(WindowType.Tab);
            //go to public Mailinator inbox
            driver.Url = ("https://www.mailinator.com/v4/public/inboxes.jsp?to=" + Email);
            //open email
            wait.Until(driver => inboxPage.Map.emailWP.Displayed);
            inboxPage.openEmail(inboxPage.Map.emailWP);
            //Now switch to the email body iframe and move back afterwards                
            //note - the frame will either be html_msg_body or texthtml_msg_body 
            //so use this try catch block to try both
            try {
            driver.SwitchTo().Frame("texthtml_msg_body");
            wait.Until(driver => messagePage.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame("html_msg_body");
                wait.Until(driver => messagePage.Map.textLink.Displayed);
            }
            //Click on the email link 
            messagePage.clickTextLink(driver);
             //user auto-navigates to tab 3  
             //switch back to window from iframe
            driver.SwitchTo().Window(driver.WindowHandles[2]);
            //wait for auto-generated password to appear
            wait.Until(driver => passwordPage.Map.passStrengthResult.Displayed);
            //clear field and enter password
            passwordPage.enterPassword(Password);
            passwordPage.clickResetPasswordButton(driver);
            //login
            resetPasswordPage.clickLogin(driver);
            registerPage.Login(Email, Password);
            //assert landed on the correct page
            String title = driver.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }





        

        [Test, Category("e2e")]
        public void e2e_private_mailbox_signup_test()
        {         
            //pre-requisites - generate pages
            var wordPressPage = new WordPressPage(driver);
            var registerPage = new RegisterPage(driver); 
            var homePage = new HomePage(driver);
            var loginPage = new LoginPage(driver);
            var inboxPage = new InboxPage(driver);
            var messagePage = new MessagePage(driver);
            var passwordPage = new PasswordPage(driver);
            var resetPasswordPage = new ResetPasswordPage(driver);
            var wordPressAdminPage = new WordPressAdminPage(driver);
            //generate explicit wait condition
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(driver);
            string EmailPrefix = generateUniquePrivateMailinatorEmailPrefix(driver);
            string Password = generateUniquePassword(driver);
            //go to sign in
            driver.Url = ("https://timelesstales.in/wp-login.php?action=register");
            //click sign in button
            registerPage.CreateAccount(Username, EmailPrefix);
            //switch to new tab
            driver = driver.SwitchTo().NewWindow(WindowType.Tab);
            //go to inbox
            driver.Url = ("https://www.mailinator.com/");   
            homePage.clickLoginButton(driver);
            loginPage.Login(username, password);  
            //go to Mailinator
            driver.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix);   
             //open email
            driver.Url = ("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix); 
            wait.Until(driver => inboxPage.Map.emailWP.Displayed);
            inboxPage.openEmail(inboxPage.Map.emailWP);
             //Now switch to the email body iframe  
             // AND REMEMBER TO SWITCH BACK AFTERWARDS          
             //Note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
            try {
            driver.SwitchTo().Frame("texthtml_msg_body");
            wait.Until(driver => messagePage.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame("html_msg_body");
                wait.Until(driver => messagePage.Map.textLink.Displayed);
            }
            //Click on the email link 
            messagePage.clickTextLink(driver);
            //switch back to window from iframe
            driver.SwitchTo().Window(driver.WindowHandles[2]);
            //wait for auto-generated password to appear
            wait.Until(driver => passwordPage.Map.passStrengthResult.Displayed);
            //clear field and enter password
            passwordPage.enterPassword(Password);
            passwordPage.clickResetPasswordButton(driver);
            //login
            resetPasswordPage.clickLogin(driver);
            registerPage.Login(EmailPrefix, Password);
            //assert landed on the correct page
            String title = driver.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }

    }
}