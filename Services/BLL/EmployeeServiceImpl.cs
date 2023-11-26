using Services.Common;
using Services.Dto;
using Services.Models;
using Services.Vo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Services.Common.Constants;

namespace Services.BLL
{
    public class EmployeeServiceImpl : IEmployeeService
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeSkillView> GetAllSkillList()
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
                            Skill = item.Description,
                            SelectedSkill = false,
                            YearsOfExperience = 0,
                            HourlyWage = 0,
                            Level = (int)Constants.SkillLevel.Novice,
                            LevelName = Constants.SkillLevel.Novice.ToString(),
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
                    var targetEmployee = db.Queryable<Employees>().Where(x => x.HomePhone == dto.HomePhone).ToList().FirstOrDefault();

                    var employeeId = -1;

                    db.BeginTran();


                    if (targetEmployee != null)
                    {
                        var updateCnt = db.Updateable<Employees>(targetEmployee).ExecuteCommand();
                        employeeId = targetEmployee.EmployeeId;
                    }
                    else
                    {
                        var saveEmployeeData = new Employees();
                        saveEmployeeData.HomePhone = dto.HomePhone;
                        saveEmployeeData.FirstName = dto.FirstName;
                        saveEmployeeData.LastName = dto.LastName;
                        employeeId = db.Insertable(saveEmployeeData).ExecuteReturnIdentity();
                    }

                    var batchDeleteCnt = db.Deleteable<EmployeeSkills>().Where(x => x.EmployeeID == employeeId).ExecuteCommand();

                    var empSkillSaveList = new List<EmployeeSkills>();

                    foreach (var empSkillItem in dto.EmployeeSkills)
                    {

                        empSkillSaveList.Add(new EmployeeSkills() {
                            EmployeeID = employeeId,
                            HourlyWage = empSkillItem.HourlyWage,
                            YearsOfExperience = empSkillItem.YearsOfExperience,
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


        public static Dictionary<int, string> GetSkillLevelDictionary()
        {
            return Enum.GetValues<SkillLevel>()
                .ToDictionary(level => (int)level, level => level.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public PagedInfo<EmployeeInfoAndSillInfoListItem> QueryEmployeeAndSkillInfoList(QueryEmployeeAndSkillsDto dto)
        {
            using (var db = DbProvider.GetSugarDbContext("WorkSchedule"))
            {
                try
                {
                    var pageData = new PagedInfo<EmployeeInfoAndSillInfoListItem>();

 
                    var totalCount = 0;
                    var query = db.Queryable<Employees, EmployeeSkills>((emp, skill) => emp.EmployeeId == skill.EmployeeID);
                    
                    if(false == string.IsNullOrEmpty(dto.HomePhone))
                    {
                        query = query.Where(emp => emp.HomePhone.Contains(dto.HomePhone.Trim()));
                    }
                    var list = query
                     .OrderBy(emp => emp.EmployeeId)
                     .Select((emp, skill) => new
                     {
                         Employee = emp,
                         Skill = skill

                     })
                     .ToPageList(dto.PageIndex, dto.PageSize, ref totalCount);

                    pageData.Result = new List<EmployeeInfoAndSillInfoListItem>();

                    var skillList = db.Queryable<Skills>().ToList();

                    var levelDict = GetSkillLevelDictionary();

                    foreach (var item in list) {


                        pageData.Result.Add(new EmployeeInfoAndSillInfoListItem() { 
                        
                             EmployeeId =item.Employee.EmployeeId,
                             FirstName =item.Employee.FirstName,
                             LastName =item.Employee.LastName,
                             HomePhone = item.Employee.HomePhone,
                             YearsOfExperience =item.Skill.YearsOfExperience,
                             HourlyWage =item.Skill.HourlyWage,
                             SkillID = item.Skill.SkillID,
                             Skill = skillList.Where(x=>x.SkillID==item.Skill.SkillID).FirstOrDefault()?.Description,
                             LevelName = levelDict.TryGetValue(item.Skill.Level, out var levelName) ? levelName : " "
                        });
                    }
                    pageData.TotalNum = totalCount;
                    pageData.PageIndex = dto.PageIndex;
                    pageData.PageSize = dto.PageSize;
                    return pageData;

                }
                catch (Exception ex)
                {

                    return null;
                }

            }
        }

    }
}

