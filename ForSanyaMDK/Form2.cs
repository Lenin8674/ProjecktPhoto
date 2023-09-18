using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForSanyaMDK
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "1")
            {
                MessageBox.Show("Добро пожаловать admin", "Успешно");
                Form3 form = new Form3();
                form.Show();
                this.Hide();
             
            }
            else if (textBox1.Text == "user" && textBox2.Text == "2")
            {
                MessageBox.Show("Добро пожаловать user", "Успешно");
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль", "Ошибка авторизации");
            }
        }
    }
}
