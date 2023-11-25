using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRegistrationView
    {

        public EmployeeRegistrationView() {


            EmployeeSkills =new List<EmployeeSkillView> { };

            MockData();
        }   

        public  void MockData()
        {
            for (int i = 1; i <= 100; i++)
            {
                EmployeeSkills.Add(new EmployeeSkillView
                {
                    SelectedSkill = i % 2 == 0,   
                    SkillID = i,
                    Skill = $"Skill {i}",
                    Level = i % 3 + 1,  
                    LevelName = $"Level {i}",
                    YearsOfExperience = i % 2 == 0 ? (int?)i : null,   
                    HourlyWage = 10.5m + i * 2.0m   
                });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string LastName { get; set; }= string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string HomePhone { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool isActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<EmployeeSkillView> EmployeeSkills { get; set; } = new List<EmployeeSkillView>();    
    }
}
