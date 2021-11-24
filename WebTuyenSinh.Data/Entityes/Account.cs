namespace WebTuyenSinh.Data.Entityes
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Account")]
    public partial class Account:IdentityUser
    {

        public Account()
        {
            ProfileStudents = new HashSet<ProfileStudent>();
        }

    
        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string FirtName { get; set; }

        [StringLength(200)]
        public string Adress { get; set; }

        [StringLength(11)]
        public string Telephone { get; set; }


        public DateTime? CreateDate { get; set; }

        public DateTime? BrithDay { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        public virtual ICollection<ProfileStudent> ProfileStudents { get; set; }
    }
}
