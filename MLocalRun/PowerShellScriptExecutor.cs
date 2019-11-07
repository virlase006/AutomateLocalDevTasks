using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    class PowerShellScriptExecutor
    {
        //ISynchronizeInvoke Invoker;
        int result;
        RichTextBox OutputTextBox;
        public PowerShellScriptExecutor(ISynchronizeInvoke invoker)
        {
           
            runSpace = RunspaceFactory.CreateRunspace();
            // open it
            runSpace.Open();
        }
        static private Runspace runSpace;

        /// <summary>
        /// The active PipelineExecutor instance
        /// </summary>
        private PipelineExecutor pipelineExecutor;
        public void ExecutePowerShellScript(string script, ISynchronizeInvoke invoker,  List<KeyValuePair<string, string>> parameters, RichTextBox outputTextBox)
        {

            result = 0;
            OutputTextBox = outputTextBox;
         //   OutputTextBox.AppendText("Starting script... \n");
            Command command = new Command(script);
            foreach (var param in parameters)
            {
                command.Parameters.Add(param.Key, param.Value);
            }
            pipelineExecutor = new PipelineExecutor(runSpace, invoker, command);
            pipelineExecutor.OnDataReady += new PipelineExecutor.DataReadyDelegate(pipelineExecutor_OnDataReady);
            pipelineExecutor.OnDataEnd += new PipelineExecutor.DataEndDelegate(pipelineExecutor_OnDataEnd);
            pipelineExecutor.OnOutputReady += new PipelineExecutor.ErrorReadyDelegate(pipelineExecutor_OnOutputReady);
            pipelineExecutor.Start();
          
        }
        private void pipelineExecutor_OnDataEnd(PipelineExecutor sender)
        {
            if (sender.Pipeline.PipelineStateInfo.State == PipelineState.Failed)
            {
                result = -1;
                OutputTextBox.AppendText(string.Format("Error in script: {0}", sender.Pipeline.PipelineStateInfo.Reason));
            }
            else
            {

                var allOutputs = OutputTextBox.Text.Split('\n');
                result = Convert.ToInt32(allOutputs[allOutputs.Length - 2]);
            }

        }

        private void pipelineExecutor_OnDataReady(PipelineExecutor sender, ICollection<PSObject> data)
        {
            foreach (PSObject obj in data)
            {
                OutputTextBox.AppendText(obj.ToString() + "\n");
            }
        }

        void pipelineExecutor_OnOutputReady(PipelineExecutor sender, ICollection<object> data)
        {
            foreach (object e in data)
            {
                OutputTextBox.AppendText(e.ToString() + "\n");
            }
        }

       public int GetResult() 
        {
            return result;
        }
    }
}
