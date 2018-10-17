using System;

using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form, IView
    {
        public GameState state { get; set; } = GameState.MainMenu;
        private Model Mod { get; set; }

        private int playRows = 10;
        private int playColumns = 10;
        TableLayoutPanel tLP;
        PictureBox[,] gamamap;
        Button startButton;

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

            mainMenuStateBegin();
            //playStateBegin();
        }

        void playStateBegin()
        {
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

        void playState()
        {
            for (int i = 0; i < playRows; i++) {
                for (int j = 0; j < playColumns; j++) {
                    gamamap[i, j].BackColor = Color.LightGray;
                }
            }

            int sLength = Mod.TheSnake.Segments.Count;
            if (sLength > 0)
            {
                int color = 50;
                int maxColor = 180;
                for (int i=0; i<sLength; i++)
                {
                    Place p = Mod.TheSnake.Segments[i];
                    if(i < 7 || i > sLength - 7)
                        color += 11;
                    gamamap[p.Coordinates.Y, p.Coordinates.X].BackColor = Color.FromArgb(color, color, color);
                }
            }
            gamamap[Mod.TheApple.place.Coordinates.Y, Mod.TheApple.place.Coordinates.X].BackColor = Color.Black;
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

        void IView.update()
        {
            switch (state)
            {
                case GameState.Play:
                    playState();
                    break;
            }

        }

        void endGameStateBegin()
        {
            // deleting map
            flowLayoutPanel1.Controls.Remove(tLP);

            //startButton.
            startButton = new Button();
            startButton.AutoSize = true;
            startButton.Text = Mod.applesEaten + " apples eaten.\nRetry?";
            startButton.Click += new System.EventHandler(this.startButtonClicked);
            flowLayoutPanel1.Controls.Add(startButton);
        }

        void mainMenuStateBegin()
        {
            //startButton.
            startButton = new Button();
            startButton.AutoSize = true;
            startButton.Text = "Start Game";
            startButton.Click += new System.EventHandler(this.startButtonClicked);
            flowLayoutPanel1.Controls.Add(startButton);
        }

        void startButtonClicked(object sender, EventArgs e)
        {
            // delete button
            flowLayoutPanel1.Controls.Remove(startButton);
            Mod.StartButtonClicked = true;

            state = GameState.Play;
            playStateBegin();
        }

        public void setGameState(GameState nState)
        {
            state = nState;
            switch (state)
            {
                case GameState.EndGame:
                    endGameStateBegin();
                    break;
            }
        }
    }
}
