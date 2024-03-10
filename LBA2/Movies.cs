using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LBAMemoryModule;

namespace LBATrainer
{
    class Movies
    {
        public Movie[] movies;
        string LBAFilesPath;
        public Movies(string LBAFilesPath)
        {
            this.LBAFilesPath = LBAFilesPath;
            Load();
        }
        private string getMovieFilePath()
        {
            return LBAFilesPath + "movies.xml";
        }

        //Load Movie List from file
        public void Load()
        {
            string filePath = getMovieFilePath();
            if (File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList nodeList;
                nodeList = doc.SelectNodes("//movie");
                movies = new Movie[nodeList.Count];
                for(int i = 0; i < nodeList.Count; i++)
                {
                    string name = nodeList[i].SelectSingleNode("name").InnerText;
                    uint offset = uint.Parse(nodeList[i].SelectSingleNode("offset").InnerText, System.Globalization.NumberStyles.HexNumber);
                    byte bitNumber = byte.Parse(nodeList[i].SelectSingleNode("bitNumber").InnerText);
                    movies[i] = new Movie(name, offset, bitNumber);
                }
            }
        }

        public class Movie
        {
            string Name;
            uint Offset;
            byte BitNumber;
            bool IsSet;
            public Movie()
            {
                ;
            }
            public Movie(string Name, uint Offset, byte BitNumber)
            {
                this.Name = Name;
                this.Offset = Offset;
                this.BitNumber = BitNumber;
            }

            public override string ToString()
            {
                return Name;
            }
            private void Toggle(bool TurnOn)
            {
                bool Enabled = IsEnabled();
                if (TurnOn && Enabled || !TurnOn && !Enabled)
                    return;
                Mem m = new Mem();
                byte CurrentValue = (byte)m.readVal(Offset, 1);
                byte BitToMask = (byte)( 1 << (BitNumber - 1));
                if(TurnOn)
                    CurrentValue = (byte)(CurrentValue ^ BitToMask);
                else
                    CurrentValue ^= (byte)(CurrentValue & BitToMask);
                m.WriteVal(Offset, CurrentValue, 1);
            }
            public void Enable()
            {
                Toggle(true);
            }
            public void Disable()
            {
                Toggle(false);
            }

            public bool IsEnabled()
            {
                Mem m = new Mem();
                byte CurrentValue = (byte)m.readVal(Offset, 1);
                return 0 < (CurrentValue & (byte)(1 << BitNumber - 1));
            }
        }
    }
}
