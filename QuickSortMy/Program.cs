using System;

namespace QuickSortMy
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] a = new int[] { 7, 1, 5, 9, 0, 0, 5, 7, 8, 8, 2, 7, 3, 5, 7, 8, 8, 2, 7, 3, 8, 2, 7, 3, 5, 7, 8, 8, 2, 7, 3, -100, - 45, 999 };
            //int[] a = new int[] {-19, 26, -54, -11, 70, 77, 29};
            //int[] a = new string[] {"-19", "-29", "-52", "-12", "83", "-75", "26", "-54", "-11", "70", "77", "29"};   // для реализации <T> в дальнейшем

            int[] a = new int[40];

            Random rnd = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(-100, 100);
            }

            PrintArray(a);

            QuickSort(a, 0, a.Length - 1);

            Console.ReadLine();
        }


        public static void PrintArray(int[] a)
        {
            Console.WriteLine("Mассив: {0}", string.Join(", ", a));
        }

        public static void PrintArray(int[]a, int k, int i, int j)
        {
            Console.WriteLine("Mассив: {0}, опорный = {1}, меньший = {2}, j = {3} \n", string.Join(", ", a), k, i, j);
        }

        public static void SwapItems<T>(ref T item1, ref T item2) where T : IComparable<T>
        {
            var tmp = item1;
            item1 = item2;
            item2 = tmp;
        }

        public static void QuickSort(int[] a, int leftEnd, int rightEnd)
        {
            int idxLeft1 = leftEnd;     // левый индекс/граница левого массива
            int idxRight1 = default;    // правый индекс/граница левого массива
            int idxLeft2 = default;     // левый индекс/граница правого массива
            int idxRight2 = rightEnd;   // правый индекс/граница правого массива

            (idxRight1, idxLeft2) = FindIndices(a, leftEnd, rightEnd);

            if (idxLeft1 < idxRight1)
            {
                Console.WriteLine("Сортировка левого рекурсивно: Left 1 = {0}, Right 1 = {1} \n", idxLeft1, idxRight1);
                QuickSort(a, idxLeft1, idxRight1); 
            }

            if (idxLeft2 < idxRight2)
            {
                Console.WriteLine("Сортировка правого рекурсивно: Left 2 = {0}, Right 2 = {1} \n", idxLeft2, idxRight2);
                QuickSort(a, idxLeft2, idxRight2); 
            }
        }

        public static (int indexRight1, int indexLeft2) FindIndices(int[] a, int leftEnd, int rightEnd)
        {
            int idxLess = leftEnd;      // начальный индекс/граница меньших = leftEnd
            int idxRefer = leftEnd;     // начальный индекс/граница опорного(ых)
            int idxItem = default;      // индекс/граница текущего элемента

            int idxRight1 = default;
            int idxLeft2 = default;

            // Поиск элементов меньше опорного
            for (idxItem = idxRefer + 1; idxItem <= rightEnd; idxItem++)
            {
                if (a[idxItem] < a[idxRefer])                               // меньше опорного (необх убрать проверку если j == i !!!
                {
                    SwapItems(ref a[idxItem], ref a[idxLess + 1]);          // равный опорному меняем с первым большим после индекса/границы меньших   
                    idxLess++;                                              // смещаем/обновляем индекс/границу меньших на 1 вправо
                    //PrintArray(a);
                }

                if (a[idxItem] == a[idxRefer])                              // равен опорному
                {
                    SwapItems(ref a[idxItem], ref a[idxLess + 1]);          // равный опорному меняем с первым большим после индекса/границы меньших
                    SwapItems(ref a[idxRefer + 1], ref a[idxLess + 1]);     // равный опорному меняем с первым меньшим после индекса/границы опорных
                    idxLess++;                                              // смещаем/обновляем индекс/границу меньших на 1 вправо
                    idxRefer++;                                             // смещаем/обновляем индекс/границу опорных на 1 вправо
                }
                //idxRight2 = idxItem;                                      // правая граница всего массива {опорные/k/, меньшие /i/, большие /j/}
            }
            //PrintArray(a, idxRefer, idxLess, idxItem);

            int idxSafe = idxLess;                                          // сохраняем индекс меньших

            // сместим опорные в середину
            // {опорные/k/, меньшие/i/, большие} =>
            // {меньшие/i/, опорные/k/, большие}
            for (idxItem = idxRefer - leftEnd + 1; idxItem > 0; idxItem--)  // idxItem = разница смещения между индексом опорного и левой границей, т.е. n - итераций
            {
                SwapItems(ref a[idxRefer], ref a[idxLess]);                 //последний опорный меняем с последним меньшим
                idxLess--;                                                  // смещаем/обновляем границу меньших на 1 влево
                idxRefer--;                                                 // смещаем/обновляем границу опорных на 1 влево
                //PrintArray(a);
            }
            idxRefer = idxSafe;                                             // новый индекс опороных {меньшие/i/, опорные/k/, большие}

            Console.WriteLine("Массив после смещения опорных в середину: \n");
            PrintArray(a, idxRefer, idxLess, idxItem);

            idxRight1 = idxLess;        // правый индекс/граница  левого массива (индекс меньших перед опорным(и))
            idxLeft2 = idxRefer + 1;    // левый индекс/граница (индекс после опорных)

            return (idxRight1, idxLeft2);
        }

        #region Recycle Bin
        //public static void PrintArray(int[] a)
        //{
        //    Console.WriteLine("Mассив: {0}", string.Join(", ", a));
        //}

        //public static void PrintArray(int[] a, int k, int i, int j)
        //{
        //    Console.WriteLine("Mассив: {0}, k = {1}, i = {2}, j = {3}", string.Join(", ", a), k, i, j);
        //}


        //public static void QuickSort(int[] a, int l, int r)
        //{

        //    int i = l;  // граница элемментов < l
        //    int j = default;    //
        //    int k = l;  // опорный элемент первый

        //    int L1 = default;
        //    int R1 = default;
        //    int L2 = default;
        //    int R2 = default;

        //    // Поиск
        //    for (j = k + 1; j <= r; j++)
        //    {
        //        if (a[j] < a[k] /*|| a[j] < 0*/ /*&& (j != i)*/)        // если меньше опорного
        //        {
        //            int tmp = a[j];     // сохр. меньший
        //            a[j] = a[i + 1];    // меняем местами j и (i + 1)
        //            a[i + 1] = tmp;     // меняем местами j и (i + 1) (необх убрать проверку если j == i !!!
        //            i++;                // смещаем границу меньших
        //            PrintArray(a);
        //        }

        //        if (a[j] == a[k] /*&& (j-k) > 1*/)           // равен опорному
        //        {
        //            int tmp = a[j];         // сохраняем значение равное пороному
        //            a[j] = a[i + 1];        // смещаем первый больший элемент за границей последнего меньшего в текущий
        //            a[i + 1] = a[k + 1];    // смещаем первый меньший элемент за границей опорного на новую границу меньших i++
        //            a[k + 1] = tmp;         // сохраняем опорный на новую границу опорных k++
        //            i++;                    // смещаем границу опорных на 1 вправо
        //            k++;                    // смещаем границу меньших на 1 вправо
        //            PrintArray(a);
        //        }
        //        R2 = j;                 // правая граница всего массива {опорные/k/, меньшие /i/, большие /j/}
        //    }

        //    PrintArray(a, k, i, j);

        //    // сместим опорные в середину  {опорные/k/, меньшие /i/, большие /j/} =>  {меньшие /i/, опорные/k/, большие }
        //    //for (j = k; j >= l; j--)    // i - k даст разницу смещения между
        //    //{
        //    //    int tmp = a[j];             // последний меньший сохраняем
        //    //    a[j] = a[i];             //a[j - (i + k)];      // последний опорный схраняем на место последнего меньшего
        //    //    a[i] = tmp;       //a[j - (i + k)]; // на место последнего опорного и так n раз равное количесту опорных т.е. k
        //    //    i--;
        //    //    PrintArray(a);
        //    //}
        //    // i - новая граница меньших
        //    //k = i + k + 1;                  // новая граница опроных  {меньшие /i/, опорные/k/, большие }
        //    //if (k > r)
        //    //{
        //    //    k = r;                     // выход за правую границу границу
        //    //}

        //    int x = i;
        //    int y = k - l + 1; //iterazij
        //    for (j = k - l + 1; j > 0; j--)    // i - k даст разницу смещения между
        //    {
        //        int tmp = a[k];             // последний меньший сохраняем
        //        a[k] = a[i];             //a[j - (i + k)];      // последний опорный схраняем на место последнего меньшего
        //        a[i] = tmp;       //a[j - (i + k)]; // на место последнего опорного и так n раз равное количесту опорных т.е. k
        //        i--;
        //        k--;
        //        PrintArray(a);
        //    }
        //    // i - новая граница меньших
        //    k = x;                  // новая граница опроных  {меньшие /i/, опорные/k/, большие }



        //    Console.WriteLine("Массив после смещения опорных в середину:");
        //    PrintArray(a, k, i, j);

        //    L1 = l;         // крайняя левая граница
        //    R1 = i;         // краяняя правая (она же граница меньших перед опорными)
        //    L2 = k + 1;     // крайняя левая (первый элеент после опорных)
        //    //R2 = r;

        //    if (L1 < R1)
        //    {
        //        Console.WriteLine("Сортировка левого рекурсивно: L1 = {0}, R1 = {1}", L1, R1);
        //        QuickSort(a, L1, R1);
        //    }

        //    if (L2 < R2)
        //    {
        //        Console.WriteLine("Сортировка правого рекурсивно: L2 = {0}, R2 = {1}", L2, R2);
        //        QuickSort(a, L2, R2);
        //    }
        //}


        #region Recycle Bin 2
        //public static void QuickSort(int[] a, int l, int r)
        //{
        //    int k = 0;
        //    int i = 0;
        //    int j;

        //    for (j = 1; j < a.Length; j++)
        //    {
        //        if (a[j] < a[k])
        //        {
        //            int tmp = a[j];
        //            a[j] = a[i + 1];
        //            a[i + 1] = tmp;
        //            i++;
        //            PrintArray(a);
        //        }

        //        if (a[j] == a[k])
        //        {
        //            int tmp = a[j];
        //            a[j] = a[i + 1];
        //            a[i + 1] = a[k + 1];
        //            a[k + 1] = tmp;
        //            i++;
        //            k++;
        //            PrintArray(a);
        //        }
        //    }

        //    PrintArray(a, k, i, j);

        //    for (j = k; j >= 0; j--)
        //    {
        //        int tmp = a[i];
        //        a[i] = a[j];
        //        a[j] = tmp;
        //        i--;
        //        PrintArray(a);
        //    }
        //    k = i + k + 1;

        //    PrintArray(a, k, i, j);
        //} 
        #endregion
        #endregion
    }
}
