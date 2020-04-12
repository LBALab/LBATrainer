using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace LBATrainer.objects
{
    class LBA2Quests
    {
        public LBA2Quest[] quests;

        public LBA2Quests(string LBAFilesPath)
        {
            string filePath = LBAFilesPath + "Quests.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/quests/quest");
            quests = getQuests(nodes);
        }

        private LBA2Quest[] getQuests(XmlNodeList nodes)
        {
            LBA2Quest[] quests = new LBA2Quest[nodes.Count];
            int i;
            for (i = 0; i < nodes.Count; i++)
            {
                //MessageBox.Show(nodes[i].SelectSingleNode("name").InnerText.Trim());
                if (nodes[i].SelectSingleNode("name").InnerText.Trim() == "Unknown") continue;
                quests[i] = getQuest(nodes[i]);
            }
            int notNull = 0;
            //Return an array of non-null quests, first let's count them
            for (i = 0; i < nodes.Count; i++)
                if (null != quests[i]) notNull++;
            LBA2Quest[] nonNullQuests = new LBA2Quest[notNull];
            for (i = 0, notNull = 0; i < quests.Count(); i++)
                if (null != quests[i])
                    nonNullQuests[notNull++] = quests[i];
            return nonNullQuests;
        }
        private LBA2Quest getQuest(XmlNode xn)
        {
            LBA2Quest quest = new LBA2Quest();
            quest.name = xn.SelectSingleNode("name").InnerText.Trim();
            
            string s = xn.SelectSingleNode("memoryOffset").InnerText.Trim();
            quest.memoryOffset = uint.Parse(s, System.Globalization.NumberStyles.HexNumber);
            quest.size = ushort.Parse(xn.SelectSingleNode("size").InnerText.Trim());
            quest.subquests = getSubquests(xn.ChildNodes.Item(xn.ChildNodes.Count-1).ChildNodes); //This is a hack
            return quest;
        }

        private LBA2Quest.Subquest[] getSubquests(XmlNodeList nodes)
        {
            LBA2Quest.Subquest[] subquests = new LBA2Quest.Subquest[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
                subquests[i] = getSubquest(nodes[i]);
            return subquests;
        }

        private LBA2Quest.Subquest getSubquest(XmlNode xn)
        {
            LBA2Quest.Subquest sq = new LBA2Quest.Subquest();
            sq.name = xn.SelectSingleNode("name").InnerText.Trim();
            sq.value = ushort.Parse(xn.SelectSingleNode("value").InnerText.Trim());
            return sq;
        }
    }
}
