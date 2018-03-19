using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.IO;

namespace Repository
{
    public class Data : Interface.IData, IDisposable
    {
        static object locker = new object();

        SQLite.SQLiteConnection conn = null;

        List<Model.JobRepositoryModel> jobs = new List<Model.JobRepositoryModel>()
        {
            new Model.JobRepositoryModel(){ Id=1, Title="Junior Developer", Grade = "C"},
            new Model.JobRepositoryModel(){ Id=2, Title="Software Developer", Grade = "B" },
            new Model.JobRepositoryModel(){ Id=3, Title="Senior Developer", Grade="A"}
        };
        public Data()
        {
            //Using sqlite for local storage so that it is compatible in most environments (Usually i work with Microsoft SQL Server, but I don't know my audience in this case)
            //The file will be located in your documents
            var sqliteFilename = "jobs.db3";
            string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);

            conn = new SQLite.SQLiteConnection(path);
            conn.CreateTable<Model.JobRepositoryModel>();
            //checks to see if the job table exists and has the first item
            var firstTimeCheck = conn.Table<Model.JobRepositoryModel>().FirstOrDefault(x => x.Id == 1);
            if (firstTimeCheck == null)
            {
                conn.InsertAll(jobs);
            }
        }

      

        public IEnumerable<Model.JobRepositoryModel> GetAllJobs()
        {
            try
            {
                var model = (from job in conn.Table<Model.JobRepositoryModel>()
                  select new Model.JobRepositoryModel() { Id = job.Id, Title = job.Title, Grade = job.Grade }).ToList();

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model.JobRepositoryModel> GetJobById(int id)
        {
            try
            {
                List<Model.JobRepositoryModel> returnModel = new List<Model.JobRepositoryModel>();

                var job = (from item in conn.Table<Model.JobRepositoryModel>()
                           where item.Id == id
                           select new Model.JobRepositoryModel()
                           { Id = item.Id, Title = item.Title, Grade= item.Grade }).FirstOrDefault();

                if (job == null)
                {
                    return null;
                }
                returnModel.Add(job);
                return returnModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdateJob(Model.JobRepositoryModel model)
        {
            try
            {
                lock (locker)
                {
                    if (model.Id != 0)
                    {
                        conn.Update(model);
                        conn.Commit();
                    }
                    else
                    {
                        conn.Insert(model);
                        conn.Commit();
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (jobs != null)
                jobs = null;
            if (conn != null)
                conn.Dispose();
        }

    }
}
