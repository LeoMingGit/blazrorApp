using Services.Common;
using Services.Models;
using Services.Vo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Services.BLL
{
    public class EmployeeServiceImpl : IEmployeeService
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeSkillView>  GetAllSkillList()
        {

            var resList = new List<EmployeeSkillView>();
			try
			{
                using (var db = DbProvider.GetSugarDbContext("WorkSchedule"))
                {

                    var skillList = db.Queryable<Skills>().ToList();

                    foreach (var item in skillList)
                    {

                        resList.Add(new EmployeeSkillView {
                          SkillID = item.SkillID,
                          Skill =item.Description,
                          SelectedSkill=false,
                          YearsOfExperience=0,
                          HourlyWage=0,
                          Level= (int)Constants.SkillLevel.Novice,
                          LevelName= Constants.SkillLevel.Novice.ToString(),
                        });
                    }
                    return resList;
                }
            }
			catch (Exception ex)
			{

                return resList;
  			}


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (bool success, string msg) SaveEmployeeAndSkills(EmployeeRegistrationView dto)
        {
            using (var db = DbProvider.GetSugarDbContext("WorkSchedule"))
            {

                try
                {
                    var targetEmployee=db.Queryable<Employees>().Where(x => x.HomePhone == dto.HomePhone).ToList().FirstOrDefault();

                    var employeeId = -1;

                    db.BeginTran();

                  
                    if(targetEmployee != null)
                    {
                       var updateCnt= db.Updateable<Employees>(targetEmployee).ExecuteCommand();
                        employeeId = targetEmployee.EmployeeId;
                    }
                    else
                    {
                        var saveEmployeeData=new Employees();
                        saveEmployeeData.HomePhone = dto.HomePhone;
                        saveEmployeeData.FirstName = dto.FirstName;
                        saveEmployeeData.LastName = dto.LastName;
                        employeeId = db.Insertable(saveEmployeeData).ExecuteReturnIdentity();
                    }

                    var batchDeleteCnt =db.Deleteable<EmployeeSkills>().Where(x=>x.EmployeeID==employeeId).ExecuteCommand();

                    var  empSkillSaveList =new List<EmployeeSkills>();

                    foreach (var empSkillItem in dto.EmployeeSkills)
                    {

                        empSkillSaveList.Add(new EmployeeSkills() { 
                            EmployeeID =employeeId,
                            HourlyWage  = empSkillItem.HourlyWage,
                            YearsOfExperience =empSkillItem.YearsOfExperience,
                            Level = empSkillItem.Level,
                            SkillID = empSkillItem.SkillID, 
                        });
                    }

                    var batchInsertCnt = db.Insertable<EmployeeSkills>(empSkillSaveList).ExecuteCommand();

                    db.CommitTran();

                    return (true, "success");
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    return (false, ex.Message);
                }

            }
        }



    }
}

