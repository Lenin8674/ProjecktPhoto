using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ForSanyaMDK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadDataFromSQLite(int rowCount)
        {
            string connectionString = "Data Source=your_database.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM ImagesTable LIMIT {rowCount};";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataGridView1.ColumnCount = reader.FieldCount;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataGridView1.Columns[i].Name = reader.GetName(i);

                                if (reader.GetFieldType(i) == typeof(byte[]))
                                {
                                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                                    imageColumn.HeaderText = reader.GetName(i);
                                    dataGridView1.Columns.RemoveAt(i);
                                    dataGridView1.Columns.Insert(i, imageColumn);
                                }

                                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            }
                        }

                        while (reader.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.GetFieldType(i) == typeof(byte[]))
                                {
                                    byte[] imageData = (byte[])reader[i];
                                    if (imageData != null && imageData.Length > 0)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            Image image = Image.FromStream(ms);
                                            row.Cells.Add(new DataGridViewImageCell
                                            {
                                                Value = image
                                            });
                                        }
                                    }
                                    else
                                    {
                                        row.Cells.Add(new DataGridViewImageCell());
                                    }
                                }
                                else
                                {
                                    row.Cells.Add(new DataGridViewTextBoxCell
                                    {
                                        Value = reader[i]
                                    });
                                }
                            }

                            dataGridView1.Rows.Add(row);
                        }
                    }
                }

                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowCountToLoad = 4; // количество строк
            LoadDataFromSQLite(rowCountToLoad);

            //  Высота строк в DataGridView.
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 400; // высота строк в пикснлях.
            }
        }
    }
}
