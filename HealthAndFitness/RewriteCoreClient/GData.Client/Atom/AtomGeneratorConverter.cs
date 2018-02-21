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
    /// <summary>TypeConverter, so that AtomGenerator shows up in the property pages
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [ComVisible(false)]
    public class AtomGeneratorConverter : ExpandableObjectConverter
    {
        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomGenerator))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            AtomGenerator generator = value as AtomGenerator;
            if (destinationType == typeof(System.String) && generator != null)
            {
                return generator.Text;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
    /////////////////////////////////////////////////////////////////////////////
}
