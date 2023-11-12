using CollegeVotingSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CollegeVotingSystem.Models
{
    public class tbl_Candidate
    {
        dbcontrol s = new dbcontrol();
        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public Int32 ID { get; set; }

        [Display(Name = "Election")]
        public Int32 ElectionID { get; set; }

        [Display(Name = "Voters")]
        public Int32 UserID { get; set; }

        [Display(Name = "Position")]
        public Int32 Position { get; set; }

        [Display(Name = "Plataforma")]
        public String Plataforma { get; set; }

        [Display(Name = "Active")]
        [ScaffoldColumn(false)]
        public Boolean? Active { get; set; }

        [Display(Name = "Timestamp")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? Timestamp { get; set; }

        public tbl_Candidate()
        {
        }
        public List<tbl_Candidate> List()
        {

            return s.Query<tbl_Candidate>("SELECT * FROM [tbl_Candidate]")
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public tbl_Candidate Find(int ID)
        {

            return s.Query<tbl_Candidate>("SELECT * FROM tbl_Candidate where ID = @ID", p => p.Add("@ID", ID))
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public int Create(tbl_Candidate obj)
        {
            var ID = s.Insert("[tbl_Candidate]", p =>
            {
                p.Add("ElectionID", obj.ElectionID);
                p.Add("UserID", obj.UserID);
                p.Add("Position", obj.Position);
                p.Add("Plataforma", obj.Plataforma);

            });
            return ID;
        }

        public void Update(tbl_Candidate obj)
        {
            s.Update("[tbl_Candidate]", obj.ID, p =>
            {
                p.Add("ElectionID", obj.ElectionID);
                p.Add("UserID", obj.UserID);
                p.Add("Position", obj.Position);
                p.Add("Plataforma", obj.Plataforma);

            });
        }
        public void Delete(tbl_Candidate obj)
        {
            s.Query("DELETE FROM [tbl_Candidate] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }
    }


}