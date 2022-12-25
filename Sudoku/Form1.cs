using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using static ClassSudoku.Sudoku;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int dific=0;
        Stopwatch stopwatch = new Stopwatch(); //Переменная для подсчёта времени
        TableLayoutPanel tabel = new TableLayoutPanel();// Таблица судоку
        int[,] result = new int[9, 9];


        private void Form1_Load(object sender, EventArgs e)
        {
            Info(false);
        }
        /// <summary>
        /// Отображение элементов во время игры
        /// </summary>
        /// <param name="c"></param>
        public void Info(bool c)
        {
            Check.Enabled = c;
            label1.Enabled = c;
            Time.Enabled = c;
            panel1.Enabled = c;
            restart.Enabled = c;
            Check.Visible = c;
            label1.Visible = c;
            Time.Visible = c;
            panel1.Visible = c;
            restart.Visible = c;
            button1.Enabled = c;
            button1.Visible = c;
            label5.Enabled = c;
            label5.Visible = c;
            Rules.Visible = c;
            Rules.Enabled = c;
            label5.ForeColor = Color.Green;
        }

        public void Loads()
        {
            Application.Run(new Load());

        }
        /// <summary>
        /// Определение уровня сложности
        /// </summary>
        /// <returns>Количество пустых ячеек</returns>
        void Diffic()
        {
            if (r1.Checked)
                dific = 20;
            else if(r2.Checked)
                dific = 35;
            else
                dific = 45;
        }
        /// <summary>
        /// Создание и настройка TableLayoutPanel
        /// </summary>
        void Tabel()
        {
            string path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\image\setka.png");
            tabel = new TableLayoutPanel();
            tabel.BackgroundImage = Image.FromFile(path);
            tabel.Size = new Size(502, 500);
            tabel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tabel.RowCount = 9;
            tabel.ColumnCount = 9;
            Diffic();
            result = Sudok(dific);
        }
        /// <summary>
        /// Создание и настройка TextBox
        /// </summary>
        /// <returns></returns>
        TextBox Lab()
        {
            TextBox lab = new TextBox();
            lab.Font = new Font("Tobota", 27, FontStyle.Bold);
            lab.Multiline = true;
            lab.Dock = DockStyle.Fill;
            lab.ForeColor = Color.Black;
            lab.TextAlign = HorizontalAlignment.Center;
            lab.BackColor = Color.PowderBlue;
            lab.BorderStyle = BorderStyle.None;
            lab.KeyPress += Lab_KeyPress;
            return lab;
        }
        /// <summary>
        /// Функция для создания игрового поля и заполнения его элеметами
        /// </summary>
        void Game()
        {   //Начинается экран загрузки
            Thread t = new Thread(new ThreadStart(Loads));
            t.Start();
            //Создаются новый TableLayoutPanel
            Tabel();
            //Ширина и высота ячеек в TableLayoutPanel
            int w = 100 / tabel.ColumnCount;
            int h = 100 / tabel.RowCount;
            for (int i = 0; i < tabel.ColumnCount; i++)
            {
                //Добавление столбка
                tabel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, w));
                for (int j = 0; j < tabel.RowCount; j++)
                {
                    if (i == 0)
                        tabel.RowStyles.Add(new RowStyle(SizeType.Percent, h));//Добавление строки
                    TextBox lab = Lab();

                    if (result[i, j] != 0)
                    {
                        lab.Text = result[i, j].ToString();
                        lab.ReadOnly = true;
                    }
                    else
                    {
                        lab.Text = "";
                        lab.ForeColor = Color.Blue;
                    }
                    tabel.Controls.Add(lab, i, j);
                }
            }
            Controls.Add(tabel);
            t.Abort();


        }
        /// <summary>
        /// Проверка на ввод числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
        /// <summary>
        /// Считывание элементов из TableLayoutPanel в массив
        /// </summary>
        /// <returns></returns>
        int[,] Result()
        {
            int[,] result = new int[9, 9];
            int c = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox text = tabel.Controls[c] as TextBox;
                    if (text.Text != "")
                        result[i, j] = Convert.ToInt32(text.Text);
                    else
                        result[i, j] = 0;
                    c++;
                }
            }
            return result;
        }
        /// <summary>
        /// Проверка на правильность решения судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {

            int[,] result = Result();

            if (Prov(result))
            {
                stopwatch.Stop();
                string path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy_Best.txt");
                StreamReader first = new StreamReader(path);
                path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium_Best.txt");
                StreamReader second = new StreamReader(path);
                path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard_Best.txt");
                StreamReader third = new StreamReader(path);
                int rekord1 = 0;
                int rekord2 = 0;
                int rekord3 = 0;
                switch (dific)
                {
                    case 20:

                        rekord1 = Convert.ToInt32(first.ReadToEnd());
                        first.Close();
                        break;
                    case 35:
                        rekord2 = Convert.ToInt32(second.ReadToEnd());
                        second.Close();
                        break;
                    case 45:
                        rekord3 = Convert.ToInt32(third.ReadToEnd());
                        third.Close();
                        break;
                }
                int vib = 0;
                int vremya = Convert.ToInt32(Time.Text);
                switch (dific)
                {
                    case 20:
                        vib = rekord1;
                        break;
                    case 35:
                        vib = rekord2;
                        break;
                    case 45:
                        vib = rekord3;
                        break;
                }
                if (vib > vremya)
                {
                    label6.Enabled = true;
                    label6.Visible = true;
                    label5.Text = Time.Text;
                    switch (dific)
                    {
                        case 20:
                            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy_Best.txt");
                            break;
                        case 35:
                            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium_Best.txt");
                            break;
                        case 45:
                            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard_Best.txt");
                            break;
                    }
                    File.Create(path).Close();
                    string cool = Time.Text;
                    StreamWriter writ = new StreamWriter(path, true);
                    await writ.WriteAsync(cool);
                    writ.Close();
                }
                Record();
            }
            else
                MessageBox.Show("Где-то допущена ошибка...");
        }
         void Record()
        {
            Records form = new Records(Time.Text,dific);
            form.ShowDialog();
        }
        /// <summary>
        /// Кнопка начать игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Start();
        }
        /// <summary>
        /// Изменения времени раз в секунду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Time.Text= (stopwatch.ElapsedMilliseconds/1000).ToString();
        }
        /// <summary>
        /// Кнопка начать заново
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Start();
        }
        /// <summary>
        /// Метод для начала игры
        /// </summary>
        void Start()
        {
            foreach (Control s in Controls)
                s.Visible = false;
            Info(true);
            //Создание игры
            Game();
            timer1.Enabled = true;
            timer1.Start();
            string path = "";
            switch (dific)
            {
                case 20:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy_Best.txt");
                    StreamReader first = new StreamReader(path);
                    label5.Text = (first.ReadToEnd());
                    first.Close();
                    break;
                case 35:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium_Best.txt");
                    StreamReader second = new StreamReader(path);
                    label5.Text = (second.ReadToEnd());
                    second.Close();
                    break;
                case 45:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard_Best.txt");
                    StreamReader third = new StreamReader(path);
                    label5.Text = (third.ReadToEnd());
                    third.Close();
                    break;
            }
            label5.Enabled = true;
            label5.Visible = true;
            label6.Enabled = false;
            label6.Visible = false;
            stopwatch.Restart();
            stopwatch.Start();
        }

        /// <summary>
        /// Отображение элементов главного меню
        /// </summary>
        /// <param name="c"></param>
        void Menu(bool c)
        {
            label3.Enabled = c;
            label3.Visible=c;
            r1.Enabled = c;
            r1.Visible = c;
            r2.Enabled = c;
            r2.Visible = c;
            r3.Enabled = c;
            r3.Visible = c;
            button2.Enabled = c;
            button2.Visible = c;
            records.Visible = c;
            records.Enabled = c;
            label4.Visible = c;
            label4.Enabled = c;
        }
        /// <summary>
        /// Возврат в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_2(object sender, EventArgs e)
        {
            label5.Text = "";
            foreach (Control s in Controls)
            {
                s.Visible = false;
                s.Enabled = false;
            }
            Menu(true);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void r2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Правила игры:");
            sb.AppendLine("");
            sb.AppendLine("Игровое поле представляет собой квадрат рамером 9x9, разделённый на меньшие квадраты со стороной в 3 клетки. Таким образом, всё игровое поле состоит из 81 клетки. В них уже в начале игры стоят некоторые числа (от 1 до 9), называвиые подказками. Требуется заполнить свободные клетки цифрами от 1 до 9 так, чтобы В каждой строке, в каждом столбце и в каждом малом квадрате 3x3 каждая цифра встречалась только один раз.");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Краткие правила:");
            sb.AppendLine("");
            sb.AppendLine("1. В каждой клетке должны быть проставлены цифры от 1 до 9");
            sb.AppendLine("");
            sb.AppendLine("2. Цифры не должны повторяться в пределах одного столбика, строки или квадрата");
            sb.AppendLine("");
            sb.AppendLine("3. При внесении цифры стоит помнить, что её можно вписать в клетку только, если она отсутствует в горизонтальной и вертикальной линии и в малом квадрате 3x3");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Чтобы открыть новое судоку - нажмите кнопку \"Начать новую игру\"");
            sb.AppendLine("Чтобы сменить уровень сложности - нажмите кнопку \"Вернуться в меню\"");
            MessageBox.Show(sb.ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void records_Click(object sender, EventArgs e)
        {
            string path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy.txt");
            StreamReader rd = new StreamReader(path);
            string result = "ЛЁГКИЙ УРОВЕНЬ СЛОЖНОСТИ" + "\r\n";
            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy_Best.txt");
            StreamReader vrem = new StreamReader(path);
            result += "Лучшее время: " + vrem.ReadToEnd() + " sec" + "\r\n\n" + rd.ReadToEnd() + "\r\n" + "_______________________________" + "\n\n";
            path =  Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium.txt");
            rd= new StreamReader(path);
            result += "СРЕДНИЙ УРОВЕНЬ СЛОЖНОСТИ" + "\r\n";
            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium_Best.txt");
            vrem = new StreamReader(path);
            result += "Лучшее время: " + vrem.ReadToEnd() + " sec" + "\r\n\n" + rd.ReadToEnd() + "\r\n" + "_______________________________" + "\n\n";
            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard.txt");
            rd = new StreamReader(path);
            result += "СЛОЖНЫЙ УРОВЕНЬ СЛОЖНОСТИ" + "\r\n";
            path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard_Best.txt");
            vrem = new StreamReader(path);
            result += "Лучшее время: " + vrem.ReadToEnd() + " sec" + "\r\n\n" + rd.ReadToEnd();
            MessageBox.Show(result);
        }
    }


}

