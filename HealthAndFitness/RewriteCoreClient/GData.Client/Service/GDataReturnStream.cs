#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Collections.Specialized;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// used to cover a return stream and add some additional data to it. 
    /// </summary>
    public class GDataReturnStream : Stream, ISupportsEtag
    {
        private string etag;
        private Stream innerStream;

        /// <summary>
        /// default constructor based on a gdatarequest object
        /// </summary>
        /// <param name="r"></param>
        public GDataReturnStream(IGDataRequest r)
        {
            this.innerStream = r.GetResponseStream();
            ISupportsEtag ise = r as ISupportsEtag;
            if (ise != null)
            {
                this.etag = ise.Etag;
            }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override bool CanRead
        {
            get { return this.innerStream.CanRead; }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override bool CanSeek
        {
            get { return this.innerStream.CanSeek; }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override bool CanTimeout
        {
            get { return this.innerStream.CanTimeout; }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override void Close()
        {
            this.innerStream.Close();
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override bool CanWrite
        {
            get { return this.innerStream.CanWrite; }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override long Length
        {
            get { return this.innerStream.Length; }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override long Position
        {
            get
            {
                return this.innerStream.Position;
            }
            set
            {
                this.innerStream.Position = value;
            }
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        public override void Flush()
        {
            this.innerStream.Flush();
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.innerStream.Seek(offset, origin);
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            this.innerStream.SetLength(value);
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="count"/>
        /// <param name="offset"/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.innerStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// default override, delegates to the real stream
        /// </summary>
        /// <param name="buffer"/>
        /// <param name="count"/>
        /// <param name="offset"/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.innerStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// implements the etag interface
        /// </summary>
        public string Etag
        {
            get { return this.etag; }
            set { this.etag = value; }
        }
    }
}
