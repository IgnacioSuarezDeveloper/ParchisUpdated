using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ParchisFresh
{
    public class Game1 : Game
    {
        #region properties
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //almacena el objeto tablero.
        private Board board;
        private Vector2 boardSize = new Vector2(1100, 1100);
        private Vector2 InitialBoardPosition = new Vector2(0, 0);

        //tamaño de las fichas.
        private Vector2 chipSize = new Vector2(50, 50);

        //array de jugadores.
        private Player[] players;

        //turno 
        private ColorChip turn;
        #endregion

        //numero de casillas que hay en el tablero.
        int NumberOfCells = 60;
        #region methods
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //ancho alto de la ventana.
            _graphics.PreferredBackBufferWidth = (int)boardSize.X;   // ancho en píxeles
            _graphics.PreferredBackBufferHeight = (int)boardSize.Y;  // alto en píxeles

            _graphics.ApplyChanges();
        }



        protected override void Initialize()
        {
            //crea objeto tablero.
            board = new Board(new Vector2((int)InitialBoardPosition.X, (int)InitialBoardPosition.Y), new Vector2((int)boardSize.X, (int)boardSize.Y));

            //jugadores init.
            players = new Player[4];

            for(int i = 0; i < players.Length; i++)
            {
                players[i] = new Player((ColorChip)i, boardSize, chipSize);
            }

            turn = ColorChip.red;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //cargando la imagen del tablero.
            board.Load(Content);

            //cargando el sprite de la ficha.
            Chip.Load(Content);

            //cargando el sprite de los dados.
            Dice.Load(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            //si cerramos el juego.
            if (WantToExit())
                Exit();

            //se actualiza la posicion del mouse.
            MouseHandeler.Position = MouseHandeler.GetPos();

            //click dados.
            AllDicesClick();


            foreach(Chip c in players[(int)turn].Fichas)
            {
                if(players[(int)turn].Dice.FaceUp != null)
                {
                    c.Click(MouseHandeler.Position, ref turn, (int)players[(int)turn].Dice.FaceUp,ref players, boardSize);
                }
            }

            CheckChipsInSameCell();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CadetBlue);

            _spriteBatch.Begin();

            //dibuja la tabla.
            board.Draw(_spriteBatch);

            //dibujando todas las fichas del jugador.
            DrawAllChips();

            //dibujando todos los dados.
            DrawAllDices();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        
        public bool WantToExit()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return true;
            else return false;
        }

        public void DrawAllChips()
        {
            foreach (Player p in players)
            {
                p.DrawAllChips(_spriteBatch);
            }
        }

        public void DrawAllDices()
        {
            foreach (Player p in players)
            {
                p.Dice.Draw(_spriteBatch, new Vector2(0, 0));
            }
        }

        public void AllDicesClick()
        {
            foreach (Player p in players)
            {
               bool click = p.Dice.Click(ref turn);
                if (click)
                {
                    Debug.WriteLine(p.Dice.FaceUp);
                }
            }
        }

        public void ChangeTurn(ref ColorChip turn)
        {
            if (turn < ColorChip.blue)
            {
                turn++;
            }
            else
            {
                turn = ColorChip.red;
            }
        }
        
        public  void CheckChipsInSameCell()
        {
            //comprobar cuantas fichas hay en la misma casilla.
            //para si hay mas de dos mostrar en la salida la casilla
            //en la que hay mas de dos para posteriormente arreglar la posicion de cada ficha.
            for(int i = 0; i < NumberOfCells; i++)
            {
                int count = 0;
                foreach(Player p in players)
                {
                    foreach(Chip c in p.Fichas)
                    {
                        if(c.Casilla == i)
                        {
                            count++;
                        }
                    }
                }
                if(count >= 2)
                {
                    //Debug.WriteLine($"en la casilla {i} hay {count} jugadores.");
                }
            }
        }
        #endregion
    }
}
