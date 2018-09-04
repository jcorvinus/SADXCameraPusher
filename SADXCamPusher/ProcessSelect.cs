using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SADXCamPusher
{
    public partial class ProcessSelect : Form
    {
        DialogResult diagEval;
        Process[] ActiveProcList;
        int[] pid_list;
        public Process returnProcess; // may change this to an int for PID or something like that
        public int selected_id = 0; // we have to store the selection so the clear doesn't eliminate it.

        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        public ProcessSelect()
        {
            InitializeComponent();            
        }

        private void ProcessSelect_Load(object sender, EventArgs e)
        {
            ProcessListUpdate();
        }

        public void ProcessListUpdate()
        {
            ActiveProcList = Process.GetProcesses();
            pid_list = new int[ActiveProcList.Count()];
            for (int i = 0; i < ActiveProcList.Count(); i++)
            {
                // Add listview item here
                procListView.Items.Add(ActiveProcList[i].ProcessName);
                procListView.Items[i].SubItems.Add(String.Format("{0:g}", ActiveProcList[i].Id));
                pid_list[i] = ActiveProcList[i].Id;

                /*if (selected_id == pid_list[i])
                {
                    listView1.Items[i].Selected = true;
                    listView1.Select();
                }*/
            }
            //toolStripStatusLabel1.Text = String.Format("Processes: {0:g}", process_list.Count());
        }

        // Button Methods
        private void loadButton_Click(object sender, EventArgs e)
        {
            // implement check for validity
            selected_id = procListView.SelectedIndices[0];
            if (ActiveProcList[selected_id].HasExited == false)
            {
                diagEval = System.Windows.Forms.DialogResult.OK;
                returnProcess = ActiveProcList[procListView.SelectedIndices[0]];
                this.DialogResult = diagEval;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Process");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            diagEval = System.Windows.Forms.DialogResult.Cancel;
            this.DialogResult = diagEval;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            ProcessListUpdate();
        }
    }
}
