using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IData
    {
        IEnumerable<Model.JobRepositoryModel> GetAllJobs();
        List<Model.JobRepositoryModel> GetJobById(int id);

        void InsertUpdateJob(Model.JobRepositoryModel model);
        void Dispose();
    }
}
