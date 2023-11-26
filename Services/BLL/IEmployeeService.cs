using Services.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BLL
{
    public interface IEmployeeService
    {

        public List<EmployeeSkillView> GetAllSkillList();

        public (bool success, string msg) SaveEmployeeAndSkills(EmployeeRegistrationView dto);


    }
}
