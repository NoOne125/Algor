using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Game_6
{
    public partial class Form1 : Form
    {
        Graphics gr_1, gr_3;
        bool[] turn = new bool[4];
        int hand = 0;
        int score_lose = 0;
        int score_win = 0;
        int N_gr;
        Belt game;

        System.Timers.Timer fps_10, fps_100;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            hand = 0;
            score_lose = 0;
            score_win = 0;

            label1.Text = "Мой максимальный ранг: —";
            label4.Text = "Врага максимальный ранг: —";
            label5.Text = "Кол - во ваших комбинаций: 0";
            label6.Text = "Кол - во врага комбинаций: 0";
            
            gr_1 = pictureBox1.CreateGraphics();
            gr_1.Clear(ColorTranslator.FromHtml("#FFE4D7"));
            gr_3 = pictureBox3.CreateGraphics();
            gr_3.Clear(ColorTranslator.FromHtml("#FFE4D7"));

            if (radioButton1.Checked)
                N_gr = 3;
            else
                N_gr = 4;

            game = new Belt(N_gr);

            fps_10 = new System.Timers.Timer();
            fps_10.Interval = 200;

            fps_100 = new System.Timers.Timer();
            fps_100.Interval = 1000;

            int N_get_2;
            List<Card> Get_1;
            game.Full_Hand(out N_get_2, out Get_1);

            Anim_Full_Hand(N_get_2, Get_1);
        }

        void Anim_Full_Hand(int N_get_2, List<Card> Get_1)
        {
            bool get = false;
            bool my = true;
            double x = 930;
            double y = 300;
            fps_10.Elapsed += (o, e) => Anim_Get(ref N_get_2, ref Get_1, ref get, ref x, ref y, ref my);
            fps_10.Start();

            while (N_get_2!=0 || Get_1.Count != 0)
            {

            }


            Draw_Field();
            turn = new bool[4];
            fps_10.Stop();
        }

        void Anim_Get(ref int N_get_2, ref List<Card> Get_1, ref bool get, ref double x, ref double y, ref bool my)
        {
            gr_1.Clear(ColorTranslator.FromHtml("#FFE4D7"));
            if (game.N + N_get_2 + Get_1.Count > 0)
                Draw_Card(gr_1, 905, 300, Math.PI / 2, false, game.trump, false);
            if (game.N + N_get_2 + Get_1.Count > 1)
            {
                Draw_Card(gr_1, 930, 300, 0, true, new Card(), false);
            }

            if (Get_1.Count == 0 && N_get_2 == 0)
                return;

            double y_0;
            if (my)
                y_0 = 600;
            else
                y_0 = 0;

            if (!get)
            {
                get = true;
                x = 930;
                y = 300;
            }
            else
            {
                x -= (930 - 700) / (1000 / fps_10.Interval);
                y -= (300 - y_0) / (1000 / fps_10.Interval);
                if (!my)
                    Draw_Card(gr_1, x, y, 0, true, game.trump, false);
                else
                    Draw_Card(gr_1, x, y, 0, false, Get_1[0], false);
                if (y == y_0)
                {
                    if (my)
                    {
                        Draw_Card(gr_3, 4 * (hand + 1) + hand * 100 + 50, 76, 0, false, Get_1[0], false);
                        Get_1.RemoveAt(0);
                        hand++;
                    }
                    else
                        N_get_2--;
                    x = 930;
                    y = 300;
                    my = !my;
                    if (Get_1.Count == 0)
                        my = false;
                    else if (N_get_2 == 0)
                        my = true;
                    get = false;
                    if (my)
                    {
                        y_0 = 600;
                    }
                    else
                    {
                        y_0 = 0;
                    }
                }
            }
        }

        void Num_cards()
        {
            if (game.N > 0)
                Draw_Card(gr_1, 905, 300, Math.PI / 2, false, game.trump, false);
            if (game.N > 1)
            {
                Draw_Card(gr_1, 930, 300, 0, true, new Card(), false);
                gr_1.DrawString(Convert.ToString(game.N), new Font(FontFamily.GenericSansSerif, 24.0F, FontStyle.Bold), Brushes.Black, 908, 280);
            }
        }

        void Draw_Card(Graphics gr, double x, double y, double angle, bool close, Card card, bool small)
        {
            int width = 100;
            int height = 150;
            if (small)
            {
                width = 50;
                height = 75;
            }
            x -= Math.Sin(angle) * height / 2;
            y -= Math.Cos(angle) * height / 2;
            x -= Math.Cos(angle) * width / 2;
            y += Math.Sin(angle) * width / 2;
            double x_2 = x + Math.Cos(angle) * width;
            double y_2 = y - Math.Sin(angle) * width;
            double x_3 = x + Math.Sin(angle) * height;
            double y_3 = y + Math.Cos(angle) * height;
            double x_4 = x_3 + Math.Cos(angle) * width;
            double y_4 = y_3 - Math.Sin(angle) * width;
            PointF[] loc = new PointF[4] { new PointF((float)x, (float)y), new PointF((float)x_2, (float)y_2), new PointF((float)x_4, (float)y_4), new PointF((float)x_3, (float)y_3) };

            if (close){
                gr.FillPolygon(new SolidBrush(Color.LightGray), loc);
                gr.DrawPolygon(new Pen(Color.Black), loc);
                return;
            }

            gr.FillPolygon(new SolidBrush(Color.White), loc);
            gr.DrawPolygon(new Pen(Color.Black), loc);

            gr.TranslateTransform((float)x, (float)y);
            gr.RotateTransform((float)(-angle*180/Math.PI));
            SizeF textSize = gr.MeasureString(card.Card_Get(), DefaultFont);

            Brush k;
            if (card.suit % 2 == 0)
                k = Brushes.Red;
            else
                k = Brushes.Black;

            Font j = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Bold);
            if(small)
                j = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            gr.DrawString(card.Card_Get(), j , k, (textSize.Width / 2) - 7, (textSize.Height / 2) - 5);

            gr.RotateTransform((float)(angle * 180 / Math.PI));
            gr.TranslateTransform((float)(-x), (float)(-y));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gr_1 == null)
                return;
            gr_1.Clear(ColorTranslator.FromHtml("#FFE4D7"));
            gr_3.Clear(ColorTranslator.FromHtml("#FFE4D7"));

            label1.Text = "Мой максимальный ранг: —";
            label4.Text = "Врага максимальный ранг: —";
            label5.Text = "Кол - во ваших комбинаций: 0";
            label6.Text = "Кол - во врага комбинаций: 0";

            game = new Belt(N_gr);

            score_lose++;
            hand = 0;

            label2.Text = $"{score_win} : {score_lose}";

            int N_get_2;
            List<Card> Get_1;
            game.Full_Hand(out N_get_2, out Get_1);

            Anim_Full_Hand(N_get_2, Get_1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gr_1 == null && (turn[0] || turn[1] || turn[2] || turn[3]))
                return;
            fps_100.Stop();
            System.Threading.Thread.Sleep(500);
            gr_3.Clear(ColorTranslator.FromHtml("#FFE4D7"));
            Draw_Field();
            game.Turn(turn, true);
            List<Card> hand_card = game.Get_Hand();
            turn = new bool[4];
            System.Threading.Thread.Sleep(500);
            for (int i = 0; i < hand_card.Count; i++)
                Draw_Card(gr_3, 4 * (i + 1) + i * 100 + 50, 76, 0, false, hand_card[i], false);
            hand = hand_card.Count();
            int N_get_2;
            List<Card> Get_1;
            System.Threading.Thread.Sleep(2000);
            if (game.gamer_comb.Count > 0)
                label1.Text = $"Мой максимальный ранг: {game.gamer_comb.Max()}";
            label5.Text = $"Кол - во ваших комбинаций: {game.gamer_comb.Count}";
            System.Threading.Thread.Sleep(1000);
            game.Turn(game.Bot(), false);
            Draw_Field();
            System.Threading.Thread.Sleep(2000);
            if (game.opponent_comb.Count > 0)
                label4.Text = $"Врага максимальный ранг: {game.opponent_comb.Max()}";
            label6.Text = $"Кол - во врага комбинаций: {game.opponent_comb.Count}";
            game.Full_Hand(out N_get_2, out Get_1);
            Anim_Full_Hand(N_get_2, Get_1);
            if(game.N==0 && game.Get_Hand().Count == 0)
            {
                System.Threading.Thread.Sleep(5000);
                if (game.opponent_comb.Count > game.gamer_comb.Count)
                {
                    score_lose++;
                }
                else if(game.opponent_comb.Count < game.gamer_comb.Count)
                {
                    score_win++;
                }
                else
                {
                    score_win++;
                    score_lose++;
                }
                gr_1.Clear(ColorTranslator.FromHtml("#FFE4D7"));
                gr_3.Clear(ColorTranslator.FromHtml("#FFE4D7"));

                label1.Text = "Мой максимальный ранг: —";
                label4.Text = "Врага максимальный ранг: —";
                label5.Text = "Кол - во ваших комбинаций: 0";
                label6.Text = "Кол - во врага комбинаций: 0";

                game = new Belt(N_gr);

                hand = 0;

                label2.Text = $"{score_win} : {score_lose}";

                game.Full_Hand(out N_get_2, out Get_1);

                Anim_Full_Hand(N_get_2, Get_1);
            }
        }

        void Draw_Field()
        {
            gr_1.Clear(ColorTranslator.FromHtml("#FFE4D7"));
            Num_cards();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 4 && j == 1)
                        return;
                    gr_1.DrawRectangle(Pens.Black, 20 + 210 * j, 100 + i * 80, 200, 75);
                    List<Card> loc = game.Get_Field()[i*3+j];
                    List<Card> loc_2 = game.Get_Hand();

                    for (int m = loc_2.Count - 1; m >= 0; m--)
                    {
                        if (turn[m] && loc_2[m].rank == i * 3 + j + 2)
                            loc.Add(loc_2[m]);
                    }
                    for(int k =0; k < loc.Count(); k++)
                    {
                        Draw_Card(gr_1, 20 + 210 * j + 50 * k + 25, 100 + i * 80 + 37.5, 0, false, loc[k], true);
                    }
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int cur_x = (pictureBox3.PointToClient(Cursor.Position).X - 15) / 105;

            if (gr_3 != null)
            {
                if (!turn[cur_x])
                {
                    turn[cur_x] = true;
                    gr_3.FillEllipse(Brushes.Green, 4 * (cur_x + 1) + cur_x * 100 + 20, 46, 60, 60);
                }
                else
                {
                    turn[cur_x] = false;
                    gr_3.FillEllipse(Brushes.White, 4 * (cur_x + 1) + cur_x * 100 + 20, 46, 60, 60);
                }

                if (turn[0] || turn[1] || turn[2] || turn[3])
                {
                    if (!fps_100.Enabled)
                        Anim_Turn();
                }
                else
                {
                    if (fps_100.Enabled)
                        fps_100.Stop();
                }
            }
        }

        private void Anim_Turn()
        {
            fps_100.Elapsed += (o, e) => Draw_Field();
            fps_100.Start();
        }

    }
}
