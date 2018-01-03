using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace MonitorDirectory
{
    public partial class Form1 : Form
    {
        private string openFileName, folderName;
        private bool fileOpened = false;

        static string conString = @"Data Source=DAX-SQL;Initial Catalog=DirectoryMonitor;Integrated Security=True";
        SqlConnection conSQL = new SqlConnection(conString);
        SqlDataAdapter adapter;
        SqlDataAdapter adapterExt;
        SqlCommand cmd, cmdExt;
        DataTable dt = new DataTable();
        DataTable dtExt = new DataTable();
        string msg;

        public Form1()
        {
            InitializeComponent();
            retrieve(false);
            retrieveExt(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                //listBox1.Items.Add(folderName);
                add(folderName);
            }
        }

        private void directoriesBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void add(string directoryName)
        {
            String sql = "INSERT INTO directories(DirectoryName) VALUES(@PNAME)";
            cmd = new SqlCommand(sql, conSQL);

            cmd.Parameters.AddWithValue("@PNAME", directoryName);

            try
            {
                conSQL.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                }

                conSQL.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void addExt(string Ext)
        {
            String sql = "INSERT INTO Extension(Extension) VALUES(@PEXT)";
            cmd = new SqlCommand(sql, conSQL);

            cmd.Parameters.AddWithValue("@PEXT", Ext);

            try
            {
                conSQL.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                }

                conSQL.Close();
                retrieveExt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void retrieve(bool  clear=true)
        {
            if(clear == true)
                listBox1.Items.Clear();

            string sql = "SELECT * from Directories";
            cmd = new SqlCommand(sql, conSQL);

            try
            {
                conSQL.Open();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    listBox1.Items.Add(row[1].ToString());
                }

                conSQL.Close();

                dt.Rows.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void retrieveExt(bool clear = true)
        {
            if (clear == true)
                lbExt.Items.Clear();

            string sql = "SELECT * from Extension";
            cmd = new SqlCommand(sql, conSQL);

            try
            {
                conSQL.Open();
                adapterExt = new SqlDataAdapter(cmd);
                adapterExt.Fill(dtExt);

                foreach (DataRow row in dtExt.Rows)
                {
                    string item = row[1].ToString();
                    lbExt.Items.Add(row[1].ToString());
                }

                conSQL.Close();

                dtExt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update(string id, string newName)
        {
            String sql = "UPDATE directories set DirectoryName='" + newName + "' WHERE DirectoryName='" + id + "'";
            cmd = new SqlCommand(sql, conSQL);

            cmd.Parameters.AddWithValue("@PNAME", newName);

            try
            {
                conSQL.Open();

                adapter = new SqlDataAdapter(cmd);
                adapter.UpdateCommand = conSQL.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {

                }
                conSQL.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void delete(string id)
        {
            String sql = "DELETE FROM directories WHERE DirectoryName='" + id + "'";
            cmd = new SqlCommand(sql, conSQL);

            try
            {
                conSQL.Open();

                adapter = new SqlDataAdapter(cmd);

                adapter.DeleteCommand = conSQL.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;

                if (MessageBox.Show("Are you sure?", "Detlete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                    {

                    }

                }
                conSQL.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBox1_Enter(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = listBox1.SelectedItem.ToString();
            delete(id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addExt(tbExt.Text.ToString());
            
        }


        private void deleteExt(string id)
        {
            String sql = "DELETE FROM Extension WHERE Extension='" + id + "'";
            cmd = new SqlCommand(sql, conSQL);

            try
            {
                conSQL.Open();

                adapter = new SqlDataAdapter(cmd);

                adapter.DeleteCommand = conSQL.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;

                if (MessageBox.Show("Are you sure?", "Detlete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                    {

                    }

                }
                conSQL.Close();
                retrieveExt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelExt_Click(object sender, EventArgs e)
        {
            string id = lbExt.SelectedItem.ToString();
            if(id != "")
                deleteExt(id);


        }

        private void tbCheck_Click(object sender, EventArgs e)
        {

            string sql = "SELECT * from Directories";
            cmd = new SqlCommand(sql, conSQL);

            try
            {
                conSQL.Open();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conSQL.Close();

                foreach (DataRow row in dt.Rows)
                {
                    string dir = row[1].ToString();
                    dir = dir.Replace(@"\\", @"\");
                    try
                    {
                        string sqlExt = "SELECT * from Extension";
                        cmdExt = new SqlCommand(sqlExt, conSQL);
                        conSQL.Open();

                        adapterExt = new SqlDataAdapter(cmdExt);
                        adapterExt.Fill(dtExt);
                        conSQL.Close();

                        foreach (DataRow rowExt in dtExt.Rows)
                        {
                            string item = rowExt[1].ToString();
                            item = item.Replace(".", "");

                            string[] fileEntries  = Directory.GetFiles(dir, "*." + item);
                            

                            if (fileEntries.Length == 0)
                            {
                                //No Match

                            }
                            else
                            {
                                //Match

                                foreach (string fileName in fileEntries)
                                {
                                    FileInfo fi = new FileInfo(fileName);
                                    if (fi.LastAccessTime < DateTime.Now.AddMinutes(-5))
                                    {
                                        TimeSpan span = (DateTime.Now - DateTime.Now);

                                        msg += String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", 
                                        span.Days, span.Hours, span.Minutes, span.Seconds);

                                        //MessageBox.Show("File " + fileName + " is " + (DateTime.Now - fi.LastAccessTime) + " Hours Old");
                                    }
                                }
                                //MessageBox.Show(fileName);
                            }
                        }

                        dtExt.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                conSQL.Close();

                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public static void ShowChars(char[] charArray)
        {
            Console.WriteLine("Char\tHex Value");
            // Display each invalid character to the console.
            foreach (char someChar in charArray)
            {
                if (Char.IsWhiteSpace(someChar))
                {
                    Console.WriteLine(",\t{0:X4}", (int)someChar);
                }
                else
                {
                    Console.WriteLine("{0:c},\t{1:X4}", someChar, (int)someChar);
                }
            }
        }
    }
}
