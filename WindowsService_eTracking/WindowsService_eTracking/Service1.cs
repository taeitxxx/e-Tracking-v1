using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService_eTracking
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer1 = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                // Uncomment this line to debug...
                //System.Diagnostics.Debugger.Break();

                // Create the thread object that will do the service's work.
                timer1 = new Timer();
                timer1.Interval = 30000;// Every 30 Sec
                timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
                timer1.Enabled = true;

            }
            catch (Exception ex)
            {
                // Log the exception.
                WriteLog(ex.ToString());
            }


        }

        private void WriteLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(message);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            WriteLog("Process..." + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            connection c = new connection();
            var user = c.getUserFollow();
            if (user.Rows.Count != 0)
            {
                for (int i = 0; i < user.Rows.Count; i++)
                {
                    var last_stat = c.getLastStatusByUserid(user.Rows[i]["userid"].ToString(), user.Rows[i]["pr_number"].ToString());
                    if (last_stat.Rows.Count == 0)
                    {
                        var status = c.getLastStatus_PR(user.Rows[i]["pr_number"].ToString());
                        if (status.Rows.Count != 0)
                        {
                            WriteLog("Insert New Status log : " + user.Rows[i]["pr_number"].ToString());

                            //// Insert last log
                            c.Insert_last_Status(user.Rows[i]["userid"].ToString(), user.Rows[i]["pr_number"].ToString(),
                                status.Rows[0]["statusid"].ToString(), user.Rows[i]["userid"].ToString());

                            //// Sent Email 
                            sent_email mail = new sent_email();
                            mail.CallMail(user.Rows[i]["email"].ToString(), "USERNAME", user.Rows[i]["pr_number"].ToString() + " Current Status",
                                 user.Rows[i]["pr_number"].ToString() + " Current Status : " + status.Rows[0]["statusname"].ToString());
                        }
                    }
                    else
                    {
                        var status = c.getLastStatus_PR(user.Rows[i]["pr_number"].ToString());
                        if (status.Rows.Count != 0)
                        {
                            if (last_stat.Rows[0]["last_status"].ToString() != status.Rows[0]["statusid"].ToString())
                            {
                                WriteLog("Update New Status log : " + user.Rows[i]["pr_number"].ToString());

                                //// Update last status
                                c.Update_last_Status(status.Rows[0]["statusid"].ToString(), user.Rows[i]["userid"].ToString(), user.Rows[i]["pr_number"].ToString());


                                /// Sent Email ////
                                ///    //sent_email mail = new sent_email();
                                //mail.CallMail(user.Rows[i]["email"].ToString(), "USERNAME", user.Rows[i]["pr_number"].ToString() + " Current Status",
                                //     user.Rows[i]["pr_number"].ToString() + " Current Status : " + status.Rows[0]["statusname"].ToString());
                            }
                        }

                    }
                }
            }
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;

        }
    }
}
