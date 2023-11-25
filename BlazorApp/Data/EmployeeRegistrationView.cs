using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
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
