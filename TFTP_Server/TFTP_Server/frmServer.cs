using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFTP_Server
{
    public partial class frmServer : Form
    {
        private Button btnDemarrer;
        private Button btnArreter;
        private TextBox txtStatus;

        public frmServer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnDemarrer = new System.Windows.Forms.Button();
            this.btnArreter = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnDemarrer
            // 
            this.btnDemarrer.Location = new System.Drawing.Point(48, 28);
            this.btnDemarrer.Name = "btnDemarrer";
            this.btnDemarrer.Size = new System.Drawing.Size(106, 41);
            this.btnDemarrer.TabIndex = 0;
            this.btnDemarrer.Text = "Démarrer";
            this.btnDemarrer.UseVisualStyleBackColor = true;
            // 
            // btnArreter
            // 
            this.btnArreter.Location = new System.Drawing.Point(235, 28);
            this.btnArreter.Name = "btnArreter";
            this.btnArreter.Size = new System.Drawing.Size(107, 41);
            this.btnArreter.TabIndex = 1;
            this.btnArreter.Text = "Arrêter ";
            this.btnArreter.UseVisualStyleBackColor = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(13, 90);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(361, 264);
            this.txtStatus.TabIndex = 2;
            // 
            // frmServer
            // 
            this.ClientSize = new System.Drawing.Size(386, 366);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnArreter);
            this.Controls.Add(this.btnDemarrer);
            this.Name = "frmServer";
            this.Text = "Serveur TFTP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
