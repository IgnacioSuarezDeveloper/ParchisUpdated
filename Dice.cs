using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ParchisFresh
{
    internal class Dice
    {
        #region properties
        static Texture2D dicesTexture;
        Vector2 position;
        Vector2 size;
        ColorChip color;
        static Random rnd = new Random();
        int? faceUp;
        bool enable;
        private int sixesInRow;
        Vector2 faceAnimation;
        bool endedAnimation = true;
        public bool EndedAnimation
        {
            get { return endedAnimation; }
        }
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        public Vector2 FaceAnimation
        {
            get { return faceAnimation; }
        }
        public int ?FaceUp
        {
            get { return faceUp; }
            set {  faceUp = value;  }
        }

        #endregion

        #region metodos

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
            faceUp = null;
            enable = true;

        }//Dice();
        public static void Load(ContentManager Content)
        {

            //carga la imagen del dado.
            dicesTexture = Content.Load<Texture2D>("dices.png");

        }//Load();
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

        }//Draw();

        public async void DiceAnimation(SpriteBatch _spriteBatch, Vector2 cut)
        {
            endedAnimation = false;
            for(int i = 0; i < 6; i++)
            {
                Random rnd = new Random();

                //numero entre 0 y 4.
                faceUp = rnd.Next(0, 6);

                Debug.WriteLine($"{faceUp + 1} \n");

                faceAnimation.X = (int)faceUp;

                
                await (Task.Delay(1));
               
            }
            faceUp += 1;
            endedAnimation = true;
        }
        public bool Click(ref ColorChip turn, SpriteBatch _spriteBatch)
        {

            //posicion del mouse.
            Vector2 mousePos = MouseHandeler.GetPos();

            //si se hace click en el dado y esta habilitado.
            if (
                mousePos.X >= position.X && mousePos.X <= position.X + size.X  &&
                mousePos.Y >= position.Y && mousePos.Y <= position.Y + size.Y  && 
                MouseHandeler.GetClick() && turn == this.color && enable == true )
            {
                DiceAnimation(_spriteBatch, faceAnimation);
                enable = false;

                

                //se tira el dado.


                //muestra la cara arriba en el debug console.
                if(faceUp != null)
                {
                    Debug.WriteLine($"dado tirado {faceUp}");
                }

                //dado habilitado.
                return true;
            }
            else { return false; }

        }//Click();

        #endregion
    }
}
