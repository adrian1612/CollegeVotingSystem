using CollegeVotingSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CollegeVotingSystem.Models
{
    public class tbl_User
    {
        dbcontrol s = new dbcontrol();
        [Display(Name = "ID")]
        public Int32 ID { get; set; }

        [Display(Name = "Username")]
        [MinLength(4, ErrorMessage = "Must be a minimum of 4 character")]
        [Required]
        public String Username { get; set; }

        [Display(Name = "Password")]
        [MinLength(4, ErrorMessage = "Must be a minimum of 4 character")]
        [Required]
        public String Password { get; set; }

        [Display(Name = "Role")]
        [ScaffoldColumn(false)]
        public UserRole Role { get; set; }

        [Display(Name = "Active")]
        [ScaffoldColumn(false)]
        public Boolean? Active { get; set; }

        [Display(Name = "Fingerprint")]
        [ScaffoldColumn(false)]
        public Byte[] Fingerprint1 { get; set; }

        [Display(Name = "StudentID")]
        [Required]
        public String StudentID { get; set; }

        [Display(Name = "First name")]
        [Required]
        public String fname { get; set; }

        [Display(Name = "Middle name")]
        public String mn { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public String lname { get; set; }

        public string Fullname { get; set; }

        [Display(Name = "Img")]
        public Byte[] Img { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public String Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? bday { get; set; }

        public int Age { get; set; }

        [Display(Name = "Course")]
        [Required]
        public Int32 Course { get; set; }

        [Display(Name = "Timestamp")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? Timestamp { get; set; }

        public tbl_User()
        {
        }
        public List<tbl_User> List()
        {

            return s.Query<tbl_User>("tbl_User_Proc", p => { p.Add("@Type", "Search"); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public tbl_User Find(int ID)
        {

            return s.Query<tbl_User>("tbl_User_Proc", p => { p.Add("@Type", "Find"); p.Add("@ID", ID); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public void Create(tbl_User obj)
        {
            s.Query("tbl_User_Proc", p =>
            {
                p.Add("@Type", "Create");
                p.Add("@Username", obj.Username);
                p.Add("@Password", obj.Password);
                p.Add("@StudentID", obj.StudentID);
                p.Add("@fname", obj.fname);
                p.Add("@mn", obj.mn);
                p.Add("@lname", obj.lname);
                p.Add("@Img", obj.Img);
                p.Add("@Gender", obj.Gender);
                p.Add("@bday", obj.bday);
                p.Add("@Course", obj.Course);

            }, CommandType.StoredProcedure);
        }

        public void Update(tbl_User obj)
        {
            s.Query("tbl_User_Proc", p =>
            {
                p.Add("@Type", "Update");
                p.Add("@ID", obj.ID);
                p.Add("@Username", obj.Username);
                p.Add("@Password", obj.Password);
                p.Add("@StudentID", obj.StudentID);
                p.Add("@fname", obj.fname);
                p.Add("@mn", obj.mn);
                p.Add("@lname", obj.lname);
                p.Add("@Img", obj.Img);
                p.Add("@Gender", obj.Gender);
                p.Add("@bday", obj.bday);
                p.Add("@Course", obj.Course);

            }, CommandType.StoredProcedure);
        }
        public void Delete(tbl_User obj)
        {
            s.Query("DELETE FROM [tbl_User] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }

        public tbl_User Login(string Username, string Password)
        {
            return s.Query<tbl_User>("Login", p => { p.Add("@Username", Username); p.Add("@Password", Password); }, CommandType.StoredProcedure).SingleOrDefault();
        }
    }

    public enum UserRole
    {
        Admin, User, Auditor
    }


}