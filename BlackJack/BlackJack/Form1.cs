using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        struct Player
        {
            public string login;
            public int balance;
        }
        public Form1()
        {
            InitializeComponent();
        }

        Form2 Form2 = new Form2();
        public bool PasswordCheck(string data, string input)
        {
            if (data.Substring(data.IndexOf(']') + 1, data.IndexOf('|') - 1 - data.IndexOf(']')) == input)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string inputLogin = textBox1.Text;
            string inputPassword = textBox2.Text;
            bool flag = true;
            string temporaryString = "";

            StreamReader SR = new StreamReader("database.txt");
            while (!SR.EndOfStream)
            {
                temporaryString = SR.ReadLine();

                if (inputLogin == temporaryString.Substring(1, temporaryString.IndexOf(']') - 1))
                {
                    flag = false;
                    break;
                }
            }

            if (inputPassword.IndexOf('|') != -1 || inputPassword.IndexOf(']') != -1 || inputPassword.IndexOf('[') != -1 || inputPassword.IndexOf('\\') != -1 || inputLogin.IndexOf('|') != -1 || inputLogin.IndexOf(']') != -1 || inputLogin.IndexOf('[') != -1 || inputLogin.IndexOf('\\') != -1)
            {
                flag = false;
            }

            SR.Close();

            if (flag)
            {
                Player User = new Player();

                User.login = inputLogin;
                User.balance = 5000;

               

                StreamWriter SW = new StreamWriter("database.txt", true);
                SW.WriteLine("[{0}]{1}|{2}", User.login, inputPassword, User.balance);

                SW.Close();

                this.Hide();
                Form2.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Пользователь уже зарегистрирован или присутствует недопустимый символ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string inputLogin = textBox1.Text;
            string inputPassword = textBox2.Text;
            bool flag = false;
            string temporaryString = "";

            StreamReader SR = new StreamReader("database.txt");
            while (!SR.EndOfStream)
            {
                temporaryString = SR.ReadLine();

                if (inputLogin == temporaryString.Substring(1, temporaryString.IndexOf(']') - 1))
                {
                    flag = true;
                    break;
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                if (PasswordCheck(temporaryString, inputPassword))
                {
                    Player User = new Player();

                    User.login = inputLogin;
                    User.balance = Convert.ToInt32(temporaryString.Substring(temporaryString.IndexOf('|') + 1, temporaryString.Length - temporaryString.IndexOf('|') - 1));

                    this.Hide();
                    Form2.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }

            SR.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
