using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class JobController : ApiController
    {
        //Using mapster for quicker translations so that I don't have to write it out myself
        Mapster.Adapter mapper;
        public JobController()
        {
            mapper = new Mapster.Adapter();
        }
        public IEnumerable<Models.JobModel> GetAllJobs()
        {
            IEnumerable<Models.JobModel> model = null;
            using (Repository.Data repo = new Repository.Data())
            {
               model = mapper.Adapt<IEnumerable<Models.JobModel>>(repo.GetAllJobs());
            }
            return model;
        }

        public IHttpActionResult GetJobById(int id)
        {
            IEnumerable<Models.JobModel> model = null;
            using (Repository.Data repo = new Repository.Data())
            {
                model = mapper.Adapt<IEnumerable<Models.JobModel>>(repo.GetJobById(id));
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult SaveUpdateNewJob([FromBody]Models.JobModel model)
        {
            using (Repository.Data repo = new Repository.Data())
            {
                repo.InsertUpdateJob(mapper.Adapt<Repository.Model.JobRepositoryModel>(model));
            }
            return Ok();
        }
    }
}
