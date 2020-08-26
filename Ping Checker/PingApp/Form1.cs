using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

using Timer = System.Windows.Forms.Timer;

namespace PingApp
{
    public partial class Form1 : Form
            
    {
        Timer t = new Timer();
     
        public Form1()
        {
            InitializeComponent();
        }       
        private void Form1_Load(object sender, EventArgs e)
        {        
            t.Tick += delegate (object _s, EventArgs _e)
            {
                string a = (txtAddress.Text);
                Ping p = new Ping();
                PingReply pr = p.Send(a);

                
                    
                    txtResult.Text += string.Format("Result: {0}, {1} -> {2} ms. Time: {3}. {4}", pr.Status.ToString(),
                        pr.Address, pr.RoundtripTime.ToString(), DateTime.Now, Environment.NewLine);

                    System.IO.File.AppendAllText(string.Format("Ping {0}.txt", Dns.GetHostAddresses(a).FirstOrDefault()),
                     (string.Format("{0}, {1}, {2}, {3} {4}", pr.Status.ToString(),
                        pr.Address, pr.RoundtripTime.ToString(), DateTime.Now, Environment.NewLine)));

                if (txtResult.Lines.Length > 11)
                {
                    int index = txtResult.Text.IndexOf(Environment.NewLine);
                    txtResult.Select(0,index + 2);
                    txtResult.Cut();
                }                                           
                    
                
                
            };            
        }
        
        private void btnPing_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrEmpty((txtAddress.Text)))
            {                
                MessageBox.Show("Error, Enter an IP address!\nExample: 0.0.0.0 or www.example123.com");
            }
           

            
            
            
        }
        private void txtReset_Click(object sender, EventArgs e)
        {
            txtAddress.Text = String.Empty;
            txtResult.Text = String.Empty;            
            t.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {

            t.Stop();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = String.Empty;
            txtAddress.Text = String.Empty;
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

           
                t.Interval = (int)numericUpDown1.Value;
                t.Start();
           
           
        }
    }
}
