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
        
        //General
        public bool alwaysOnTop;
        public string language;
        public int DefaultRefreshInterval;

        //LBA1
        public string LBA1SaveFileDirectory;
        public bool LBA1Autozoom;
        public bool LBA1GodMode;
        public bool LBA1InfMagic;
        public bool LBA1ToggleWallDamage;

        //LBA2
        public bool LBA2GodMode;
        public bool LBA2InfMagic;

        public XmlDocument doc;
        private string path;
        public Options()
        {
            readOptionsFile();
        }
        private void readOptionsFile()
        {
#pragma warning disable CA3075 // Insecure DTD processing in XML
            doc = new XmlDocument();
#pragma warning restore CA3075 // Insecure DTD processing in XML
            path = AppDomain.CurrentDomain.BaseDirectory + "files\\options.xml";
            doc.Load(path);
#pragma warning disable CA1304, CA1305 // Specify CultureInfo, Specify IFormatProvider
            //General
            alwaysOnTop = "true" == doc.DocumentElement.SelectNodes("/options/general/alwaysOnTop")[0].InnerText.Trim().ToLower();
            language = doc.DocumentElement.SelectNodes("/options/general/language")[0].InnerText.Trim().ToUpper();
            DefaultRefreshInterval = int.Parse(doc.DocumentElement.SelectNodes("/options/general/DefaultRefreshInterval")[0].InnerText.Trim());

            //LBA1
            LBA1SaveFileDirectory = doc.DocumentElement.SelectSingleNode("/options/lba1/SaveFileDirectory").InnerText.Trim();
            LBA1Autozoom =  "true" == doc.DocumentElement.SelectNodes("/options/lba1/autozoom")[0].InnerText.Trim().ToLower();
            LBA1GodMode = "true" == doc.DocumentElement.SelectNodes("/options/lba1/GodMode")[0].InnerText.Trim().ToLower();
            LBA1InfMagic = "true" == doc.DocumentElement.SelectNodes("/options/lba1/InfMagic")[0].InnerText.Trim().ToLower();
            LBA1ToggleWallDamage = "true" == doc.DocumentElement.SelectNodes("/options/lba1/toggleWallDamage")[0].InnerText.Trim().ToLower();

            //LBA2
            LBA2GodMode = "true" == doc.DocumentElement.SelectNodes("/options/lba2/GodMode")[0].InnerText.Trim().ToLower();
            LBA2InfMagic = "true" == doc.DocumentElement.SelectNodes("/options/lba2/InfMagic")[0].InnerText.Trim().ToLower();

#pragma warning restore CA1304, CA1305 // Specify CultureInfo
            //XmlNodeList nodes = doc.DocumentElement.SelectNodes("/options/LBA1/SaveFileDirectory");
        }

        public void save()
        {
#pragma warning disable CA1305 // Specify IFormatProvider
            if (null == doc) throw new Exception();
            //General
            doc.DocumentElement.SelectNodes("/options/general/alwaysOnTop")[0].InnerText = alwaysOnTop.ToString();
            doc.DocumentElement.SelectNodes("/options/general/language")[0].InnerText = language;
            doc.DocumentElement.SelectNodes("/options/general/DefaultRefreshInterval")[0].InnerText = DefaultRefreshInterval.ToString();

            //LBA1
            doc.DocumentElement.SelectNodes("/options/LBA1/SaveFileDirectory")[0].InnerText = LBA1SaveFileDirectory.Trim();
            doc.DocumentElement.SelectNodes("/options/LBA1/autozoom")[0].InnerText = LBA1Autozoom.ToString();
            doc.DocumentElement.SelectNodes("/options/LBA1/GodMode")[0].InnerText = LBA1GodMode.ToString();
            doc.DocumentElement.SelectNodes("/options/LBA1/InfMagic")[0].InnerText = LBA1InfMagic.ToString();
            doc.DocumentElement.SelectNodes("/options/LBA1/toggleWallDamage")[0].InnerText = LBA1ToggleWallDamage.ToString();


            //LBA2
            doc.DocumentElement.SelectNodes("/options/LBA2/GodMode")[0].InnerText = LBA2GodMode.ToString();
            doc.DocumentElement.SelectNodes("/options/LBA2/InfMagic")[0].InnerText = LBA2InfMagic.ToString();

            //XmlNodeList nodes = ;
            //doc.DocumentElement.SelectNodes("/options/LBA1/SaveFileDirectory")[0].InnerText = LBA1SaveFileDirectory;

            doc.Save(path);
#pragma warning restore CA1305 // Specify IFormatProvider
        }
    }
}
