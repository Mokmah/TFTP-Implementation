namespace Client
{
    partial class frmClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblIpServer = new System.Windows.Forms.Label();
            this.txtServerIPAdress = new System.Windows.Forms.TextBox();
            this.btnFileSearch = new System.Windows.Forms.Button();
            this.lblRemoteFileName = new System.Windows.Forms.Label();
            this.txtRemoteFileName = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(15, 50);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(238, 25);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "Chemin du fichier source :";
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(259, 49);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(319, 26);
            this.txtFilePath.TabIndex = 1;
            // 
            // lblIpServer
            // 
            this.lblIpServer.AutoSize = true;
            this.lblIpServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpServer.Location = new System.Drawing.Point(37, 120);
            this.lblIpServer.Name = "lblIpServer";
            this.lblIpServer.Size = new System.Drawing.Size(216, 25);
            this.lblIpServer.TabIndex = 2;
            this.lblIpServer.Text = "Adresse IP du serveur :";
            this.lblIpServer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtServerIPAdress
            // 
            this.txtServerIPAdress.Location = new System.Drawing.Point(259, 121);
            this.txtServerIPAdress.Name = "txtServerIPAdress";
            this.txtServerIPAdress.Size = new System.Drawing.Size(499, 26);
            this.txtServerIPAdress.TabIndex = 3;
            // 
            // btnFileSearch
            // 
            this.btnFileSearch.Location = new System.Drawing.Point(621, 39);
            this.btnFileSearch.Name = "btnFileSearch";
            this.btnFileSearch.Size = new System.Drawing.Size(137, 47);
            this.btnFileSearch.TabIndex = 4;
            this.btnFileSearch.Text = "Parcourir...";
            this.btnFileSearch.UseVisualStyleBackColor = true;
            // 
            // lblRemoteFileName
            // 
            this.lblRemoteFileName.AutoSize = true;
            this.lblRemoteFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoteFileName.Location = new System.Drawing.Point(44, 184);
            this.lblRemoteFileName.Name = "lblRemoteFileName";
            this.lblRemoteFileName.Size = new System.Drawing.Size(209, 25);
            this.lblRemoteFileName.TabIndex = 5;
            this.lblRemoteFileName.Text = "Nom du fichier distant :";
            this.lblRemoteFileName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRemoteFileName
            // 
            this.txtRemoteFileName.Location = new System.Drawing.Point(259, 185);
            this.txtRemoteFileName.Name = "txtRemoteFileName";
            this.txtRemoteFileName.Size = new System.Drawing.Size(499, 26);
            this.txtRemoteFileName.TabIndex = 6;
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(98, 242);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(240, 71);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(421, 242);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(240, 71);
            this.btnDownload.TabIndex = 8;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(20, 365);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(768, 359);
            this.txtStatus.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 339);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 20);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status :";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 736);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtRemoteFileName);
            this.Controls.Add(this.lblRemoteFileName);
            this.Controls.Add(this.btnFileSearch);
            this.Controls.Add(this.txtServerIPAdress);
            this.Controls.Add(this.lblIpServer);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Name = "frmClient";
            this.Text = "Client TFTP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblIpServer;
        private System.Windows.Forms.TextBox txtServerIPAdress;
        private System.Windows.Forms.Button btnFileSearch;
        private System.Windows.Forms.Label lblRemoteFileName;
        private System.Windows.Forms.TextBox txtRemoteFileName;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}

