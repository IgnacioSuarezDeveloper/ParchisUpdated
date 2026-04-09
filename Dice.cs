using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace ParchisFresh
{
    internal class Dice
    {
        static Texture2D dicesTexture;

        ColorChip color;

        Vector2 position;

        Vector2 size;

        int faceUp;

        static Random rnd = new Random();

        private int sixesInRow;

        public int FaceUp
        {
            get { return faceUp; }
        }

        //constructor.
        public Dice(Vector2 positionInit, Vector2 sizeInit, ColorChip Color)
        {
            if(Color == ColorChip.red ||Color == ColorChip.green)
            {
                positionInit.X -= 60;
                positionInit.Y -= 90;
            }else if(Color == ColorChip.blue || Color == ColorChip.yellow)
            {
                positionInit.X += 190;
                positionInit.Y -= 90;
            }

            position = positionInit;
            size = sizeInit;
            color = Color;
            faceUp = 1;
            
        }

        //carga imagen del dado.
        public static void Load(ContentManager Content)
        {
            dicesTexture = Content.Load<Texture2D>("dices.png");
        }

        //dibuaja el dado.
        public  void Draw(SpriteBatch _spriteBatch, Vector2 cut)
        {

            // Definimos qué parte del sprite queremos (x, y, ancho, alto)
            Rectangle fuente = new Rectangle((int)cut.X * dicesTexture.Width / 6,(int)cut.Y ,dicesTexture.Width / 6 , dicesTexture.Height);


            // Dibujamos
            _spriteBatch.Draw(
                dicesTexture,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );
        }

        public bool Click(ref ColorChip turn)
        {
            //posicion del mouse.
            Vector2 mousePos = MouseHandeler.GetPos();

            //si se hace click en el dado y esta habilitado.
            if (
                mousePos.X >= position.X && mousePos.X <= position.X + size.X  &&
                mousePos.Y >= position.Y && mousePos.Y <= position.Y + size.Y  && 
                MouseHandeler.GetClick() && turn == this.color)
            {
                //se tira el dado.
                faceUp = rnd.Next(1, 7);

                //muestra la cara arriba en el debug console.
                Debug.WriteLine($"dado tirado {faceUp}");

                //dado habilitado.
                if (faceUp == 6)
                {
                    sixesInRow++;
                }
                else
                {
                    if(turn < ColorChip.blue)
                    {
                        turn++;
                    }
                    else
                    {
                        turn = ColorChip.red;
                    }
                        sixesInRow = 0;
                }
                return true;
            }
            else { return false; }


        }
    }
}
