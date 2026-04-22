using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Security.Cryptography.X509Certificates;

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
        public Vector2 position;
        Vector2 size;
        ColorChip color;
        bool atHome;
        int ?casilla;
        public int ? Casilla
        {
            get { return casilla; }
        }
        #endregion

        #region methods
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

            //casilla en la que esta 
            casilla = null;
            
        }
        public static void Load(ContentManager Content)
        {
            //carga la imagen de fichas.png contiene red,yellow,green,blue.
            chipsFullSprite = Content.Load<Texture2D>("chips.png");

        }
        public  void Draw(SpriteBatch _spriteBatch)
        {
            //dibuja esta ficha.
            int SpriteSpaceBetweenChips = 8;
            int spriteDivided = (chipsFullSprite.Width / 4);
            int spriteChipsWidth = spriteDivided - SpriteSpaceBetweenChips;


            // Definimos qué parte del sprite queremos (x, y, ancho, alto)
            Rectangle fuente = new Rectangle( (int)color * (spriteDivided + SpriteSpaceBetweenChips) , 0, spriteChipsWidth, chipsFullSprite.Height);

            // Dibujamos
            _spriteBatch.Draw(
                chipsFullSprite,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );

        }
        public void Click(Vector2 MousePos, ref ColorChip turn, int faceUp, ref Player[] players, Vector2 boardSize)
        {
            if (MouseHandeler.GetClick())
            {

            }
            if (players[(int)turn].Dice.EndedAnimation)
            {

            
            //cuando hago click en esta ficha.
            if(this.atHome && faceUp != 5)
            {
                if (turn < ColorChip.blue)
                {
                    turn++;
                }
                else
                {
                    turn = ColorChip.red;
                    foreach(Player p in players)
                    {
                        p.Dice.Enable = true;
                        p.Dice.FaceUp = null;
                    }
                }
            } else if (
               MousePos.X >= this.position.X && MousePos.X <= this.position.X + this.size.X &&
               MousePos.Y >= this.position.Y && MousePos.Y <= this.position.Y + this.size.Y && MouseHandeler.GetClick() &&
               turn == color
              )
            {
                Debug.WriteLine(casilla);
                //si se hace click en ficha.
                if (this.atHome)
                {
                    //casilla de salida para cada color
                    if(color == ColorChip.red)
                    {
                        casilla = 0;
                    }else if(color == ColorChip.green)
                    {
                        casilla = 15;
                    }else if(color == ColorChip.yellow)
                    {
                        casilla = 30;
                    }else if(color == ColorChip.blue)
                    {
                        casilla = 45;
                    }

                    //posicion de salida para cada ficha.
                    if (color == ColorChip.red || color == ColorChip.green)
                    {
                        int ofset = 400 - (int)position.X;
                        position.X += ofset;
                    }
                    else if (color == ColorChip.blue || color == ColorChip.yellow)
                    {
                        position.X = position.X - ( (int)position.X - 650 );
                    }
                    if (color == ColorChip.red || color == ColorChip.blue)
                    {
                        position.Y += 40;
                    }
                    else if (color == ColorChip.green || color == ColorChip.yellow)
                    {
                        position.Y -= 40;
                    }

                    //fuera de casa.
                    atHome = false;
                }

                //cambio de turno
                if (turn < ColorChip.blue)
                {
                    turn++;
                }
                else
                {
                    turn = ColorChip.red;
                    foreach (Player p in players)
                    {
                        p.Dice.Enable = true;
                        p.Dice.FaceUp = null;
                    }
                }

            }

            }
        }
        public void Move()
        {

            //movimiento ficha.
            position.X += 1;
        }
        #endregion


    }
}
