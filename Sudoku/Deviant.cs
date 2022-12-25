using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sudoku
{
    public partial class Records : Form
    {
        static int dific=0;
        static string time = "";
        public Records(string c,int f)
        {
            dific = f;
            time = c;
            InitializeComponent();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            switch (dific)
            {
                case 20:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Easy.txt");
                    break;
                case 35:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Medium.txt");
                    break;
                case 45:
                    path = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Records\Hard.txt");
                    break;
            }
            string result = "\"" + textBox1.Text + "\" " + "решил судоку за " + time.ToString()+" sec";
            StreamWriter writ = new StreamWriter(path, true);
            await writ.WriteLineAsync(result);
            writ.Close();
            this.Close();
        }
    }
}
