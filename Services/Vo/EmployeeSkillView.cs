using System.ComponentModel.DataAnnotations;

namespace Services.Vo
{
    public class EmployeeSkillView
    {
        /// <summary>
        /// 
        /// </summary>
        public bool SelectedSkill { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SkillID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// /
        /// </summary>
         public int YearsOfExperience { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public decimal HourlyWage { get; set; }
    }

}
