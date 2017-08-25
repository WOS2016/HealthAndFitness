using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Timers;
using System.Collections.Specialized;
using System.Configuration;

namespace WinServiceSyncFiles
{
    public class SyncFiles
    {
        static Configuration _oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //static string domain = ConfigurationManager.AppSettings["remoteDomain"];
        static readonly string SrcRoot = ConfigurationManager.AppSettings["sourceFolder"];
        static readonly string DstRoot = ConfigurationManager.AppSettings["destinationFolder"];
        //static string userID = ConfigurationManager.AppSettings["userName"];
        static readonly Timer MyTimer = new Timer();

        //static string _password = null;
        static string driveName;
        static bool connected;
        static EventLog _eventLog;

        public SyncFiles(EventLog log)
        {
            _eventLog = log;
            MyTimer.Elapsed += new ElapsedEventHandler(unmapDrive);
            MyTimer.Interval = 5000;

            //handlePassword();
        }

        public void CopyFile(System.IO.FileSystemEventArgs e)
        {
            MyTimer.Stop();
            try
            {
                string source = e.FullPath;
                bool exists = System.IO.Directory.Exists(source);
                string dest = source.Replace(SrcRoot, DstRoot);

                if (exists)
                {
                    Directory.CreateDirectory(dest);
                }
                else
                {
                    for (; ; )
                    {
                        if (!IsFileLocked(source)) continue;
                        File.Copy(source, dest, true);
                        break;
                    }

                }
            }
            catch (Exception ioe)
            {
                _eventLog.WriteEntry("FileSync Copy File failed: " + ioe.Message);
            }
            MyTimer.Start();

        }

        public void RenameFile(System.IO.RenamedEventArgs e)
        {
            MyTimer.Stop();

            try
            {
                string strOldSourceFileName = Path.GetFileName(e.OldFullPath);

                string strSourceFileName = Path.GetFileName(e.FullPath);
                bool exists = System.IO.Directory.Exists(e.OldFullPath);

                string strSourceFile = $@"{DstRoot}\\{strOldSourceFileName}";
                string strDesFilePath = $@"{DstRoot}\\{strSourceFileName}";

                if (exists)
                {
                    Directory.Move(strSourceFile, strDesFilePath);
                }
                else
                {
                    File.Move(strSourceFile, strDesFilePath);
                }
            }
            catch (Exception ioe)
            {
                _eventLog.WriteEntry("FileSync Rename File failed: " + ioe.Message);
            }

            MyTimer.Start();
        }

        public void DeleteFile(System.IO.FileSystemEventArgs e)
        {
            MyTimer.Stop();

            try
            {
                string dest = e.FullPath.Replace(SrcRoot, DstRoot);
                bool exists = System.IO.Directory.Exists(dest);
                if (exists)
                {
                    Directory.Delete(dest, true);
                }
                else
                {
                    File.Delete(dest);
                }
            }
            catch (Exception ioe)
            {
                _eventLog.WriteEntry("FileSync Delete File failed: " + ioe.Message);
            }

            MyTimer.Start();

        }

        private static bool IsFileLocked(string strSourcePath)
        {
            try
            {
                using (Stream stream = new FileStream(strSourcePath, FileMode.Open))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void unmapDrive(object source, ElapsedEventArgs e)
        {
            NetworkDrivers oNetDrive = new NetworkDrivers();
            try
            {
                //unmap the drive
                oNetDrive.Force = false;
                oNetDrive.LocalDrive = driveName;
                oNetDrive.UnMapDrive();
                connected = false;
                _eventLog.WriteEntry(driveName + " drive unmapped");
            }
            catch (Exception err)
            {
                _eventLog.WriteEntry("FileSync unmap drive failed: " + err.Message);
            }
            finally
            {
                oNetDrive = null;
            }
            MyTimer.Stop();
        }

    }
}
