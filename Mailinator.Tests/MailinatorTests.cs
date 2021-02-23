using System.Data;
using System.Security.Cryptography;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Mailinator.Pages;
using OpenQA.Selenium.Support.UI;
using Tests.Base;
using Framework.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Mailinator.Tests
{
    public class MailinatorTests : TestBase

    {

        [Test, Category("e2e")]
        public void private_inbox_Click_Email_Link()
        {         
            //Login to mailinator and open private inbox  
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth");
            Pages.Pages.Home.clickLoginButton();
            Pages.Pages.Login.Login(username, password);
            //Go to private inbox
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=beth");     
            Pages.Pages.Inbox.selectInbox("beth");
            //give the email time to land in the inbox
            Driver.Wait.Until(drvr => Pages.Pages.Inbox.Map.emailSW.Displayed);
            // Click on the email
            Pages.Pages.Inbox.openEmail(Pages.Pages.Inbox.Map.emailSW);
            
            // Now switch to the email body iframe
             //note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
             try {
             Driver.Current.SwitchTo().Frame("html_msg_body");
             Driver.Wait.Until(drvr => Pages.Pages.Message.Map.viewEventButton.Displayed);}
             catch (WebDriverException ) {
                 Driver.Current.SwitchTo().DefaultContent();
                 Driver.Current.SwitchTo().Frame("texthtml_msg_body");
                 Driver.Wait.Until(drvr => Pages.Pages.Message.Map.viewEventButton.Displayed);
             }
            // Click on the email link 
            Pages.Pages.Message.clickViewEvent(Driver.Current);
            // If you need to go back to the menu, don't forget to switch back:
            Driver.Current.SwitchTo().DefaultContent();
            //check a new tab has been opened
            Driver.Wait.Until(drvr => Driver.Current.WindowHandles.Count == 2);
            Assert.AreEqual(2, Driver.Current.WindowHandles.Count);
        }


[Test, Category("e2e")]
        public void public_inbox_switch_to_and_from_mailinator_tab()
        {         
            //go to public inbox
            Driver.Goto("https://www.mailinator.com/v4/public/inboxes.jsp?to=beth123");
            //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //open new URL
            Driver.Goto("https://testproject.io/platform/");
            //go back to first tab
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[0]);
            //open email
      ////      var wait = new WebDriver.CurrentWait(Driver.Current, TimeSpan.FromSeconds(10));
      ////      wait.Until(Driver.Current => inboxPage.Map.emailSW.Displayed);
            // Click on the email
            Pages.Pages.Inbox.openEmail(Pages.Pages.Inbox.Map.emailSW);
            // Now switch to the email body iframe:
            Driver.Current.SwitchTo().Frame("html_msg_body");
      ////      wait.Until(Driver.Current => messagePage.Map.viewEventButton.Displayed);
            // Click on the email link 
            Pages.Pages.Message.clickViewEvent(Driver.Current);
            // If you need to go back to the menu, don't forget to switch back:
            Driver.Current.SwitchTo().DefaultContent();
            //check a new tab has been opened
      ////      wait.Until(Driver.Current => Driver.Current.WindowHandles.Count == 3);
            Assert.AreEqual(3, Driver.Current.WindowHandles.Count);
        }

        [Test, Category("e2e")]
        public void e2e_public_mailbox_signup_test()
        {   //pre-requisites - generate pages
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string Email = generateUniquePublicMailinatorEmail(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //set wait
            var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
            //go to sign in
            Driver.Goto("https://timelesstales.in/");
            Pages.Pages.WordPress.ScrollToBottom(Driver.Current);
            Pages.Pages.WordPress.ClickRegister(Driver.Current);
            //click sign in button         
            Pages.Pages.Register.CreateAccount(Username, Email);      
             //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to public Mailinator inbox
            Driver.Goto("https://www.mailinator.com/v4/public/inboxes.jsp?to=" + Email);
            //open email
            wait.Until(driver => Pages.Pages.Inbox.Map.emailWP.Displayed);
            Pages.Pages.Inbox.openEmail(Pages.Pages.Inbox.Map.emailWP);
            //Now switch to the email body iframe and move back afterwards                
            //note - the frame will either be html_msg_body or texthtml_msg_body 
            //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("texthtml_msg_body");}
    ////        wait.Until(Driver.Current => messagePage.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("html_msg_body");
    ////            wait.Until(Driver.Current => Pages.Pages.Message.Map.textLink.Displayed);
            }
            //Click on the email link 
            Pages.Pages.Message.clickTextLink(Driver.Current);
             //user auto-navigates to tab 3  
             //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //wait for auto-generated password to appear
    ////        wait.Until(Driver.Current => Pages.Pages.Password.Map.passStrengthResult.Displayed);
            //clear field and enter password
            Pages.Pages.Password.enterPassword(Password);
            Pages.Pages.Password.clickResetPasswordButton(Driver.Current);
            //login
            Pages.Pages.ResetPassword.clickLogin(Driver.Current);
            Pages.Pages.Register.Login(Email, Password);
            //assert landed on the correct page
            String title = Driver.Current.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }





        

        [Test, Category("e2e")]
        public void e2e_private_mailbox_signup_test()
        {         
            //generate explicit wait condition
            var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
            //generate random Mailinator Email address
            string Username = generateUniqueUsername(Driver.Current);
            string EmailPrefix = generateUniquePrivateMailinatorEmailPrefix(Driver.Current);
            string Password = generateUniquePassword(Driver.Current);
            //go to sign in
            Driver.Goto("https://timelesstales.in/wp-login.php?action=register");
            //click sign in button
            Pages.Pages.Register.CreateAccount(Username, EmailPrefix);
            //switch to new tab
            Driver.Current.SwitchTo().NewWindow(WindowType.Tab);
            //go to inbox
            Driver.Goto("https://www.mailinator.com/");   
            Pages.Pages.Home.clickLoginButton();
            Pages.Pages.Login.Login(username, password);  
            //go to Mailinator
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix);   
             //open email
            Driver.Goto("https://www.mailinator.com/v4/private/inboxes.jsp?to=" + EmailPrefix); 
       ////    wait.Until(Driver.Current => Pages.Pages.Inbox.Map.emailWP.Displayed);
            Pages.Pages.Inbox.openEmail(Pages.Pages.Inbox.Map.emailWP);
             //Now switch to the email body iframe  
             // AND REMEMBER TO SWITCH BACK AFTERWARDS          
             //Note - the frame will either be html_msg_body or texthtml_msg_body 
             //so use this try catch block to try both
            try {
            Driver.Current.SwitchTo().Frame("texthtml_msg_body");}
     ////       wait.Until(Driver.Current => Pages.Pages.Message.Map.textLink.Displayed);}
            catch (WebDriverException ) {
                Driver.Current.SwitchTo().DefaultContent();
                Driver.Current.SwitchTo().Frame("html_msg_body");
      ////          wait.Until(Driver.Current => Pages.Pages.Message.Map.textLink.Displayed);
            }
            //Click on the email link 
            Pages.Pages.Message.clickTextLink(Driver.Current);
            //switch back to window from iframe
            Driver.Current.SwitchTo().Window(Driver.Current.WindowHandles[2]);
            //wait for auto-generated password to appear
      ////      wait.Until(Driver.Current => Pages.Pages.Password.Map.passStrengthResult.Displayed);
            //clear field and enter password
            Pages.Pages.Password.enterPassword(Password);
            Pages.Pages.Password.clickResetPasswordButton(Driver.Current);
            //login
            Pages.Pages.ResetPassword.clickLogin(Driver.Current);
            Pages.Pages.Register.Login(EmailPrefix, Password);
            //assert landed on the correct page
            String title = Driver.Current.Title;
            Assert.AreEqual(title, "Dashboard ‹ My Blog — WordPress");
        }

    }
}