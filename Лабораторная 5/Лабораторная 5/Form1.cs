using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool start = false;
        int moves = 1;
        int complexity = 0;
        int[,] field = new int[8, 8];
        Graphics gr;
        private void button1_Click(object sender, EventArgs e)
        {
            gr = pictureBox1.CreateGraphics();

            label1.Text = "";

            start = true;
            moves = 0;
            field = new int[8, 8];

            if (radioButton1.Checked)
                complexity = 0;
            else if (radioButton2.Checked)
                complexity = 1;
            else
                complexity = 2;

            Draw_Field();
            moves++;
            Draw_Figure(3, 4);
            moves++;
            Draw_Figure(3, 3);
            moves++;
            Draw_Figure(4, 3);
            moves++;
            Draw_Figure(4, 4);

            moves = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (start)
            {
                int cur_x = (PointToClient(Cursor.Position).X - 15) / 50;
                int cur_y = (PointToClient(Cursor.Position).Y - 15) / 50;

                if (field[cur_x, cur_y] == 0)
                {
                    moves++;

                    if (Result_Move(cur_x, cur_y, false, field))
                    {
                        Draw_Figure(cur_x, cur_y);
                        if (IsFinal(field))
                            Final();
                        else
                        {
                            Bot_Turn();
                        }
                    }
                    else
                        moves--;
                }
            }
        }

        void Draw_Field()
        {
            gr.Clear(Color.Plum);
            Pen black = new Pen(Brushes.Black);
            for (int i = 1; i < 8; i++)
            {
                gr.DrawLine(black, i * 50, 0, i * 50, 400);
                gr.DrawLine(black, 0, i * 50, 400, i * 50);
            }
        }

        void Draw_Figure(int x, int y)
        {
            Brush circle;
            if (moves % 2 == 1)
                circle = Brushes.Black;
            else
                circle = Brushes.White;

            field[x, y] = moves % 2 + 1;

            x = x * 50 + 5;
            y = y * 50 + 5;
            gr.FillEllipse(circle, x, y, 40, 40);
        }

        void Bot_Turn()
        {
            moves++;

            int[,] cur = new int[8, 8];

            for (int r = 0; r < 8; r++)
            {
                for (int t = 0; t < 8; t++)
                {
                    cur[r, t] = field[r, t];
                }
            }

            Minimax(field, complexity * 2 + 1, -65, 65, true, ref cur);

            field = cur;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (field[i, j] == 1)
                    {
                        Draw_Figure(i, j);
                    }
                }
            }
            if (IsFinal(field))
                Final();
        }

        bool IsFinal(int[,] loc_field)
        {
            bool end = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (loc_field[i, j] == (moves + 1) % 2 + 1)
                    {
                        moves++;
                        end = !Result_Move(i, j, true, loc_field);
                        moves--;
                        if (!end)
                            return end;
                    }
                }
            }
            return end;
        }

        bool Result_Move(int x, int y, bool test, int[,] loc_field)
        {
            int cur = moves % 2 + 1;
            bool good_res = false;
            for (int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i == j && i==0)
                        continue;
                    int loc_i = 0;
                    int x_loc = x;
                    int y_loc = y;
                    do
                    {
                        loc_i++;
                        x_loc += i;
                        y_loc += j;
                        if (x_loc == 8 || y_loc == 8 || x_loc == -1 || y_loc == -1)
                            break;
                        if(loc_field[x_loc, y_loc] == 0)
                        {
                            if (test && loc_i>1)
                            {
                                return true;
                            }
                            break;
                        }
                        if (loc_field[x_loc, y_loc] != cur) 
                            continue;
                        if (loc_field[x_loc, y_loc] == cur)
                        {
                            if (!test)
                            {
                                if (loc_i - 1 != 0)
                                    good_res = true;
                            }

                            for (int k = 0; k < loc_i - 1; k++)
                            {
                                x_loc -= i;
                                y_loc -= j;

                                if (!test)
                                {
                                    Draw_Figure(x_loc, y_loc);
                                    field[x_loc, y_loc] = cur;
                                }
                            }

                            break;
                        }

                    } while (true);
                }
            }

            return good_res;
        }

        void Final()
        {
            start = false;
            int N_black = 0;
            int N_white = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (field[i, j] == 2)
                        N_black++;
                    else if (field[i, j] == 1)
                        N_white++;
                }
            }
            if (N_white > N_black) 
                label1.Text = "Победа Белых";
            else if(N_black > N_white)
                label1.Text = "Победа Чёрных";
            else
                label1.Text = "Победила Дружба";
        }

        int Minimax(int[,] loc_field, int depth, int alpha, int beta, bool maxPlay, ref int[,] res_field)
        {
            int[,] cur = new int[8, 8];

            for (int r = 0; r < 8; r++)
            {
                for (int t = 0; t < 8; t++)
                {
                    cur[r, t] = loc_field[r, t];
                }
            }

            if (depth == 0 || IsFinal(cur))
            {
                return Cool_Game(cur);
            }

            if (maxPlay)
            {
                int max = -65;
                List<int[,]> loc = HightWay_ToHell(1, cur);
                foreach (int[,] k in loc)
                {
                    int val = Minimax(k, depth - 1, alpha, beta, false, ref res_field);
                    if (max < val)
                    {
                        max = val;
                        if (depth == complexity * 2 + 1)
                        {
                            res_field = k;
                        }
                    }
                    alpha = Math.Max(alpha, val);
                    if (beta <= alpha)
                        break;
                }
                return max;
            }
            else
            {
                int min = 65;
                List<int[,]> loc = HightWay_ToHell(2, cur);
                foreach (int[,] k in loc)
                {
                    int val = Minimax(k, depth - 1, alpha, beta, true, ref res_field);
                    min = Math.Min(min, val);
                    beta = Math.Min(beta, val);
                    if (beta <= alpha)
                        break;
                }
                return min;
            }
        }

        List<int[,]> HightWay_ToHell(int color, int[,] loc_field)
        {
            List<int[,]> turns = new List<int[,]>();
            for (int k = 0; k < 8; k++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if (loc_field[k, l] == 0)
                    {
                        bool edit = false;
                        int[,] new_field = new int[8, 8];
                        for (int r = 0; r < 8; r++)
                        {
                            for (int t = 0; t < 8; t++)
                            {
                                new_field[r, t] = loc_field[r, t];
                            }
                        }
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (i == j && i == 0)
                                    continue;
                                int loc_i = 0;
                                int x_loc = k;
                                int y_loc = l;
                                do
                                {
                                    loc_i++;
                                    x_loc += i;
                                    y_loc += j;
                                    if (x_loc == 8 || y_loc == 8 || x_loc == -1 || y_loc == -1)
                                        break;
                                    if (new_field[x_loc, y_loc] == 0)
                                        break;
                                    if (new_field[x_loc, y_loc] != color)
                                        continue;
                                    if (new_field[x_loc, y_loc] == color)
                                    {
                                        if (loc_i > 1)
                                            edit = true;
                                        for (int m = 0; m < loc_i; m++)
                                        {
                                            x_loc -= i;
                                            y_loc -= j;

                                            new_field[x_loc, y_loc] = color;
                                        }
                                        break;
                                    }
                                } while (true);
                            }
                        }
                        if (edit)
                            turns.Add(new_field);
                    }
                }
            }
            return turns;
        }

        int Cool_Game(int[,] fieled)
        {
            int scores = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (fieled[i, j] == 1)
                    {
                        scores++;
                    }
                    else if (fieled[i, j] == 2)
                        scores--;
                }
            }
            return scores;
        }

    }
}
