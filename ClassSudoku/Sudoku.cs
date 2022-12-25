using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace ClassSudoku
{
    public class Sudoku
    {
        static int[,] result = new int[9, 9];
        /// <summary>
        /// Случайно присваивает элементам значение 0
        /// </summary>
        /// <param name="n">Возращает массив с пустыми ячейками</param>
        public static void Complexity(int n)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                if (result[rnd.Next(1, 9), rnd.Next(1, 9)] != 0)
                    result[rnd.Next(1, 9), rnd.Next(1, 9)] = 0;
                else
                    i--;
            }
            
        }
        /// <summary>
        /// Обнуляет массив
        /// </summary>
        private static void InitField()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    result[i, j] = 0;
        }
        /// <summary>
        /// Главная функция
        /// </summary>
        /// <param name="n"></param>
        /// <returns>Возращает массив с числами судоку</returns>
        public static int[,] Sudok(int n)
        {
            bool c = Generat();
            while (!c)
            {
                InitField();
                c = Generat();
            }
            Complexity(n);
            return result;
        }
        
        /// <summary>
        /// Случайная генерация чисел для судоку
        /// </summary>
        /// <returns>Возрщает сгенерированный судоку</returns>
        static bool Generat()
        {
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                List<int> List = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int j = 0; j < 9; j++)
                {
                    bool c = true;
                    List<int> List1 = Count(List);
                    while (c)
                    {
                        int f = List1[rnd.Next(List1.Count)];
                        if (CheckBlock(i, j, f) || CheckColumn(j, f))
                        {
                            List1.Remove(f);
                            if (List1.Count < 1)
                                return false;
                        }
                        else
                        {
                            c = false;
                            result[i, j] = f;
                            List.Remove(f);
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Проверка на отсутствие повторений в строке
        /// </summary>
        /// <param name="line"></param>
        /// <param name="number"></param>
        /// <param name="ind"></param>
        /// <returns>Значение true или false взависимости от того, есть ли повторы в строке</returns>
        private static bool CheckLine(int line, int number,int ind=-1)
        {
            for (int i = 0; i < 9; i++)
                if (result[line, i] == number&&i!=ind)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверка на отсутствие повторений в квадрате
        /// </summary>
        /// <param name="line"></param>
        /// <param name="col"></param>
        /// <param name="number"></param>
        /// <returns>Значение true или false взависимости от того, есть ли повторы в столбце</returns>
        private static bool CheckBlock(int line, int col, int number)
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
            for (int i = line1; i < line1 + 3; i++)
                for (int j = col1; j < col1 + 3; j++)
                    if (result[i, j] == number)
                        return true;
            return false;
        }
        /// <summary>
        /// Проверка на отсутствие повторений в столбце
        /// </summary>
        /// <param name="col"></param>
        /// <param name="number"></param>
        /// <param name="ind"></param>
        /// <returns>Значение true или false взависимости от того, есть ли повторы в квадрате</returns>
        private static bool CheckColumn(int col, int number, int ind = -1)
        {
            for (int i = 0; i < 9; i++)
                if (result[i, col] == number&&ind!=i)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверка на правельность решения судоку
        /// </summary>
        /// <param name="mas"></param>
        /// <returns>Значение true или false взависимости от того, правильно ли решёно судоку</returns>
        public static bool Prov(int[,] mas)
        {
            result = mas;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (CheckBlock2(i, j, result[i, j]) || CheckColumn(j, result[i, j],i) || CheckLine(i, result[i, j],j))
                        return false;
            return true;
        }

        /// <summary>
        /// Создание копии List'a f
        /// </summary>
        /// <param name="f"></param>
        /// <returns>копию List'a</returns>
        static public List<int> Count(List<int> f)
        {
            List<int> List = new List<int>();
            for (int i = 0; i < f.Count; i++)
                List.Add(f[i]);
            return List;
        }

        private static bool CheckBlock2(int line, int col, int number)
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
            int h = 0;
            for (int i = line1; i < line1 + 3; i++)
                for (int j = col1; j < col1 + 3; j++)
                {
                    if (result[i, j] == number)
                    {
                        h++;
                        if(h>1)
                            return true;
                    }
                }
            return false;
        }
    }
}
