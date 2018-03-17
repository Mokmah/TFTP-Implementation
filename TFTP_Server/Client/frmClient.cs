using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/************************
 *  Par William Garneau 
 ***********************/

namespace Client
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
            txtServerIPAdress.Text = "127.0.0.1";
        }

        // Évènement pour chercher un fichier dans un dialogue et retourner un string correspondant
        private void btnFileSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
