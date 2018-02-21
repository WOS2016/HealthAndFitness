using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
//using Google.GData.Client;
//using Google.GData.Extensions;
using System.Collections.Generic;
//using Google.GData.Extensions.AppControl;
using System.Security.Cryptography;
using System.ComponentModel;
using RewriteCoreClient.GData.Client.Extensions.AppControl;

namespace RewriteCoreClient.GData.Client
{
    public abstract class Entry
    {
        private AtomEntry e;

        /// <summary>
        ///  default public constructor, needed for generics.
        /// </summary>
        /// <returns></returns>
        public Entry()
        {
        }

        /// <summary>
        /// override for ToString, returns the Entries Title
        /// </summary>
        public override string ToString()
        {
            return this.Title;
        }

        /// <summary>
        /// needs to be subclassed to ensure the creation of the corrent AtomEntry based object
        /// </summary>
        protected abstract void EnsureInnerObject();

        /// <summary>
        /// the original AtomEntry object that this object is standing in for
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("The original AtomEntry object that this object is standing in for")]
        public AtomEntry AtomEntry
        {
            get
            {
                return this.e;
            }
            set
            {
                this.e = value;
            }
        }

        /// <summary>
        /// returns the Id of an entry
        /// </summary>
        [Category("Basic Entry Data"),
        Description("The unique Id of the entry")]
        public string Id
        {
            get
            {
                EnsureInnerObject();
                return this.e.Id.AbsoluteUri;
            }
            set
            {
                EnsureInnerObject();
                this.e.Id = new AtomId(value);
            }
        }

        /// <summary>
        /// returns the value of the self uri as a string
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("The value of the self uri as a string")]
        public string Self
        {
            get
            {
                EnsureInnerObject();
                if (this.e.SelfUri != null)
                {
                    return this.e.SelfUri.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// the title of the Entry.
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("Specifies the title of the entry.")]
        public virtual string Title
        {
            get
            {
                EnsureInnerObject();
                return this.e.Title.Text;
            }
            set
            {
                EnsureInnerObject();
                this.e.Title.Text = value;
            }
        }

        /// <summary>
        /// returns the appControl sublement
        /// </summary>
        [Category("Basic Entry Data"),
        Description("The AppControl subobject.")]
        public AppControl AppControl
        {
            get
            {
                EnsureInnerObject();
                return this.e.AppControl;
            }
            set
            {
                EnsureInnerObject();
                this.e.AppControl = value;
            }
        }

        /// <summary>
        /// returns the appControl sublement
        /// </summary>
        [Category("Basic Entry Data"),
        Description("Specifies if the entry is considered a draft entry.")]
        public bool IsDraft
        {
            get
            {
                EnsureInnerObject();
                return this.e.IsDraft;
            }
        }

        /// <summary>
        /// returns true, if the entry has an edit link
        /// </summary>
        [Category("Basic Entry Data"),
        Description("If then entry has no edit uri, it is considered read only.")]
        public bool ReadOnly
        {
            get
            {
                EnsureInnerObject();
                return this.e.EditUri == null;
            }
        }

        /// <summary>
        ///  returns the first author name in the atom.entry.authors collection
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("returns the first author name in the atom.entry.authors collection.")]
        public string Author
        {
            get
            {
                EnsureInnerObject();
                if (this.e.Authors.Count > 0 && this.e.Authors[0] != null)
                {
                    return this.e.Authors[0].Name;
                }
                return null;
            }
            set
            {
                EnsureInnerObject();
                AtomPerson p = null;
                if (this.e.Authors.Count == 0)
                {
                    p = new AtomPerson(AtomPersonType.Author);
                    this.e.Authors.Add(p);
                }
                else
                {
                    p = this.e.Authors[0];
                }
                p.Name = value;
            }
        }

        /// <summary>
        /// returns the string representation of the atom.content element
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("returns the string representation of the atom.content element.")]
        public string Content
        {
            get
            {
                EnsureInnerObject();
                return this.e.Content.Content;
            }
            set
            {
                EnsureInnerObject();
                this.e.Content.Content = value;
            }
        }

        /// <summary>
        /// returns the string representation of the atom.Summary element
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("returns the string representation of the atom.Summary element.")]
        public string Summary
        {
            get
            {
                EnsureInnerObject();
                return this.e.Summary.Text;
            }
            set
            {
                EnsureInnerObject();
                this.e.Summary.Text = value;
            }
        }

        /// <summary>
        /// just a thin layer on top of the existing updated of the
        /// underlying atomentry
        /// </summary>
        [Category("Basic Entry Data"),
        Description("The datetime at which the entry was updated the last time.")]
        public DateTime Updated
        {
            get
            {
                EnsureInnerObject();
                return this.e.Updated;
            }
            set
            {
                EnsureInnerObject();
                this.e.Updated = value;
            }
        }

        /// <summary>
        /// this returns the batch data for the inner atom object
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("The batchdata subobject.")]
        public GDataBatchEntryData BatchData
        {
            get
            {
                EnsureInnerObject();
                return this.e.BatchData;
            }
            set
            {
                EnsureInnerObject();
                this.e.BatchData = value;
            }
        }

        /// <summary>
        /// returns the categories for the entry
        /// </summary>
        /// <returns></returns>
        [Category("Basic Entry Data"),
        Description("The Categories collection.")]
        public AtomCategoryCollection Categories
        {
            get
            {
                EnsureInnerObject();
                return this.e.Categories;
            }
        }

        /// <summary>access the associated media element. Note, that setting this
        /// WILL cause subsequent updates to be done using MIME multipart posts
        /// </summary>
        /// <returns> </returns>
        [Category("Media Data"),
        Description("The Mediasource subobject.")]
        public MediaSource MediaSource
        {
            get
            {
                EnsureInnerObject();
                AbstractEntry ae = this.e as AbstractEntry;

                if (ae != null)
                {
                    return ae.MediaSource;
                }

                return null;
            }
            set
            {
                EnsureInnerObject();
                AbstractEntry ae = this.e as AbstractEntry;

                if (ae != null)
                {
                    ae.MediaSource = value;
                }
                else
                {
                    throw new InvalidOperationException("The AtomEntry contained does not support Media operations");
                }
            }
        }

        /// <summary>
        /// access the associated media element. Note, that setting this
        /// WILL cause subsequent updates to be done using MIME multipart posts
        /// </summary>
        /// <returns> </returns>
        [Category("State Data"),
        Description("The etag information.")]
        public string ETag
        {
            get
            {
                EnsureInnerObject();
                AbstractEntry ae = this.e as AbstractEntry;

                if (ae != null)
                {
                    return ae.Etag;
                }

                return null;
            }
            set
            {
                EnsureInnerObject();
                AbstractEntry ae = this.e as AbstractEntry;

                if (ae != null)
                {
                    ae.Etag = value;
                }
                else
                {
                    throw new InvalidOperationException("The AtomEntry contained does not support ETags operations");
                }
            }
        }
    }
}
