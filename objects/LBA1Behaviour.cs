using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;

namespace LBATrainer
{
    class LBA1Behaviour
    {

        public static void SetBehaviour(byte val)
        {
            switch(val)
            {
                case 0:    SetNormal();
                           break;
                case 1:    SetAthletic();
                           break;
                case 2:    SetAggressive();
                           break;
                case 3:    SetDiscreet();
                           break;
                default: return;
            }
        }
        //0xD4F8 (2Byte)  38345   36804   37247   37696
        //0xD560		0	    38	    44	    53		    Current Animation
        public static void SetNormal()
        {
            Set(0, 38345);
        }
        public static void SetAthletic()
        {
            Set(38, 36804);
        }
        public static void SetAggressive()
        {
            Set(44, 37247);
        }
        public static void SetDiscreet()
        {
            Set(53, 37696);
        }

        private static void Set(byte animation, ushort behaviour)
        {
            Mem m = new Mem();
            m.WriteVal(0xD4F8, behaviour, 2);
            m.WriteVal(0xD560, animation, 1);
        }
        /*
Offset		Normal	Ath	    Aggr	Discreet	Desc
0xE08		0	    1	    2	    3		    Mode?
0x1144		0	    1	    4	    3		    Initial is 44
0xD4F8 (2Byte)  38345   36804   37247   37696
0xD4F8		201	    196	    127	    64		    Crashed
0xD4F9		149	    143	    145	    147		    Crashed
0xD504		11	    12	    13	    14		    Crashed	
0xD560		0	    38	    44	    53		    Current Animation	
0x10751		111	    116	    103	    105		    No Crash
0x10752		114	    104	    103 	115	    	No Crash
0x10753		109	    108	    114	    99	    	No Crash
0x10755		108	    116	    115	    101		    No Crash
0x10756		0	    105	    115 	101		    No Crash
0x1C27F		111	    116	    103 	105		    No Crash
0x1C280		114	    104	    103	    115		    No Crash
0x1C281		109	    108	    114	    99		    No Crash
0x1C283		108	    116	    115	    101		    No Crash
0x1C284		0	    105	    115	    101

Current Animation
Mode		Standing	Forward		Backward	Turn left	Turn right
Normal		0		1		2		3		4
Athletic	38		8		39		40		32
Aggressive	44		45		46		47		48
Discreet	53		54		55		56		57

*/

    }
}
