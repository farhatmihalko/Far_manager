using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace F
{
    /*
     * 
     * This is class describes only statis methods,
     * for working with console
     * 
     */
    class @kit
    {
        public static void setPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public static void writeChar(char x)
        {
            Console.Write(x);
        }
        public static void writeString(string x)
        {
            for (int i = 0; i < x.Length; i++)
                writeChar(x[i]);
        }
        public static void writeLine(string line)
        {
            Console.WriteLine(line);
        }
        public static void setBackgroundColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
        public static void setFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        

        //drawing methods
        public static void draw(int _x_from, int _y_from, int _x_end, int _y_end, char alpha = ' ')
        {
            for (int i = _x_from; i < _x_end; i++)
            {
                for (int j = _y_from; j < _y_end; j++)
                {
                    setPosition(i, j);
                    writeChar(alpha);
                }
            }
        }

        //working with files and directives
        public static bool isDir(string _path_)
        {
            if(Directory.Exists(_path_))
                return true;
            return false;
        }
        public static bool isFile(string _path_)
        {
            if (File.Exists(_path_))
                return true;
            return false;
        }

        // Removes an ACL entry on the specified directory for the specified account. 
        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the  
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public static bool hasReadAccess(string _path_)
        {
            if(Directory.Exists(_path_))
            {
                try
                {
                    System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(_path_);
                    DirectoryInfo d = new DirectoryInfo(_path_);
                    try
                    {
                        d.GetFiles();
                        d.GetDirectories();
                    }
                    catch(StackOverflowException e)
                    {
                        return false;
                    }
                    return true;
                }
                catch (UnauthorizedAccessException e)
                {
                    return false;
                }
            }
            return false;
        }

        public static string sizeReduce(string size)
        {
            long _size = long.Parse(size);
            string rr = "B";
            if (_size / (1024 * 1024 * 1024) >= 1)
            {
                _size /= 1024 * 1024 * 1024;
                rr = "GB";
            }
            else if (_size / (1024 * 1024) >= 1)
            {
                _size /= 1024 * 1024;
                rr = "MB";
            }
            else if (_size > 1024 - 1)
            {
                _size /= 1024;
                rr = "KB";
            }
            return _size.ToString() + rr;
        }
    }
}
