using CollegeVotingSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CollegeVotingSystem.Models
{
    public class tbl_Course
    {
        dbcontrol s = new dbcontrol();
        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public Int32 ID { get; set; }

        [Display(Name = "Course")]
        [Required]
        public String Course { get; set; }

        [Display(Name = "Active")]
        [ScaffoldColumn(false)]
        public Boolean? Active { get; set; }

        [Display(Name = "Timestamp")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? Timestamp { get; set; }

        public tbl_Course()
        {
        }
        public List<tbl_Course> List()
        {

            return s.Query<tbl_Course>("tbl_Course_Proc", p => { p.Add("@Type", "Search"); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public tbl_Course Find(int ID)
        {

            return s.Query<tbl_Course>("tbl_Course_Proc", p => { p.Add("@Type", "Find"); p.Add("@ID", ID); }, CommandType.StoredProcedure)
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public void Create(tbl_Course obj)
        {
            s.Query("tbl_Course_Proc", p =>
            {
                p.Add("@Type", "Create");
                p.Add("@Course", obj.Course);

            }, CommandType.StoredProcedure);
        }

        public void Update(tbl_Course obj)
        {
            s.Query("tbl_Course_Proc", p =>
            {
                p.Add("@Type", "Update");
                p.Add("@ID", obj.ID);
                p.Add("@Course", obj.Course);

            }, CommandType.StoredProcedure);
        }
        public void Delete(tbl_Course obj)
        {
            s.Query("DELETE FROM [tbl_Course] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }
    }


}