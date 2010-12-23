using System;
using System.Drawing;
using System.Threading;

namespace ImageApprox
{
    /// <summary>
    /// Реализует метод неискажающего сглаживания экспериментальных зависимостей для изображений и последовательностей.
    /// </summary>
    public static class Approximation
    {
        /// <summary>
        /// Реализует проведение сглаживания одного канала изображения в отдельном потоке.
        /// </summary>
        private class ChannelApproximation
        {
            private double[] channel;
            private int width, height;
            private AutoResetEvent completed;
            private double[] matrix;
            private int unknownNum;
            private byte[] output;
            private float k;
            private Thread thread;
            private Action made;

            public ChannelApproximation()
            {
                k = 1;
                unknownNum = 4;
                Completed = new AutoResetEvent(false);
                thread = new Thread(new ThreadStart(ChannelThread));
            }

            public double[] Data
            {
                get
                {
                    return channel;
                }
                set
                {
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    channel = value;
                }
            }

            public Action Process
            {
                get
                {
                    return made;
                }
                set
                {
                    made = value;
                }
            }

            public int Width
            {
                get
                {
                    return width;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException("Ширина должна быть больше нуля.");
                    }
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    width = value;
                }
            }

            public int Height
            {
                get
                {
                    return height;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException("Высота должна быть больше нуля.");
                    }
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    height = value;
                }
            }

            public AutoResetEvent Completed
            {
                get
                {
                    return completed;
                }
                protected set
                {
                    completed = value;
                }
            }

            public float Deviation
            {
                get
                {
                    return k;
                }
                set
                {
                    if (k <= 0)
                    {
                        throw new Exception("Неверное значение коэффициента.");
                    }
                    else if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    k = value;
                }
            }


            public byte[] Result
            {
                get
                {
                    return output;
                }
                protected set
                {
                    output = value;
                }
            }

            public double[] Matrix
            {
                get
                {
                    return matrix;
                }
                set
                {
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    matrix = value;
                }
            }

            public int UnknownNumber
            {
                get
                {
                    return unknownNum;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException("UnknownNum должен быть больше нуля.");
                    }
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        throw new ArgumentException("Нельзя менять значения свойств во время вычисления.");
                    }
                    unknownNum = value;
                }
            }

            public void Start()
            {
                Result = new byte[Data.Length];
                thread.Start();
            }

            /// <summary>
            /// Используется для запуска в отдельном потоке для раздельного сглаживания каждого канала изображения.
            /// </summary>
            private void ChannelThread()
            {
                double dev = CalculateDev(Data, Width, Height) * Deviation;
                double[] normalized = new double[Data.Length];
                int[] weights = new int[Data.Length];
                double[] line = new double[Width];
                double[] appr = new double[Width];
                int[] wline = new int[Width];
                int rr = Width > Height ? Width : Height;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        line[j] = Data[i * Width + j];
                    }
                    ApproximateLine(line, Width, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < Width; j++)
                    {
                        normalized[i * Width + j] = appr[j] * wline[j];
                        weights[i * Width + j] = wline[j];
                    }
                    if (Process != null)
                    {
                        Process();
                    }
                }
                line = new double[Height];
                appr = new double[Height];
                wline = new int[Height];
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        line[j] = Data[j * Width + i];
                    }
                    ApproximateLine(line, Height, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < Height; j++)
                    {
                        normalized[j * Width + i] += appr[j] * wline[j];
                        weights[j * Width + i] += wline[j];
                    }
                    if (made != null)
                    {
                        made();
                    }
                }
                dev *= Math.Sqrt(2);
                line = new double[Height < Width ? Height : Width];
                appr = new double[Height < Width ? Height : Width];
                wline = new int[Height < Width ? Height : Width];
                for (int i = Height - 2; i >= 0; i--)
                {
                    int s = 0;
                    for (; i + s < Height && s < Width; s++)
                    {
                        line[s] = Data[(s + i) * Width + s];
                    }
                    ApproximateLine(line, s, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < s; j++)
                    {
                        normalized[(j + i) * Width + j] += appr[j] * wline[j];
                        weights[(j + i) * Width + j] += wline[j];
                    }
                    if (made != null)
                    {
                        made();
                    }
                }
                for (int i = 1; i < Width - 1; i++)
                {
                    int s = 0;
                    for (; s < Height && i + s < Width; s++)
                    {
                        line[s] = Data[s * Width + i + s];
                    }
                    ApproximateLine(line, s, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < s; j++)
                    {
                        normalized[j * Width + i + j] += appr[j] * wline[j];
                        weights[j * Width + i + j] += wline[j];
                    }
                    if (made != null)
                    {
                        made();
                    }
                }
                for (int i = Height - 1; i >= 0; i--)
                {
                    int s = 0;
                    for (; i + s < Height && s < Width; s++)
                    {
                        line[s] = Data[(s + i + 1) * Width - 1 - s];
                    }
                    ApproximateLine(line, s, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < s; j++)
                    {
                        Result[(j + i + 1) * Width - 1 - j] = (byte)Math.Round(Limit((normalized[(j + i + 1) * Width - 1 - j] + appr[j] * wline[j]) / (weights[(j + i + 1) * Width - 1 - j] + wline[j]), 0, 255));
                    }
                    if (made != null)
                    {
                        made();
                    }
                }
                for (int i = 0; i < Width - 1; i++)
                {
                    int s = 0;
                    for (; s < Height && i - s >= 0; s++)
                    {
                        line[s] = Data[s * Width + i - s];
                    }
                    ApproximateLine(line, s, Matrix, UnknownNumber, dev, rr, ref wline, ref appr);
                    for (int j = 0; j < s; j++)
                    {
                        Result[j * Width + i - j] = (byte)Math.Round(Limit((normalized[j * Width + i - j] + appr[j] * wline[j]) / (weights[j * Width + i - j] + wline[j]), 0, 255));
                    }
                    if (made != null)
                    {
                        made();
                    }
                }
                Completed.Set();
            }
        }

        /// <summary>
        /// Сглаживает изображение.
        /// </summary>
        /// <param name="original_img">Оригинальное изображение.</param>
        /// <param name="unknownNum">Число неизвестных в полиноме для сглаживания. Рекомендуется значение, равное четырем.</param>
        /// <param name="k">Коеффициент сглаживания - чем больше. тем грубее сглаживание.</param>
        /// <param name="made">Процедура, выполняемая после обработки каждой строки изображения.</param>
        /// <returns></returns>
        public static Image ApproximateImage(Image original_img, int unknownNum, float k, Action made)
        {
            if (original_img.Width <= 1 && original_img.Height <= 1)
            {
                return original_img;
            }
            Bitmap image = new Bitmap(original_img);
            double[] r = new double[image.Height * image.Width];
            double[] g = new double[image.Height * image.Width];
            double[] b = new double[image.Height * image.Width];
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    r[i * image.Width + j] = pixel.R;
                    g[i * image.Width + j] = pixel.G;
                    b[i * image.Width + j] = pixel.B;
                }
            }

            double[] matrix = CalculateMatrix(image.Width > image.Height ? image.Width : image.Height, unknownNum);

            ChannelApproximation rData, gData, bData;
            rData = new ChannelApproximation();
            rData.Data = r;
            rData.Width = image.Width;
            rData.Height = image.Height;
            rData.Matrix = matrix;
            rData.UnknownNumber = unknownNum;
            rData.Deviation = k;
            rData.Process = made;
            rData.Start();
            gData = new ChannelApproximation();
            gData.Data = g;
            gData.Width = image.Width;
            gData.Height = image.Height;
            gData.Matrix = matrix;
            gData.UnknownNumber = unknownNum;
            gData.Deviation = k;
            gData.Process = made;
            gData.Start();
            bData = new ChannelApproximation();
            bData.Data = b;
            bData.Width = image.Width;
            bData.Height = image.Height;
            bData.Matrix = matrix;
            bData.UnknownNumber = unknownNum;
            bData.Deviation = k;
            bData.Process = made;
            bData.Start();

            rData.Completed.WaitOne();
            gData.Completed.WaitOne();
            bData.Completed.WaitOne();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    byte rc = rData.Result[i * image.Width + j];
                    byte gc = gData.Result[i * image.Width + j];
                    byte bc = bData.Result[i * image.Width + j];

                    image.SetPixel(j, i, Color.FromArgb(rc, gc, bc));
                }
            }
            return image;
        }

        /// <summary>
        /// Расчитывает верхнюю границу сглаживания.
        /// </summary>
        /// <param name="input">Последовательность входных данных - изображение или последовательность.</param>
        /// <param name="width">Ширина изображения. (для последовательности равно её длине)</param>
        /// <param name="height">Высота изображения. (для последовательности равно единице)</param>
        /// <returns>Возвращает верхнюю границу сглаживания.</returns>
        private static double CalculateDev(double[] input, int width, int height)
        {
            double[] appr = new double[width];
            double[] line = new double[width];
            double[] series = new double[input.Length];

            int num = 0;
            int i = 0;
            double c = 0;

            for (i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    line[j] = input[i * width + j];
                }
                FastLineApproximation(line, ref appr);
                double ser = line[0] - appr[0];
                for (int j = 1; j < width; j++)
                {
                    c = line[j] - appr[j];
                    if (Math.Sign(ser) == Math.Sign(c) || c == 0 || ser == 0)
                    {
                        ser += c;
                    }
                    else
                    {
                        series[num] = Math.Abs(ser);
                        num++;
                        ser = c;
                    }
                }
                series[num] = Math.Abs(ser);
                num++;
            }
            Array.Sort(series, 0, num);
            i = (int)Math.Sqrt(16 * width * height);
            if (i > num / 10)
            {
                i = num / 10;
            }
            return series[num - 1 - i];
        }

        private static void FastLineApproximation(double[] line, ref double[] appr)
        {
            int width = line.Length; 
            double p0, p1, p2, p3;
            int i = width - 1;
            int n1 = -2;
            switch (width) 
            {
                case 1:
                    appr[0] = line[0];
                    break;
                case 2:
                    appr[0] = (2.0 * line[0] + line[1]) / 3.0;
                    appr[1] = (line[0] + 2.0 * line[1]) / 3.0;
                    break;
                case 3:
                    appr[1] = (line[0] + line[1] + line[2]) / 3.0;
                    appr[0] = (2.0 * line[0] + appr[1]) / 3.0;
                    appr[2] = (2.0 * line[2] + appr[1]) / 3.0;
                    break;
                case 4:
                    appr[1] = (line[0] + line[1] + line[2]) / 3.0;
                    appr[2] = (line[3] + line[1] + line[2]) / 3.0;
                    appr[0] = (2.0 * line[0] + appr[1]) / 3.0;
                    appr[3] = (2.0 * line[3] + appr[2]) / 3.0;
                    break;
                default:
                   
                    p2 = (2.0 * (line[n1 + i - 2] + line[n1 + i + 2]) - line[n1 + i - 1] - 2.0 * line[n1 + i] - line[n1 + i + 1]) / 14.0;
                    p0 = (line[n1 + i - 2] + line[n1 + i - 1] + line[n1 + i] + line[n1 + i + 1] + line[n1 + i + 2] - 10.0 * p2) / 5.0;
                    p3 = (-line[n1 + i - 2] + line[n1 + i + 2] + 2.0 * (-line[n1 + i - 1] + line[n1 + i + 1])) / 12.0;
                    p1 = (2.0 * (-line[n1 + i - 2] + line[n1 + i + 2]) - line[n1 + i - 1] + line[n1 + i + 1] - 34.0 * p3) / 10.0;

                    for (int j = -2; j <= 2; j++)
                    {
                        appr[i + n1 + j] = p0 + j * (p1 + j * (p2 + j * p3));
                    }

                    int iNum = 5 * ((width - 4) / 5) - 1;
                    n1 = 2;

                    for (i = 0; i < iNum; i += 5)
                    {
                        p2 = (2.0 * (line[n1 + i -2] + line[n1 + i + 2]) - line[n1 + i - 1] - 2.0 * line[n1 + i] - line[n1 + i + 1]) / 14.0;
                        p0 = (line[n1 + i - 2] + line[n1 + i - 1] + line[n1 + i] + line[n1 + i + 1] + line[n1 + i + 2] - 10.0 * p2) / 5.0;
                        p3 = (-line[n1 + i - 2] + line[n1 + i + 2] + 2.0 * (-line[n1 + i - 1] + line[n1 + i + 1])) / 12.0;
                        p1 = (2.0 * (-line[n1 + i - 2] + line[n1 + i + 2]) - line[n1 + i - 1] + line[n1 + i + 1] - 34.0 * p3) / 10.0;
                        for (int j = -2; j <= 2; j++)
                        {
                            appr[i + n1 + j] = p0 + j * (p1 + j * (p2 + j * p3));
                        }
                    }
                    break;    
            }
        }

        /// <summary>
        /// Расчитывает и возвращает матрицу, используемую для составления и решения систем уравнений
        /// для нахождения коеффициентов полинома.
        /// </summary>
        /// <param name="rr">Размер матрицы по горизонтали.</param>
        /// <param name="unknownNum">Число неизвестных членов в полиноме (размер матрицы по вертикали).</param>
        /// <returns>Возвращает вспомогательную матрицу для решения систем уравнений.</returns>
        private static double[] CalculateMatrix(int rr, int unknownNum)
        {
            double[] matrix = new double[(unknownNum * 2 + 1) * rr];

            for (int i = 0; i < rr; i++)
            {
                long s = 1;
                if (i == 0)
                {
                    for (int j = 0; j <= unknownNum * 2; j++)
                    {
                        matrix[j * rr] = s;
                        s *= i;
                    }
                }
                else
                {
                    for (int j = 0; j <= unknownNum * 2; j++)
                    {
                        matrix[j * rr + i] = matrix[j * rr + i - 1] + s;
                        s *= i;
                    }
                }

            }
            return matrix;
        }

        /// <summary>
        /// Ограничивает данное число между верхней и нижней границами.
        /// </summary>
        /// <param name="n">Входное число.</param>
        /// <param name="min">Нижняя граница.</param>
        /// <param name="max">Верхняя граница.</param>
        /// <returns>Ограниченное число.</returns>
        public static T Limit<T>(T n, T min, T max) where T : IComparable<T>
        {
            return n.CompareTo(min) < 0 ? min : (n.CompareTo(max) > 0 ? max : n);
        }

        /// <summary>
        /// Сглаживает одну строку последовательности
        /// </summary>
        /// <param name="line">Строка входных данных - изображения или последовательности.</param>
        /// <param name="width">Длина строки</param>
        /// <param name="matrix">Вспомогательная матрица.</param>
        /// <param name="unknownNum">Число неизвестных в сглаживающем полиноме.</param>
        /// <param name="dev">Верхняя граница сглаживания.</param>
        /// <param name="rr">Размер квадрата, в который может быть помещено изображение.</param>
        /// <param name="weights">Последовательность весов точек изображения. (выходной параметр)</param>
        /// <param name="output">Последовательность выходных данных. (выходной параметр)</param>
        private static void ApproximateLine(double[] line, int width, double[] matrix, int unknownNum, double dev,
            int rr, ref int[] weights, ref double[] output)
        {
            if (width <= 1)
            {
                line.CopyTo(output, 0);
                weights[0] = 1;
                return;
            }

            double[] rightPart = new double[unknownNum * width];
            double[] z = new double[width]; 
            double[] diff = new double[width];
            double[] polyK = new double[unknownNum];
            double[] y = new double[unknownNum];
            double[] fMatrix = new double[unknownNum * unknownNum];
            int uNum;
            int hmin = 0;

            do
            {
                int h1 = hmin;
                int h2 = h1 + unknownNum;
                if (h2 >= width)
                {
                    h2 = width - 1;
                }
                int h2success = h1;
                int h2fail = width;

                do
                {
                    if (h2 - h1 <= 1)
                    {
                        uNum = 1;
                    }
                    else
                    {
                        uNum = h2 - h1 >= unknownNum ? unknownNum : h2 - h1;
                    }
                    y[0] = 0;
                    for (int i = 0; i <= h2 - h1; i++)
                    {
                        z[i] = 1;
                        y[0] += line[h1 + i];
                    }
                    for (int j = 1; j <= uNum - 1; j++)
                    {
                        y[j] = 0;
                        for (int i = 0; i <= h2 - h1; i++)
                        {
                            z[i] *= i;
                            y[j] += z[i] * line[h1 + i];
                        }
                    }

                    for (int j = 0; j < uNum; j++)
                    {
                        for (int i = 0; i < uNum; i++)
                        {
                            fMatrix[j * unknownNum + i] = matrix[(i + j) * rr + h2 - h1];
                        }
                    }

                    LSMSolve(y, fMatrix, uNum, ref polyK);

                    diff[h1] = polyK[0];

                    double ser = line[h1] - diff[h1];
                    double serMax = Math.Abs(ser);

                    int serLen = 1;
                    int n = 0;

                    if (h2 - h1 > 0)
                    {
                        while (n < uNum || h1 + n < h2 && serMax <= 3 * dev)
                        {
                            n++;
                            diff[h1 + n] = polyK[uNum - 1];
                            for (int i = 0; i <= uNum - 2; i++)
                            {
                                diff[h1 + n] = diff[h1 + n] * n + polyK[uNum - 2 - i];
                            }
                            double s = line[h1 + n] - diff[h1 + n];
                            if (Math.Sign(ser) == Math.Sign(s) || s == 0 || ser == 0)
                            {
                                ser += s;
                                serLen++;
                                if (serMax < Math.Abs(ser * serLen))
                                {
                                    serMax = Math.Abs(ser * serLen);
                                }
                            }
                            else
                            {
                                ser = s;
                                serLen = 1;
                                if (serMax < Math.Abs(ser))
                                {
                                    serMax = Math.Abs(ser);
                                }
                            }
                        }
                    }

                    if (serMax <= 3 * dev)
                    {
                        h2success = h2;
                        for (int i = h1; i <= h2success; i++)
                        {
                            output[i] = diff[i];
                        }
                        if (h2fail == width)
                        {
                            h2 = h2 + h2 - h1;
                            if (h2 > width - 1)
                            {
                                h2 = width - 1;
                            }
                        }
                        else
                        {
                            h2 = (h2fail + h2) / 2;
                        }
                    }
                    else
                    {
                        if (h2 - h1 <= uNum)
                        {
                            h2success = h2;
                            h2fail = h2;
                            for (int i = h1; i <= h2success; i++)
                            {
                                output[i] = diff[i];
                            }
                        }
                        else
                        {
                            h2fail = h2;
                            h2 = (h2 + h2success) / 2;
                        }
                    }
                } while (h2fail - h2success > 1);
                hmin = h2success + 1;
                int w = h2success - h1 - uNum + 1;
                if (w < 1)
                {
                    w = 1;
                }
                for (int i = h1; i <= h2success; i++)
                {
                    weights[i] = w;
                }
            } while (hmin < width);
        }

        /// <summary>
        /// Вычисляет  коэффициенты  аппроксимации базисными функциям по набору точек с указанными весами.
        /// </summary>
        /// <param name="y">Набор значений функции.</param>
        /// <param name="fMatrix">Таблица значений базисных функций.</param>
        /// <param name="uNum">Число точек.</param>
        /// <param name="polyK">Коеффициенты разложения. (выходной параметр)</param>
        private static void LSMSolve(double[] y, double[] fMatrix, int uNum, ref double[] polyK)
        {
            double[] lsm_matrix = new double[uNum * uNum];
            double[] master_lsm = new double[uNum * uNum];
            double[] right_part = new double[uNum];
            double[] master_right = new double[uNum];

            for (int i = 0; i < uNum; i++)
            {
                for (int j = 0; j < uNum; j++)
                {
                    lsm_matrix[i * uNum + j] = fMatrix[i * y.Length + j];
                    master_lsm[i * uNum + j] = fMatrix[i * y.Length + j];
                }
                right_part[i] = y[i];
                master_right[i] = y[i];
            }

            bool reg = false;

            do
            {
                if (reg)
                {
                    double s = master_lsm[0] * master_lsm[0];
                    double amin = Math.Abs(master_lsm[0]);
                    for (int i = 1; i < uNum; i++)
                    {
                        s += master_lsm[i * uNum + i] * master_lsm[i * uNum + i];
                        if (amin > Math.Abs(master_lsm[i * uNum + i]))
                        {
                            amin = Math.Abs(master_lsm[i * uNum + i]);
                        }
                    }
                    s = Math.Sqrt(s / uNum) / 10000;
                    if (s > amin / 10 && amin != 0)
                    {
                        s = amin / 10;
                    }
                    for (int i = 0; i < uNum; i++)
                    {
                        for (int j = 0; j < uNum; j++)
                        {
                            if (i == j)
                            {
                                master_lsm[j * uNum + j] += s;
                            }
                            lsm_matrix[i * uNum + j] = master_lsm[i * uNum + j];
                        }
                        right_part[i] = master_right[i];
                    }
                    reg = false;
                }

                for (int m = uNum - 1; m >= 0; m--)
                {
                    int k = 0;
                    bool exitfor = false;
                    double buff;
                    while (lsm_matrix[m * uNum + m] == 0)
                    {
                        k++;
                        if (m - k < 0)
                        {
                            reg = true;
                            exitfor = true;
                            break;
                        }
                        buff = right_part[m];
                        right_part[m] = right_part[m - k];
                        right_part[m - k] = buff;
                        for (int i = 0; i < uNum; i++)
                        {
                            buff = lsm_matrix[m * uNum + i];
                            lsm_matrix[m * uNum + i] = lsm_matrix[(m - k) * uNum + i];
                            lsm_matrix[(m - k) * uNum + i] = buff;
                        }
                    }
                    if (exitfor)
                    {
                        break;
                    }
                    double amin = lsm_matrix[m * uNum + m];
                    buff = right_part[m] / amin;
                    for (int i = 0; i < m; i++)
                    {
                        right_part[i] -= lsm_matrix[i * uNum + m] * buff;
                        double mink = lsm_matrix[i * uNum + m] / amin;
                        for (int j = 0; j < m; j++)
                        {
                            lsm_matrix[i * uNum + j] -= lsm_matrix[m * uNum + j] * mink;
                        }
                    }
                }

                if (lsm_matrix[0] == 0)
                {
                    reg = true;
                }
            } while (reg);

            polyK[0] = right_part[0] / lsm_matrix[0];

            for (int i = 1; i < uNum; i++)
            {
                double buff = right_part[i];
                for (int j = 0; j < i; j++)
                {
                    buff -= polyK[j] * lsm_matrix[i * uNum + j];
                }
                polyK[i] = buff / lsm_matrix[i * uNum + i];
            }
        }

        /// <summary>
        /// Сглаживает последовательность чисел.
        /// </summary>
        /// <param name="input">Входной поток чисел.</param>
        /// <param name="unknownNum">Число неизвестных в полиноме для сглаживания. Рекомендуется значение, равное четырем.</param>
        /// <param name="k">Коеффициент сглаживания - чем больше. тем грубее сглаживание.</param>
        /// <returns>Возвращает обработанную последовательность.</returns>
        public static double[] ApproximateSequence(double[] input, int unknownNum, float k)
        {
            if (input.Length <= 1)
            {
                return input;
            }
            double[] matrix = CalculateMatrix(input.Length, unknownNum);
            double dev = CalculateDev(input, input.Length, 1) * k;
            int[] weights = new int[input.Length];
            double[] result = new double[input.Length];
            ApproximateLine(input, input.Length, matrix, unknownNum, dev, input.Length, ref weights, ref result);
            return result;
        }
    }
}