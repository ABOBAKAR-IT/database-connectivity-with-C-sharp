using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace database_connectivity_with_C_sharp
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        DataTable data = new DataTable();//table = table
      
            string db_connection = @"Data Source=RANA-ABOBAKAR\SQLEXPRESS;Initial Catalog=students;Integrated Security=true";
        public Form1()
        {
            InitializeComponent();
            this.connection = new SqlConnection(db_connection);
            data.Columns.Add("Name");
            data.Columns.Add("Roll");
            data.Columns.Add("CNIC");
            dataGridView1.DataSource = data;
            read_database();

        }



        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {



            DataRow row1 = data.NewRow();

            row1["Name"] = name.Text;//textBox1.Text
            row1["Roll"] = roll.Text;
            row1["CNIC"] = cnic.Text;
            data.Rows.Add(row1);
            write_database();
            dataGridView1.Refresh();

            name.Text = "";
            roll.Text = "";
            cnic.Text = "";

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(name.Text) && !String.IsNullOrWhiteSpace(cnic.Text))
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row["Roll"].ToString() == roll.Text)
                    {
                        row["Name"] = name.Text;
                        row["CNIC"] = cnic.Text;
                        update_database();
                        break;
                    }
                }

            }
            else
            {
                MessageBox.Show("Kindly Fill the Requirements!");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(roll.Text))
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row["Roll"].ToString() == roll.Text)
                    {
                        delete_database();
                        data.Rows.Remove(row);

                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Kindly Fill the Requirements!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void read_database()
        {
            try
            {
                SqlDataReader dataReader;
                command = new SqlCommand("Select * from student_table", this.connection);
                this.connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    DataRow row = this.data.NewRow();
                    row["Name"] = dataReader.GetValue(0);
                    row["Roll"] = dataReader.GetValue(1);
                    row["CNIC"] = dataReader.GetValue(2);
                    this.data.Rows.Add(row);
                }
                dataGridView1.Refresh();
                this.connection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
   
        private void write_database()
        {
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(roll.Text) && !string.IsNullOrWhiteSpace(cnic.Text) )
            {
                try
                {
                 
                  string  sql = $"Insert into student_table(Name,Roll,CNIC)" +
                        $"values('{name.Text}','{roll.Text}','{cnic.Text}')";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    MessageBox.Show(adapter.InsertCommand.ExecuteNonQuery().ToString() + " New Record Added");
                    connection.Close();
                  
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("Something Missing!");
            }
        }
        private void update_database()
        {
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(roll.Text) && !string.IsNullOrWhiteSpace(cnic.Text))
            {
                try
                {

                    string sql = $"Update student_table " +
                            $"set Name='{name.Text}',Roll={roll.Text},CNIC='{cnic.Text}' where (Roll={roll.Text})";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = command;
                    MessageBox.Show(adapter.UpdateCommand.ExecuteNonQuery().ToString() + "  Record Updated");
                    connection.Close();

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("Something Missing!");
            }
        }
        private void delete_database()
        {
            try
            {
                connection.Open();
             
                string sql = $"DELETE from student_table where Roll={roll.Text}";
                command = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.DeleteCommand = command;
                MessageBox.Show(adapter.DeleteCommand.ExecuteNonQuery().ToString() + "  Rows Deleted!");
             
                dataGridView1.Refresh();
              
                
                connection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
    }

}
