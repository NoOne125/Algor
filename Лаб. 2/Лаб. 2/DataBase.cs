using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Лаб._2
{
    class DataBase
    {
        Dictionary<int, int> ind_area = new Dictionary<int, int>();
        string index_area;
        int N;
        int empty;

        public DataBase(string name)
        {
            index_area = name;
            string path = $"..\\netcoreapp3.1\\{index_area}";
            StreamReader sr;
            try
            {
                 sr = new StreamReader(path);
            }
            catch
            {
                Fill();
                sr = new StreamReader(path);
            }
            string line;
            N = Convert.ToInt32(sr.ReadLine());
            empty = (int)(0.9 * N);
            while ((line = sr.ReadLine()) != null)
            {
                string[] k = new string[2];
                k = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                ind_area.Add(Convert.ToInt32(k[0]), Convert.ToInt32(k[1]));
            }
            sr.Close();
        }

        public void Fill()
        {
            StreamWriter sw = new StreamWriter(index_area, false, Encoding.Default);
            sw.WriteLine("100");
            for (int i = 1; i <= 100; i++)
            {
                sw.WriteLine($"{i * 100} {i - 1}");
                StreamWriter sw_main = new StreamWriter($"..\\netcoreapp3.1\\Main\\{i-1}.txt", false, Encoding.Default);
                Random rand = new Random();
                for (int j = 0; j < 80 + rand.Next(0,10); j++)
                {
                    if(rand.Next(0,10)!=2)
                        sw_main.WriteLine($"{(i-1) * 100 + j + 1} {rand.Next(0,10000)} 1");
                }

                sw_main.Close();
            }
            sw.Close();
        }



        public string Find(int key, bool delete, bool edit, string res, bool add, int key_res)
        {
            int real_key = ((int)(key / 100) + 1) * 100;
            int index = ind_area[real_key];
            StreamReader sr_cur = new StreamReader($"..\\netcoreapp3.1\\Main\\{index}.txt");
            List<int> l_k = new List<int>();
            List<string> l_v = new List<string>();
            string line;
            int n = 0;
            while ((line = sr_cur.ReadLine()) != null)
            {
                string[] arr = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                l_k.Add(Convert.ToInt32(arr[0]));
                l_v.Add(arr[1]);
                n++;
            }
            sr_cur.Close();
            int k = (int)Math.Log2(n);
            int ind = (int)Math.Pow(2, k);
            int h = 1;
            
            if (key < l_k[ind])
            {
                while(h!=0)
                {
                    if (key < l_k[ind])
                    {
                        h = (int)Math.Pow(2, k--);
                        ind = ind - (h / 2 + 1);
                    }
                    else if (key > l_k[ind])
                    {
                        h = (int)Math.Pow(2, k--);
                        ind = ind + (h / 2 + 1);
                    }
                    else if (key == l_k[ind])
                    {
                        if (add)
                            edit = true;
                        Edit(ind, index, delete, edit, res, false, key_res);
                        return l_v[ind];
                    }
                }
            }
            else if (key > l_k[ind] && n > (int)Math.Pow(2, k))
            {
                int l = (int)Math.Log2(n - (int)Math.Pow(2, k) + 1) + 1;
                ind = n + 1 - (int)Math.Pow(2, l);
                while (h != 0)
                {
                    if (key < l_k[ind])
                    {
                        h = (int)Math.Pow(2, --l);
                        ind = ind - (h / 2 + 1);
                    }
                    else if (key > l_k[ind])
                    {
                        h = (int)Math.Pow(2, --l);
                        ind = ind + (h / 2 + 1);
                    }
                    else if (key == l_k[ind])
                    {
                        if (add)
                            edit = true;
                        Edit(ind, index, delete, edit, res, false, key_res);
                        return l_v[ind];
                    }
                }
            }
            else if (key == l_k[ind])
            {
                if (add)
                    edit = true;
                Edit(ind, index, delete, edit, res, false, key_res);
                return l_v[ind];
            }
            Edit(ind, index, delete, edit, res, true, key_res);
            return null;
        }

        public void Edit(int index, int real_key, bool delete, bool edit, string res, bool add, int key_res)
        {
            StreamReader sr_cur = new StreamReader($"..\\netcoreapp3.1\\Main\\{real_key}.txt");
            string line;
            List<string> k = new List<string>();
            while ((line = sr_cur.ReadLine()) != null)
            {
                k.Add(line);
            }
            if (add)
            {
                k.Add($"{key_res} {res} 1");
                k.Sort();
            }
            if(k[index].Substring(k[index].Length-1, 1) == "0")
            {
                Console.WriteLine("Element none...");
            }
            sr_cur.Close();
            StreamWriter sw = new StreamWriter($"..\\netcoreapp3.1\\Main\\{real_key}.txt", false, Encoding.Default);
            for(int i = 0; i< k.Count; i++)
            {
                if(index == i)
                {
                    if (delete)
                        sw.WriteLine(k[i].Substring(0, k[i].Length - 1) + "0");
                    else if (edit)
                    {
                        string[] arr = k[i].Split(" ");
                        sw.WriteLine($"{arr[0]} {res} {arr[2]}");
                    }
                    else
                    {
                        sw.WriteLine(k[i]);
                    }
                }
                else
                {
                    sw.WriteLine(k[i]);
                }
            }
            sw.Close();
        }

            //public void Perestroyka()
            //{
            //    N = N * 2;
            //    empty = (int)(0.9 * N);
            //    string path = $"..\\netcoreapp3.1\\{index_area}";
            //    StreamReader sr = new StreamReader(path);
            //    sr.ReadLine();
            //    string text = $"{N}\r\n{sr.ReadToEnd()}";
            //    sr.Close();
            //    StreamWriter sw = new StreamWriter(index_area, false, Encoding.Default);
            //    sw.Write(text);
            //    sw.Close();
            //    int j = 0;
            //    int i = 0;
            //    int k = 0;
            //    text = "";
            //    while (j < 100)
            //    {
            //        StreamReader sr_cur = new StreamReader($"..\\netcoreapp3.1\\Main\\{j}.txt");
            //        while ((text = sr_cur.ReadLine()) != null)
            //        {
            //            text += "\n";
            //            k++;
            //            if (k == empty)
            //            {
            //                StreamWriter sw_cur = new StreamWriter($"..\\netcoreapp3.1\\Main\\{i}.txt", false, Encoding.Default);
            //                sw_cur.Write(text);
            //                sw_cur.Close();
            //                i++;
            //                text = "";
            //            }
            //        }
            //        j++;
            //        sr_cur.Close();
            //    }
            //    StreamWriter sw_cur_3 = new StreamWriter($"..\\netcoreapp3.1\\Main\\{i}.txt", false, Encoding.Default);
            //    sw_cur_3.Write(text);
            //    sw_cur_3.Close();
            //    i++;
            //    for (int i_0 = i; i_0 < j; i_0++)
            //    {
            //        StreamWriter sw_cur_2 = new StreamWriter($"..\\netcoreapp3.1\\Main\\{i_0}.txt", false, Encoding.Default);
            //        sw_cur_2.Write("");
            //        sw_cur_2.Close();
            //    }
            //}
        }
}
