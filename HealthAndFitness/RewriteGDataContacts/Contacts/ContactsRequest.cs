using System;
using System.IO;
using System.Net;
using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using RewriteGDataContacts.Contacts;
using RewriteGDataContacts.GData.Contacts;

namespace RewriteGDataContacts.Contacts
{
    public class ContactsRequest : FeedRequest<ContactsService>
    {
        /// <summary>
        /// default constructor for a YouTubeRequest
        /// </summary>
        /// <param name="settings"></param>
        public ContactsRequest(RequestSettings settings)
            : base(settings)
        {
            this.Service = new ContactsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// returns a Feed of contacts for the default user
        /// </summary>
        /// <returns>a feed of Contacts</returns>
        public Feed<Contact> GetContacts()
        {
            return GetContacts(null);
        }

        /// <summary>
        /// returns a Feed of contacts for the given user
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Contacts</returns>
        public Feed<Contact> GetContacts(string user)
        {
            ContactsQuery q = PrepareQuery<ContactsQuery>(ContactsQuery.CreateContactsUri(user));
            return PrepareFeed<Contact>(q);
        }

        /// <summary>
        /// returns a feed of Groups for the default user
        /// </summary>
        /// <returns>a feed of Groups</returns>
        public Feed<Group> GetGroups()
        {
            return GetGroups(null);
        }

        /// <summary>
        ///  returns a feed of Groups for the given user
        /// </summary>
        /// <param name="user">the user for whom to retrieve the feed</param>
        /// <returns>a feed of Groups</returns>
        public Feed<Group> GetGroups(string user)
        {
            GroupsQuery q = PrepareQuery<GroupsQuery>(GroupsQuery.CreateGroupsUri(user));
            return PrepareFeed<Group>(q);
        }

        /// <summary>
        /// returns the photo stream for a given contact. If there is no photo,
        /// the 404 is catched and null is returned.
        /// </summary>
        /// <param name="c">the contact that you want to get the photo of</param>
        /// <returns></returns>
        public Stream GetPhoto(Contact c)
        {
            Stream retStream = null;
            try
            {
                if (c.PhotoUri != null)
                {
                    retStream = this.Service.Query(c.PhotoUri, c.PhotoEtag);
                }
            }
            catch (GDataRequestException e)
            {
                HttpWebResponse r = e.Response as HttpWebResponse;
                if (r != null && r.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
            }
            return retStream;
        }

        /// <summary>
        /// sets the photo of a given contact entry
        /// </summary>
        /// <param name="c">the contact that should be modified</param>
        /// <param name="photoStream">a stream to an JPG image</param>
        /// <returns></returns>
        public void SetPhoto(Contact c, Stream photoStream)
        {
            Stream res = this.Service.StreamSend(c.PhotoUri, photoStream, GDataRequestType.Update, "image/jpg", null, c.PhotoEtag);
            GDataReturnStream r = res as GDataReturnStream;
            if (r != null)
            {
                c.PhotoEtag = r.Etag;
            }
            res.Close();
        }

        /// <summary>
        /// sets the photo of a given contact entry
        /// </summary>
        /// <param name="c">the contact that should be modified</param>
        /// <param name="photoStream">a stream to an JPG image</param>
        /// <param name="mimeType">specifies the MIME type of the image, e.g. image/jpg</param>
        /// <returns></returns>
        public void SetPhoto(Contact c, Stream photoStream, string mimeType)
        {
            Stream res = this.Service.StreamSend(c.PhotoUri, photoStream, GDataRequestType.Update, mimeType, null, c.PhotoEtag);
            res.Close();
        }
    }
}
