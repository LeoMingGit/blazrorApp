using System.ComponentModel.DataAnnotations;

namespace Services.Vo
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRegistrationView
    {

   
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
        [RegularExpression(@"^[1-9][0-9][0-9]\.[0-9][0-9][0-9]\.[0-9][0-9][0-9][0-9]$", ErrorMessage = "Home Phone not valid.")]
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
