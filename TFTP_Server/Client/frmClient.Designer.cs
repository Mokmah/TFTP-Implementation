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
            this.txtDownloadRemoteFIle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDownloadIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewFileDownload = new System.Windows.Forms.TextBox();
            this.lblDownloadSourceFile = new System.Windows.Forms.Label();
            this.gbUpload = new System.Windows.Forms.GroupBox();
            this.gbDownload = new System.Windows.Forms.GroupBox();
            this.gbUpload.SuspendLayout();
            this.gbDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(5, 44);
            this.lblFilePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(172, 17);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "Chemin du fichier source :";
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(175, 43);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(208, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // lblIpServer
            // 
            this.lblIpServer.AutoSize = true;
            this.lblIpServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpServer.Location = new System.Drawing.Point(13, 90);
            this.lblIpServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIpServer.Name = "lblIpServer";
            this.lblIpServer.Size = new System.Drawing.Size(156, 17);
            this.lblIpServer.TabIndex = 2;
            this.lblIpServer.Text = "Adresse IP du serveur :";
            this.lblIpServer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtServerIPAdress
            // 
            this.txtServerIPAdress.Location = new System.Drawing.Point(173, 90);
            this.txtServerIPAdress.Margin = new System.Windows.Forms.Padding(2);
            this.txtServerIPAdress.Name = "txtServerIPAdress";
            this.txtServerIPAdress.Size = new System.Drawing.Size(328, 20);
            this.txtServerIPAdress.TabIndex = 3;
            // 
            // btnFileSearch
            // 
            this.btnFileSearch.Location = new System.Drawing.Point(415, 37);
            this.btnFileSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnFileSearch.Name = "btnFileSearch";
            this.btnFileSearch.Size = new System.Drawing.Size(86, 29);
            this.btnFileSearch.TabIndex = 4;
            this.btnFileSearch.Text = "Parcourir...";
            this.btnFileSearch.UseVisualStyleBackColor = true;
            this.btnFileSearch.Click += new System.EventHandler(this.btnFileSearch_Click);
            // 
            // lblRemoteFileName
            // 
            this.lblRemoteFileName.AutoSize = true;
            this.lblRemoteFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoteFileName.Location = new System.Drawing.Point(16, 133);
            this.lblRemoteFileName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRemoteFileName.Name = "lblRemoteFileName";
            this.lblRemoteFileName.Size = new System.Drawing.Size(153, 17);
            this.lblRemoteFileName.TabIndex = 5;
            this.lblRemoteFileName.Text = "Nom du fichier distant :";
            this.lblRemoteFileName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRemoteFileName
            // 
            this.txtRemoteFileName.Location = new System.Drawing.Point(173, 132);
            this.txtRemoteFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtRemoteFileName.Name = "txtRemoteFileName";
            this.txtRemoteFileName.Size = new System.Drawing.Size(328, 20);
            this.txtRemoteFileName.TabIndex = 6;
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(173, 172);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(154, 44);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(184, 169);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(160, 46);
            this.btnDownload.TabIndex = 8;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(11, 285);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(1041, 409);
            this.txtStatus.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(11, 270);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status :";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDownloadRemoteFIle
            // 
            this.txtDownloadRemoteFIle.Location = new System.Drawing.Point(184, 130);
            this.txtDownloadRemoteFIle.Margin = new System.Windows.Forms.Padding(2);
            this.txtDownloadRemoteFIle.Name = "txtDownloadRemoteFIle";
            this.txtDownloadRemoteFIle.Size = new System.Drawing.Size(328, 20);
            this.txtDownloadRemoteFIle.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Nom du fichier distant :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDownloadIP
            // 
            this.txtDownloadIP.Location = new System.Drawing.Point(184, 89);
            this.txtDownloadIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtDownloadIP.Name = "txtDownloadIP";
            this.txtDownloadIP.Size = new System.Drawing.Size(328, 20);
            this.txtDownloadIP.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Adresse IP du serveur :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNewFileDownload
            // 
            this.txtNewFileDownload.Location = new System.Drawing.Point(184, 42);
            this.txtNewFileDownload.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewFileDownload.Name = "txtNewFileDownload";
            this.txtNewFileDownload.Size = new System.Drawing.Size(328, 20);
            this.txtNewFileDownload.TabIndex = 12;
            // 
            // lblDownloadSourceFile
            // 
            this.lblDownloadSourceFile.AutoSize = true;
            this.lblDownloadSourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloadSourceFile.Location = new System.Drawing.Point(13, 42);
            this.lblDownloadSourceFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownloadSourceFile.Name = "lblDownloadSourceFile";
            this.lblDownloadSourceFile.Size = new System.Drawing.Size(166, 17);
            this.lblDownloadSourceFile.TabIndex = 11;
            this.lblDownloadSourceFile.Text = "Nom du nouveau fichier :";
            this.lblDownloadSourceFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbUpload
            // 
            this.gbUpload.Controls.Add(this.txtFilePath);
            this.gbUpload.Controls.Add(this.lblFilePath);
            this.gbUpload.Controls.Add(this.lblIpServer);
            this.gbUpload.Controls.Add(this.txtServerIPAdress);
            this.gbUpload.Controls.Add(this.btnFileSearch);
            this.gbUpload.Controls.Add(this.lblRemoteFileName);
            this.gbUpload.Controls.Add(this.txtRemoteFileName);
            this.gbUpload.Controls.Add(this.btnUpload);
            this.gbUpload.Location = new System.Drawing.Point(12, 13);
            this.gbUpload.Name = "gbUpload";
            this.gbUpload.Size = new System.Drawing.Size(517, 241);
            this.gbUpload.TabIndex = 18;
            this.gbUpload.TabStop = false;
            this.gbUpload.Text = "Téléverser un fichier";
            // 
            // gbDownload
            // 
            this.gbDownload.Controls.Add(this.txtDownloadIP);
            this.gbDownload.Controls.Add(this.btnDownload);
            this.gbDownload.Controls.Add(this.txtDownloadRemoteFIle);
            this.gbDownload.Controls.Add(this.lblDownloadSourceFile);
            this.gbDownload.Controls.Add(this.label1);
            this.gbDownload.Controls.Add(this.txtNewFileDownload);
            this.gbDownload.Controls.Add(this.label2);
            this.gbDownload.Location = new System.Drawing.Point(535, 13);
            this.gbDownload.Name = "gbDownload";
            this.gbDownload.Size = new System.Drawing.Size(517, 241);
            this.gbDownload.TabIndex = 19;
            this.gbDownload.TabStop = false;
            this.gbDownload.Text = "Télécharger un fichier";
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 705);
            this.Controls.Add(this.gbDownload);
            this.Controls.Add(this.gbUpload);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtStatus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmClient";
            this.Text = "Client TFTP";
            this.gbUpload.ResumeLayout(false);
            this.gbUpload.PerformLayout();
            this.gbDownload.ResumeLayout(false);
            this.gbDownload.PerformLayout();
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
        private System.Windows.Forms.TextBox txtDownloadRemoteFIle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownloadIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewFileDownload;
        private System.Windows.Forms.Label lblDownloadSourceFile;
        private System.Windows.Forms.GroupBox gbUpload;
        private System.Windows.Forms.GroupBox gbDownload;
    }
}

