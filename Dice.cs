using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Threading;

namespace ParchisFresh
{
    internal class Dice
    {
        static Texture2D dicesTexture;

        Vector2 position;

        Vector2 size;

        int faceUp;

        public int FaceUp
        {
            get { return faceUp; }
        }

        static Random rnd = new Random();

        ColorChip color;

        public Dice(Vector2 positionInit, Vector2 sizeInit, ColorChip Color)
        {
            position = positionInit;
            size = sizeInit;
            color = Color;
            faceUp = 1;
        }

        public static void Load(ContentManager Content)
        {
            dicesTexture = Content.Load<Texture2D>("dices.png");
        }

        public  void Draw(SpriteBatch _spriteBatch, Vector2 cut)
        {

            // Definimos qué parte del sprite queremos (x, y, ancho, alto)
            Rectangle fuente = new Rectangle((int)cut.X * (int)size.X,(int)cut.Y ,dicesTexture.Width , dicesTexture.Height);


            // Dibujamos
            _spriteBatch.Draw(
                dicesTexture,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );
        }
    }
}
