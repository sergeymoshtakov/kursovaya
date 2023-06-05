using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private TextBox textBox1;
        private Button button3;
        private Label label1;
        private int targetNumber;
        private int attempts = 10;
        private int min = 1;
        private int max = 1000;
        private int guess;
        private Button button4;
        private Button button5;
        private bool answ;
        private int moneyBalance = 1000;
        private Label label3;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Hide();

            textBox1 = new TextBox();
            textBox1.Location = new System.Drawing.Point(button2.Left, button2.Top);
            Controls.Add(textBox1);

            button3 = new Button();
            button3.Text = "Ввести число";
            button3.Location = new System.Drawing.Point(textBox1.Left, textBox1.Top + 50);
            button3.Click += button3_Click;
            Controls.Add(button3);

            label1 = new Label();
            label1.Text = "Введіть число від 1 до 1000";
            label1.Location = new System.Drawing.Point(textBox1.Left, textBox1.Top - 50);
            label1.Size = new System.Drawing.Size(294, 25);
            Controls.Add(label1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num;
            if (int.TryParse(textBox1.Text, out num))
            {
                if ((num < min) || (num > max))
                {
                    MessageBox.Show("Число повинно бути у діапазоні від 1 до 1000!");
                    textBox1.Text = "";
                }
                else
                {
                    targetNumber = num;
                    textBox1.Hide();
                    button3.Hide();
                    Game();
                }
            }
            else
            {
                MessageBox.Show("У текстовому поли повинні бути тільки цифри!");
                textBox1.Text = "";
            }
        }
        private void Game()
        {
            createButtons();
            showQuestion();
        }

        public void createButtons()
        {
            button4 = new Button();
            button4.Text = "Так";
            button4.Location = new System.Drawing.Point(textBox1.Left - 50, textBox1.Top + 50);
            button4.Click += button4_Click;
            Controls.Add(button4);

            button5 = new Button();
            button5.Text = "Ні";
            button5.Location = new System.Drawing.Point(textBox1.Left + 50, textBox1.Top + 50);
            button5.Click += button5_Click;
            Controls.Add(button5);

            label1.Text = "Питання";
            label1.Location = new System.Drawing.Point(textBox1.Left - 50, textBox1.Top);
            label1.Size = new System.Drawing.Size(294, 25);
            Controls.Add(label1);

            label3 = new Label();
            label3.Text = "Ваш баланс складає " + moneyBalance + " $.";
            label3.Location = new System.Drawing.Point(20, 20);
            label3.Size = new System.Drawing.Size(294, 25);
            Controls.Add(label3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            answ = true;
            if (targetNumber > guess)
            {
                moneyBalance += 100;
                updateMoney();
            }
            else
            {
                moneyBalance -= 100;
                updateMoney();
            }
            min = guess + 1;

            showQuestion();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            answ = false;
            if (targetNumber < guess)
            {
                moneyBalance += 100;
                updateMoney();
            }
            else
            {
                moneyBalance -= 100;
                updateMoney();
            }
            max = guess - 1;

            showQuestion();
        }

        private void showQuestion()
        {
            if (attempts > 0)
            {
                guess = (min + max) / 2;
                label1.Text = $"Чи правда, що задане число більше за {guess} ?";
                attempts--;
            }
            else if (attempts == 0)
            {
                label1.Location = new System.Drawing.Point(textBox1.Left, textBox1.Top);
                guess = guess + 1;
                label1.Text = $"Це число дорівнює {guess} ?";
                attempts--;
            }
            else
            {
                EndGame();
            }
        }
        private void EndGame()
        {
            label1.Location = new System.Drawing.Point(textBox1.Left - 50, textBox1.Top - 20);
            label3.Hide();
            string endGame = "Кінець гри - ";
            if (answ)
            {
                endGame += $"Ви вийграли {moneyBalance} $!";
                DisplayCenteredImage("win.jpg");
            }
            else
            {
                endGame += $"Ви програли {moneyBalance} $!";
                DisplayCenteredImage("lose.png");
            }
            label1.Text = endGame;
            button4.Hide();
            button5.Hide();
        }

        private void updateMoney()
        {
            label3.Text = "Ваш баланс складає " + moneyBalance + " $.";
            if (moneyBalance < 0)
            {
                MessageBox.Show("Ви дуже багато брехали, тому ващі грощі закінчилися.");
                answ = false;
                EndGame();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void DisplayCenteredImage(string imagePath)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Image image = Image.FromFile(imagePath);
            pictureBox1.Image = image;
            pictureBox1.Visible = true;
        }
    }
}
