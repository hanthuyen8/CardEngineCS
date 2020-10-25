using System;
using System.Collections.Generic;
using System.Text;

namespace engine
{
    public static class BitMan
    {
        public static uint BitPopCount(ulong n)
        {
            const ulong k0 = ~0 / 3;
            const ulong k1 = ~0 / 15 * 3;
            const ulong k2 = ~0 / 255 * 15;
            const ulong k3 = ~0 / 255;

            n = n - ((n >> 1) & k0);
            n = (n & k1) + ((n >> 2) & k1);
            n = (n + (n >> 4)) & k2;
            return (uint)(n* k3) >> (sizeof(ulong) - 1) * 8;
        }

        public static ulong ClearLowestBit(ulong n)
        {
            return n & (n - 1);
        }

        public static uint BitScanForward(ulong n)
        {
            uint[] seq = {
        0,  47, 1,  56, 48, 27, 2,  60, 57, 49, 41, 37, 28, 16, 3,  61,
        54, 58, 35, 52, 50, 42, 21, 44, 38, 32, 29, 23, 17, 11, 4,  62,
        46, 55, 26, 59, 40, 36, 15, 53, 34, 51, 20, 43, 31, 22, 10, 45,
        25, 39, 14, 33, 19, 30, 9,  24, 13, 18, 8,  12, 7,  6,  5,  63};

            return seq[( (n ^ (n - 1)) * 0x03f79d71b4cb0a89UL) >> 58];
        }
    }
}
