using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ResponsibilitiesModel> Responsibilities { get; set; }

    }
}