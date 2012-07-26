﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Playground
{
    public partial class frmGameMainForm : Form
    {
        public frmGameMainForm()
        {
            InitializeComponent();
        }

        private List<frmPlayerMainForm> lisForms = new List<frmPlayerMainForm>();

        private void button1_Click(object sender, EventArgs e)
        {
            frmPlayerMainForm frm = new frmPlayerMainForm();
            lisForms.Add(frm);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (frmPlayerMainForm aktForm in lisForms)
            {
                aktForm.Tick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtKey.Text = Program.m_objCampaign.saveCurrentGameState();

            Program.objinf.strCCKey = this.Text;
            Program.objinf.strSaveKey = txtKey.Text;
            Program.objinf.save();
        }
    }
}
