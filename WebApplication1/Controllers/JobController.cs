using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class JobController : ApiController
    {
        List<Models.JobModel> jobs = new List<Models.JobModel>()
        {
            new Models.JobModel(){ Id=1, Title="Junior Developer", Responsibilities = new List<Models.ResponsibilitiesModel>()
                                                                    {
                                                                        new Models.ResponsibilitiesModel() { Id= 1, Title="Fix code" },
                                                                        new Models.ResponsibilitiesModel() { Id= 2, Title="Maintain code" },
                                                                        new Models.ResponsibilitiesModel() { Id= 3, Title="Answer support calls" },
                                                                    }
            },
            new Models.JobModel(){ Id=2, Title="Software Developer" },
            new Models.JobModel(){ Id=3, Title="Senior Developer" }
        };

        public IEnumerable<Models.JobModel> GetAllJobs()
        {
            return jobs;
        }

        public IHttpActionResult GetSpecificJob(int id)
        {
            var job = jobs.Where(x => x.Id == id).FirstOrDefault();
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }
    }
}
