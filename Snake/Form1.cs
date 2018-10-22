using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form, IView
    {
        public GameState state { get; set; } = GameState.MainMenu;
        private Model Mod { get; set; }

        Label label;
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
        
        void createStartButton(String text)
        {
            startButton = new Button();
            startButton.AutoSize = true;
            startButton.Anchor = AnchorStyles.Top;
            startButton.Text = text;
            startButton.Click += new System.EventHandler(this.startButtonClicked);

            startButton.Left = (this.panel1.Width - startButton.Width) / 2;
            startButton.Top = (this.panel1.Height - startButton.Height) * 2;
            panel1.Controls.Add(startButton);
        }

        void mainMenuStateBegin()
        {
            label = new Label();
            label.Text = "SNAKE";
            label.Font = new Font("Arial", 18, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Left = (this.panel1.Width - label.Width) / 2;
            label.Top = (this.panel1.Height - label.Height) / 3;
            panel1.Controls.Add(label);

            createStartButton("StartGame");
        }

        void mainMenuStateEnd()
        {
            // delete button
            panel1.Controls.Remove(startButton);
            Mod.StartButtonClicked = true;

            panel1.Controls.Remove(label);
        }

        void playStateBegin()
        {
            tLP = new TableLayoutPanel();

            tLP.ColumnCount = Mod.PlayColumns;
            tLP.RowCount = Mod.PlayRows;

            gamamap = new PictureBox[playRows, playColumns];

            for (int i = 0; i < playRows; i++) {
                for (int j = 0; j < playColumns; j++) {
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
            tLP.AutoSize = true;
            tLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(tLP);
        }

        void playState()
        {
            for (int i = 0; i < playRows; i++) {
                for (int j = 0; j < playColumns; j++) {
                    gamamap[i, j].BackColor = Color.FromArgb(225,225,225);
                }
            }
            drawSnake();
            drawApple();
        }
        void drawSnake()
        {
            int sLength = Mod.TheSnake.Segments.Count;
            if (sLength > 0)
            {
                Place p = Mod.TheSnake.Segments[0];
                // head
                gamamap[p.Coordinates.Y, p.Coordinates.X].BackColor = Color.FromArgb(0, 0, 0);

                int color = 30;
                // rest of segments
                for (int i = 1; i < sLength; i++) {
                    p = Mod.TheSnake.Segments[i];
                    if (i < 6 || i > sLength - 7) {
                        color += 10;
                    }
                    gamamap[p.Coordinates.Y, p.Coordinates.X].BackColor = Color.FromArgb(color, color, color);
                }
            }
        }
        void drawApple()
        {
            gamamap[Mod.TheApple.place.Coordinates.Y, Mod.TheApple.place.Coordinates.X].BackColor = Color.DarkRed;
        }

        void playStateEnd()
        {
            // deleting map
            for (int i = 0; i < playRows; i++) {
                for (int j = 0; j < playColumns; j++) {
                    gamamap[i, j] = null;
                }
            }
            panel1.Controls.Remove(tLP);
            tLP = null;
        }

        void endGameStateBegin()
        {
            label = new Label();
            label.Font = new Font("Arial", 10, FontStyle.Regular);
            label.AutoSize = true;
            label.Text = Mod.applesEaten + " apples eaten.";
            panel1.Controls.Add(label);

            createStartButton("Retry?");
        }

        void endGameStateEnd()
        {
            // delete button
            panel1.Controls.Remove(startButton);
            Mod.StartButtonClicked = true;

            panel1.Controls.Remove(label);
        }

        void startButtonClicked(object sender, EventArgs e)
        {
            setGameState(GameState.Play);
        }

        public void setGameState(GameState nState)
        {
            endState();
            state = nState;
            switch (state)
            {
                case GameState.Play:
                    playStateBegin();
                    break;
                case GameState.EndGame:
                    endGameStateBegin();
                    break;
            }
        }
        private void endState()
        {
            switch(state)
            {
                case GameState.MainMenu:
                    mainMenuStateEnd();
                    break;
                case GameState.Play:
                    playStateEnd();
                    break;
                case GameState.EndGame:
                    endGameStateEnd();
                    break;
                    
            }
        }
    }
}
