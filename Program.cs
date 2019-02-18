using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAppsProcessChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int pid = 0;

            while (true)
            {
                bool restartDelv = false;
                bool isGooleRunned = false;
                {
                    Process[] processes = Process.GetProcessesByName("_ah_exe");
                    if (processes.Length > 0)
                    {
                        isGooleRunned = true;

                        if (pid != processes[0].Id)
                        {
                            pid = processes[0].Id;
                            restartDelv = true;
                        }
                    }
                    else
                    {
                        pid = 0;
                    }
                }


                if (isGooleRunned && pid > 0) {
                    Process[] processes = Process.GetProcessesByName("dlv");
                    if (processes.Length == 0) {
                        restartDelv = true;
                    }
                }


                if(restartDelv)
                {
                    //Process[] dlv = Process.GetProcessesByName("dlv");
                    //foreach (var tmp in dlv)
                    //{
                    //    //tmp.Kill();
                    //}

                    if (pid > 0)
                    {
                        Process.Start("dlv", "--headless -l \"localhost: 2345\" attach " + pid.ToString());
                    }

                }


                Thread.Sleep(10000);
            }
            
//            _ah_exe


        }
    }
}
