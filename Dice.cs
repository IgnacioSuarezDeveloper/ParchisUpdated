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
            //posicion inicial.
            if(Color == ColorChip.red ||Color == ColorChip.green)
            {
                positionInit.X -= 60;
                positionInit.Y -= 90;

            }else if(Color == ColorChip.blue || Color == ColorChip.yellow)
            {
                positionInit.X += 190;
                positionInit.Y -= 90;
            }

            //inicializando variables.
            position = positionInit;
            size = sizeInit;
            color = Color;
            faceUp = null;
            enable = true;

        }//Dice.
        public static void Load(ContentManager Content)
        {
            //carga la imagen del dado.
            dicesTexture = Content.Load<Texture2D>("dices.png");
        }//Load.
        public  void Draw(SpriteBatch _spriteBatch, Vector2 cut)
        {
            // Zona de recorte del Sprite de Dados.
            Rectangle fuente = new Rectangle((int)cut.X * dicesTexture.Width / 6,(int)cut.Y ,dicesTexture.Width / 6 , dicesTexture.Height);

            // Dibujamos el sprite de Dados por la zona recortada.
            _spriteBatch.Draw(
                dicesTexture,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );
        }//Draw.
        public bool Click(ref ColorChip turn, SpriteBatch _spriteBatch)
        {

            //posicion del mouse.
            Vector2 mousePos = MouseHandeler.GetPos();

            //click en el dado esta habilitado y es del color del turno ? 
            if (
                mousePos.X >= position.X && mousePos.X <= position.X + size.X &&
                mousePos.Y >= position.Y && mousePos.Y <= position.Y + size.Y &&
                MouseHandeler.GetClick() && turn == this.color && enable == true)
            {

                //animar el dado.
                DiceAnimation(_spriteBatch, faceAnimation);

                //deshabilitar dado.
                enable = false;

                //muestra la cara arriba en la salida.
                if (faceUp != null)
                {
                    Debug.WriteLine($"dado tirado {faceUp}");
                }

                return true;
            }
            else
            {

                return false;
            }

        }//Click;
        public async void DiceAnimation(SpriteBatch _spriteBatch, Vector2 cut)
        {
            //la animacion comienza.
            endedAnimation = false;

            //por cada cara.
            for(int i = 0; i < 6; i++)
            {

                //numero entre 0 y 6.
                faceUp = rnd.Next(0, 6);

                Debug.WriteLine($"face up : {faceUp + 1} \n");

                //x de la animación.
                faceAnimation.X = (int)faceUp;

                //delay de la animacion.
                await (Task.Delay(1));
               
            }
            //suma uno porque los numeros del dado comienzan en el 1 y no en 0 y para que llegue al 6.
            faceUp += 1;

            //final de la animacion.
            endedAnimation = true;

        }//DiceAnimation.
        #endregion
    }
}
