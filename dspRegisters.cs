
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeaExampleNet{
    using Mcs.Usb;

    public static class ForEachExtensions
    {
        public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in enumerable)
                handler(item, idx++);
        }
    }

    public partial class DSPComms {

        static uint MAIL_BASE          =  0x1000;
        static uint REQUEST_ID         =  MAIL_BASE;
        static uint DAC_ID             =  MAIL_BASE + 0x8;
        static uint ELECTRODES         =  MAIL_BASE + 0xc;
        static uint ELECTRODES1        =  MAIL_BASE + 0xc;
        static uint ELECTRODES2        =  MAIL_BASE + 0x10;
        static uint PERIOD             =  MAIL_BASE + 0x14;
        static uint SAMPLE             =  MAIL_BASE + 0x18;
        static uint REQUEST_ACK        =  MAIL_BASE + 0x1c;

        static uint DUMP_STIM_GROUP    =  MAIL_BASE + 0x20;

        static uint DEBUG1             =  MAIL_BASE + 0x80;
        static uint DEBUG2             =  MAIL_BASE + 0x84;
        static uint DEBUG3             =  MAIL_BASE + 0x88;

        static uint DEBUG4             = DEBUG1 + 0xc;
        static uint DEBUG5             = DEBUG2 + 0xc;
        static uint DEBUG6             = DEBUG3 + 0xc;

        static uint DEBUG7             = DEBUG4 + 0xc;
        static uint DEBUG8             = DEBUG5 + 0xc;
        static uint DEBUG9             = DEBUG6 + 0xc;

        static uint DEBUG10            = DEBUG7 + 0xc;
        static uint DEBUG11            = DEBUG8 + 0xc;
        static uint DEBUG12            = DEBUG9 + 0xc;


        static uint DEBUG_DAC_SEL11    = (DEBUG12 + 0x4);
        static uint DEBUG_DAC_SEL12    = (DEBUG12 + 0x8);
        static uint DEBUG_DAC_SEL13    = (DEBUG12 + 0xc);
        static uint DEBUG_DAC_SEL14    = (DEBUG12 + 0x10);

        static uint DEBUG_DAC_SEL21    = (DEBUG_DAC_SEL14 + 0x4);
        static uint DEBUG_DAC_SEL22    = (DEBUG_DAC_SEL14 + 0x8);
        static uint DEBUG_DAC_SEL23    = (DEBUG_DAC_SEL14 + 0xc);
        static uint DEBUG_DAC_SEL24    = (DEBUG_DAC_SEL14 + 0x10);

        static uint DEBUG_DAC_SEL31    = (DEBUG_DAC_SEL24 + 0x4);
        static uint DEBUG_DAC_SEL32    = (DEBUG_DAC_SEL24 + 0x8);
        static uint DEBUG_DAC_SEL33    = (DEBUG_DAC_SEL24 + 0xc);
        static uint DEBUG_DAC_SEL34    = (DEBUG_DAC_SEL24 + 0x10);


        static uint STIM_BASE          = 0x9000;

        static uint ELECTRODE_ENABLE   = STIM_BASE + 0x158;
        static uint ELECTRODE_ENABLE1  = STIM_BASE + 0x158;
        static uint ELECTRODE_ENABLE2  = STIM_BASE + 0x15c;

        static uint ELECTRODE_MODE     = STIM_BASE + 0x120;
        static uint ELECTRODE_MODE1    = STIM_BASE + 0x120;
        static uint ELECTRODE_MODE2    = STIM_BASE + 0x124;
        static uint ELECTRODE_MODE3    = STIM_BASE + 0x128;
        static uint ELECTRODE_MODE4    = STIM_BASE + 0x12c;

        static uint ELECTRODE_DAC_SEL  = STIM_BASE + 0x160;
        static uint ELECTRODE_DAC_SEL1 = STIM_BASE + 0x160;
        static uint ELECTRODE_DAC_SEL2 = STIM_BASE + 0x164;
        static uint ELECTRODE_DAC_SEL3 = STIM_BASE + 0x168;
        static uint ELECTRODE_DAC_SEL4 = STIM_BASE + 0x16c;

        static uint TRIGGER_REPEAT1    = STIM_BASE + 0x190;
        static uint TRIGGER_REPEAT2    = STIM_BASE + 0x194;
        static uint TRIGGER_REPEAT3    = STIM_BASE + 0x198;


        static uint TRIGGER_CTRL_BASE  = 0x0200;

        static uint TRIGGER_CTRL       = TRIGGER_CTRL_BASE;
        static uint TRIGGER_CTRL1      = TRIGGER_CTRL_BASE;
        static uint START_STIM1        = TRIGGER_CTRL_BASE + 0x4;
        static uint END_STIM1          = TRIGGER_CTRL_BASE + 0x8;
        static uint WRITE_START1       = TRIGGER_CTRL_BASE + 0xc;
        static uint READ_START1        = TRIGGER_CTRL_BASE + 0x10;

        static uint TRIGGER_CTRL2      = TRIGGER_CTRL_BASE + 0x20;
        static uint START_STIM2        = TRIGGER_CTRL_BASE + 0x24;
        static uint END_STIM2          = TRIGGER_CTRL_BASE + 0x28;
        static uint WRITE_START2       = TRIGGER_CTRL_BASE + 0x2c;
        static uint READ_START2        = TRIGGER_CTRL_BASE + 0x30;

        static uint TRIGGER_CTRL3      = TRIGGER_CTRL_BASE + 0x40;
        static uint START_STIM3        = TRIGGER_CTRL_BASE + 0x44;
        static uint END_STIM3          = TRIGGER_CTRL_BASE + 0x48;
        static uint WRITE_START3       = TRIGGER_CTRL_BASE + 0x4c;
        static uint READ_START3        = TRIGGER_CTRL_BASE + 0x50;

        static uint MANUAL_TRIGGER     = TRIGGER_CTRL_BASE + 0x14;




        public uint[] readSegment(uint baseRegister, int reads)
        {
            uint[] readBuffer = new uint[reads];
            for (uint ii = 0; ii < reads; ii++)
            {
                readBuffer[ii] = dspDevice.ReadRegister(baseRegister + ii*4);
            }
            return readBuffer;
        }

        public void writeSegment(uint baseRegister, int writes, uint[] sendBuf)
        {
            for (uint ii = 0; ii < writes; ii++)
            {
                dspDevice.WriteRegister(baseRegister + ii*4, sendBuf[ii]);
            }
        }

        public void clearDebug()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                writeSegment(DEBUG1, 12, Enumerable.Repeat<uint>(0, 13).ToArray());
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }


        public void barfDebug()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                uint[] debugRegisters = readSegment(DEBUG1, 12);
                String[] asBinary = debugRegisters.Select( a => Convert.ToString( a, 2 )).ToArray();
                String[] asDecimal = debugRegisters.Select( a => Convert.ToString( a, 10 )).ToArray();
                String[] asHex = debugRegisters.Select( a => Convert.ToString( a, 16 )).ToArray();
                uint SG = dspDevice.ReadRegister(DUMP_STIM_GROUP);

                Console.WriteLine($"Debug registers for SG {SG}:");
                Console.WriteLine("--------------------");
                Console.WriteLine("As Binary");
                asBinary.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
                Console.WriteLine("As Decimal");
                asDecimal.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
                Console.WriteLine("As Hex");
                asHex.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }

        public void barfSG()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                uint[] debugRegisters = readSegment(DEBUG1, 12);
                String[] asBinary = debugRegisters.Select( a => Convert.ToString( a, 2 )).ToArray();
                String[] asDecimal = debugRegisters.Select( a => Convert.ToString( a, 10 )).ToArray();
                String[] asHex = debugRegisters.Select( a => Convert.ToString( a, 16 )).ToArray();

                Console.WriteLine("--------------------");
                Console.WriteLine($"Stim group   {asDecimal[4-1]}");
                Console.WriteLine($"electrodes 1 0b{asBinary[5-1]}");
                Console.WriteLine($"electrodes 2 0b{asBinary[6-1]}");
                Console.WriteLine($"period       {asDecimal[7-1]}");
                Console.WriteLine($"tick         {asDecimal[8-1]}");
                Console.WriteLine($"sample       {asDecimal[9-1]}");
                Console.WriteLine($"fires        {asDecimal[10-1]}");
                Console.WriteLine("--------------------");
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }

        public void barfMail()
        {

            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                uint[] debugRegisters = readSegment(MAIL_BASE, 10);
                String[] asBinary = debugRegisters.Select( a => Convert.ToString( a, 2 )).ToArray();
                String[] asDecimal = debugRegisters.Select( a => Convert.ToString( a, 10 )).ToArray();
                String[] asHex = debugRegisters.Select( a => Convert.ToString( a, 16 )).ToArray();

                Console.WriteLine($"Debug registers from MAILBOX:");
                Console.WriteLine("--------------------");
                Console.WriteLine("As Binary");
                asBinary.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
                Console.WriteLine("As Decimal");
                asDecimal.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
                Console.WriteLine("As Hex");
                asHex.ForEachWithIndex ((item, idx) => Console.WriteLine("DEBUG{0}: {1}", idx + 1, item));
            }

            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }

        public void barfDAC()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                uint[] DACdebugRegisters = readSegment(DEBUG_DAC_SEL11, 12);
                string[] asBinary = DACdebugRegisters.Select( a => Convert.ToString( a, 2 )).ToArray();

                asBinary.ForEachWithIndex( (item, idx) => Console.WriteLine("{0}{1} - {2}", (idx/4) + 1, (idx % 4) + 1, item));

                uint[] deviceDACRegisters = readSegment(ELECTRODE_DAC_SEL, 4);
                string[] DACasBinary = deviceDACRegisters.Select( a => Convert.ToString( a, 2)).ToArray();

                DACasBinary.ForEachWithIndex( (item, idx) => Console.WriteLine("DAC{0} - {1}", idx + 1, item));
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }

        public void sg_debug(uint d)
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                Console.WriteLine(d);
                Console.WriteLine(DUMP_STIM_GROUP);

                dspDevice.WriteRegister(DUMP_STIM_GROUP, d );
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }
    }
}
