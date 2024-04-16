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
    public partial class AddForm : Form
    {
        private int userId;
        public AddForm(int id)
        {
            InitializeComponent();
            userId = id;
            TaskName.Text = "Введите задачу";
            CatigoryName.Text = "Введите категорию";
            DataName.Text = "Введите дату";
        }



        private void Close_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm(userId);
            mainForm.Show();
            DB db = new DB();
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

        

        private void TaskName_Enter(object sender, EventArgs e)
        {
            if (TaskName.Text == "Введите задачу")
            {
                TaskName.Text = "";
                TaskName.ForeColor = Color.Black;
            }
        }

        private void TaskName_Leave(object sender, EventArgs e)
        {
            if (TaskName.Text == "")
            {
                TaskName.Text = "Введите задачу";
                TaskName.ForeColor = Color.Gray;
            }
        }

        private void CatigoryName_Enter(object sender, EventArgs e)
        {
            if (CatigoryName.Text == "Введите категорию")
            {
                CatigoryName.Text = "";
                CatigoryName.ForeColor = Color.Black;
            }
        }

        private void CatigoryName_Leave(object sender, EventArgs e)
        {
            if (CatigoryName.Text == "")
            {
                CatigoryName.Text = "Введите категорию";
                CatigoryName.ForeColor = Color.Gray;
            }
        }

        private void DataName_Enter(object sender, EventArgs e)
        {
            if (DataName.Text == "Введите дату")
            {
                DataName.Text = "";
                DataName.ForeColor = Color.Black;
            }
        }

        private void DataName_Leave(object sender, EventArgs e)
        {
            if (DataName.Text == "")
            {
                DataName.Text = "Введите дату";
                DataName.ForeColor = Color.Gray;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (TaskName.Text == "Введите задачу")
            {
                MessageBox.Show("Введите задачу");
                return;
            }

            DB db = new DB();
            db.openConnection(); // Открываем подключение

            string title = TaskName.Text; // Получаем название задачи

            NpgsqlCommand command;

            // Проверяем наличие введенных данных в текстовых полях для категории и даты
            if (!string.IsNullOrEmpty(CatigoryName.Text) && CatigoryName.Text != "Введите категорию" && !string.IsNullOrEmpty(DataName.Text))
            {
                string category = CatigoryName.Text;
                string dat = DataName.Text;

                // Проверка, существует ли категория в таблице "Categories"
                string checkCategoryQuery = "SELECT id FROM public.\"Categories\" WHERE title = @category AND users_id = @userId";
                NpgsqlCommand checkCategoryCommand = new NpgsqlCommand(checkCategoryQuery, db.getConnection());
                checkCategoryCommand.Parameters.AddWithValue("@category", category);
                checkCategoryCommand.Parameters.AddWithValue("@userId", userId);

                object categoryId = checkCategoryCommand.ExecuteScalar(); // Получаем id категории, если она существует

                // Если категория не существует, добавляем ее в таблицу "Categories"
                if (categoryId == null || categoryId == DBNull.Value)
                {
                    string insertCategoryQuery = "INSERT INTO public.\"Categories\" (title, users_id) VALUES (@category, @userId) RETURNING id";
                    NpgsqlCommand insertCategoryCommand = new NpgsqlCommand(insertCategoryQuery, db.getConnection());
                    insertCategoryCommand.Parameters.AddWithValue("@category", category);
                    insertCategoryCommand.Parameters.AddWithValue("@userId", userId);

                    categoryId = insertCategoryCommand.ExecuteScalar(); // Получаем id новой категории
                }

                DateTime dateValue;
                DateTime.TryParse(dat, out dateValue);


                // Проверка, существует ли дата в таблице "Priorities"
                string checkDateQuery = "SELECT id FROM public.\"Priorities\" WHERE date_time = @data AND users_id = @userId";
                NpgsqlCommand checkDateCommand = new NpgsqlCommand(checkDateQuery, db.getConnection());
                checkDateCommand.Parameters.AddWithValue("@data", dateValue);
                checkDateCommand.Parameters.AddWithValue("@userId", userId);

                object priorityId = checkDateCommand.ExecuteScalar(); // Получаем id даты, если она существует

                // Если дата не существует, добавляем ее в таблицу "Priorities"
                if (priorityId == null || priorityId == DBNull.Value)
                {
                    string insertDateQuery = "INSERT INTO public.\"Priorities\" (date_time, users_id) VALUES (@dateValue, @userId) RETURNING id";
                    NpgsqlCommand insertDateCommand = new NpgsqlCommand(insertDateQuery, db.getConnection());
                    insertDateCommand.Parameters.AddWithValue("@dateValue", dateValue);
                    insertDateCommand.Parameters.AddWithValue("@userId", userId);

                    priorityId = insertDateCommand.ExecuteScalar(); // Получаем id новой даты
                }

                // Создание и выполнение запроса на вставку задачи с категорией и датой
                command = new NpgsqlCommand("INSERT INTO public.\"Tasks\" (title, completed, category_id, priority_id, users_id) VALUES(@title, @completed, @category_id, @priority_id, @users_id)", db.getConnection());
                command.Parameters.Add("@title", NpgsqlTypes.NpgsqlDbType.Text).Value = title;
                command.Parameters.Add("@completed", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0; // Значение завершенности задачи
                command.Parameters.Add("@category_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = categoryId; // ID категории, связанной с задачей
                command.Parameters.Add("@priority_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = priorityId; // ID приоритета, связанного с задачей
                command.Parameters.Add("@users_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = userId; // ID пользователя, связанного с задачей
            }
            else
            {
                // Если категория или дата не указаны, добавляем только задачу без категории и даты
                command = new NpgsqlCommand("INSERT INTO public.\"Tasks\" (title, completed, users_id) VALUES(@title, @completed, @users_id)", db.getConnection());
                command.Parameters.Add("@title", NpgsqlTypes.NpgsqlDbType.Text).Value = title;
                command.Parameters.Add("@completed", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0; // Значение завершенности задачи
                command.Parameters.Add("@users_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = userId; // ID пользователя, связанного с задачей
            }

            db.openConnection(); // Открываем подключение

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Задача добавлена");
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
            {
                MessageBox.Show("Не удалось добавить задачу");
            }

            db.closeConnection();
        }
    }
}
