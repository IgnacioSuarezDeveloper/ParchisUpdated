using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ParchisFresh
{
    //los colores de los jugadores y de las fichas.
   public enum ColorChip
    {
        red,
        yellow,
        green,
        blue
    }
    //clase para crear instancias de las fichas que seran 4 fichas X numero de jugadores.
    internal class Chip 
    {
        #region properties
        static Texture2D chipsFullSprite;

        ColorChip color;

        Vector2 position;

        Vector2 size;

        bool atHome;
        #endregion

        #region methods
        //constructor.
        public Chip(Vector2 InitPos, Vector2 size ,ColorChip ChipColor, bool atHome) 
        {
            //color de la ficha.
            this.color = ChipColor;

            //posicion en la pantalla.
            this.position = InitPos;

            //tamaño de la ficha.
            this.size = size;

            //esta en casa?
            this.atHome = atHome;
            
        }

        //carga la imagen de fichas.png contiene red,yellow,green,blue.
        public static void Load(ContentManager Content)
        {
            chipsFullSprite = Content.Load<Texture2D>("chips.png");
        }

        //dibuja esta ficha.
        public  void Draw(SpriteBatch _spriteBatch)
        {
            int SpriteSpaceBetweenChips = 8;
            int spriteDivided = (chipsFullSprite.Width / 4);
            int spriteChipsWidth = spriteDivided - SpriteSpaceBetweenChips;


            // Definimos qué parte del sprite queremos (x, y, ancho, alto)
            Rectangle fuente = new Rectangle( (int)color * (spriteDivided + SpriteSpaceBetweenChips) , 0, spriteChipsWidth, chipsFullSprite.Height);

            //Rectangle fuente = new Rectangle( (int)color * (spriteDivided + SpriteSpaceBetweenChips / (int)color) , 0, spriteChipsWidth, chipsFullSprite.Height);

            // Dibujamos
            _spriteBatch.Draw(
                chipsFullSprite,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );
        }

        //cuando hago click en esta ficha.
        public void Click(Vector2 MousePos)
        {
            if (
               MousePos.X >= this.position.X && MousePos.X <= this.position.X + this.size.X &&
               MousePos.Y >= this.position.X && MousePos.Y <= this.position.Y + this.size.X && MouseHandeler.GetClick() 
               )
               {
                    Debug.WriteLine($"ficha {this.color}");
               }
        }
        #endregion

        //movimiento ficha.
        public void Move()
        {
            position.X += 1;
        }
    }
}
