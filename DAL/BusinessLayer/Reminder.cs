using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DAL
{
   
    public class Reminder : EmailService
    {
        //public void SendPendingTasksReminder()
        //{
        //    TaskController cnt = new TaskController();
        //    List<tblTask> lst = cnt.GetTaskToEmail();
        
        //    foreach (tblTask itm in lst)
        //    {
        //        SendTaskAsEmail(itm);
        //    }
        //}

        //private void SendTaskAsEmail(object _task)
        //{
        //    tblTask tsk = (tblTask)_task;
        //    SendEmail(tsk);
        //}

    }
}
