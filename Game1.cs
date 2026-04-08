using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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

        //ficha de prueba.
        private Chip chip;
        private Vector2 chipSize = new Vector2(50, 50);


        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = (int)boardSize.X;   // ancho en píxeles
            _graphics.PreferredBackBufferHeight = (int)boardSize.Y;  // alto en píxeles
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            board = new Board(new Vector2((int)InitialBoardPosition.X, (int)InitialBoardPosition.Y), new Vector2((int)boardSize.X, (int)boardSize.Y));
            chip = new Chip(new Vector2(100, 100), chipSize, ColorChip.red, true);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            board.Load(Content);
            chip.Load(Content);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (WantToExit())
                Exit();
            //se actualiza la posicion del mouse.
            MouseHandeler.Position = MouseHandeler.GetPos();


            chip.Click(MouseHandeler.Position);

                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CadetBlue);

            _spriteBatch.Begin();

            board.Draw(_spriteBatch);
            chip.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        
        public bool WantToExit()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return true;
            else return false;
        }
    }
}
