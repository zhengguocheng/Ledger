using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{

    public class TaskController: BaseController
    {
        public TaskController()
        {
            this.EntitySetName = "tblTasks";
        }

        public bool Save(tblTask record)
        {
            if (record.ID == 0)
            {
                return this.AddEntity(record);
            }
            else 
            {
                return this.UpdateEntity(record);
            }
        }

        public bool Delete(tblTask record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblTask Find(long id)
        {
            tblTask rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTasks.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblTask> FetchAll()
        {
            List<tblTask> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblTasks.ToList();
            }
            
            return rec;
        }

        //View is for getting Assignee, Assigner names and task status
        public List<tblTask> FetchView()
        {
            List<tblTask> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                var query = from t in context.tblTasks
                            join u in context.Users on t.CreatedBy equals u.ID
                            join c in context.Users on t.CreatedBy equals c.ID
                            join a in context.Users on t.AssignedTo equals a.ID
                            join tbStats in context.tblStatus on t.StatusID equals tbStats.ID
                            select new
                            {
                                AssignedTo = t.AssignedTo,
                                StatusID = t.StatusID,
                                CreatedAT = t.CreatedAT,
                                CreatedBy = t.CreatedBy,
                                Detail = t.Detail,
                                ID = t.ID,
                                Title = t.Title,
                                UpdatedAt = t.UpdatedAt,
                                UpdatedBy = t.UpdatedBy,
                                IsArchived = t.IsArchived,

                                UpdatedByName = u.UserName,
                                CreatedByName = c.UserName,
                                AssignedToName = a.UserName,

                                StatusName = tbStats.StatusName,
                            };

                rec = query.ToList().Select(t => new tblTask
                {
                    AssignedTo = t.AssignedTo,

                    CreatedAT = t.CreatedAT,
                    CreatedBy = t.CreatedBy,
                    Detail = t.Detail,
                    ID = t.ID,
                    Title = t.Title,
                    IsArchived = t.IsArchived,

                    UpdatedAt = t.UpdatedAt,
                    UpdatedBy = t.UpdatedBy,
                    UpdatedByName = t.UpdatedByName,
                    AssignedToName = t.AssignedToName,

                    CreatedByName = t.CreatedByName,

                    StatusName = t.StatusName,
                    StatusID = t.StatusID,


                }).ToList();
            }
            return rec;
        }
     
        public List<tblTask> FetchView(int? assTo, int? assBy, int? statusID,bool onlyArchived)
        {
            List<tblTask> rec = FetchView();
            if(assTo.HasValue)
                rec = rec.Where(x => x.AssignedTo == assTo).ToList<tblTask>();
            if (assBy.HasValue)
                rec = rec.Where(x => x.CreatedBy == assBy).ToList<tblTask>();
            if (statusID.HasValue)
                rec = rec.Where(x => x.StatusID == statusID).ToList<tblTask>();
            
            if (onlyArchived)
                rec = rec.Where(x => x.IsArchived == true).ToList<tblTask>();
            else 
                rec = rec.Where(x => x.IsArchived == null || x.IsArchived == false).ToList<tblTask>();
            
            return rec;
        }

        public List<tblTask> GetTaskToEmail()
        {
            List<tblTask> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                //get all task whose end date is yet to come
                rec = context.tblTasks.Where(x => x.IsReminderOn.Value == true && x.EndDate >= DateTime.Today).ToList<tblTask>();
            }

            List<tblTask> newlst = new List<tblTask>();
            foreach (tblTask item in rec)
            {
                DateTime reminderDateStart = item.EndDate.Value.AddDays(-1 * item.ReminderBeforeDays.Value);
                //if enddate is not past and reminderstartdate is past or equal
                if (item.EndDate >= DateTime.Today && reminderDateStart.Date <= DateTime.Today)
                {
                    newlst.Add(item);
                }
            }
            return newlst;
        }

        

    }

    public partial class tblTask : iEntityBase
    {
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedToName { get; set; }
        public string StatusName { get; set; }
        
        
    }
}
