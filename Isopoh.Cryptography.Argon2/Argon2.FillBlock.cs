﻿// <copyright file="Argon2.FillBlock.cs" company="Isopoh">
// To the extent possible under law, the author(s) have dedicated all copyright
// and related and neighboring rights to this software to the public domain
// worldwide. This software is distributed without any warranty.
// </copyright>

namespace Isopoh.Cryptography.Argon2
{
    /// <summary>
    /// Argon2 Hashing of passwords
    /// </summary>
    public sealed partial class Argon2
    {
        private static ulong FblaMka(ulong c, ulong d)
        {
            return c + d + (2 * (c & 0xFFFFFFFF) * (d & 0xFFFFFFFF));
        }

        private static ulong Rotr64(ulong original, int bits)
        {
            return (original >> bits) | (original << (64 - bits));
        }

        private static void G(ref ulong a, ref ulong b, ref ulong c, ref ulong d)
        {
            a = FblaMka(a, b);
            d = Rotr64(d ^ a, 32);
            c = FblaMka(c, d);
            b = Rotr64(b ^ c, 24);
            a = FblaMka(a, b);
            d = Rotr64(d ^ a, 16);
            c = FblaMka(c, d);
            b = Rotr64(b ^ c, 63);
        }

        private static void G1(ref ulong a, ref ulong b, ref ulong c, ref ulong d)
        {
            a = a + b + (2 * (a & 0xFFFFFFFF) * (b & 0xFFFFFFFF));
            ulong tmp = d ^ a;
            d = (tmp >> 32) | (tmp << 32);
            c = c + d + (2 * (c & 0xFFFFFFFF) * (d & 0xFFFFFFFF));
            tmp = b ^ c;
            b = (tmp >> 24) | (tmp << 40);
            a = a + b + (2 * (a & 0xFFFFFFFF) * (b & 0xFFFFFFFF));
            tmp = d ^ a;
            d = (tmp >> 16) | (tmp << 48);
            c = c + d + (2 * (c & 0xFFFFFFFF) * (d & 0xFFFFFFFF));
            tmp = b ^ c;
            b = (tmp >> 63) | (tmp << 1);
        }


        private static void BlakeRoundNoMsg2(
            ref ulong v0,
            ref ulong v1,
            ref ulong v2,
            ref ulong v3,
            ref ulong v4,
            ref ulong v5,
            ref ulong v6,
            ref ulong v7,
            ref ulong v8,
            ref ulong v9,
            ref ulong v10,
            ref ulong v11,
            ref ulong v12,
            ref ulong v13,
            ref ulong v14,
            ref ulong v15)
        {
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            ulong tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
        }

        private static void BlakeRoundNoMsg1(
            ref ulong v0,
            ref ulong v1,
            ref ulong v2,
            ref ulong v3,
            ref ulong v4,
            ref ulong v5,
            ref ulong v6,
            ref ulong v7,
            ref ulong v8,
            ref ulong v9,
            ref ulong v10,
            ref ulong v11,
            ref ulong v12,
            ref ulong v13,
            ref ulong v14,
            ref ulong v15)
        {
            G1(ref v0, ref v4, ref v8, ref v12);
            G1(ref v1, ref v5, ref v9, ref v13);
            G1(ref v2, ref v6, ref v10, ref v14);
            G1(ref v3, ref v7, ref v11, ref v15);
            G1(ref v0, ref v5, ref v10, ref v15);
            G1(ref v1, ref v6, ref v11, ref v12);
            G1(ref v2, ref v7, ref v8, ref v13);
            G1(ref v3, ref v4, ref v9, ref v14);
        }

        private static void BlakeRoundNoMsg(
            ref ulong v0,
            ref ulong v1,
            ref ulong v2,
            ref ulong v3,
            ref ulong v4,
            ref ulong v5,
            ref ulong v6,
            ref ulong v7,
            ref ulong v8,
            ref ulong v9,
            ref ulong v10,
            ref ulong v11,
            ref ulong v12,
            ref ulong v13,
            ref ulong v14,
            ref ulong v15)
        {
            G(ref v0, ref v4, ref v8, ref v12);
            G(ref v1, ref v5, ref v9, ref v13);
            G(ref v2, ref v6, ref v10, ref v14);
            G(ref v3, ref v7, ref v11, ref v15);
            G(ref v0, ref v5, ref v10, ref v15);
            G(ref v1, ref v6, ref v11, ref v12);
            G(ref v2, ref v7, ref v8, ref v13);
            G(ref v3, ref v4, ref v9, ref v14);
        }

        private static void Blake2ColumnRoundNoMsg2(BlockValues blockR, int i7)
        {
            ulong v0;
            ulong v1;
            ulong v2;
            ulong v3;
            ulong v4;
            ulong v5;
            ulong v6;
            ulong v7;
            ulong v8;
            ulong v9;
            ulong v10;
            ulong v11;
            ulong v12;
            ulong v13;
            ulong v14;
            ulong v15;

            v0 = blockR[16 * i7];
            v1 = blockR[(16 * i7) + 1];
            v2 = blockR[(16 * i7) + 2];
            v3 = blockR[(16 * i7) + 3];
            v4 = blockR[(16 * i7) + 4];
            v5 = blockR[(16 * i7) + 5];
            v6 = blockR[(16 * i7) + 6];
            v7 = blockR[(16 * i7) + 7];
            v8 = blockR[(16 * i7) + 8];
            v9 = blockR[(16 * i7) + 9];
            v10 = blockR[(16 * i7) + 10];
            v11 = blockR[(16 * i7) + 11];
            v12 = blockR[(16 * i7) + 12];
            v13 = blockR[(16 * i7) + 13];
            v14 = blockR[(16 * i7) + 14];
            v15 = blockR[(16 * i7) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            ulong tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * i7] = v0;
            blockR[(16 * i7) + 1] = v1;
            blockR[(16 * i7) + 2] = v2;
            blockR[(16 * i7) + 3] = v3;
            blockR[(16 * i7) + 4] = v4;
            blockR[(16 * i7) + 5] = v5;
            blockR[(16 * i7) + 6] = v6;
            blockR[(16 * i7) + 7] = v7;
            blockR[(16 * i7) + 8] = v8;
            blockR[(16 * i7) + 9] = v9;
            blockR[(16 * i7) + 10] = v10;
            blockR[(16 * i7) + 11] = v11;
            blockR[(16 * i7) + 12] = v12;
            blockR[(16 * i7) + 13] = v13;
            blockR[(16 * i7) + 14] = v14;
            blockR[(16 * i7) + 15] = v15;
        }

        private static void Blake2RowRoundNoMsg2(BlockValues blockR, int I7)
        {
            ulong v0;
            ulong v1;
            ulong v2;
            ulong v3;
            ulong v4;
            ulong v5;
            ulong v6;
            ulong v7;
            ulong v8;
            ulong v9;
            ulong v10;
            ulong v11;
            ulong v12;
            ulong v13;
            ulong v14;
            ulong v15;
            ulong tmp;
            v0 = blockR[2 * I7];
            v1 = blockR[(2 * I7) + 1];
            v2 = blockR[(2 * I7) + 16];
            v3 = blockR[(2 * I7) + 17];
            v4 = blockR[(2 * I7) + 32];
            v5 = blockR[(2 * I7) + 33];
            v6 = blockR[(2 * I7) + 48];
            v7 = blockR[(2 * I7) + 49];
            v8 = blockR[(2 * I7) + 64];
            v9 = blockR[(2 * I7) + 65];
            v10 = blockR[(2 * I7) + 80];
            v11 = blockR[(2 * I7) + 81];
            v12 = blockR[(2 * I7) + 96];
            v13 = blockR[(2 * I7) + 97];
            v14 = blockR[(2 * I7) + 112];
            v15 = blockR[(2 * I7) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I7] = v0;
            blockR[(2 * I7) + 1] = v1;
            blockR[(2 * I7) + 16] = v2;
            blockR[(2 * I7) + 17] = v3;
            blockR[(2 * I7) + 32] = v4;
            blockR[(2 * I7) + 33] = v5;
            blockR[(2 * I7) + 48] = v6;
            blockR[(2 * I7) + 49] = v7;
            blockR[(2 * I7) + 64] = v8;
            blockR[(2 * I7) + 65] = v9;
            blockR[(2 * I7) + 80] = v10;
            blockR[(2 * I7) + 81] = v11;
            blockR[(2 * I7) + 96] = v12;
            blockR[(2 * I7) + 97] = v13;
            blockR[(2 * I7) + 112] = v14;
            blockR[(2 * I7) + 113] = v15;
        }

        private static void Blake2ColumnRoundNoMsg(BlockValues blockR, int i)
        {
            ulong v0 = blockR[16 * i];
            ulong v1 = blockR[(16 * i) + 1];
            ulong v2 = blockR[(16 * i) + 2];
            ulong v3 = blockR[(16 * i) + 3];
            ulong v4 = blockR[(16 * i) + 4];
            ulong v5 = blockR[(16 * i) + 5];
            ulong v6 = blockR[(16 * i) + 6];
            ulong v7 = blockR[(16 * i) + 7];
            ulong v8 = blockR[(16 * i) + 8];
            ulong v9 = blockR[(16 * i) + 9];
            ulong v10 = blockR[(16 * i) + 10];
            ulong v11 = blockR[(16 * i) + 11];
            ulong v12 = blockR[(16 * i) + 12];
            ulong v13 = blockR[(16 * i) + 13];
            ulong v14 = blockR[(16 * i) + 14];
            ulong v15 = blockR[(16 * i) + 15];
            BlakeRoundNoMsg(
                ref v0,
                ref v1,
                ref v2,
                ref v3,
                ref v4,
                ref v5,
                ref v6,
                ref v7,
                ref v8,
                ref v9,
                ref v10,
                ref v11,
                ref v12,
                ref v13,
                ref v14,
                ref v15);
            blockR[16 * i] = v0;
            blockR[(16 * i) + 1] = v1;
            blockR[(16 * i) + 2] = v2;
            blockR[(16 * i) + 3] = v3;
            blockR[(16 * i) + 4] = v4;
            blockR[(16 * i) + 5] = v5;
            blockR[(16 * i) + 6] = v6;
            blockR[(16 * i) + 7] = v7;
            blockR[(16 * i) + 8] = v8;
            blockR[(16 * i) + 9] = v9;
            blockR[(16 * i) + 10] = v10;
            blockR[(16 * i) + 11] = v11;
            blockR[(16 * i) + 12] = v12;
            blockR[(16 * i) + 13] = v13;
            blockR[(16 * i) + 14] = v14;
            blockR[(16 * i) + 15] = v15;
        }

        private static void Blake2RowRoundNoMsg(BlockValues blockR, int i)
        {
            ulong v0 = blockR[2 * i];
            ulong v1 = blockR[(2 * i) + 1];
            ulong v2 = blockR[(2 * i) + 16];
            ulong v3 = blockR[(2 * i) + 17];
            ulong v4 = blockR[(2 * i) + 32];
            ulong v5 = blockR[(2 * i) + 33];
            ulong v6 = blockR[(2 * i) + 48];
            ulong v7 = blockR[(2 * i) + 49];
            ulong v8 = blockR[(2 * i) + 64];
            ulong v9 = blockR[(2 * i) + 65];
            ulong v10 = blockR[(2 * i) + 80];
            ulong v11 = blockR[(2 * i) + 81];
            ulong v12 = blockR[(2 * i) + 96];
            ulong v13 = blockR[(2 * i) + 97];
            ulong v14 = blockR[(2 * i) + 112];
            ulong v15 = blockR[(2 * i) + 113];
            BlakeRoundNoMsg(
                ref v0,
                ref v1,
                ref v2,
                ref v3,
                ref v4,
                ref v5,
                ref v6,
                ref v7,
                ref v8,
                ref v9,
                ref v10,
                ref v11,
                ref v12,
                ref v13,
                ref v14,
                ref v15);
            blockR[2 * i] = v0;
            blockR[(2 * i) + 1] = v1;
            blockR[(2 * i) + 16] = v2;
            blockR[(2 * i) + 17] = v3;
            blockR[(2 * i) + 32] = v4;
            blockR[(2 * i) + 33] = v5;
            blockR[(2 * i) + 48] = v6;
            blockR[(2 * i) + 49] = v7;
            blockR[(2 * i) + 64] = v8;
            blockR[(2 * i) + 65] = v9;
            blockR[(2 * i) + 80] = v10;
            blockR[(2 * i) + 81] = v11;
            blockR[(2 * i) + 96] = v12;
            blockR[(2 * i) + 97] = v13;
            blockR[(2 * i) + 112] = v14;
            blockR[(2 * i) + 113] = v15;
        }

        private static void Blake2RowAndColumnRoundsNoMsg2(BlockValues blockR)
        {
            // apply Blake2 on columns of 64-bit words:
            //    (0,1,...,15), then
            //    (16,17,..31)... finally
            //    (112,113,...127)
            const int I0 = 0;
            const int I1 = 1;
            const int I2 = 2;
            const int I3 = 3;
            const int I4 = 4;
            const int I5 = 5;
            const int I6 = 6;
            const int I7 = 7;

            ulong tmp;
            ulong v0 = blockR[16 * I0];
            ulong v1 = blockR[(16 * I0) + 1];
            ulong v2 = blockR[(16 * I0) + 2];
            ulong v3 = blockR[(16 * I0) + 3];
            ulong v4 = blockR[(16 * I0) + 4];
            ulong v5 = blockR[(16 * I0) + 5];
            ulong v6 = blockR[(16 * I0) + 6];
            ulong v7 = blockR[(16 * I0) + 7];
            ulong v8 = blockR[(16 * I0) + 8];
            ulong v9 = blockR[(16 * I0) + 9];
            ulong v10 = blockR[(16 * I0) + 10];
            ulong v11 = blockR[(16 * I0) + 11];
            ulong v12 = blockR[(16 * I0) + 12];
            ulong v13 = blockR[(16 * I0) + 13];
            ulong v14 = blockR[(16 * I0) + 14];
            ulong v15 = blockR[(16 * I0) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I0] = v0;
            blockR[(16 * I0) + 1] = v1;
            blockR[(16 * I0) + 2] = v2;
            blockR[(16 * I0) + 3] = v3;
            blockR[(16 * I0) + 4] = v4;
            blockR[(16 * I0) + 5] = v5;
            blockR[(16 * I0) + 6] = v6;
            blockR[(16 * I0) + 7] = v7;
            blockR[(16 * I0) + 8] = v8;
            blockR[(16 * I0) + 9] = v9;
            blockR[(16 * I0) + 10] = v10;
            blockR[(16 * I0) + 11] = v11;
            blockR[(16 * I0) + 12] = v12;
            blockR[(16 * I0) + 13] = v13;
            blockR[(16 * I0) + 14] = v14;
            blockR[(16 * I0) + 15] = v15;

            v0 = blockR[16 * I1];
            v1 = blockR[(16 * I1) + 1];
            v2 = blockR[(16 * I1) + 2];
            v3 = blockR[(16 * I1) + 3];
            v4 = blockR[(16 * I1) + 4];
            v5 = blockR[(16 * I1) + 5];
            v6 = blockR[(16 * I1) + 6];
            v7 = blockR[(16 * I1) + 7];
            v8 = blockR[(16 * I1) + 8];
            v9 = blockR[(16 * I1) + 9];
            v10 = blockR[(16 * I1) + 10];
            v11 = blockR[(16 * I1) + 11];
            v12 = blockR[(16 * I1) + 12];
            v13 = blockR[(16 * I1) + 13];
            v14 = blockR[(16 * I1) + 14];
            v15 = blockR[(16 * I1) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I1] = v0;
            blockR[(16 * I1) + 1] = v1;
            blockR[(16 * I1) + 2] = v2;
            blockR[(16 * I1) + 3] = v3;
            blockR[(16 * I1) + 4] = v4;
            blockR[(16 * I1) + 5] = v5;
            blockR[(16 * I1) + 6] = v6;
            blockR[(16 * I1) + 7] = v7;
            blockR[(16 * I1) + 8] = v8;
            blockR[(16 * I1) + 9] = v9;
            blockR[(16 * I1) + 10] = v10;
            blockR[(16 * I1) + 11] = v11;
            blockR[(16 * I1) + 12] = v12;
            blockR[(16 * I1) + 13] = v13;
            blockR[(16 * I1) + 14] = v14;
            blockR[(16 * I1) + 15] = v15;

            v0 = blockR[16 * I2];
            v1 = blockR[(16 * I2) + 1];
            v2 = blockR[(16 * I2) + 2];
            v3 = blockR[(16 * I2) + 3];
            v4 = blockR[(16 * I2) + 4];
            v5 = blockR[(16 * I2) + 5];
            v6 = blockR[(16 * I2) + 6];
            v7 = blockR[(16 * I2) + 7];
            v8 = blockR[(16 * I2) + 8];
            v9 = blockR[(16 * I2) + 9];
            v10 = blockR[(16 * I2) + 10];
            v11 = blockR[(16 * I2) + 11];
            v12 = blockR[(16 * I2) + 12];
            v13 = blockR[(16 * I2) + 13];
            v14 = blockR[(16 * I2) + 14];
            v15 = blockR[(16 * I2) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I2] = v0;
            blockR[(16 * I2) + 1] = v1;
            blockR[(16 * I2) + 2] = v2;
            blockR[(16 * I2) + 3] = v3;
            blockR[(16 * I2) + 4] = v4;
            blockR[(16 * I2) + 5] = v5;
            blockR[(16 * I2) + 6] = v6;
            blockR[(16 * I2) + 7] = v7;
            blockR[(16 * I2) + 8] = v8;
            blockR[(16 * I2) + 9] = v9;
            blockR[(16 * I2) + 10] = v10;
            blockR[(16 * I2) + 11] = v11;
            blockR[(16 * I2) + 12] = v12;
            blockR[(16 * I2) + 13] = v13;
            blockR[(16 * I2) + 14] = v14;
            blockR[(16 * I2) + 15] = v15;

            v0 = blockR[16 * I3];
            v1 = blockR[(16 * I3) + 1];
            v2 = blockR[(16 * I3) + 2];
            v3 = blockR[(16 * I3) + 3];
            v4 = blockR[(16 * I3) + 4];
            v5 = blockR[(16 * I3) + 5];
            v6 = blockR[(16 * I3) + 6];
            v7 = blockR[(16 * I3) + 7];
            v8 = blockR[(16 * I3) + 8];
            v9 = blockR[(16 * I3) + 9];
            v10 = blockR[(16 * I3) + 10];
            v11 = blockR[(16 * I3) + 11];
            v12 = blockR[(16 * I3) + 12];
            v13 = blockR[(16 * I3) + 13];
            v14 = blockR[(16 * I3) + 14];
            v15 = blockR[(16 * I3) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I3] = v0;
            blockR[(16 * I3) + 1] = v1;
            blockR[(16 * I3) + 2] = v2;
            blockR[(16 * I3) + 3] = v3;
            blockR[(16 * I3) + 4] = v4;
            blockR[(16 * I3) + 5] = v5;
            blockR[(16 * I3) + 6] = v6;
            blockR[(16 * I3) + 7] = v7;
            blockR[(16 * I3) + 8] = v8;
            blockR[(16 * I3) + 9] = v9;
            blockR[(16 * I3) + 10] = v10;
            blockR[(16 * I3) + 11] = v11;
            blockR[(16 * I3) + 12] = v12;
            blockR[(16 * I3) + 13] = v13;
            blockR[(16 * I3) + 14] = v14;
            blockR[(16 * I3) + 15] = v15;

            v0 = blockR[16 * I4];
            v1 = blockR[(16 * I4) + 1];
            v2 = blockR[(16 * I4) + 2];
            v3 = blockR[(16 * I4) + 3];
            v4 = blockR[(16 * I4) + 4];
            v5 = blockR[(16 * I4) + 5];
            v6 = blockR[(16 * I4) + 6];
            v7 = blockR[(16 * I4) + 7];
            v8 = blockR[(16 * I4) + 8];
            v9 = blockR[(16 * I4) + 9];
            v10 = blockR[(16 * I4) + 10];
            v11 = blockR[(16 * I4) + 11];
            v12 = blockR[(16 * I4) + 12];
            v13 = blockR[(16 * I4) + 13];
            v14 = blockR[(16 * I4) + 14];
            v15 = blockR[(16 * I4) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I4] = v0;
            blockR[(16 * I4) + 1] = v1;
            blockR[(16 * I4) + 2] = v2;
            blockR[(16 * I4) + 3] = v3;
            blockR[(16 * I4) + 4] = v4;
            blockR[(16 * I4) + 5] = v5;
            blockR[(16 * I4) + 6] = v6;
            blockR[(16 * I4) + 7] = v7;
            blockR[(16 * I4) + 8] = v8;
            blockR[(16 * I4) + 9] = v9;
            blockR[(16 * I4) + 10] = v10;
            blockR[(16 * I4) + 11] = v11;
            blockR[(16 * I4) + 12] = v12;
            blockR[(16 * I4) + 13] = v13;
            blockR[(16 * I4) + 14] = v14;
            blockR[(16 * I4) + 15] = v15;

            v0 = blockR[16 * I5];
            v1 = blockR[(16 * I5) + 1];
            v2 = blockR[(16 * I5) + 2];
            v3 = blockR[(16 * I5) + 3];
            v4 = blockR[(16 * I5) + 4];
            v5 = blockR[(16 * I5) + 5];
            v6 = blockR[(16 * I5) + 6];
            v7 = blockR[(16 * I5) + 7];
            v8 = blockR[(16 * I5) + 8];
            v9 = blockR[(16 * I5) + 9];
            v10 = blockR[(16 * I5) + 10];
            v11 = blockR[(16 * I5) + 11];
            v12 = blockR[(16 * I5) + 12];
            v13 = blockR[(16 * I5) + 13];
            v14 = blockR[(16 * I5) + 14];
            v15 = blockR[(16 * I5) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I5] = v0;
            blockR[(16 * I5) + 1] = v1;
            blockR[(16 * I5) + 2] = v2;
            blockR[(16 * I5) + 3] = v3;
            blockR[(16 * I5) + 4] = v4;
            blockR[(16 * I5) + 5] = v5;
            blockR[(16 * I5) + 6] = v6;
            blockR[(16 * I5) + 7] = v7;
            blockR[(16 * I5) + 8] = v8;
            blockR[(16 * I5) + 9] = v9;
            blockR[(16 * I5) + 10] = v10;
            blockR[(16 * I5) + 11] = v11;
            blockR[(16 * I5) + 12] = v12;
            blockR[(16 * I5) + 13] = v13;
            blockR[(16 * I5) + 14] = v14;
            blockR[(16 * I5) + 15] = v15;

            v0 = blockR[16 * I6];
            v1 = blockR[(16 * I6) + 1];
            v2 = blockR[(16 * I6) + 2];
            v3 = blockR[(16 * I6) + 3];
            v4 = blockR[(16 * I6) + 4];
            v5 = blockR[(16 * I6) + 5];
            v6 = blockR[(16 * I6) + 6];
            v7 = blockR[(16 * I6) + 7];
            v8 = blockR[(16 * I6) + 8];
            v9 = blockR[(16 * I6) + 9];
            v10 = blockR[(16 * I6) + 10];
            v11 = blockR[(16 * I6) + 11];
            v12 = blockR[(16 * I6) + 12];
            v13 = blockR[(16 * I6) + 13];
            v14 = blockR[(16 * I6) + 14];
            v15 = blockR[(16 * I6) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I6] = v0;
            blockR[(16 * I6) + 1] = v1;
            blockR[(16 * I6) + 2] = v2;
            blockR[(16 * I6) + 3] = v3;
            blockR[(16 * I6) + 4] = v4;
            blockR[(16 * I6) + 5] = v5;
            blockR[(16 * I6) + 6] = v6;
            blockR[(16 * I6) + 7] = v7;
            blockR[(16 * I6) + 8] = v8;
            blockR[(16 * I6) + 9] = v9;
            blockR[(16 * I6) + 10] = v10;
            blockR[(16 * I6) + 11] = v11;
            blockR[(16 * I6) + 12] = v12;
            blockR[(16 * I6) + 13] = v13;
            blockR[(16 * I6) + 14] = v14;
            blockR[(16 * I6) + 15] = v15;

            v0 = blockR[16 * I7];
            v1 = blockR[(16 * I7) + 1];
            v2 = blockR[(16 * I7) + 2];
            v3 = blockR[(16 * I7) + 3];
            v4 = blockR[(16 * I7) + 4];
            v5 = blockR[(16 * I7) + 5];
            v6 = blockR[(16 * I7) + 6];
            v7 = blockR[(16 * I7) + 7];
            v8 = blockR[(16 * I7) + 8];
            v9 = blockR[(16 * I7) + 9];
            v10 = blockR[(16 * I7) + 10];
            v11 = blockR[(16 * I7) + 11];
            v12 = blockR[(16 * I7) + 12];
            v13 = blockR[(16 * I7) + 13];
            v14 = blockR[(16 * I7) + 14];
            v15 = blockR[(16 * I7) + 15];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[16 * I7] = v0;
            blockR[(16 * I7) + 1] = v1;
            blockR[(16 * I7) + 2] = v2;
            blockR[(16 * I7) + 3] = v3;
            blockR[(16 * I7) + 4] = v4;
            blockR[(16 * I7) + 5] = v5;
            blockR[(16 * I7) + 6] = v6;
            blockR[(16 * I7) + 7] = v7;
            blockR[(16 * I7) + 8] = v8;
            blockR[(16 * I7) + 9] = v9;
            blockR[(16 * I7) + 10] = v10;
            blockR[(16 * I7) + 11] = v11;
            blockR[(16 * I7) + 12] = v12;
            blockR[(16 * I7) + 13] = v13;
            blockR[(16 * I7) + 14] = v14;
            blockR[(16 * I7) + 15] = v15;

            // Apply Blake2 on rows of 64-bit words:
            // (0,1,16,17,...112,113), then
            // (2,3,18,19,...,114,115).. finally
            // (14,15,30,31,...,126,127)
            v0 = blockR[2 * I0];
            v1 = blockR[(2 * I0) + 1];
            v2 = blockR[(2 * I0) + 16];
            v3 = blockR[(2 * I0) + 17];
            v4 = blockR[(2 * I0) + 32];
            v5 = blockR[(2 * I0) + 33];
            v6 = blockR[(2 * I0) + 48];
            v7 = blockR[(2 * I0) + 49];
            v8 = blockR[(2 * I0) + 64];
            v9 = blockR[(2 * I0) + 65];
            v10 = blockR[(2 * I0) + 80];
            v11 = blockR[(2 * I0) + 81];
            v12 = blockR[(2 * I0) + 96];
            v13 = blockR[(2 * I0) + 97];
            v14 = blockR[(2 * I0) + 112];
            v15 = blockR[(2 * I0) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I0] = v0;
            blockR[(2 * I0) + 1] = v1;
            blockR[(2 * I0) + 16] = v2;
            blockR[(2 * I0) + 17] = v3;
            blockR[(2 * I0) + 32] = v4;
            blockR[(2 * I0) + 33] = v5;
            blockR[(2 * I0) + 48] = v6;
            blockR[(2 * I0) + 49] = v7;
            blockR[(2 * I0) + 64] = v8;
            blockR[(2 * I0) + 65] = v9;
            blockR[(2 * I0) + 80] = v10;
            blockR[(2 * I0) + 81] = v11;
            blockR[(2 * I0) + 96] = v12;
            blockR[(2 * I0) + 97] = v13;
            blockR[(2 * I0) + 112] = v14;
            blockR[(2 * I0) + 113] = v15;

            v0 = blockR[2 * I1];
            v1 = blockR[(2 * I1) + 1];
            v2 = blockR[(2 * I1) + 16];
            v3 = blockR[(2 * I1) + 17];
            v4 = blockR[(2 * I1) + 32];
            v5 = blockR[(2 * I1) + 33];
            v6 = blockR[(2 * I1) + 48];
            v7 = blockR[(2 * I1) + 49];
            v8 = blockR[(2 * I1) + 64];
            v9 = blockR[(2 * I1) + 65];
            v10 = blockR[(2 * I1) + 80];
            v11 = blockR[(2 * I1) + 81];
            v12 = blockR[(2 * I1) + 96];
            v13 = blockR[(2 * I1) + 97];
            v14 = blockR[(2 * I1) + 112];
            v15 = blockR[(2 * I1) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I1] = v0;
            blockR[(2 * I1) + 1] = v1;
            blockR[(2 * I1) + 16] = v2;
            blockR[(2 * I1) + 17] = v3;
            blockR[(2 * I1) + 32] = v4;
            blockR[(2 * I1) + 33] = v5;
            blockR[(2 * I1) + 48] = v6;
            blockR[(2 * I1) + 49] = v7;
            blockR[(2 * I1) + 64] = v8;
            blockR[(2 * I1) + 65] = v9;
            blockR[(2 * I1) + 80] = v10;
            blockR[(2 * I1) + 81] = v11;
            blockR[(2 * I1) + 96] = v12;
            blockR[(2 * I1) + 97] = v13;
            blockR[(2 * I1) + 112] = v14;
            blockR[(2 * I1) + 113] = v15;

            v0 = blockR[2 * I2];
            v1 = blockR[(2 * I2) + 1];
            v2 = blockR[(2 * I2) + 16];
            v3 = blockR[(2 * I2) + 17];
            v4 = blockR[(2 * I2) + 32];
            v5 = blockR[(2 * I2) + 33];
            v6 = blockR[(2 * I2) + 48];
            v7 = blockR[(2 * I2) + 49];
            v8 = blockR[(2 * I2) + 64];
            v9 = blockR[(2 * I2) + 65];
            v10 = blockR[(2 * I2) + 80];
            v11 = blockR[(2 * I2) + 81];
            v12 = blockR[(2 * I2) + 96];
            v13 = blockR[(2 * I2) + 97];
            v14 = blockR[(2 * I2) + 112];
            v15 = blockR[(2 * I2) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I2] = v0;
            blockR[(2 * I2) + 1] = v1;
            blockR[(2 * I2) + 16] = v2;
            blockR[(2 * I2) + 17] = v3;
            blockR[(2 * I2) + 32] = v4;
            blockR[(2 * I2) + 33] = v5;
            blockR[(2 * I2) + 48] = v6;
            blockR[(2 * I2) + 49] = v7;
            blockR[(2 * I2) + 64] = v8;
            blockR[(2 * I2) + 65] = v9;
            blockR[(2 * I2) + 80] = v10;
            blockR[(2 * I2) + 81] = v11;
            blockR[(2 * I2) + 96] = v12;
            blockR[(2 * I2) + 97] = v13;
            blockR[(2 * I2) + 112] = v14;
            blockR[(2 * I2) + 113] = v15;

            v0 = blockR[2 * I3];
            v1 = blockR[(2 * I3) + 1];
            v2 = blockR[(2 * I3) + 16];
            v3 = blockR[(2 * I3) + 17];
            v4 = blockR[(2 * I3) + 32];
            v5 = blockR[(2 * I3) + 33];
            v6 = blockR[(2 * I3) + 48];
            v7 = blockR[(2 * I3) + 49];
            v8 = blockR[(2 * I3) + 64];
            v9 = blockR[(2 * I3) + 65];
            v10 = blockR[(2 * I3) + 80];
            v11 = blockR[(2 * I3) + 81];
            v12 = blockR[(2 * I3) + 96];
            v13 = blockR[(2 * I3) + 97];
            v14 = blockR[(2 * I3) + 112];
            v15 = blockR[(2 * I3) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I3] = v0;
            blockR[(2 * I3) + 1] = v1;
            blockR[(2 * I3) + 16] = v2;
            blockR[(2 * I3) + 17] = v3;
            blockR[(2 * I3) + 32] = v4;
            blockR[(2 * I3) + 33] = v5;
            blockR[(2 * I3) + 48] = v6;
            blockR[(2 * I3) + 49] = v7;
            blockR[(2 * I3) + 64] = v8;
            blockR[(2 * I3) + 65] = v9;
            blockR[(2 * I3) + 80] = v10;
            blockR[(2 * I3) + 81] = v11;
            blockR[(2 * I3) + 96] = v12;
            blockR[(2 * I3) + 97] = v13;
            blockR[(2 * I3) + 112] = v14;
            blockR[(2 * I3) + 113] = v15;

            v0 = blockR[2 * I4];
            v1 = blockR[(2 * I4) + 1];
            v2 = blockR[(2 * I4) + 16];
            v3 = blockR[(2 * I4) + 17];
            v4 = blockR[(2 * I4) + 32];
            v5 = blockR[(2 * I4) + 33];
            v6 = blockR[(2 * I4) + 48];
            v7 = blockR[(2 * I4) + 49];
            v8 = blockR[(2 * I4) + 64];
            v9 = blockR[(2 * I4) + 65];
            v10 = blockR[(2 * I4) + 80];
            v11 = blockR[(2 * I4) + 81];
            v12 = blockR[(2 * I4) + 96];
            v13 = blockR[(2 * I4) + 97];
            v14 = blockR[(2 * I4) + 112];
            v15 = blockR[(2 * I4) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I4] = v0;
            blockR[(2 * I4) + 1] = v1;
            blockR[(2 * I4) + 16] = v2;
            blockR[(2 * I4) + 17] = v3;
            blockR[(2 * I4) + 32] = v4;
            blockR[(2 * I4) + 33] = v5;
            blockR[(2 * I4) + 48] = v6;
            blockR[(2 * I4) + 49] = v7;
            blockR[(2 * I4) + 64] = v8;
            blockR[(2 * I4) + 65] = v9;
            blockR[(2 * I4) + 80] = v10;
            blockR[(2 * I4) + 81] = v11;
            blockR[(2 * I4) + 96] = v12;
            blockR[(2 * I4) + 97] = v13;
            blockR[(2 * I4) + 112] = v14;
            blockR[(2 * I4) + 113] = v15;

            v0 = blockR[2 * I5];
            v1 = blockR[(2 * I5) + 1];
            v2 = blockR[(2 * I5) + 16];
            v3 = blockR[(2 * I5) + 17];
            v4 = blockR[(2 * I5) + 32];
            v5 = blockR[(2 * I5) + 33];
            v6 = blockR[(2 * I5) + 48];
            v7 = blockR[(2 * I5) + 49];
            v8 = blockR[(2 * I5) + 64];
            v9 = blockR[(2 * I5) + 65];
            v10 = blockR[(2 * I5) + 80];
            v11 = blockR[(2 * I5) + 81];
            v12 = blockR[(2 * I5) + 96];
            v13 = blockR[(2 * I5) + 97];
            v14 = blockR[(2 * I5) + 112];
            v15 = blockR[(2 * I5) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I5] = v0;
            blockR[(2 * I5) + 1] = v1;
            blockR[(2 * I5) + 16] = v2;
            blockR[(2 * I5) + 17] = v3;
            blockR[(2 * I5) + 32] = v4;
            blockR[(2 * I5) + 33] = v5;
            blockR[(2 * I5) + 48] = v6;
            blockR[(2 * I5) + 49] = v7;
            blockR[(2 * I5) + 64] = v8;
            blockR[(2 * I5) + 65] = v9;
            blockR[(2 * I5) + 80] = v10;
            blockR[(2 * I5) + 81] = v11;
            blockR[(2 * I5) + 96] = v12;
            blockR[(2 * I5) + 97] = v13;
            blockR[(2 * I5) + 112] = v14;
            blockR[(2 * I5) + 113] = v15;

            v0 = blockR[2 * I6];
            v1 = blockR[(2 * I6) + 1];
            v2 = blockR[(2 * I6) + 16];
            v3 = blockR[(2 * I6) + 17];
            v4 = blockR[(2 * I6) + 32];
            v5 = blockR[(2 * I6) + 33];
            v6 = blockR[(2 * I6) + 48];
            v7 = blockR[(2 * I6) + 49];
            v8 = blockR[(2 * I6) + 64];
            v9 = blockR[(2 * I6) + 65];
            v10 = blockR[(2 * I6) + 80];
            v11 = blockR[(2 * I6) + 81];
            v12 = blockR[(2 * I6) + 96];
            v13 = blockR[(2 * I6) + 97];
            v14 = blockR[(2 * I6) + 112];
            v15 = blockR[(2 * I6) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I6] = v0;
            blockR[(2 * I6) + 1] = v1;
            blockR[(2 * I6) + 16] = v2;
            blockR[(2 * I6) + 17] = v3;
            blockR[(2 * I6) + 32] = v4;
            blockR[(2 * I6) + 33] = v5;
            blockR[(2 * I6) + 48] = v6;
            blockR[(2 * I6) + 49] = v7;
            blockR[(2 * I6) + 64] = v8;
            blockR[(2 * I6) + 65] = v9;
            blockR[(2 * I6) + 80] = v10;
            blockR[(2 * I6) + 81] = v11;
            blockR[(2 * I6) + 96] = v12;
            blockR[(2 * I6) + 97] = v13;
            blockR[(2 * I6) + 112] = v14;
            blockR[(2 * I6) + 113] = v15;

            v0 = blockR[2 * I7];
            v1 = blockR[(2 * I7) + 1];
            v2 = blockR[(2 * I7) + 16];
            v3 = blockR[(2 * I7) + 17];
            v4 = blockR[(2 * I7) + 32];
            v5 = blockR[(2 * I7) + 33];
            v6 = blockR[(2 * I7) + 48];
            v7 = blockR[(2 * I7) + 49];
            v8 = blockR[(2 * I7) + 64];
            v9 = blockR[(2 * I7) + 65];
            v10 = blockR[(2 * I7) + 80];
            v11 = blockR[(2 * I7) + 81];
            v12 = blockR[(2 * I7) + 96];
            v13 = blockR[(2 * I7) + 97];
            v14 = blockR[(2 * I7) + 112];
            v15 = blockR[(2 * I7) + 113];
            // G(ref v0, ref v4, ref v8, ref v12);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v4 + (2 * (v0 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v12 ^ v0;
            v12 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v12 + (2 * (v8 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v4 ^ v8;
            v4 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v5, ref v9, ref v13);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v5 + (2 * (v1 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v13 ^ v1;
            v13 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v13 + (2 * (v9 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v5 ^ v9;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v6, ref v10, ref v14);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v6 + (2 * (v2 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v14 ^ v2;
            v14 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v14 + (2 * (v10 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v6 ^ v10;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v7, ref v11, ref v15);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v7 + (2 * (v3 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v15 ^ v3;
            v15 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v15 + (2 * (v11 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v7 ^ v11;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v0, ref v5, ref v10, ref v15);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 32) | (tmp << 32);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 24) | (tmp << 40);
            v0 = v0 + v5 + (2 * (v0 & 0xFFFFFFFF) * (v5 & 0xFFFFFFFF));
            tmp = v15 ^ v0;
            v15 = (tmp >> 16) | (tmp << 48);
            v10 = v10 + v15 + (2 * (v10 & 0xFFFFFFFF) * (v15 & 0xFFFFFFFF));
            tmp = v5 ^ v10;
            v5 = (tmp >> 63) | (tmp << 1);

            // G(ref v1, ref v6, ref v11, ref v12);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 32) | (tmp << 32);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 24) | (tmp << 40);
            v1 = v1 + v6 + (2 * (v1 & 0xFFFFFFFF) * (v6 & 0xFFFFFFFF));
            tmp = v12 ^ v1;
            v12 = (tmp >> 16) | (tmp << 48);
            v11 = v11 + v12 + (2 * (v11 & 0xFFFFFFFF) * (v12 & 0xFFFFFFFF));
            tmp = v6 ^ v11;
            v6 = (tmp >> 63) | (tmp << 1);

            // G(ref v2, ref v7, ref v8, ref v13);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 32) | (tmp << 32);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 24) | (tmp << 40);
            v2 = v2 + v7 + (2 * (v2 & 0xFFFFFFFF) * (v7 & 0xFFFFFFFF));
            tmp = v13 ^ v2;
            v13 = (tmp >> 16) | (tmp << 48);
            v8 = v8 + v13 + (2 * (v8 & 0xFFFFFFFF) * (v13 & 0xFFFFFFFF));
            tmp = v7 ^ v8;
            v7 = (tmp >> 63) | (tmp << 1);

            // G(ref v3, ref v4, ref v9, ref v14);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 32) | (tmp << 32);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 24) | (tmp << 40);
            v3 = v3 + v4 + (2 * (v3 & 0xFFFFFFFF) * (v4 & 0xFFFFFFFF));
            tmp = v14 ^ v3;
            v14 = (tmp >> 16) | (tmp << 48);
            v9 = v9 + v14 + (2 * (v9 & 0xFFFFFFFF) * (v14 & 0xFFFFFFFF));
            tmp = v4 ^ v9;
            v4 = (tmp >> 63) | (tmp << 1);
            blockR[2 * I7] = v0;
            blockR[(2 * I7) + 1] = v1;
            blockR[(2 * I7) + 16] = v2;
            blockR[(2 * I7) + 17] = v3;
            blockR[(2 * I7) + 32] = v4;
            blockR[(2 * I7) + 33] = v5;
            blockR[(2 * I7) + 48] = v6;
            blockR[(2 * I7) + 49] = v7;
            blockR[(2 * I7) + 64] = v8;
            blockR[(2 * I7) + 65] = v9;
            blockR[(2 * I7) + 80] = v10;
            blockR[(2 * I7) + 81] = v11;
            blockR[(2 * I7) + 96] = v12;
            blockR[(2 * I7) + 97] = v13;
            blockR[(2 * I7) + 112] = v14;
            blockR[(2 * I7) + 113] = v15;
        }

        private static void Blake2RowAndColumnRoundsNoMsg(BlockValues blockR)
        {
            // TODO: figure out and lift the code from Blake2BCore-FullyUnrolled.cs
            // apply Blake2 on columns of 64-bit words:
            //    (0,1,...,15), then
            //    (16,17,..31)... finally
            //    (112,113,...127)
            for (int i = 0; i < 8; ++i)
            {
                Blake2ColumnRoundNoMsg2(blockR, i);
            }

            // Apply Blake2 on rows of 64-bit words:
            // (0,1,16,17,...112,113), then
            // (2,3,18,19,...,114,115).. finally
            // (14,15,30,31,...,126,127)
            for (int i = 0; i < 8; ++i)
            {
                Blake2RowRoundNoMsg2(blockR, i);
            }
        }

        private static void FillBlock(BlockValues prevBlock, BlockValues refBlock, BlockValues nextBlock)
        {
            var buf = new ulong[Argon2.QwordsInBlock * 2];
            var blockR = new BlockValues(buf, 0);
            var blockTmp = new BlockValues(buf, 1);
            blockR.Copy(refBlock);
            blockR.Xor(prevBlock);
            blockTmp.Copy(blockR);
            Blake2RowAndColumnRoundsNoMsg(blockR);
            nextBlock.Copy(blockTmp);
            nextBlock.Xor(blockR);
        }

        private static void FillBlockWithXor(BlockValues prevBlock, BlockValues refBlock, BlockValues nextBlock)
        {
            var buf = new ulong[Argon2.QwordsInBlock * 2];
            var blockR = new BlockValues(buf, 0);
            var blockTmp = new BlockValues(buf, 1);
            blockR.Copy(refBlock);
            blockR.Xor(prevBlock);
            blockTmp.Copy(blockR);
            blockTmp.Xor(nextBlock); // saving the next block for XOR over
            Blake2RowAndColumnRoundsNoMsg(blockR);
            nextBlock.Copy(blockTmp);
            nextBlock.Xor(blockR);
        }
    }
}