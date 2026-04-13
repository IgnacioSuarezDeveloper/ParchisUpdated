using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ParchisFresh
{
    internal class Board
    {
        //textura tablero.
        private Texture2D texture;

        //posicion tablero
        private Vector2 position;

        //tamaño tablero.
        private Vector2 size;
            
        //tamaño casilla blanca
        private Vector2 TileSize;
  
        public Vector2 tileSize
        {
            get { return TileSize; }
        }
        //constructor tablero.

        public Board(Vector2 _position, Vector2 _size)
        {
            position = _position;
            size = _size;
            TileSize = new Vector2((size.X * 3 / 11) / 3,((size.Y * 4/11)) / 8);
        }

        //carga imagen del tabler.
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("tablero.png");
        }

        //dibuja el tablero.
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
        }
        
    }
}
