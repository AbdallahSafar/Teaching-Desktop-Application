using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Encrypt
    {
        public static string pass(string original)
        {
            string cypher = string.Empty; ;
            for (int i = 0; i < original.Length; i++)
                cypher += (char)((int)original[i] + 10);

            return cypher;
        }
        public static int round(string[,] map, int x, int y)
        {
            int count = 0, m = map.GetLength(0), n = map.GetLength(1);
            if (x == 0)
            {
                if (y == 0)
                {
                    if (string.CompareOrdinal(map[1, 0], "*") == 0) count++;
                    if (string.CompareOrdinal(map[0, 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[1, 1], "*") == 0) count++;
                }
                if (y == n - 1)
                {
                    if (string.CompareOrdinal(map[1, n - 2], "*") == 0) count++;
                    if (string.CompareOrdinal(map[1, n - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[0, n - 2], "*") == 0) count++;
                }
                if ((y != 0) && (y != n - 1))
                {
                    if (string.CompareOrdinal(map[0, y - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[0, y + 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[1, y], "*") == 0) count++;
                    if (string.CompareOrdinal(map[1, y - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[1, y + 1], "*") == 0) count++;

                }


            }

            if (x == m - 1)
            {
                if (y == 0)
                {
                    if (string.CompareOrdinal(map[m - 2, 0], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 1, 1], "*") == 0) count++;
                }
                if (y == n - 1)
                {
                    if (string.CompareOrdinal(map[m - 1, n - 2], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, n - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, n - 2], "*") == 0) count++;
                }
                if ((y != 0) && (y != n - 1))
                {
                    if (string.CompareOrdinal(map[m - 1, y - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 1, y + 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, y], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, y - 1], "*") == 0) count++;
                    if (string.CompareOrdinal(map[m - 2, y + 1], "*") == 0) count++;

                }

            }

            if ((x != 0) && (x != m - 1) && (y != 0) && (y != n - 1))
            {
                if (string.CompareOrdinal(map[x - 1, y - 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x, y - 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, y - 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x - 1, y], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, y], "*") == 0) count++;
                if (string.CompareOrdinal(map[x - 1, y + 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x, y + 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, y + 1], "*") == 0) count++;
            }
            if ((x != 0) && (x != m - 1) && (y == 0))
            {
                if (string.CompareOrdinal(map[x - 1, 0], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, 0], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x - 1, 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x, 1], "*") == 0) count++;
            }
            if ((x != 0) && (x != m - 1) && (y == n - 1))
            {
                if (string.CompareOrdinal(map[x - 1, n - 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, n - 1], "*") == 0) count++;
                if (string.CompareOrdinal(map[x + 1, n - 2], "*") == 0) count++;
                if (string.CompareOrdinal(map[x - 1, n - 2], "*") == 0) count++;
                if (string.CompareOrdinal(map[x, n - 2], "*") == 0) count++;
            }

            return count;
        }
       

        public static string[,] Map(int bomb, int lines, int columns)
        {
            int i, j;
            Random g = new Random();
            int x, y;
            string[,] map;
            int[] Bombx, Bomby;
            Bombx = new int[bomb];
            Bomby = new int[bomb];
            map = new string[lines, columns];
            Bombx[0] = g.Next(columns);
            Bomby[0] = g.Next(lines);
            for (i = 1; i < bomb; i++)
            {
                do
                {
                    x = g.Next(columns);
                    y = g.Next(lines);
                } while ((Array.IndexOf(Bombx, x) != -1) && (Bomby[Array.IndexOf(Bombx, x)] == y));
                Bombx[i] = x; Bomby[i] = y;
            }

            //Bomb x is j columns (x)  //  and Bomby is i lines (y)
            for (i = 0; i < lines; i++)
                for (j = 0; j < columns; j++)
                    map[i, j] = "-";


            for (i = 0; i < bomb; i++)
                map[Bomby[i], Bombx[i]] = "*";


            // numbering
            for (i = 0; i < lines; i++)
                for (j = 0; j < columns; j++)
                    if (string.CompareOrdinal(map[i, j], "*") != 0)
                        map[i, j] = round(map, i, j).ToString();
            return map;
        }
    }
}
