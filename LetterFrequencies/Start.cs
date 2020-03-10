using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace LetterFrequencies
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            this.CenterToScreen();
           // this.WindowState = FormWindowState.Maximized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
                     
        }

        private void Start_Load(object sender, EventArgs e)
        {
            lvLetterFrequencies.View = View.Details;
            lvLetterFrequencies.FullRowSelect = true;
            lvLetterFrequencies.Columns.Add("Letter");
            lvLetterFrequencies.Columns.Add("Frequency");
            lvLetterFrequencies.Columns[1].Width = 100;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtAvailableText.Text = "";
            lvLetterFrequencies.Items.Clear();
            zedGraphControl1.GraphPane.CurveList.Clear();
            

            string[] caractere;
            double[] frecvente;
            int[] c = new int[(int)char.MaxValue];
            char ch;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();

            openFileDialog1.Title = "Load the text file";
            openFileDialog1.InitialDirectory = "E:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (result == DialogResult.OK)
            {
                try
                {
                    string s = File.ReadAllText(openFileDialog1.FileName.ToString());
                    txtAvailableText.Text = s;

                    foreach (char t in s)
                    {                        
                        c[(int)t]++;
                    }

                    for (int i = 0; i < (int)char.MaxValue; i++)
                    {
                        if (c[i] > 0 && char.IsLetterOrDigit((char)i))
                        {
                            ch = (char)i;
                            ListViewItem lvi = new ListViewItem(ch.ToString());
                            lvi.SubItems.Add(c[i].ToString());
                            lvLetterFrequencies.Items.Add(lvi);
                        }
                    }

                    caractere = new string[lvLetterFrequencies.Items.Count];
                    frecvente = new double[lvLetterFrequencies.Items.Count];

                    for (int i = 0; i < lvLetterFrequencies.Items.Count; i++)
                    {
                        caractere[i] = lvLetterFrequencies.Items[i].SubItems[0].Text.ToString();
                        frecvente[i] = Convert.ToDouble(lvLetterFrequencies.Items[i].SubItems[1].Text);
                    }
                   

                    //** creez diagrama de frecventa a caracterelor/cifrelor
                    GraphPane myPane = zedGraphControl1.GraphPane;

                    myPane.Title = "Letter Frequency";
                    myPane.XAxis.Title = "Letter";
                    myPane.YAxis.Title = "Frequency";

                    BarItem myBar = myPane.AddBar("Character", null, frecvente, Color.Blue);
                    myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);

                    myPane.XAxis.IsTicsBetweenLabels = true;

                    myPane.XAxis.TextLabels = caractere;

                    myPane.XAxis.Type = AxisType.Text;

                    myPane.AxisFill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);

                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    zedGraphControl1.AxisChange();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }   
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }       
    }
}
