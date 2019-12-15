using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = Lab1.Models.Message;

namespace FormLab1
{
    public partial class Form1 : Form
    {
        public string login { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
                return;
            login = textBox1.Text;
            string password= textBox3.Text;
            if (!await WebService.Login(login,password))
            {
                MessageBox.Show("Неверный логин и (или) пароль");
                return;
            }
            UpdateWall();
            listBox1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            textBox2.Visible = true;
            label3.Visible = true;
            label3.Text = "Логин: "+login;
            button1.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }

        async Task UpdateWall()
        {
            while (true)
            {
                bool flag = await WebService.CheckUpdates(login);
                if (flag)
                {

                    List<Message> messages = await WebService.GetWall(login);
                    listBox1.Items.Clear();
                    foreach (Message message in messages)
                    {
                        listBox1.Items.Add($"{message.Login}: {message.Text}");
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string message = textBox2.Text;
            try
            {
                await WebService.SendMessage(message, login);
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка соединения");
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            textBox1.Text = "";
            textBox3.Text = "";
            button1.Visible = true;
            textBox1.Visible = true;
            textBox3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
        }
    }
}
