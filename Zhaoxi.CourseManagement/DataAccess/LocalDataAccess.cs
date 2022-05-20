using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.CourseManagement.Common;
using Zhaoxi.CourseManagement.DataAccess.DataEntity;
using Zhaoxi.CourseManagement.Model;

namespace Zhaoxi.CourseManagement.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;

        private LocalDataAccess()
        {
        }

        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        MySqlConnection conn;
        MySqlCommand comm;
        MySqlDataAdapter adapter;

        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }

            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }

            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        private bool DBConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            if (conn == null)
                conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserEntity CheckUserInfo(string userName, string pwd)
        {
            try
            {
                if (DBConnection())
                {
                    var userSql = "select * from users where user_name=@user_name and password=@pwd and is_validation=1;";

                    adapter = new MySqlDataAdapter(userSql, conn);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@user_name", MySqlDbType.VarChar) { Value = userName });
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@pwd", MySqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName) });

                    var table = new DataTable();
                    var count = adapter.Fill(table);
                    if (count <= 0)
                    {
                        throw new Exception("用户名或密码不正确！");
                    }
                    var dr = table.Rows[0];
                    if (dr.Field<int>("is_can_login") == 0)
                    {
                        throw new Exception("当前用户没有权限使用次平台！");
                    }
                    var userInfo = new UserEntity();
                    userInfo.UserName = dr.Field<string>("user_name");
                    userInfo.RealName = dr.Field<string>("real_name");
                    userInfo.Password = dr.Field<string>("password");
                    userInfo.Avatar = dr.Field<string>("avatar");
                    userInfo.Gender = dr.Field<int>("gender");
                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
            return null;
        }

        public List<CourseSeriesModel> GetCoursePlayRecord()
        {
            try
            {
                var result = new List<CourseSeriesModel>();
                if (DBConnection())
                {
                    var userSql = @"SELECT
                            a.course_name,
	                        a.course_id,
	                        b.play_count,
	                        b.is_growing,
	                        b.growing_rate,
	                        c.platform_name
                        FROM
                            courses a
                            LEFT JOIN play_record b ON a.course_id = b.course_id
                            LEFT JOIN platforms c ON b.platform_id = c.platform_id
                        ORDER BY
                            a.course_id,
	                        c.platform_id;";

                    adapter = new MySqlDataAdapter(userSql, conn);
                    var table = new DataTable();
                    var count = adapter.Fill(table);

                    var courseId = "";
                    CourseSeriesModel model = null;
                    foreach (var dr in table.AsEnumerable())
                    {
                        var tempId = dr.Field<string>("course_id");
                        if (courseId != tempId)
                        {
                            courseId = tempId;
                            model = new CourseSeriesModel();
                            model.CourseName = dr.Field<string>("course_name");
                            model.SeriesCollection = new LiveCharts.SeriesCollection();
                            model.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();
                            result.Add(model);
                        }

                        model.SeriesCollection.Add(
                            new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
                                DataLabels = false
                            });

                        model.SeriesList.Add(
                            new SeriesModel
                            {
                                SeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<decimal>("play_count"),
                                IsGrowing = dr.Field<int>("is_growing") == 1,
                                ChangeRate = (int)dr.Field<decimal>("growing_rate")
                            });
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }
    }
}
