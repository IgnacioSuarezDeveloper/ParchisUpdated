using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ParchisFresh
{
    //los colores de los jugadores , fichas y dados.
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
        public static bool AllAtHome = true;
        public int ? Casilla
        {
            get { return casilla; }
        }
        public bool AtHome
        {
            get { return atHome; }
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

        }//Load.
        public  void Draw(SpriteBatch _spriteBatch)
        {
            //calculos para recortar la ficha del sprite
            int SpriteSpaceBetweenChips = 8;
            int spriteDivided = (chipsFullSprite.Width / 4);
            int spriteChipsWidth = spriteDivided - SpriteSpaceBetweenChips;


            // Rectangulo para recortar la ficha del sprite.
            Rectangle fuente = new Rectangle( (int)color * (spriteDivided + SpriteSpaceBetweenChips) , 0, spriteChipsWidth, chipsFullSprite.Height);

            //dibuja el recorte del sprite que es la ficha deseada y la dibuja en pantalla.
            _spriteBatch.Draw(
                chipsFullSprite,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                fuente,
                Color.White
            );

        }//Draw.
        public void Click(Vector2 MousePos, ref ColorChip turn, int faceUp, ref Player[] players, Vector2 boardSize , bool allAtHome, int nOfChipsBeguining)
        {
            //ha terminado la animación del dado?
            if (players[(int)turn].Dice.EndedAnimation)
            {
                //Por cada ficha del jugador que tiene el turno.
                foreach (Chip c in players[(int)turn].Fichas)
                {
                    //la ficha esta fuera?
                    if (!c.AtHome)
                    {
                        AllAtHome = false;
                        break;
                    }
                    else AllAtHome = true;
                }
           
            //todas las fichas en casa y no ha salido un 5 ?
            if( faceUp != 5 && AllAtHome)
            {
                ChangeTurn(ref turn, players);
            } else if (//click en la ficha y color de turno es el color de la ficha ?
               MousePos.X >= this.position.X && MousePos.X <= this.position.X + this.size.X &&
               MousePos.Y >= this.position.Y && MousePos.Y <= this.position.Y + this.size.Y && MouseHandeler.GetClick() &&
               turn == color
              )
            {
                    //fichas en la misma casilla 
                    int nConcurrentChipsStartCell = 0;

                //ficha clikada en casa y ha salido 5 ?
                if (this.atHome && faceUp == 5)
                {
                        //recorrer cada ficha del jugador 
                        foreach (Chip C in players[(int)turn].Fichas)
                        {
                                //el color es rojo y la casilla de la ficha es 0?
                                if(color == ColorChip.red && C.casilla == 0)
                                {

                                    nConcurrentChipsStartCell++;
 
                                }else if (//el color es verde y la casilla de la ficha es 15?
                                color == ColorChip.green && C.casilla == 15)
                                {

                                    nConcurrentChipsStartCell++;

                                }else if (//el color es amarillo y la casilla de la ficha es 30?
                                color == ColorChip.yellow && C.casilla == 30)
                                {
                                    nConcurrentChipsStartCell++;

                                }else if(//el color es azul y la casilla de la ficha es 45?
                                color == ColorChip.blue && C.casilla == 45)
                                {
                                    nConcurrentChipsStartCell++;
                                }
                           
                        }
                        
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

                    //color de la ficha es rojo o verde y en la casilla de salida de cada color hay menos de 2 fichas ?
                    if (color == ColorChip.red && nConcurrentChipsStartCell < 2 || color == ColorChip.green && nConcurrentChipsStartCell < 2)
                    {
                        int ofset = 400 - (int)position.X;
                        position.X += ofset;
                    }
                    //color de la ficha es azul o amarillo y en la casilla de salida de cada color hay menos de 2 fichas ?
                    else if (color == ColorChip.blue && nConcurrentChipsStartCell < 2 || color == ColorChip.yellow && nConcurrentChipsStartCell < 2)
                    {
                        position.X = position.X - ( (int)position.X - 650 );
                    }
                    //color de la ficha es rojo o azul y en la casilla de salida de cada color hay menos de 2 fichas ?
                    if (color == ColorChip.red && nConcurrentChipsStartCell < 2 || color == ColorChip.blue && nConcurrentChipsStartCell < 2)
                    {
                        position.Y += 40;
                    }
                    //color de la ficha es  verde o amarillo y en la casilla de salida de cada color hay menos de 2 fichas ?
                    else if (color == ColorChip.green && nConcurrentChipsStartCell < 2|| color == ColorChip.yellow && nConcurrentChipsStartCell < 2)
                    {
                        position.Y -= 40;
                    }

                    //fuera de casa.
                    atHome = false;

                        //hay menos de dos fichas en la de salida?
                        if(nConcurrentChipsStartCell < 2)
                        {
                            //cambio de turno.
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
                }else if (!this.atHome)
                    {
                        //cambio de turno
                        ChangeTurn(ref turn, players);
                    }
            }

            }
        }
        private void ChangeTurn(ref ColorChip  turn, Player[] players )
        {
            //turno no es azul?
            if (turn < ColorChip.blue)
            {
                //cambio de turno.
                turn++;
            }
            else //el turno es azul?
            {
                //cambiar turno a rojo.
                turn = ColorChip.red;
                foreach (Player p in players)
                {
                    //se habilitan todos los dados.
                    p.Dice.Enable = true;

                    //la cara arriba de todos los dados nula.
                    p.Dice.FaceUp = null;
                }
            }
        }
        
        #endregion

    }
}
