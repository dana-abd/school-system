using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SchoolMangementSystem
{
    class AddSubjectData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-QUI2HH8\SQLEXPRESS;Initial Catalog=school;Integrated Security=True;");

        public int ID { set; get; }
        public string SubjectID { set; get; }
        public string SubjectName { set; get; }
        public string SubjectDescription { set; get; }
        public string SubjectGrade { set; get; }
        public string Status { set; get; }
        public string DateInsert { set; get; }

        // لجلب جميع المواد
        public List<AddSubjectData> subjectData()
        {
            List<AddSubjectData> listData = new List<AddSubjectData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string sql = "SELECT * FROM subjects WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AddSubjectData addSD = new AddSubjectData();

                            addSD.ID = (int)reader["id"];
                            addSD.SubjectID = reader["subject_id"].ToString();
                            addSD.SubjectName = reader["subject_name"].ToString();
                            addSD.SubjectDescription = reader["subject_description"].ToString();
                            addSD.SubjectGrade = reader["subject_grade"].ToString();
                            addSD.Status = reader["status"].ToString();
                            addSD.DateInsert = reader["date_insert"].ToString();

                            listData.Add(addSD);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting Database: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listData;
        }

        // لجلب المواد المضافة اليوم (Dashboard)
        public List<AddSubjectData> dashboardSubjectData()
        {
            List<AddSubjectData> listData = new List<AddSubjectData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    DateTime today = DateTime.Today;

                    string sql = "SELECT * FROM subjects WHERE date_insert = @dateInsert " +
                                 "AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        cmd.Parameters.AddWithValue("@dateInsert", today.ToString());

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AddSubjectData addSD = new AddSubjectData();

                            addSD.ID = (int)reader["id"];
                            addSD.SubjectID = reader["subject_id"].ToString();
                            addSD.SubjectName = reader["subject_name"].ToString();
                            addSD.SubjectDescription = reader["subject_description"].ToString();
                            addSD.SubjectGrade = reader["subject_grade"].ToString();
                            addSD.Status = reader["status"].ToString();
                            addSD.DateInsert = reader["date_insert"].ToString();

                            listData.Add(addSD);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listData;
        }
    }
}