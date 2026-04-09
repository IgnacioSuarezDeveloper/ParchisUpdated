using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace ParchisFresh
{
    //para crear cada jugador de la partida con sus fichas del mismo color que el jugador.
    internal class Player
    {
        //4 fichas.
        Chip[] fichas;

        //color del jugador.
        ColorChip color;

        //posicion del jugador.
        Vector2 position;

        //geter de Color.
        public ColorChip Color
        {
            get { return color; }
        }

        //geter fichas.
        public Chip[] Fichas
        {
            get { return fichas; }
        }

        //constructor.
        public Player(ColorChip _color, Vector2 boardSize, Vector2 chipSize)
        {
            //fichas del jugador.
            fichas = new Chip[4];

            //color jugador.
            color = _color;
            
            //inicializando la posicion del jugador
            if(color == ColorChip.red)
            {
                position.X = (boardSize.X / 4) - 180;
                position.Y = (boardSize.Y / 4) - 150;
            }else if (color == ColorChip.green)
            {
                position.X = (boardSize.X / 4) - 180;
                position.Y = (boardSize.Y) - 250;
            }else if(color == ColorChip.blue)
            {
                position.X = boardSize.X - 270;
                position.Y = (boardSize.Y / 4) - 150;
            }else if(color == ColorChip.yellow)
            {
                position.X = boardSize.X - 270;
                position.Y = boardSize.Y - 250;
            }

                int ofset = 50;
            for (int i = 0; i < fichas.Length; i++)
            {
                Vector2 positionFixed = new Vector2(position.X + ofset * i , position.Y);
                fichas[i] = new Chip(positionFixed, chipSize, color, true);
            }
           
        }


        //dibuja todas las fichas del jugador.
        public void DrawAllChips(SpriteBatch _spriteBatch)
        {
            foreach(Chip c in fichas)
            {
                c.Draw(_spriteBatch);
            }
        }
    }
}
