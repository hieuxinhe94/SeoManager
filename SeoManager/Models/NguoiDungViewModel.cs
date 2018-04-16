using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoManager.Models
{
    public class NguoiDungViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string  Email { get; set; }
         
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string Phone { get; set; }
        public string ImageUrl { get; set; }

        public int PercentOfProfileInfo { get; set; }
        public float MoneyInAccount { get; set; }
        public int DomainAssign { get; set; }
        public int PercentOfDocumentViewer { get; set; }

        public string Message { get; set; }
    }
}