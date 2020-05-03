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
            Mem m = new Mem();
            m.WriteVal(0xD4F8, 38345, 2);
            m.WriteVal(0xE08, 0, 1);
            m.WriteVal(0x1144, 0, 1);
            //m.WriteVal(0xD504, 11, 1);
            m.WriteVal(0xD560, 0, 1);
            m.WriteVal(0x10750, 78, 1); //
            m.WriteVal(0x10751, 111, 1);
            m.WriteVal(0x10752, 114, 1);
            m.WriteVal(0x10753, 109, 1);
            m.WriteVal(0x10754, 97, 1); //
            m.WriteVal(0x10755, 108, 1);
            m.WriteVal(0x10756, 0, 1);
            m.WriteVal(0x1C27E, 78, 1); //
            m.WriteVal(0x1C27F, 111, 1);
            m.WriteVal(0x1C280, 114, 1);
            m.WriteVal(0x1C281, 109, 1);
            m.WriteVal(0x1C282, 97, 1); //
            m.WriteVal(0x1C283, 108, 1);
            m.WriteVal(0x1C284, 0, 1);
        }
        public static void SetAthletic()
        {
            Mem m = new Mem();
            m.WriteVal(0xD4F8, 36804, 2);
            m.WriteVal(0xE08, 1, 1);
            m.WriteVal(0x1144, 1, 1);
            //m.WriteVal(0xD504, 12, 1);
            m.WriteVal(0xD560, 38, 1);
            m.WriteVal(0x10750, 65, 1); //
            m.WriteVal(0x10751, 116, 1);
            m.WriteVal(0x10752, 104, 1);
            m.WriteVal(0x10753, 108, 1);
            m.WriteVal(0x10754, 101, 1); //
            m.WriteVal(0x10755, 116, 1);
            m.WriteVal(0x10756, 105, 1);
            m.WriteVal(0x1C27E, 65, 1); //
            m.WriteVal(0x1C27F, 116, 1);
            m.WriteVal(0x1C280, 104, 1);
            m.WriteVal(0x1C281, 108, 1);
            m.WriteVal(0x1C282, 101, 1); //
            m.WriteVal(0x1C283, 116, 1);
            m.WriteVal(0x1C284, 105, 1);
        }

        public static void SetAggressive()
        {
            Mem m = new Mem();
            m.WriteVal(0xD4F8, 37247, 2);
            m.WriteVal(0xE08, 2, 1);
            m.WriteVal(0x1144, 4, 1);
            //m.WriteVal(0xD504, 13, 1);
            m.WriteVal(0xD560, 44, 1);
            m.WriteVal(0x10750, 65, 1); //
            m.WriteVal(0x10751, 103, 1);
            m.WriteVal(0x10752, 103, 1);
            m.WriteVal(0x10753, 114, 1);
            m.WriteVal(0x10754, 101, 1); //
            m.WriteVal(0x10755, 115, 1);
            m.WriteVal(0x10756, 115, 1);
            m.WriteVal(0x1C27E, 65, 1); //
            m.WriteVal(0x1C27F, 103, 1);
            m.WriteVal(0x1C280, 103, 1);
            m.WriteVal(0x1C281, 114, 1);
            m.WriteVal(0x1C282, 101, 1); //
            m.WriteVal(0x1C283, 115, 1);
            m.WriteVal(0x1C284, 115, 1);
        }
	    	    
        public static void SetDiscreet()
        {
            Mem m = new Mem();
            m.WriteVal(0xD4F8, 37696, 2);
            m.WriteVal(0xE08, 3, 1);
            m.WriteVal(0x1144, 3, 1);
            //m.WriteVal(0xD504, 14, 1);
            m.WriteVal(0xD560, 53, 1);

            m.WriteVal(0x10750, 68, 1); //
            m.WriteVal(0x10751, 105, 1);
            m.WriteVal(0x10752, 115, 1);
            m.WriteVal(0x10753, 99, 1);

            m.WriteVal(0x10754, 114, 1); //
            m.WriteVal(0x10755, 101, 1);
            m.WriteVal(0x10756, 101, 1);

            m.WriteVal(0x1C27E, 68, 1); //
            m.WriteVal(0x1C27F, 105, 1);
            m.WriteVal(0x1C280, 115, 1);

            m.WriteVal(0x1C281, 99, 1);
            m.WriteVal(0x1C282, 114, 1); //
            m.WriteVal(0x1C283, 101, 1);
            m.WriteVal(0x1C284, 101, 1);
        }

        private static void Set(byte mode, byte animation, ushort behaviour)
        {
            Mem m = new Mem();
            m.WriteVal(0xE08, mode, 1);
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
