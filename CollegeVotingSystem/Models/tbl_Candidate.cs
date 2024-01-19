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

        public Guid UniqueID { get; set; }

        [Display(Name = "Election")]
        public Int32 ElectionID { get; set; }

        [Display(Name = "Voters")]
        public Int32 UserID { get; set; }
        public string Fullname { get; set; }
        public byte[] Img { get; set; }
        public string Img64
        {
            get
            {
                var output = "";
                if (Img != null)
                {
                    output = $"data:image/jpeg;base64,{Convert.ToBase64String(Img)}";
                }
                return output;
            }
        }
        public string Course { get; set; }

        [Display(Name = "Position")]
        public Int32 Position { get; set; }
        public string PositionName { get; set; }

        [Display(Name = "Plataforma")]
        public String Plataforma { get; set; }

        public int VoteCount { get; set; }

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

            return s.Query<tbl_Candidate>("SELECT * FROM [vw_Candidate]")
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public List<tbl_Candidate> ListMyCandidate()
        {

            return s.Query<tbl_Candidate>("SELECT * FROM [vw_Candidate] WHERE UniqueID IN (SELECT Candidate FROM tbl_UnverifiedVote WHERE UserID = @ID) OR  UniqueID IN (SELECT Candidate FROM tbl_Vote WHERE UserID = @ID)", p => p.Add("@ID", SystemSession.User.ID))
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public List<tbl_Candidate> List(int Election)
        {

            return s.Query<tbl_Candidate>("SELECT * FROM [vw_Candidate] WHERE ElectionID = @ID", p => p.Add("@ID", Election))
            .Select(r =>
            {

                return r;
            }).ToList();
        }

        public tbl_Candidate Find(int ID)
        {

            return s.Query<tbl_Candidate>("SELECT * FROM vw_Candidate where ID = @ID", p => p.Add("@ID", ID))
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