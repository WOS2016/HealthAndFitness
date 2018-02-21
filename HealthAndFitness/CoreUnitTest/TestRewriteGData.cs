using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RewriteCoreClient.GData.Client;
using RewriteGDataContacts.Contacts;

namespace CoreUnitTest
{
    [TestClass]
    public class TestRewriteGData
    {
        [TestMethod]
        public void TestContactFeed()
        {

            Contact contact = new Contact();
            //RequestSettings settings = new RequestSettings("Google contacts tutorial", parameters);
            RequestSettings settings = new RequestSettings("Google contacts tutorial");
            ContactsRequest cr = new ContactsRequest(settings);

            Feed<Contact> feed = cr.GetContacts();
            foreach (Contact c in feed.Entries)
            {
                //Console.WriteLine(c.Name.FullName);
            }

            //Feed<Document> feed = r.GetDocuments();
            Feed<Group> fg = cr.GetGroups();

        }


        [TestMethod]
        public void TestRewriteCoreClient()
        {

            //Contact contact = new Contact();

            //RequestSettings settings = new RequestSettings("Google contacts tutorial");

            //ContactsRequest cr = new ContactsRequest(settings);

            //Feed<Contact> feed = cr.GetContacts();
            //foreach (Contact c in feed.Entries)
            //{
            //    Console.WriteLine(c.Name.FullName);
            //}

        }
    }
}
