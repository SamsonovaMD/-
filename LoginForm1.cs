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
    public partial class LoginForm1 : Form
    {
        public LoginForm1()
        {
            InitializeComponent();
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

        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            String LoginUser = LoginField.Text;
            String PassUser = PasswordField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.\"Users\" WHERE login = @ul AND password_user = @up", db.getConnection());
            command.Parameters.Add("@ul", NpgsqlTypes.NpgsqlDbType.Text).Value = LoginUser;
            command.Parameters.Add("@up", NpgsqlTypes.NpgsqlDbType.Text).Value = PassUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            int userId = Convert.ToInt32(table.Rows[0]["id"]);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm(userId);
                mainForm.Show();
                string sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
             "FROM public.\"Tasks\" t " +
             "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
             "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
             "WHERE t.users_id = @userId ORDER BY t.completed";
                NpgsqlCommand command_t = new NpgsqlCommand(sql, db.getConnection());
                command_t.Parameters.Add("@userId", NpgsqlTypes.NpgsqlDbType.Integer).Value = userId;

                NpgsqlDataAdapter adapter_t = new NpgsqlDataAdapter(command_t);
                DataTable tasksTable = new DataTable();
                adapter_t.Fill(tasksTable);
                mainForm.DisplayTasks(tasksTable);
                mainForm.PopulateComboBoxWithUniqueColumnValues(tasksTable, "completed", mainForm.comboCompleted);
                mainForm.PopulateComboBoxWithUniqueColumnValues(tasksTable, "category_id", mainForm.comboCategory);
                mainForm.comboCategory.Items.Add("");
                mainForm.comboCompleted.Items.Add("");
                mainForm.comboTasks.Items.Add("");
                mainForm.comboData.Items.Add("");
            }
            else
                MessageBox.Show("Can't find user");
        }

        private void NotRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
