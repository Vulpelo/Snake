using System;
using System.Collections.Generic;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Snake
{
    public partial class Form1 : Form, IView
    {
        private Model Mod { get; set; }

        private int playRows = 10;
        private int playColumns = 10;
        TableLayoutPanel tLP;
        PictureBox[,] gamamap;



        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Model nModel)
        {
            Mod = nModel;
            
            InitializeComponent();


            tLP = new TableLayoutPanel();

            tLP.ColumnCount = playColumns;
            tLP.RowCount = playRows;

            gamamap = new PictureBox[playColumns, playRows];

            for (int i = 0; i < playColumns; i++)
            {
                for (int j = 0; j < playRows; j++)
                {
                    gamamap[i, j] = new PictureBox();
                    gamamap[i, j].Height = 20;
                    gamamap[i, j].Width = 20;
                    gamamap[i, j].BackColor = Color.LightGray;
                    var margs = gamamap[i, j].Margin;
                    margs.All = 1;
                    gamamap[i, j].Margin = margs;

                    tLP.Controls.Add(gamamap[i, j]);
                }
            }
            tLP.Width = 400;
            tLP.Height = 400;
            tLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(tLP);
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void PlayButton_Click(object sender, EventArgs e)
        //{
        //    PlayButton.Enabled = false;
        //}

        void IView.update()
        {
            for (int i = 0; i < playColumns; i++)
            {
                for(int j=0; j< playRows; j++)
                {

                    switch(Mod.gameMap[i, j].Has)
                    {
                        case PlaceHas.None:
                            gamamap[i, j].BackColor = Color.LightGray;
                            break;
                        case PlaceHas.Apple:
                            gamamap[i, j].BackColor = Color.DarkGray;
                            break;
                    }

                }
            }

        }
    }
}
