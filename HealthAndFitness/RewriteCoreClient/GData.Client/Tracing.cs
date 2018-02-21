using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Tracing helper class. Uses conditional compilation to 
    ///  exclude tracing code in release builds</summary> 
    //////////////////////////////////////////////////////////////////////
    public sealed class Tracing
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor does nothing</summary> 
        //////////////////////////////////////////////////////////////////////
        private Tracing()
        {

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default initializer, does nothing right now</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void InitTracing()
        {
            return;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Default deinitializer, closes the listener streams</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void ExitTracing()
        {
            return;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        /// <param name="msg"> msg string to display</param>
        /// <param name="startFrame">the startFrame to uses</param>
        /// <param name="indent"> intendation</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static private void TraceCall(string msg, StackFrame startFrame, int indent)
        {
            // puts out the callstack and the msg
            try
            {
                if (startFrame != null)
                {
                    StringBuilder outMsg = new StringBuilder();
                    MethodBase method = startFrame.GetMethod();

                    while (indent-- > 0)
                        outMsg.Append("    ");

                    outMsg.Append("--> " + method.DeclaringType.Name + "." + method.Name + "()");

                    if (!String.IsNullOrEmpty(msg))
                        outMsg.Append(": " + msg);

                    Tracing.TraceMsg(outMsg.ToString());
                }
                else
                {
                    Tracing.TraceMsg("Method Unknown: " + msg);
                }
                Trace.Flush();
            }
            catch
            {
                Tracing.TraceMsg(msg);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void TraceCall(string msg)
        {
            // puts out the callstack and the msg
            StackTrace trace = new StackTrace();
            if (trace != null && trace.FrameCount > 1)
            {
                StackFrame frame = trace.GetFrame(1);
                Tracing.TraceCall(msg, frame, trace.FrameCount);
            }
            else
                Tracing.TraceCall(msg, null, 0);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void TraceCall()
        {
            // puts out the callstack and the msg
            StackTrace trace = new StackTrace();
            if (trace != null && trace.FrameCount > 1)
            {
                StackFrame frame = trace.GetFrame(1);
                Tracing.TraceCall("", frame, trace.FrameCount);
            }
            else
                Tracing.TraceCall("", null, 0);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void TraceInfo(string msg)
        {
            Tracing.TraceMsg(msg);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace a message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void TraceMsg(string msg)
        {
            try
            {
                Trace.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss:ffff", CultureInfo.InvariantCulture) + "] - " + msg);
                Trace.Flush();
            }
            catch
            {
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to assert + trace a message</summary> 
        /// <param name="condition"> if false, raises assert</param>
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("TRACE")]
        static public void Assert(bool condition, string msg)
        {
            if (!condition)
            {
                Trace.WriteLine(msg);
                Trace.Assert(condition, msg);
            }
        }

    }
}
