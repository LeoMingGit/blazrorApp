namespace BlazorApp.Data
{
    public class EmployeeSkillView
    {
        public bool SelectedSkill { get; set; }
        public int SkillID { get; set; }
        public string Skill { get; set; }
        public int Level { get; set; }
        public string LevelName { get; set; }
        public int? YearsOfExperience { get; set; }
        public decimal HourlyWage { get; set; }
    }

}
