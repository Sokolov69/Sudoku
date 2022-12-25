using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClassLibrary1
{
    public class LibSudoku
    {
        private static int[,] playField = new int[9, 9];
        private static List<int> wrongNumbers = new List<int>();

        private static void InitField()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    playField[i, j] = 0;
        }
        static public int[,] Field(int n)
        {
            InitField();
            bool results = false;
            results = GenerateField();
            while (!results)
            {
                results = GenerateField();
            }
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                playField[rnd.Next(0, 9), rnd.Next(0, 9)] = 0;
            }
            return playField;
        }
        private static bool CheckLine(int line, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (playField[line, i] == number)
                {
                    if (!wrongNumbers.Contains(number))
                        wrongNumbers.Add(number);
                    return true;
                }
            }
            return false;
        }

        private static bool CheckColumn(int col, int number)
        {
            for (int j = 0; j < 9; j++)
            {
                if (playField[j, col] == number)
                {
                    if (!wrongNumbers.Contains(number))
                        wrongNumbers.Add(number);
                    return true;
                }
            }
            return false;
        }

        private static bool CheckBlock(int line, int col, int number)
        {
            int blockLine = 0;
            int blockCol = 0;

            double blockNum = 0;
            blockNum = line + 1;
            blockNum = blockNum / 3;
            blockNum = Math.Ceiling(blockNum);
            blockLine = Convert.ToInt32(blockNum);

            blockNum = col + 1;
            blockNum = blockNum / 3;
            blockNum = Math.Ceiling(blockNum);
            blockCol = Convert.ToInt32(blockNum);

            if (blockLine == 0) blockLine = 1;
            if (blockCol == 0) blockCol = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playField[i + (blockLine - 1) * 3, j + (blockCol - 1) * 3] == number)
                    {

                        if (!wrongNumbers.Contains(number))
                            wrongNumbers.Add(number);
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool GenerateField()
        {
            Random rnd = new Random();
            int number = 0;
            bool result = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    number = rnd.Next(1, 10);
                    result = CheckLine(i, number);
                    while (result)
                    {
                        number = rnd.Next(1, 10);
                        if (wrongNumbers.Count > 8)
                        {
                            InitField();
                            wrongNumbers.Clear();
                            return false;
                        }
                        while (wrongNumbers.Contains(number))
                            number = rnd.Next(1, 10);
                        result = CheckLine(i, number);
                    }

                    result = CheckColumn(j, number);
                    if (result)
                    {
                        j--;
                        continue;
                    }

                    result = CheckBlock(i, j, number);
                    if (result)
                    {
                        j--;
                        continue;
                    }

                    playField[i, j] = number;
                    wrongNumbers.Clear();
                }
            }
            
            return true;
        }
        private static bool CheckLine1(int line, int number, int[,] resul, int j)
        {
            for (int i = 0; i < 9; i++)
            {
                if (resul[line, i] == number && i != j)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckColumn1(int col, int number, int[,] resul,int j)
        {
            for (int i = 0; i < 9; i++)
            {
                if (resul[i, col] == number&&i!=j)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckBlock1(int line, int col, int number, int[,] resul)
        {
            int line1;
            int col1;
            if (line < 3)
                line1 = 0;
            else if (line < 6)
                line1 = 3;
            else
                line1 = 6;
            if (col < 3)
                col1 = 0;
            else if (col < 6)
                col1 = 3;
            else
                col1 = 6;
            int f = 0;
            for (int i = line1; i < line1 + 3; i++)
            {
                for (int j = col1; j < col1 + 3; j++)
                {
                    if (resul[i, j] == number)
                    {
                        f++;
                        if (f == 2)
                            return true;
                    }
                }
            }
            return false;
        }
        public static bool Prov(int[,] resul)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (CheckLine1(i,resul[i,j],resul,j)|| CheckColumn1(j, resul[i, j], resul, i) || CheckBlock1(i, j, resul[i, j], resul))
                    {
                        return false;
                    }
            return true;
        }


    }
}
