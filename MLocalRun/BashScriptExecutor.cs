using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    public class BashScriptExecutor : IScriptExecutor
    {
        int result=0;
        bool shouldReturn = false;
        RichTextBox OutputTextBox;
       
        public BashScriptExecutor(RichTextBox outputTextBox)
        {
            OutputTextBox = outputTextBox;
        }
        public int ExecuteScript(string command)
        {
            shouldReturn = false;
           // Output = new StringBuilder();

            Task.Factory.StartNew(() => RunProcess(command));
            return Task.Factory.StartNew(() => GetResult()).ContinueWith((result) =>
            {
                return result;
            }).Result.Result;
        }

        public int GetResult()
        {
            while (!shouldReturn)
            {
                Thread.Sleep(1000);
            }
            return result;
        }



        private void Proc_Exited(object sender, EventArgs e)
        {

            shouldReturn = true;
        }

        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputTextBox.Invoke((MethodInvoker)delegate
            {
                
                OutputTextBox.AppendText("\n" + e.Data);
            });
           
        }

        private void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                OutputTextBox.AppendText ( "\n" + e.Data);

                ((Process)sender).Kill();
            }

        }

        private void RunProcess(string command)
        {
            ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\bash.exe")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            info.Arguments = command;
            var proc = new Process
            {
                StartInfo = info,


            };
            proc.EnableRaisingEvents = true;


            proc.ErrorDataReceived += Proc_ErrorDataReceived;
            proc.OutputDataReceived += Proc_OutputDataReceived;
            
           

            proc.Exited += Proc_Exited;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
           
        }

    }
}
