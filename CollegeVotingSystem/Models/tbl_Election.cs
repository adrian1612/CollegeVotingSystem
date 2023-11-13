using CollegeVotingSystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CollegeVotingSystem.Models
{
    public class tbl_Election
    {
        dbcontrol s = new dbcontrol();
        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public Int32 ID { get; set; }

        [Display(Name = "Title")]
        [Required]
        public String Title { get; set; }

        [Display(Name = "Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? ElectionDate { get; set; }

        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [Display(Name = "Active")]
        [ScaffoldColumn(false)]
        public Boolean? Active { get; set; }

        [Display(Name = "Timestamp")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? Timestamp { get; set; }

        public List<tbl_Candidate> Candidates { get; set; }

        public tbl_Election()
        {
        }
        public List<tbl_Election> List()
        {
            var candidates = new tbl_Candidate().List();
            return s.Query<tbl_Election>("tbl_Election_Proc", p => { p.Add("@Type", "Search"); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                r.Candidates = candidates.Where(f => f.ElectionID == r.ID).ToList();
                return r;
            }).ToList();
        }

        public tbl_Election Election()
        {
            var candidates = new tbl_Candidate().List();
            return s.Query<tbl_Election>("tbl_Election_Proc", p => { p.Add("@Type", "Election"); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                r.Candidates = candidates.Where(f => f.ElectionID == r.ID).ToList();
                return r;
            }).SingleOrDefault();
        }

        public tbl_Election Find(int ID)
        {

            return s.Query<tbl_Election>("tbl_Election_Proc", p => { p.Add("@Type", "Find"); p.Add("@ID", ID); }, CommandType.StoredProcedure)
            .Select(r =>
            {
                r.Candidates = new tbl_Candidate().List(r.ID);
                return r;
            }).SingleOrDefault();
        }

        public void Create(tbl_Election obj)
        {
            s.Query("tbl_Election_Proc", p =>
            {
                p.Add("@Type", "Create");
                p.Add("@Title", obj.Title);
                p.Add("@ElectionDate", obj.ElectionDate);
                p.Add("@Remarks", obj.Remarks);
                p.Add("@Candidates", s.ConvertListToDataTable(obj.Candidates, "ID", "UniqueID", "ElectionID", "UserID", "Position", "Plataforma", "Active", "Timestamp"));

            }, CommandType.StoredProcedure);
        }

        public void Update(tbl_Election obj)
        {
            s.Query("tbl_Election_Proc", p =>
            {
                p.Add("@Type", "Update");
                p.Add("@ID", obj.ID);
                p.Add("@Title", obj.Title);
                p.Add("@ElectionDate", obj.ElectionDate);
                p.Add("@Remarks", obj.Remarks);
                p.Add("@Candidates", s.ConvertListToDataTable(obj.Candidates, "ID", "UniqueID", "ElectionID", "UserID", "Position", "Plataforma", "Active", "Timestamp"));
            }, CommandType.StoredProcedure);
        }
        public void Delete(tbl_Election obj)
        {
            s.Query("DELETE FROM [tbl_Election] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }
    }


}