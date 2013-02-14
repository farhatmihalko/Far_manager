using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Security.AccessControl;

namespace F
{
   partial class observer
    {
        /**
         * Observer class
         * @x - left starting position
         * @y - top starting position
         * @width - width of working area
         * @height - height of working area
         */
        public int x;
        public int y;
        public int width;
        public int height;
        public ArrayList LL_list;
        private Panel parent;
        /**
         * Constructor
         */
        public observer(int x, int y, int width, int height, Panel parent)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.parent = parent;
            LL_list = new ArrayList();
        }
    }
   partial class observer
   {
       /**
        * Drawing the current directory
        */
       public void draw(string path)
       {
           if(@kit.isDir(path))
           {
               /*
               var permission = new FileIOPermission(FileIOPermissionAccess.Write, path);
               var permissionSet = new PermissionSet(PermissionState.None);
               
               permissionSet.AddPermission(permission);
               */

               //get the information about current directory
               DirectoryInfo current_dir = new DirectoryInfo(path);
               if (@kit.hasReadAccess(path))
               {
                   //files and directories in this dir
                   FileInfo[] files = current_dir.GetFiles();
                   DirectoryInfo[] dirs = current_dir.GetDirectories();

                   //clearing the memory
                   LL_list.Clear();

                   //params to good grid
                   int _x_ = 0;
                   int _y_ = 1;
                   int _max_height = this.height - 6;
                   bool _overloading = false;
                   int maximal = 0;

                   //adding root path to container
                   LL_list.Add(new line(this.x + _x_, this.y + _y_ - 1, this.width / 2 - 1, true, "..", this.getParent(current_dir)));

                   //get dirs
                   foreach (DirectoryInfo iteration in dirs)
                   {
                       if (_y_ > _max_height)
                       {
                           _x_ = this.width / 2 + 1;
                           _y_ = 0;
                           _overloading = true;
                       }
                       line item = new line(this.x + _x_, this.y + _y_++, this.width / 2 - 1, false, iteration.Name, iteration.FullName);

                       //Adding item to container
                       LL_list.Add(item);
                   }

                   //get files from current directory and calculate the size
                   long files_size = 0;
                   int files_number = 0;

                   foreach (FileInfo iteration in files)
                   {
                       if (_y_ > _max_height)
                       {
                           _x_ = this.width / 2 + 1;
                           _y_ = 0;
                           _overloading = true;
                       }
                       files_size += iteration.Length;
                       files_number++;
                       line item = new line(this.x + _x_, this.y + _y_++, this.width / 2 - 1, false, iteration.Name, iteration.FullName);

                       //adding item to line container
                       LL_list.Add(item);
                   }

                   //adding path to header to panel
                   this.parent.setHeader(path);

                   //adding the size to footer
                   this.parent.setFooter(files_size.ToString(), files_number);

                   //working with received files and dirs
                   this.Operate();

                   this.parent.current_path = path + @"\";
               }
               else
               {
                   this.parent.refresh();
               }
           }     
       }

       private string getParent(DirectoryInfo dir)
       {
           if (dir.Parent == null)
           {
               //this is root
               return dir.FullName;
           }
           else
               return dir.Parent.FullName;
       }
       private void Operate()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line current = (line) LL_list[i];
               if(i < this.height*2 - 10)
                   current.drawLine();
           }
       }
   }

   partial class observer
   {
       public void up()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line ll = (line) LL_list[i];
               if (ll.current)
               {
                   ll.current = false;
                   ll.changeBackground(Properties.BG);
                   line nnll = (line)LL_list[(i==0?LL_list.Count-1:i-1)];
                   nnll.current = true;
                   nnll.changeBackground(ConsoleColor.DarkCyan);
                   @kit.setBackgroundColor(Properties.BG);
                   //adding to subfooter file name
                   this.setChoosedFile(nnll);
                   break;
               }
           }
       }
       public void down()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line ll = (line)LL_list[i];
               if (ll.current)
               {
                   ll.current = false;
                   ll.changeBackground(Properties.BG);
                   line nnll = (line) LL_list[(i + 1) % LL_list.Count];
                   nnll.current = true;
                   nnll.changeBackground(ConsoleColor.DarkCyan);
                   @kit.setBackgroundColor(Properties.BG);
                   //adding to subfooter file name
                   this.setChoosedFile(nnll);
                   break;
               }
           }
       }
       public string open()
       {
           string path = "";
           bool clear = true;
           for (int i = 0; i < LL_list.Count; i++)
           {
               line cc = (line)LL_list[i];
               if (cc.current)
               {
                   if(cc.isDirectory())
                       path = cc.destination;
                   else if (cc.isFile())
                   {
                       clear = false;
                       System.Diagnostics.Process.Start("notepad", cc.destination);
                   }
               }
           }
           if (clear)
           {
               if (@kit.hasReadAccess(@path))
               {

                   for (int i = 0; i < LL_list.Count; i++)
                   {
                       line cc = (line)LL_list[i];
                       cc.clearLine(Properties.BG);
                   }
                   //clearing memory to perfomance and set new path
                   LL_list.Clear();
                   this.draw(@path);
               }
               else
               {
                   this.draw(this.parent.current_path.Substring(0, this.parent.current_path.Length - 1));
                   return this.parent.current_path.Substring(0, this.parent.current_path.Length - 1);
               }
           }
           return path;
       }
       private void setChoosedFile(line choose)
       {
           this.parent.setSubFooterName(choose.name);
           if (choose.isDirectory())
           {
               DirectoryInfo dr = new DirectoryInfo(choose.destination);
               this.parent.setSubFooterDate(dr.CreationTime.ToString());
           }
           else if (choose.isFile())
           {
               FileInfo fi = new FileInfo(choose.destination);
               this.parent.setSubFooterDate(fi.CreationTime.ToString());
           }
           
       }
   }

   partial class observer
   {
       public void removeSelection()
       {
           bool status = false;
           for (int i = 0; i < LL_list.Count; i++)
           {
               //find current selected item
               line ln = (line)LL_list[i];
               if (ln.current)
               {
                   ln.removeCurrent();
                   status = true;
               }
               if (status)
               {
                   ln.deepClear();
               }
           }
           //refresh after removing
           this.parent.refresh();
       }

       public void selectDrivers()
       {
           selectDriver driver = new selectDriver(
               this.x + this.width / 6, this.y + this.height / 3, this.width  - this.width / 3 + 4, this.y + this.height /5, this.parent.app
               );
           driver.init();
       }

       public string getSelectedItemPath()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line ll = (line)LL_list[i];
               if (ll.current)
               {
                   if (ll.isFile())
                   {
                       FileInfo ff = new FileInfo(ll.destination);
                       return ff.Name;
                   }
               }
           }
           return "-1";
       }
   }
}
