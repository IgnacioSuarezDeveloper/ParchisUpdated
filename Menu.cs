using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ParchisFresh
{
    internal static class Menu
    {
        #region properties
        private static bool selectedNPlayers;
        private static int Nplayers;
        private static Vector2 MinusButtonPositionxy { get; set; }
        private static Vector2 MaxButtonPositionxy { get; set; }
        private static Vector2 OkButtonPositionxy { get; set; }
        //almacena el sprite de botones .png.
        static private Texture2D buttonsSprite { get; set; }
        //almacena el sprite de boton okay.png
        static private Texture2D bottonOkSprite { get; set; }
        #endregion

        #region methods
        //almacena la posicion del boton izquierdo.
        public static Vector2 MInusButtonPositionxy
        {
            get { return MinusButtonPositionxy; }
        }

        //almacena la posicion del boton derecho.
        public static Vector2 MAxButtonPositionxy
        {
            get { return MaxButtonPositionxy; }
        }

        //almacena la posicion del boton ok.
        public static Vector2 OKButtonPositionxy
        {
            get { return OkButtonPositionxy; }
        }

        //geter seter de n players.
        public static int NPlayers
        {
            get { return Nplayers; }
            set
            {
                if (value >= 2 && value <= 4)
                {
                    Nplayers = value;
                }

            }
        }

        //get set booleano n players seleccionado ?
        public static bool SelectedNPlayers
        {
            get { return selectedNPlayers; }
            set { selectedNPlayers = value; }
        }
        public static void LoadMenuSprites(ContentManager Content)
        {
            //cargando el sprite de botones.
            buttonsSprite = Content.Load<Texture2D>("buttons.png");

            //cargando el boton de okay
            bottonOkSprite = Content.Load<Texture2D>("okButton.png");
        }//LoadMenuSprites.
        public static void DrawLeftButton(SpriteBatch _spriteBatch, float x, float y, int BoardWidth, int BoardHeight)
        {
            // Recorte del sprite de buttons el boton izquierdo.
            Rectangle fuente = new Rectangle(345, 430, 240, 190);

            // Dibujamos el Recorte.
            _spriteBatch.Draw(
                buttonsSprite,
               new Rectangle((int)x, (int)y, BoardWidth / 10, BoardHeight / 10),
                fuente,
                Color.White
            );
        }//DrawLeftButton.
        public static void DrawRightButton(SpriteBatch _spriteBatch, float x, float y, int BoardWidth, int BoardHeight)
        {
            // Recorte del boton derecho.
            Rectangle fuente = new Rectangle(345 + 610, 430, 240, 190);

            // Dibujamos el recorte del boton derecho.
            _spriteBatch.Draw(
                buttonsSprite,
               new Rectangle((int)x, (int)y, BoardWidth / 10, BoardHeight / 10),
                fuente,
                Color.White
            );
        }//DrawRightButton.
        public static void DrawOkButton(SpriteBatch _spriteBatch, float x, float y, int BoardWidth, int BoardHeight)
        {
            //Recorte del boton OK.
            Rectangle fuente = new Rectangle(0, 0, bottonOkSprite.Width, bottonOkSprite.Height);

            // Dibujamos el boton OK.
            _spriteBatch.Draw(
                bottonOkSprite,
               new Rectangle((int)x, (int)y, (BoardWidth / 10) , BoardHeight / 10),
                fuente,
                Color.White
            );
        }//DrawOkButton.

        //Inicializa posicion de los botones del menu para seleccionar numero de Jugadores.
        public static void InitializeButtons(Vector2 Mxy, Vector2 Pxy, Vector2 Oxy)
        {
            selectedNPlayers = false;
            MinusButtonPositionxy = Mxy;
            MaxButtonPositionxy = Pxy;
            OkButtonPositionxy = Oxy;
        }//InitializaButtons.
        public static void MaxButtonClicked(bool cliked, Vector2 MousePos, ref bool clickRightButton, int BoardWidth, int BoardHeidth)
        {

            if (cliked)
            {
                if (
                    MousePos.X >= Menu.MAxButtonPositionxy.X && MousePos.X <= Menu.MAxButtonPositionxy.X + (BoardWidth / 10)
                    && MousePos.Y >= Menu.MAxButtonPositionxy.Y && MousePos.Y <= Menu.MAxButtonPositionxy.Y + (BoardHeidth / 10))
                {
                    clickRightButton = true;
                }

            }
            else if (clickRightButton && !cliked)
            {
                NPlayers++;
                clickRightButton = false;
            }
        }//MaxButtonClicked
        public static void MinButtonClicked(bool cliked, Vector2 MousePos, ref bool clickLeftButton, int BoardWidth, int BoardHeidth)
        {
            if (cliked)
            {
                if (
                    MousePos.X >= Menu.MinusButtonPositionxy.X && MousePos.X <= Menu.MinusButtonPositionxy.X + (BoardWidth / 10)
                    && MousePos.Y >= Menu.MinusButtonPositionxy.Y && MousePos.Y <= Menu.MinusButtonPositionxy.Y + (BoardHeidth / 10))
                {
                    clickLeftButton = true;
                }

            }
            else if (clickLeftButton && !cliked)
            {
                NPlayers--;
                clickLeftButton = false;
            }
        }//MinButtonClicked.
        public static void OkButtonClicked(bool cliked, Vector2 MousePos, ref bool clickOkButton, int BoardWidth, int BoardHeidth)
        {
            if (cliked)
            {
                if (
                    MousePos.X >= Menu.OKButtonPositionxy.X && MousePos.X <= Menu.OKButtonPositionxy.X + (BoardWidth / 10)
                    && MousePos.Y >= Menu.OKButtonPositionxy.Y && MousePos.Y <= Menu.OKButtonPositionxy.Y + (BoardHeidth / 10))
                {
                    clickOkButton = true;
                }

            }
            else if (clickOkButton && !cliked)
            {
                selectedNPlayers = true;
                clickOkButton = false;
            }
        }//OkButtonClicked.
        #endregion

    }
}
