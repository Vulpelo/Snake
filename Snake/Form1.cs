using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{

    public partial class Form1 : Form
    {
        private int playRows = 10;
        private int playColumns = 10;
        TableLayoutPanel tLP;
        Button[,] places;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            tLP = new TableLayoutPanel();

            tLP.ColumnCount = playColumns;
            tLP.RowCount = playRows;

            places = new Button[playColumns, playRows];

            for (int i = 0; i < playColumns; i++)
            {
                for (int j = 0; j < playRows; j++)
                {
                    places[i, j] = new Button();
                    places[i, j].Height = 10;
                    places[i, j].Width = 10;
                    var margs = places[i, j].Margin;
                    margs.All = 0;
                    places[i, j].Margin = margs;
                    places[i, j].Enabled = false;

                    tLP.Controls.Add(places[i,j]);
                }
            }
            tLP.Width = 400;
            tLP.Height = 400;
            tLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(tLP);

            PlayButton.Enabled = false;
        }
    }
}
