using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ref link:https://www.youtube.com/watch?v=fPD2KwwZsjs&list=PLAIBPfq19p2EJ6JY0f5DyQfybwBGVglcR&index=70
// Use the WebClient to download a file asynchronously.
// leopus.weebly.com/uploads/4/5/9/1/4591803bsf_file_finder_1.0.1_src.zip

namespace DownloadFileAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtAdress_TextChanged(object sender, EventArgs e)
        {
            txtFilename.Text = Path.GetFileName(txtAdress.Text);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false; // disabled button more than once while its downloading
            progressBar.Value = 0; // resets when initiates another download
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileAsync(new Uri(txtAdress.Text), txtFilename.Text);

        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                MessageBox.Show("Download successfully completed!");
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }

            ((WebClient)sender).Dispose();

            btnDownload.Enabled = true; // re-enable button when download is completed
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
