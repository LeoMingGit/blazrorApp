using Services.Common;
using Services.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BLL
{
    public class EmployeeServiceImpl : IEmployeeService
    {

        public void sayHello()
        {
            using (var db = DbProvider.GetSugarDbContext("WorkSchedule"))
            {

                var list =db.Queryable<Employees>().ToList();    


                Console.WriteLine("hello");

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public  (bool success, string message) SaveEmployees()
        {
            try
            {
                string connectionString = "YourConnectionString"; // 替换为实际的数据库连接字符串
                // 初始化数据库连接
                using (var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql, 
                    IsAutoCloseConnection = true, 
                    InitKeyType = InitKeyType.Attribute 
                }))
                {

                  
                }
                return new(true, "success");
            }
            catch (Exception ex)
            {
                return new (false,ex.Message);
            }
        }


    }
}

