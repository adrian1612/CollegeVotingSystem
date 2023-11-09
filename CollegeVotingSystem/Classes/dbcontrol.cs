using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeVotingSystem.Classes
{
    public class dbcontrol : MasterControl
    {
        public dbcontrol() : base("CollegeVotingSystem")
        {
            QueryException += Dbcontrol_QueryException;
        }

        private void Dbcontrol_QueryException(Exception e)
        {
            throw e;
        }
    }
}