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
            this.lblFilePath.Location = new System.Drawing.Point(1, 40);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(206, 20);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "Chemin du fichier source :";
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(230, 39);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(284, 22);
            this.txtFilePath.TabIndex = 1;
            // 
            // lblIpServer
            // 
            this.lblIpServer.AutoSize = true;
            this.lblIpServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpServer.Location = new System.Drawing.Point(20, 96);
            this.lblIpServer.Name = "lblIpServer";
            this.lblIpServer.Size = new System.Drawing.Size(185, 20);
            this.lblIpServer.TabIndex = 2;
            this.lblIpServer.Text = "Adresse IP du serveur :";
            this.lblIpServer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtServerIPAdress
            // 
            this.txtServerIPAdress.Location = new System.Drawing.Point(230, 97);
            this.txtServerIPAdress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtServerIPAdress.Name = "txtServerIPAdress";
            this.txtServerIPAdress.Size = new System.Drawing.Size(444, 22);
            this.txtServerIPAdress.TabIndex = 3;
            // 
            // btnFileSearch
            // 
            this.btnFileSearch.Location = new System.Drawing.Point(552, 31);
            this.btnFileSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFileSearch.Name = "btnFileSearch";
            this.btnFileSearch.Size = new System.Drawing.Size(122, 38);
            this.btnFileSearch.TabIndex = 4;
            this.btnFileSearch.Text = "Parcourir...";
            this.btnFileSearch.UseVisualStyleBackColor = true;
            this.btnFileSearch.Click += new System.EventHandler(this.btnFileSearch_Click);
            // 
            // lblRemoteFileName
            // 
            this.lblRemoteFileName.AutoSize = true;
            this.lblRemoteFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoteFileName.Location = new System.Drawing.Point(27, 149);
            this.lblRemoteFileName.Name = "lblRemoteFileName";
            this.lblRemoteFileName.Size = new System.Drawing.Size(183, 20);
            this.lblRemoteFileName.TabIndex = 5;
            this.lblRemoteFileName.Text = "Nom du fichier distant :";
            this.lblRemoteFileName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRemoteFileName
            // 
            this.txtRemoteFileName.Location = new System.Drawing.Point(230, 148);
            this.txtRemoteFileName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemoteFileName.Name = "txtRemoteFileName";
            this.txtRemoteFileName.Size = new System.Drawing.Size(444, 22);
            this.txtRemoteFileName.TabIndex = 6;
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(103, 194);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(213, 57);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(370, 194);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(213, 57);
            this.btnDownload.TabIndex = 8;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(18, 292);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(683, 288);
            this.txtStatus.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(18, 271);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 17);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status :";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 589);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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

