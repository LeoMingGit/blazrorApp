using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.ViewModels
{
    public class QueryEmployeeVo
    {
        public bool SelectedSkill { get; set; }
        public int SkillID { get; set; }
        public string Skill { get; set; }
        public int Level { get; set; }
        public string LevelName { get; set; }
        [Required]
        public int? YearsOfExperience { get; set; }
        [Required]
        public decimal HourlyWage { get; set; }
    }
}
