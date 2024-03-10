using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        const int LBA2_HCREVERSESPEEDCAP = -0x58EAB;
        const int LBA2_HCFORWARDSPEEDCAPOFFSET = -0x58E9B;
        const uint LBA2SLATE_ARRAYBASE = 0x3D657;
        const int LBA2_HCCURRENTSPEEDOFFSET = 0x52B9B;
        const uint LBA2SLATE_CURRENTMAPINDEX = 0x55BE8;
        const uint LBA2SLATE_NUMOFMAPS = 0x55BE9;
        const uint LBA2_BLOWTRON_LOCATION = 0x57BA5;
        const int LBA2_FERRY_TICKET = 0x57C0D;
        const int LBA2_RAIN0 = 0x57C59; //If 4 dinofly will give you a lift
        const int LBA2_RAIN1 = 0x57C7F; //Cut scene played after lighthouse
        const int LBA2_WIZARD_POSITION = 0x57C93;
        const int LBA2_RAIN2 = 0x57DED; //2
        const int LBA2_MAGICLEVEL = 0x57DFF;
        const int LBA2_MAGICPOINTS = 0x57E00;
        const int LBA2_HEALTH = 0x57F29;
        const int LBA2_SHOWN_FERRY_TICKET = 0x78743;
        const int LBA2_HORN_TRITON_HEALTH = 0x78805;
        const uint LBA2_MOVIE_BYTE1 = 0x57DC9;
        const uint LBA2_MOVIE_BYTE2 = 0x57DCA;
        const uint LBA2_MOVIE_BYTE3 = 0x57DCB;
        const uint LBA2_MOVIE_BYTE4 = 0x57DCC;
        const uint LBA2_MOVIE_BYTE5 = 0x57DCD;
    }
}
