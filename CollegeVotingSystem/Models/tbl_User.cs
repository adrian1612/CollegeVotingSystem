using CollegeVotingSystem.Classes;
using SourceAFIS.Simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeVotingSystem.Models
{
    public class tbl_User
    {
        string DestinationPath = "~/Scanned/";
        dbcontrol s = new dbcontrol();
        [Display(Name = "ID")]
        public Int32 ID { get; set; }

        [Display(Name = "Username")]
        [MinLength(4, ErrorMessage = "Must be a minimum of 4 character")]
        [Required]
        public String Username { get; set; }

        [Display(Name = "Password")]
        [MinLength(4, ErrorMessage = "Must be a minimum of 4 character")]
        [DataType(DataType.Password)]
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
        public string Fingerprint1 { get; set; }

        [Display(Name = "Student ID")]
        [Required]
        public String StudentID { get; set; }

        [Display(Name = "Given name")]
        [Required]
        public String fname { get; set; }

        [Display(Name = "Middle name")]
        public String mn { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public String lname { get; set; }

        public string Fullname { get; set; }

        [Display(Name = "Picture")]
        public Byte[] Img { get; set; }

        public HttpPostedFileBase Upload { get; set; }

        public string Img64
        {
            get
            {
                var output = "";
                if (Img != null)
                {
                    var string64 = Convert.ToBase64String(Img);
                    output = $"data:image/jpeg;base64,{string64}";
                }
                return output;
            }
        }

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

        public string CourseName { get; set; }

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

        

        public Person Identify(Fingerprint fingerprint)
        {
            AfisEngine afis = new AfisEngine();
            var persons = new List<Person>();
            List().ForEach(r =>
            {
                if (!string.IsNullOrEmpty(r.Fingerprint1))
                {
                    var tempuser = new Person();
                    tempuser.Id = r.ID;
                    var tempfinger = new Fingerprint();
                    tempfinger.AsBitmap = new Bitmap(HttpContext.Current.Server.MapPath(r.Fingerprint1));
                    tempuser.Fingerprints.Add(tempfinger);
                    afis.Extract(tempuser);
                    persons.Add(tempuser);
                }
            });
            var prob = new Person();
            prob.Id = 1;
            prob.Fingerprints.Add(fingerprint);
            afis.Extract(prob);
            var personprob = afis.Identify(prob, persons).SingleOrDefault();
            if (personprob != null)
            {
                var user = Find(personprob.Id);
                var finger = new Fingerprint();
                finger.AsBitmap = new Bitmap(HttpContext.Current.Server.MapPath(user.Fingerprint1));
                var item = new Person();
                item.Id = user.ID;
                item.Fingerprints.Add(finger);
                return item;
            }
            return null;
        }

        public SelectList ListUser(object Selected = null)
        {
            var list = new SelectList(List(), "ID", "Fullname", Selected);
            return list;
        }

        public tbl_User Find_Username(string Username)
        {

            return s.Query<tbl_User>("tbl_User_Proc", p => { p.Add("@Type", "Find_Username"); p.Add("@Username", Username); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public tbl_User Find(int ID)
        {

            return s.Query<tbl_User>("tbl_User_Proc", p => { p.Add("@Type", "Find"); p.Add("@ID", ID); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public bool Create(tbl_User obj)
        {
            var result = false;
            s.Query("tbl_User_Proc", p =>
            {
                p.Add("@Type", "Create");
                p.Add("@Username", obj.Username);
                p.Add("@Password", obj.Password);
                p.Add("@StudentID", obj.StudentID);
                p.Add("@fname", obj.fname);
                p.Add("@mn", obj.mn);
                p.Add("@lname", obj.lname);
                p.Add("@Gender", obj.Gender);
                p.Add("@bday", obj.bday);
                p.Add("@Course", obj.Course);
            }, CommandType.StoredProcedure).ForEach(r =>
            {
                result = (bool)r[0];
            });
            return result;
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
                if (obj.Upload != null)
                    p.Add("@Img", new BinaryReader(obj.Upload.InputStream).ReadBytes(obj.Upload.ContentLength));
                p.Add("@Gender", obj.Gender);
                p.Add("@Role", obj.Role);
                p.Add("@bday", obj.bday);
                p.Add("@Course", obj.Course);

            }, CommandType.StoredProcedure);
        }

        public void UpdateUserBiometric(tbl_User obj)
        {
            s.Query("tbl_User_Proc", p =>
            {
                p.Add("@Type", "UpdateUserBiometric");
                p.Add("@ID", obj.ID);
                p.Add("@Fingerprint1", SaveImage(obj.Username, obj.Fingerprint1));

            }, CommandType.StoredProcedure);
        }

        string SaveImage(string Username, string Base64)
        {
            CheckifDirectoryExist(Username);
            var result = "";
            var Bytes = Convert.FromBase64String(Base64);
            var Stream = new MemoryStream(Bytes);
            Image img = Image.FromStream(Stream);
            result = DestinationPath + Username + ".png";
            img.Save(HttpContext.Current.Server.MapPath(result), ImageFormat.Png);
            return result;
        }

        void CheckifDirectoryExist(string username)
        {
            var dir = HttpContext.Current.Server.MapPath(DestinationPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var file = dir + username;
            if (File.Exists(dir + username))
            {
                File.Delete(file);
            }
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
            return s.Query<tbl_User>("tbl_User_Proc", p => { p.Add("@Type", "Login"); p.Add("@Username", Username); p.Add("@Password", Password); }, CommandType.StoredProcedure).SingleOrDefault();
        }
    }

    public enum UserRole
    {
        Admin = 1, User = 2, Auditor = 3
    }

}