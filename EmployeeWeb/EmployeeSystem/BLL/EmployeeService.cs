using EmployeeSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem.Paginator;
using EmployeeSystem.DAL;
using EmployeeSystem.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static EmployeeSystem.Paginator.Constants;

namespace EmployeeSystem.BLL
{
    public class EmployeeService
    {


        #region Fields

        private readonly EmployeeContext _empContext;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empContext"></param>
        internal EmployeeService(EmployeeContext empContext)
        {
            _empContext = empContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeSkillView> GetAllSkillList()
        {

            var resList = new List<EmployeeSkillView>();
            try
            {


                var skillList = _empContext.Skills.ToList();

                foreach (var item in skillList)
                {

                    resList.Add(new EmployeeSkillView
                    {
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
            catch (Exception ex)
            {

                return resList;
            }


        }



        public (bool success, string msg) SaveEmployeeAndSkills(EmployeeRegistrationView dto)
        {
             {
                try
                {
                    var targetEmployee = _empContext.Employees.FirstOrDefault(x => x.HomePhone == dto.HomePhone);

                    var employeeId = -1;

                    if (targetEmployee != null)
                    {
                        // Update existing employee
                        _empContext.Entry(targetEmployee).State = EntityState.Modified;
                        employeeId = targetEmployee.EmployeeId;
                    }
                    else
                    {
                        // Insert new employee
                        var newEmployee = new Employees
                        {
                            HomePhone = dto.HomePhone,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                        };
                        _empContext.Employees.Add(newEmployee);
                        _empContext.SaveChanges();
                        employeeId = newEmployee.EmployeeId;
                    }

                    // Delete existing skills
                    var existingSkills = _empContext.EmployeeSkills.Where(x => x.EmployeeID == employeeId).ToList();
                    if (existingSkills.Any())
                    {
                        _empContext.EmployeeSkills.RemoveRange(existingSkills);
                    }
                    // Insert new skills
                    var empSkillSaveList = dto.EmployeeSkills.Select(empSkillItem => new EmployeeSkills
                    {
                        EmployeeID = employeeId,
                        HourlyWage = empSkillItem.HourlyWage,
                        YearsOfExperience = empSkillItem.YearsOfExperience,
                        Level = empSkillItem.Level,
                        SkillID = empSkillItem.SkillID,
                    }).ToList();
                    _empContext.EmployeeSkills.AddRange(empSkillSaveList);

                    _empContext.SaveChanges();
 
                    return (true, "success");
                }
                catch (Exception ex)
                {
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
            try
            {
                var pageData = new PagedInfo<EmployeeInfoAndSillInfoListItem>();

                var totalCount = 0;

                var query = from emp in _empContext.Employees
                            join skill in _empContext.EmployeeSkills on emp.EmployeeId equals skill.EmployeeID into skillTemp
                            from leftJoin in skillTemp.DefaultIfEmpty()
                            select new
                            {
                                Employee = emp,
                                Skill = leftJoin
                            };

                if (!string.IsNullOrEmpty(dto.HomePhone))
                {
                    query = query.Where(emp => emp.Employee.HomePhone.Contains(dto.HomePhone.Trim()));
                }
                totalCount = query.Count();

                var list = query
                    .OrderBy(emp => emp.Employee.EmployeeId)
                    .Skip((dto.PageIndex - 1) * dto.PageSize)
                    .Take(dto.PageSize)
                    .ToList();

                var skillList = _empContext.Skills.ToList();
                var levelDict = GetSkillLevelDictionary();

                pageData.Result = list.Select(item => new EmployeeInfoAndSillInfoListItem
                {
                    EmployeeId = item.Employee.EmployeeId,
                    FirstName = item.Employee.FirstName,
                    LastName = item.Employee.LastName,
                    HomePhone = item.Employee.HomePhone,
                    YearsOfExperience = item.Skill?.YearsOfExperience ?? 0,
                    HourlyWage = item.Skill?.HourlyWage ?? 0,
                    SkillID = item.Skill?.SkillID ?? 0,
                    Skill = skillList.FirstOrDefault(x => x.SkillID == (item.Skill?.SkillID ?? 0))?.Description,
                    LevelName = levelDict.TryGetValue(item.Skill?.Level ?? 0, out var levelName) ? levelName : " "
                }).ToList();

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

