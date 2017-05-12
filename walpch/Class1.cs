using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.IO;

namespace walpch
{
    class Class1
    {
        private string path;
        public string[] pictures;
        public string speed;
        

        public void createfolderAndCopyImg()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\video\\avp\\errors\\";

            bool epath = System.IO.Directory.Exists(path);
            if (!epath)
            {  
                System.IO.Directory.CreateDirectory(path);
                Resource1._1.Save(path + "_1.png");
                Resource1._2.Save(path + "_2.jpg");
                Resource1._3.Save(path + "_3.jpg");
                Resource1._4.Save(path + "_4.jpg");
                Resource1._5.Save(path + "_5.jpg");

                byte[] bt = Encoding.ASCII.GetBytes("10000");
                File.WriteAllBytes(path + "conf.txt",bt);
            }
        }

        public void getpicturesAndSpeed()
        {
            pictures = Directory.GetFiles(path);
            speed = Encoding.ASCII.GetString(File.ReadAllBytes(path + "conf.txt"));            
        }
    }
}
