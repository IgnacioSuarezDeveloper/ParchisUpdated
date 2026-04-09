using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParchisFresh
{
    public class Game1 : Game
    {
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

        private ColorChip turn;
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
                p.Dice.Click(ref turn);
            }
        }
    }
}
