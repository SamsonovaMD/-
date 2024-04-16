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
    public partial class MainForm : Form
    {
        private int userId;

        public MainForm(int id)
        {
            InitializeComponent();
            userId = id;

        }
        public void PopulateComboBoxWithUniqueColumnValues(DataTable tasksTable, string columnName, ComboBox comboBox)
        {
            comboBox.Items.Clear(); // Очистить ComboBox перед добавлением новых значений

            // Проверяем, существует ли столбец с указанным именем
            if (tasksTable.Columns.Contains(columnName))
            {
                // Создаем HashSet для хранения уникальных значений из столбца
                HashSet<string> uniqueValues = new HashSet<string>();

                // Получаем индекс столбца
                int columnIndex = tasksTable.Columns[columnName].Ordinal;

                // Добавляем уникальные непустые значения из столбца в HashSet
                foreach (DataRow row in tasksTable.Rows)
                {
                    string value = row[columnIndex].ToString().Trim(); // Удаляем возможные пробелы по краям

                    if (!string.IsNullOrWhiteSpace(value)) // Проверяем на пустоту или пробелы
                    {
                        uniqueValues.Add(value);
                    }
                }

                // Добавляем уникальные значения из HashSet в ComboBox
                foreach (var value in uniqueValues)
                {
                    comboBox.Items.Add(value);
                }
            }
            else
            {
                // Сообщаем о том, что столбец с указанным именем не найден
                MessageBox.Show("Столбец не найден.");
            }
        }
        public void DisplayTasks(DataTable tasksTable)
        {
            dataGridView1.DataSource = tasksTable;
            dataGridView1.Columns[0].Width = 20;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.ColumnHeadersVisible = false; // Скрыть заголовки столбцов
            dataGridView1.RowHeadersVisible = false;
            //AdjustDataGridViewHeight();

        }


        private void Close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void comboCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox comboBox = sender as ComboBox; // Получаем ссылку на ComboBox, который вызвал событие
            if (comboBox != null)
            {
                if (dataGridView1 is null)
                {
                    MessageBox.Show("Задач не найдено");
                }

                string selectedValue = comboBox.SelectedItem.ToString();

                DB db = new DB();

                string sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                 "FROM public.\"Tasks\" t " +
                 "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                 "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                 "WHERE t.users_id = @userId ";

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    sql += "AND t.completed = CAST(@selectedValue AS numeric)";
                }

                sql += " ORDER BY t.completed";

                NpgsqlCommand command_t = new NpgsqlCommand(sql, db.getConnection());
                command_t.Parameters.Add("@userId", NpgsqlTypes.NpgsqlDbType.Integer).Value = userId;
                command_t.Parameters.Add("@selectedValue", NpgsqlTypes.NpgsqlDbType.Text).Value = selectedValue;

                NpgsqlDataAdapter adapter_t = new NpgsqlDataAdapter(command_t);
                DataTable tasksTable = new DataTable();
                adapter_t.Fill(tasksTable);
                DisplayTasks(tasksTable);
            }
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Получаем ссылку на ComboBox, который вызвал событие
            if (comboBox != null)
            {
                if (dataGridView1 is null)
                {
                    MessageBox.Show("Задач не найдено");
                }

                string selectedValue = comboBox.SelectedItem.ToString();

                DB db = new DB();

                string sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                 "FROM public.\"Tasks\" t " +
                 "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                 "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                 "WHERE t.users_id = @userId ";

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    sql += "AND c.title = @selectedValue";
                }

                sql += " ORDER BY t.completed";

                NpgsqlCommand command_t = new NpgsqlCommand(sql, db.getConnection());
                command_t.Parameters.Add("@userId", NpgsqlTypes.NpgsqlDbType.Integer).Value = userId;
                command_t.Parameters.Add("@selectedValue", NpgsqlTypes.NpgsqlDbType.Text).Value = selectedValue;

                NpgsqlDataAdapter adapter_t = new NpgsqlDataAdapter(command_t);
                DataTable tasksTable = new DataTable();
                adapter_t.Fill(tasksTable);
                DisplayTasks(tasksTable);
            }
        }

        private void comboTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Получаем ссылку на ComboBox, который вызвал событие
            if (comboBox != null)
            {
                if (dataGridView1 is null)
                {
                    MessageBox.Show("Задач не найдено");
                }

                string selectedValue = comboBox.SelectedItem.ToString();

                DB db = new DB();
                string sql;
                if (selectedValue == "Все" || selectedValue == "")
                {
                    sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                     "FROM public.\"Tasks\" t " +
                     "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                     "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                     "WHERE t.users_id = @userId  ORDER BY t.completed";  
                }
                else 
                {
                    sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                     "FROM public.\"Tasks\" t " +
                     "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                     "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                     "WHERE t.users_id = @userId ORDER BY t.title ASC";
                }

                NpgsqlCommand command_t = new NpgsqlCommand(sql, db.getConnection());
                command_t.Parameters.Add("@userId", NpgsqlTypes.NpgsqlDbType.Integer).Value = userId;
                

                NpgsqlDataAdapter adapter_t = new NpgsqlDataAdapter(command_t);
                DataTable tasksTable = new DataTable();
                adapter_t.Fill(tasksTable);
                DisplayTasks(tasksTable);
            }
        }

        private void comboData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Получаем ссылку на ComboBox, который вызвал событие
            if (comboBox != null)
            {
                if (dataGridView1 is null)
                {
                    MessageBox.Show("Задач не найдено");
                }

                string selectedValue = comboBox.SelectedItem.ToString();

                DB db = new DB();
                string sql;
                if (selectedValue == "По возрастанию" )
                {
                    sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                     "FROM public.\"Tasks\" t " +
                     "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                     "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                     "WHERE t.users_id = @userId  ORDER BY p.date_time ASC";
                }
                else if (selectedValue == "")
                {
                    sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                     "FROM public.\"Tasks\" t " +
                     "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                     "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                     "WHERE t.users_id = @userId ORDER BY t.completed ASC";
                }
                else
                {
                    sql = "SELECT t.id, t.title AS  title, t.completed AS completed, c.title AS category_id, p.date_time AS priority_id " +
                     "FROM public.\"Tasks\" t " +
                     "LEFT JOIN public.\"Categories\" c ON t.category_id = c.id " +
                     "LEFT JOIN public.\"Priorities\" p ON t.priority_id = p.id " +
                     "WHERE t.users_id = @userId  ORDER BY p.date_time DESC";
                }

                NpgsqlCommand command_t = new NpgsqlCommand(sql, db.getConnection());
                command_t.Parameters.Add("@userId", NpgsqlTypes.NpgsqlDbType.Integer).Value = userId;


                NpgsqlDataAdapter adapter_t = new NpgsqlDataAdapter(command_t);
                DataTable tasksTable = new DataTable();
                adapter_t.Fill(tasksTable);
                DisplayTasks(tasksTable);
            }
        }

        private void AddTask_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddForm addForm = new AddForm(userId);
            addForm.Show();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            // Пример получения значения из выбранной ячейки
            if (rowIndex >= 0 && colIndex >= 0)
            {
                object cellValue = dataGridView1.Rows[rowIndex].Cells[colIndex].Value;
                if (cellValue != null)
                {
                    IdTask.Text = cellValue.ToString();
                }
            }
        
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int TaskId;
            int.TryParse(IdTask.Text, out TaskId);
            DB db = new DB();
            db.openConnection();

            string deleteTasksQuery = "DELETE FROM public.\"Tasks\" WHERE id = @taskId";
            NpgsqlCommand deleteTasksCommand = new NpgsqlCommand(deleteTasksQuery, db.getConnection());
            deleteTasksCommand.Parameters.AddWithValue("@taskId", TaskId);
            int tasksDeleted = deleteTasksCommand.ExecuteNonQuery();

            if (tasksDeleted > 0)
            {
                // После удаления задачи обновим таблицы Categories и Priorities,
                // чтобы удалить относящиеся к этой задаче записи, если таковые имеются
                string deleteUnusedCategoriesQuery = "DELETE FROM public.\"Categories\" WHERE NOT EXISTS (SELECT * FROM public.\"Tasks\" WHERE public.\"Categories\".id = public.\"Tasks\".category_id)";
                NpgsqlCommand deleteUnusedCategoriesCommand = new NpgsqlCommand(deleteUnusedCategoriesQuery, db.getConnection());
                deleteUnusedCategoriesCommand.ExecuteNonQuery();

                string deleteUnusedPrioritiesQuery = "DELETE FROM public.\"Priorities\" WHERE NOT EXISTS (SELECT * FROM public.\"Tasks\" WHERE public.\"Priorities\".id = public.\"Tasks\".priority_id)";
                NpgsqlCommand deleteUnusedPrioritiesCommand = new NpgsqlCommand(deleteUnusedPrioritiesQuery, db.getConnection());
                deleteUnusedPrioritiesCommand.ExecuteNonQuery();
            }

            try
            {

                if (tasksDeleted > 0)
                {
                    MessageBox.Show("Задача и связанные данные удалены");
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
                    DisplayTasks(tasksTable);
                    PopulateComboBoxWithUniqueColumnValues(tasksTable, "completed", comboCompleted);
                    PopulateComboBoxWithUniqueColumnValues(tasksTable, "category_id", comboCategory);
                    comboCategory.Items.Add("");
                    comboCompleted.Items.Add("");
                    comboTasks.Items.Add("");
                    comboData.Items.Add("");
                }
                else
                {
                    MessageBox.Show("Не удалось удалить задачу и связанные данные");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }
            
        }
        private void UpdateDatabase(int recordId, string columnName, object newValue)
        {
            DB db = new DB();
            // Обновление базы данных с новым значением
            db.openConnection();
                

            string updateQuery = $"UPDATE public.\"Tasks\" SET {columnName} = @newValue WHERE Id = @recordId";
            NpgsqlCommand command = new NpgsqlCommand(updateQuery, db.getConnection());
            command.Parameters.AddWithValue("@newValue", newValue);
            command.Parameters.AddWithValue("@recordId", recordId);

            command.ExecuteNonQuery();
            db.closeConnection();
               
            
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Получаем значение из измененной ячейки
                object newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Получаем идентификатор записи из первого столбца (например)
                int recordId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                // Получаем имя столбца, которое изменилось
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                // Обновляем базу данных с использованием метода UpdateDatabase
                UpdateDatabase(recordId, columnName, newValue);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm1 loginForm = new LoginForm1();
            loginForm.Show();
        }
    }
}
