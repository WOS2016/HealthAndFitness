using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace RewriteCoreClient.GData.Client
{
    /// <summary>TypeConverter, so that AtomEntry shows up in the property pages
    /// </summary> 
    [ComVisible(false)]
    public class AtomEntryConverter : ExpandableObjectConverter
    {
        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomEntry))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            AtomEntry entry = value as AtomEntry;
            if (destinationType == typeof(System.String) && entry != null)
            {
                return "Entry: " + entry.Title;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
