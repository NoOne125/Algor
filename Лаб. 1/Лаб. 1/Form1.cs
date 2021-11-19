using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Лаб._1.Other;

namespace Лаб._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void Fill(sbyte[,] field, bool IDS, bool RBFS)
        {
            string[,] arr_l = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] != 0)
                    {
                        arr_l[i, j] = Convert.ToString(field[i, j]);
                    }
                    else
                    {
                        if (IDS && RBFS)
                        {
                            arr_l[i, j] = "X";
                            label31.Text = "";
                            label32.Text = "";
                            label33.Text = "";
                            label34.Text = "";
                            label35.Text = "";
                        }
                        else
                            arr_l[i, j] = "";
                    }
                }
            }
            if (!IDS && !RBFS)
            {
                label1.Text = arr_l[0, 0];
                label2.Text = arr_l[0, 1];
                label3.Text = arr_l[0, 2];
                label4.Text = arr_l[1, 0];
                label5.Text = arr_l[1, 1];
                label6.Text = arr_l[1, 2];
                label7.Text = arr_l[2, 0];
                label8.Text = arr_l[2, 1];
                label9.Text = arr_l[2, 2];
            }
            if (IDS)
            {
                label10.Text = arr_l[0, 0];
                label11.Text = arr_l[0, 1];
                label12.Text = arr_l[0, 2];
                label13.Text = arr_l[1, 0];
                label14.Text = arr_l[1, 1];
                label15.Text = arr_l[1, 2];
                label16.Text = arr_l[2, 0];
                label17.Text = arr_l[2, 1];
                label18.Text = arr_l[2, 2];
            }
            if (RBFS)
            {
                label19.Text = arr_l[0, 0];
                label20.Text = arr_l[0, 1];
                label21.Text = arr_l[0, 2];
                label22.Text = arr_l[1, 0];
                label23.Text = arr_l[1, 1];
                label24.Text = arr_l[1, 2];
                label25.Text = arr_l[2, 0];
                label26.Text = arr_l[2, 1];
                label27.Text = arr_l[2, 2];
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Fill(new sbyte[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }, true, true);
            Random rand = new Random();
            if (textBox1.Text == "")
            {
                List<sbyte> arr = new List<sbyte> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int N = rand.Next(0, arr.Count);
                        field_bad[i, j] = arr[N];
                        arr.RemoveAt(N);
                    }
                }
            }
            else
            {
                int x = 0, y = 0;
                field_bad = new sbyte[3,3]
            {
            { 0, 1, 2},
            { 3, 4, 5},
            { 6, 7, 8}
            };
                int vrem = int.Parse(textBox1.Text);
                if (vrem < 0)
                {
                    field_bad = new sbyte[3, 3]
{
            { 0, 2, 1},
            { 3, 4, 5},
            { 6, 7, 8}
};
                    vrem = -vrem;
                }
                for (int i = 0; i < vrem; i++)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (field_bad[m, j] == 0)
                            {
                                x = m;
                                y = j;
                            }
                        }
                    }
                    int act = rand.Next(0, 4);
                    while (!(act == 0 && x != 0 || act == 1 && y != 2 || act == 2 && x != 2 || act == 3 && y != 0))
                        act = rand.Next(0, 3);
                    field_bad = Action(field_bad, act, x, y);
                }
            }
            Fill(field_bad, false, false);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List <int[]> lkj = new List<int[]>();
            G = 0;
            //RBFS
            int N_rbfs = 0;
            Node_rbfs a_rbfs = new Node_rbfs(field_bad, -1, null);
            int f_limit = 0;
            Node_rbfs result_rbfs = RBFS_m.RBFS(a_rbfs, ref f_limit, ref N_rbfs);
            Fill(result_rbfs.field, false, true);
            label34.Text = "Кол-во итераций: " + N_rbfs;
            label35.Text = "Решение: Есть";
            //IDS
            Node result_ids = new Node();
            int N_ids = 0;
            for (int i = 1; i < 20; i++)
            {
                Node a = new Node(field_bad, -1, 0, 0, null);
                if (IDS_m.IDS(i, a, out result_ids, ref N_ids))
                    break;
            }
            Fill(result_ids.field, true, false);
            label31.Text = "Глубина: " + result_ids.depth;
            label32.Text = "Кол-во итераций: " + N_ids;
            label33.Text = "Решение: Есть";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
