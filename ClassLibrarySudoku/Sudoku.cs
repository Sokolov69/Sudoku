using System;
using System.Collections.Generic;
namespace ClassLibrarySudoku
{
    public class Sudokus
    {
        int n;
        int[,] result = new int[9, 9];
        Random random = new Random();
        public Sudokus(int x)
        {
            n = x;
        }
        public int[,] Filling()
        {

           
            int c=0,r = 0;
            bool f = true;
            for(int x = 0; x < 3; x++)
            {
                List<int> List = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int i = c; i < 3; i++)
                {
                    for (int j = r; j < 3; j++)
                    {
                        int randomNumber = random.Next(List.Count);
                        if (rowSafe(i, randomNumber) && colSafe(j, randomNumber))
                        {
                            result[i, j]=List[randomNumber];
                        }
                    }
                }
                if (f)
                {
                    c += 3;
                    f = false;
                }
                else r += 3;
            }

            return result;
        }
        public bool rowSafe(int row,int x)
        {
            for (int i = 0; i < 9; i++)
                if (x == result[row, i])
                    return false;
            return true;
        }
        public bool colSafe(int col, int x)
        {
            for (int i = 0; i < 9; i++)
                if (x == result[i,col])
                    return false;
            return true;
        }
       
    }
}
