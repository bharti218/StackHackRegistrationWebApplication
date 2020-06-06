using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackHackRegistrationWebApplication.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public BookingType BookingType { get; set; }
    }
    public class UserMetaData
    {
        [Display(Name="Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full name is required")]
        public string FullName { get; set;}

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number is required")]
        public string Mobile { get; set; }

        [Display(Name = "ID Card")]
        public byte[] IDCard { get; set; }

        [Display(Name = "No of tickets")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Number of tickets is required")]
        public int NoOfTickets { get; set; }

        public BookingType BookingType { get; set; }
        
    }
    public enum BookingType
    {
        Self = 1,
        Group = 2,
        Corporate = 3,
        Others = 4
    }

}