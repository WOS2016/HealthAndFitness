using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RewriteCoreClient.GData.Client;
using RewriteGDataContacts.Contacts;

namespace UnitTesting
{
    [TestClass]
    class TestingRewriteGData
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
    }
}
