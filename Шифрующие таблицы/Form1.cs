using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Шифрующие_таблицы
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
        }

        //Глобальные переменные
        public int key = 0;
        public string text = "";
        public int leng = 0;

        //Функция дешифрования\шифрования
        public string TransformationText(int k, bool Sh)
        {
            string text2 = "";
            int l = 0;
            for (int j=0; j<k; j++)
            {
                for (int i = j; i < leng; i += k)
                {
                    text2 += text[i];
                    l++;
                    if (l == 5 && Sh)
                    {
                        text2 += " ";
                        l = 0;
                    }
                }
            }
            return text2;
        }

        //Проверка на ошибки
        public bool Error()
        {
            try
            {
                key = int.Parse(textBox1.Text);
                text = textBox2.Text.Replace(" ", string.Empty).Replace("\r\n", string.Empty).ToUpper();
                leng = text.Length;
                if (leng < key)
                {
                    MessageBox.Show("Ключ должен быть меньше длины строки", "Ошибка");
                    return true;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ключ должен быть натуральным числом", "Ошибка");
                return true;
            }
            return false;
        }

        //Шифрование
        private void button3_Click(object sender, EventArgs e)
        {
            if (!Error())
            {
                textBox3.Text= TransformationText(key, true);
            }

        }

        //Дешифровка
        private void button4_Click(object sender, EventArgs e)
        {
            if (!Error())
            {
                int k = 0;
                if (leng % key != 0) k = leng / key + 1;
                else k = leng / key;
                textBox3.Text = TransformationText(k, false);
            }
        }

        //Открытие файла
        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Текстовый файл | *.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        textBox2.Text = File.ReadAllText(openFileDialog1.FileName);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        //Сохранение файла
        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Текстовый файл|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox3.Text);
            }
        }

        //Таблица
        private void button5_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.Visible)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Visible = true;
                button5.Text = "Скрыть таблицу";
                button3.Visible = false;
                button4.Visible = false;
                int l = 0;
                if (leng % key == 0) l = leng / key;
                else l = leng / key + 1;
                for(int i=0; i<key; i++)
                {
                    dataGridView1.Columns.Add("", i + 1.ToString());
                }

            }
            else
            {
                dataGridView1.Visible = false;
                button3.Visible = true;
                button4.Visible = true;
                button5.Text = "Открыть таблицу";
            }
        }
    }
}
