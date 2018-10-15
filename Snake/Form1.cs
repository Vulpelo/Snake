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
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            InitializeComponent();
        }
        public Form1(Model nModel)
        {
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            Mod = nModel;
            
            InitializeComponent();


            tLP = new TableLayoutPanel();
            
            tLP.ColumnCount = Mod.PlayColumns;
            tLP.RowCount = Mod.PlayRows;

            gamamap = new PictureBox[playRows, playColumns];

            for (int i = 0; i < playRows; i++)
            {
                for (int j = 0; j < playColumns; j++)
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

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Mod.changeDirection(e.KeyChar);
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
            for (int i = 0; i < playRows; i++)
            {
                for(int j=0; j < playColumns; j++)
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

            gamamap[Mod.TheApple.place.Coordinates.Y, Mod.TheApple.place.Coordinates.X].BackColor = Color.Black;

            if (Mod.TheSnake.Segments.Count > 0)
                foreach(Place p in Mod.TheSnake.Segments)
                    gamamap[p.Coordinates.Y ,p.Coordinates.X].BackColor = Color.DarkGray;
        }
    }
}
