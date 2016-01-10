using System;
using System.IO;

namespace mzxrules.OcaLib.Helper
{
    public class CRC
    {
        public static void Write(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
            {
                Write(fs);
            }
        }
        public static void Write(Stream sw)
        {
            uint[] crc = new uint[2];
            byte[] data = new byte[0x00101000];
			
            uint d, r, t1, t2, t3, t4, t5, t6 = 0xDF26F436;

            t1 = t2 = t3 = t4 = t5 = t6;

            sw.Position = 0; 
            sw.Read(data, 0, 0x00101000);

            for (int i = 0x00001000; i < 0x00101000; i += 4)
            {
                d = (uint)((data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8) | data[i + 3]);
                if ((t6 + d) < t6) t4++;
                t6 += d;
                t3 ^= d;
                r = (d << (int)(d & 0x1F)) | (d >> (32 - (int)(d & 0x1F)));
                t5 += r;
                if (t2 > d) t2 ^= r;
                else t2 ^= t6 ^ d;
                t1 += (uint)((data[0x00000750 + (i & 0xFF)] << 24) | (data[0x00000751 + (i & 0xFF)] << 16) |
                      (data[0x00000752 + (i & 0xFF)] << 8) | data[0x00000753 + (i & 0xFF)]) ^ d;
            }
            crc[0] = t6 ^ t4 ^ t3;
            crc[1] = t5 ^ t2 ^ t1;

            if (BitConverter.IsLittleEndian)
            {
                crc[0] = (crc[0] >> 24) | ((crc[0] >> 8) & 0xFF00) | ((crc[0] << 8) & 0xFF0000) | ((crc[0] << 24) & 0xFF000000);
                crc[1] = (crc[1] >> 24) | ((crc[1] >> 8) & 0xFF00) | ((crc[1] << 8) & 0xFF0000) | ((crc[1] << 24) & 0xFF000000);
            }
            
            //Seek to 0x10 from rom start
            sw.Position = 0x10;
            BinaryWriter br = new BinaryWriter(sw);
            br.Write(crc[0]);
            br.Write(crc[1]);
        }
    }
}
