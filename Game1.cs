using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;

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

        SpriteFont miFuente;
        //tamaño de las fichas.
        private Vector2 chipSize = new Vector2(50, 50);

        //array de jugadores.
        private Player[] players;

        //turno 
        private ColorChip turn;
        #endregion

        //numero de casillas que hay en el tablero.
        int NumberOfCells = 60;

        List<int> indexFixed = new List<int>();
        private Vector2 vectorMinusButton;
        private Vector2 vectorPlusButton;
        private Vector2 vectorOkButton;

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
            InitMenuVariables();

            miFuente = Content.Load<SpriteFont>("File");            //cargando los esprites del menu.
            Menu.LoadMenuSprites(Content);
           
            //inicializando los botones.
            Menu.InitializeButtons(vectorMinusButton,vectorPlusButton,vectorOkButton);

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
                    c.Click(MouseHandeler.Position, ref turn, (int)players[(int)turn].Dice.FaceUp,ref players, boardSize, players[(int)turn].AllAtHome(), AmountOfChipsInCell((int)turn * 15 ));
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
                p.Dice.Draw(_spriteBatch, p.Dice.FaceAnimation);
            }
        }

        public void AllDicesClick()
        {
            foreach (Player p in players)
            {
               bool click = p.Dice.Click(ref turn, _spriteBatch);
                if (click)
                {
                   //Debug.WriteLine(p.Dice.FaceUp);
                }
            }
        }

        public  void CheckChipsInSameCell()
        {
            //comprobar cuantas fichas hay en la misma casilla.
            //para si hay mas de dos mostrar en la salida la casilla
            //en la que hay mas de dos para posteriormente arreglar la posicion de cada ficha.

            //por cada casilla.
            for(int i = 0; i < NumberOfCells; i++)
            {
                //variable que almacena numero de fichas en la casilla.
                int nFichasInCell = 0;

                //recorre la fichas de cada jugador.
                foreach(Player p in players)
                {

                    //coincide la casilla de la ficha con la i que es la casilla del tablero?
                    //si si ahumento el la variable que almacena el numero de fichas  que es nFichasInCell
                    foreach(Chip c in p.Fichas)
                    {
                        if(c.Casilla == i)
                        {
                            nFichasInCell++;
                        }
                    }

                }
                //hay almenos dos fichas en alguna casilla? y la casilla no ha sido registrada antes?
                if(nFichasInCell >= 2 && !indexFixed.Contains(i))
                {
                    indexFixed.Add(i);
                    bool primera = false;

                    bool segundo = false;

                    foreach (Player p in players)
                    {
                        foreach (Chip c in p.Fichas)
                        {
                            if(c.Casilla == i && !primera)
                            {
                                c.position.X -= 20;
                                primera = true;

                            }else if (c.Casilla == i && !segundo)
                            {

                                c.position.X += 20;
                                segundo = true;
                            }
                        }
                    }
                    nFichasInCell = 0;
                }
            }
        }

        public int AmountOfChipsInCell(int casilla)
        {
            int count = 0;
                foreach(Player p in players)
                {
                    foreach(Chip c in p.Fichas)
                    {
                        if(c.Casilla == casilla)
                        {
                            ++count;
                        }
                    }
                }
            
            return count;   
        }

        public void InitMenuVariables()
        {
            Menu.NPlayers = 2;
            int buttonsOfset = 100;
            vectorMinusButton = new System.Numerics.Vector2((boardSize.X / 2) + (boardSize.X / 10) - buttonsOfset, (boardSize.Y / 2) - (boardSize.Y / 20));
            vectorPlusButton = new System.Numerics.Vector2((boardSize.X / 2) + (boardSize.X / 10), (boardSize.Y / 2) - (boardSize.Y / 20));
            vectorOkButton = new System.Numerics.Vector2((boardSize.X / 2) - 80, (boardSize.Y / 2) + 200);

        }


        #endregion
    }
}
