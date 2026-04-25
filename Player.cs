using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParchisFresh
{
    //para crear cada jugador de la partida con sus fichas del mismo color que el jugador.
    internal class Player
    {
        #region properties
        Vector2 position;
        ColorChip color;
        Chip[] fichas;
        Dice dice;


        public ColorChip Color
        {
            get { return color; }
        }
        public Chip[] Fichas
        {
            get { return fichas; }
        }
        public Dice Dice
        {
            get { return dice; }
        }
        #endregion

        #region methods
        public Player(ColorChip _color, Vector2 boardSize, Vector2 chipSize)
        {
            //color jugador.
            color = _color;

            //fichas del jugador.
            fichas = new Chip[4];



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
            //dado del jugador.
            dice = new Dice(position, chipSize, color);

            //distancia entre fichas.
            int ofset = 50;

            //posicion de las fichas en la pantalla al iniciar.
            for (int i = 0; i < fichas.Length; i++)
            {
                Vector2 positionFixed = new Vector2(position.X + ofset * i , position.Y);
                fichas[i] = new Chip(positionFixed, chipSize, color, true);
            }
           
        }
        public void DrawAllChips(SpriteBatch _spriteBatch)
        {
            //dibuja todas las fichas del jugador.

            foreach (Chip c in fichas)
            {
                c.Draw(_spriteBatch);
            }

        }
        public bool AllAtHome()
        {
            foreach(Chip c in fichas)
            {
                if (!c.AtHome)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
