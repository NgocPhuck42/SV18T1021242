using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SV18T1021242.DomainModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BirthDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
   //     public HttpPostedFile PhotoFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get { return LastName + " " + FirstName; } }
    }
}
