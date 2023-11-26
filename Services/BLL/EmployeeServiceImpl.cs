using Services.Common;
using Services.Models;
using Services.Vo;
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
 


    }
}

