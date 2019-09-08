using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LBATrainer
{
    class Options
    {
        /**
         * An object to provide a wrapper to the Options XML file
         */

        public string LBADir;
        public int LBA1TeleportTabRefreshInterval;
        public bool alwaysOnTop;
        public XmlDocument doc;
        private string path;
        public Options()
        {
            readOptionsFile();
        }
        private void readOptionsFile()
        {
            doc = new XmlDocument();
            path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\files\\options.xml";
            doc.Load(path);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/options/LBADir");
            LBADir = nodes[0].InnerText.Trim();
            LBA1TeleportTabRefreshInterval = int.Parse(doc.DocumentElement.SelectNodes("/options/LBA1TeleportTabRefreshInterval")[0].InnerText.Trim());
            alwaysOnTop = "true" == doc.DocumentElement.SelectNodes("/options/alwaysOnTop")[0].InnerText.Trim().ToLower();
        }

        public void save()
        {
            if (null == doc) throw new Exception();

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/options/LBADir");
            nodes[0].InnerText = LBADir;

            doc.DocumentElement.SelectNodes("/options/LBA1TeleportTabRefreshInterval")[0].InnerText = LBA1TeleportTabRefreshInterval.ToString();
            doc.DocumentElement.SelectNodes("/options/alwaysOnTop")[0].InnerText = alwaysOnTop.ToString();
            doc.Save(path);
        }

    }
}
