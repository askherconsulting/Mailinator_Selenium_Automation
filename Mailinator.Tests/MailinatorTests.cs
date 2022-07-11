using System.Data;
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
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth123889");
            Pages.GetPages.Inbox.clickLoginButton();
            Pages.GetPages.Login.Login(username, password);
            //Go to private inbox
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth123889");     
            Pages.GetPages.Inbox.selectInbox("beth123889");           
            Driver.Wait.Until(drvr => Pages.GetPages.Inbox.Map.emailWP.Displayed);
            Pages.GetPages.Inbox.openEmail(Pages.GetPages.Inbox.Map.emailWP);
            // Now switch to the email body iframe
             //note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
            try {                                                                                                                    
                Driver.Current.SwitchTo().Frame("html_msg_body");
                Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("texthtml_msg_body");
                Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);
                }
            // Click on the email link 
            Pages.GetPages.Message.clickTextLink(Driver.Current);
            // If you need to go back to the menu, don't forget to switch back:
            Driver.Current.SwitchTo().DefaultContent();
            //check a new tab has been opened
            Driver.Wait.Until(drvr => Driver.Current.WindowHandles.Count == 2);
            Assert.AreEqual(2, Driver.Current.WindowHandles.Count);
        }


        //[Test, Category("e2e")]
        //NOTE - HAVE KEPT THIS BECAUSE OF LEGACY USEFUL CODE
        //public void e2e_public_mailbox_signup_test()
        //{   
            //generate random Mailinator Email address
                //string Username = generateUniqueUsername(Driver.Current);
                //string Email = generateUniquePublicMailinatorEmail(Driver.Current);
                //string Password = generateUniquePassword(Driver.Current);
            //go to sign in
                //Driver.Goto("https://wordpress.com/start/account/user");
            //clear gdpr banner
                //Pages.GetPages.Register.AcceptCookies();
            //Pages.GetPages.WordPress.ScrollToBottom(Driver.Current);
            //Pages.GetPages.WordPress.ClickRegister(Driver.Current);
            //click sign in button         
                //Pages.GetPages.Register.CreateAccount(Email, Username, Password);      
             //switch to new tab 
                //Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to public Mailinator inbox
                //Driver.Goto("https://www.mailinator.com/v4/public/inboxes.jsp?to=" + Email);
            //open email
                //Driver.Wait.Until(drvr => Pages.GetPages.Inbox.Map.emailWP.Displayed);
                //Pages.GetPages.Inbox.openEmail(Pages.GetPages.Inbox.Map.emailWP);
            //Now switch to the email body iframe and move back afterwards                
            //note - the frame will either be html_msg_body or texthtml_msg_body 
            //so use this try catch block to try both
               // try {
               // Driver.Current.SwitchTo().Frame("html_msg_body");
              //  Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);}
               // catch (WebDriverException ) {
              //      Driver.Current.SwitchTo().DefaultContent();
              //      Driver.Current.SwitchTo().Frame("texthtml_msg_body");
              //      Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);
              //  }
            //Click on the email link 
                //Pages.GetPages.Message.clickTextLink(Driver.Current);
             //user auto-navigates to tab 3  
             //switch back to window from iframe
                //Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //wait for auto-generated password to appear
            //Driver.Wait.Until(drvr => Pages.GetPages.Password.Map.passStrengthResult.Displayed);
            //clear field and enter password
            //Pages.GetPages.Password.enterPassword(Password);
            //Pages.GetPages.Password.clickResetPasswordButton(Driver.Current);
            //login
            //Pages.GetPages.ResetPassword.clickLogin(Driver.Current);
            //Pages.GetPages.Register.Login(Email, Password);
            //allow for page to fully load
                //Driver.Wait.Until(drvr => Pages.GetPages.WordPressAdmin.Map.siteText.Displayed);
                //Pages.GetPages.WordPressAdmin.removeVerifiedEmailNotification(Driver.Current);
            //assert landed on the correct page
                //String url = Driver.Current.Url;
                //Assert.AreEqual(url, "https://wordpress.com/read?verified=1");
        //}
[Test, Category("e2e")]
                public void e2e_public_mailbox_signup_test_cleanedup()
        {   
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string Email = generateUniquePublicMailinatorEmail(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //go to sign in
            Driver.Goto("https://wordpress.com/start/account/user");
            //clear gdpr banner
            Pages.GetPages.Register.AcceptCookies();
            //click sign in button         
            Pages.GetPages.Register.CreateAccount(Email, Username, Password);      
             //switch to new tab 
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to public Mailinator inbox
            Driver.Goto("https://www.mailinator.com/v4/public/inboxes.jsp?to=" + Email);
            //open email
            Driver.Wait.Until(drvr => Pages.GetPages.Inbox.Map.emailWP.Displayed);
            Pages.GetPages.Inbox.openEmail(Pages.GetPages.Inbox.Map.emailWP);
            //Now switch to the email body iframe and move back afterwards                
            //note - the frame will either be html_msg_body or texthtml_msg_body 
            //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("html_msg_body");
            Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("texthtml_msg_body");
                Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);
            }
            //Click on the email link 
            Pages.GetPages.Message.clickTextLink(Driver.Current);
             //user auto-navigates to tab 3  
             //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //allow for page to fully load
            Driver.Wait.Until(drvr => Pages.GetPages.WordPressAdmin.Map.siteText.Displayed);
            Pages.GetPages.WordPressAdmin.removeVerifiedEmailNotification(Driver.Current);
            //assert landed on the correct page
            String url = Driver.Current.Url;
            Assert.AreEqual(url, "https://wordpress.com/read?verified=1");
        }

        [Test, Category("e2e")]
        public void e2e_private_mailbox_signup_test()
        {         
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string EmailPrefix = generateUniquePrivateMailinatorEmailPrefix(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //go to sign in
            Driver.Goto("https://wordpress.com/start/account/user");
            //clear gdpr banner
            Pages.GetPages.Register.AcceptCookies();
            //click sign in button         
            Pages.GetPages.Register.CreateAccount(EmailPrefix, Username, Password);   
             //switch to new tab 
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to inbox
            Driver.Goto("https://www.mailinator.com/");   
            Pages.GetPages.Home.clickLoginButton();
            Pages.GetPages.Login.Login(username, password);  
            //go to Mailinator
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix);   
             //open email
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix); 
            Driver.Wait.Until(drvr => Pages.GetPages.Inbox.Map.emailWP.Displayed);
            Pages.GetPages.Inbox.openEmail(Pages.GetPages.Inbox.Map.emailWP);
             //Now switch to the email body iframe  
             //AND REMEMBER TO SWITCH BACK AFTERWARDS OR YOU'LL WONDER WHY YOUR ASSERTS DON'T WORK!          
             //The frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("html_msg_body");
            Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("texthtml_msg_body");
                Driver.Wait.Until(drvr => Pages.GetPages.Message.Map.textLink.Displayed);
            }
            //Click on the email link 
            Pages.GetPages.Message.clickTextLink(Driver.Current);
            //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //allow for page to fully load
            Driver.Wait.Until(drvr => Pages.GetPages.WordPressAdmin.Map.siteText.Displayed);
            Pages.GetPages.WordPressAdmin.removeVerifiedEmailNotification(Driver.Current);
            //assert landed on the correct page
            String url = Driver.Current.Url;
            Assert.AreEqual(url, "https://wordpress.com/read?verified=1");
        }

    }
}