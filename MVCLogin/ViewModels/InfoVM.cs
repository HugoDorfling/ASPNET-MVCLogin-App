using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Foolproof;

namespace MVCLogin.ViewModels
{
    public class InfoVM
    {
        [DisplayName("Person ID")]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DisplayName("New Password")]
        public string Password { get; set; }
        [NotMapped]
        [DisplayName("Confirm Password")]
        [RequiredIf("IsPasswordRequired", true, ErrorMessage = "You need to confirm your new password")]
        [Compare("Password", ErrorMessage = "Confirm Password value doesn't match New Password value")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Telephone Number")]
        public string TelNo { get; set; }
        [DisplayName("Cellphone Number")]
        public string CellNo { get; set; }
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }
        [DisplayName("Address Line 3")]
        public string AddressLine3 { get; set; }
        [DisplayName("Address Code")]
        public string AddressCode { get; set; }
        [DisplayName("Postal Address 1")]
        public string PostalAddress1 { get; set; }
        [DisplayName("Postal Address 2")]
        public string PostalAddress2 { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        public bool IsPasswordRequired
        {
            get
            {
                return Password != null;
            }
        }
    }
}