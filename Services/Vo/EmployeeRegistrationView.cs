using System.ComponentModel.DataAnnotations;

namespace Services.Vo
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRegistrationView
    {

        public EmployeeRegistrationView() {


            EmployeeSkills =new List<EmployeeSkillView> { };

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
