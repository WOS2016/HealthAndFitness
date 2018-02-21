#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;


#endregion

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>TypeConverter, so that AtomTextConstruct shows up in the property pages
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [ComVisible(false)]
    public class AtomTextConstructConverter : ExpandableObjectConverter
    {
        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomTextConstruct))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            AtomTextConstruct tc = value as AtomTextConstruct;
            if (destinationType == typeof(System.String) && tc != null)
            {
                return tc.Type + ": " + tc.Text;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
    /////////////////////////////////////////////////////////////////////////////
}
