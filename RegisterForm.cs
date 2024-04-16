using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planner_with_postgresql
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UserNameFirst.Text = "Введите логин";
            UserNameSecond.Text = "Введите пароль";
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Close_button_MouseEnter(object sender, EventArgs e)
        {
            Close_button.ForeColor = Color.Red;
        }

        private void Close_button_MouseLeave(object sender, EventArgs e)
        {
            Close_button.ForeColor = Color.White;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void UserNameFirst_Enter(object sender, EventArgs e)
        {
            if (UserNameFirst.Text == "Введите логин")
            {
                UserNameFirst.Text = "";
                UserNameFirst.ForeColor = Color.Black;
            }
                
        }

        private void UserNameFirst_Leave(object sender, EventArgs e)
        {
            if (UserNameFirst.Text == "")
            {
                UserNameFirst.Text = "Введите логин";
                UserNameFirst.ForeColor = Color.Gray;
            }
                
        }

        private void UserNameSecond_Enter(object sender, EventArgs e)
        {
            if (UserNameSecond.Text == "Введите пароль")
            {
                UserNameSecond.Text = "";
                UserNameSecond.ForeColor = Color.Black;
            }
        }

        private void UserNameSecond_Leave(object sender, EventArgs e)
        {
            if (UserNameSecond.Text == "")
            {
                UserNameSecond.Text = "Введите пароль";
                UserNameSecond.ForeColor = Color.Gray;
            }
        }

        public Boolean checkUser()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.\"Users\" WHERE login = @ul", db.getConnection());
            command.Parameters.Add("@ul", NpgsqlTypes.NpgsqlDbType.Text).Value = UserNameFirst.Text;
            
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Find user with this login. Write new login");
                return true;
            }
            else
                return false; 
        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (UserNameFirst.Text == "Введите логин")
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (UserNameSecond.Text == "Введите пароль")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (checkUser())
                return;

            DB db = new DB();
            
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.\"Users\" ( login, password_user) VALUES(@log , @passw)",db.getConnection());
            command.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Text).Value = UserNameFirst.Text;
            command.Parameters.Add("@passw", NpgsqlTypes.NpgsqlDbType.Text).Value = UserNameSecond.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                this.Hide();
                LoginForm1 loginForm = new LoginForm1();
                loginForm.Show();
            }
            else
                MessageBox.Show("Not completed");

            db.closeConnection();
            
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm1 loginForm = new LoginForm1();
            loginForm.Show();
        }
    }
}
