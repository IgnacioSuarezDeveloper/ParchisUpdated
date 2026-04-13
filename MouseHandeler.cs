using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ParchisFresh
{
    internal static class MouseHandeler
    {
        #region properties

        static MouseState mouseState = new MouseState();//estado del mouse.
        static Vector2 position = new Vector2(); //posicion del mouse.

        
        #endregion

        #region methods
        static public Vector2 GetPos()
        {
            //obtiene la posicion del raton en x e y.
            mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        static public Vector2 Position
        {
            //posicion del mouse.
            get { return position; }
            set { position = value; }
        } 

        static public bool GetClick()
        {
            //click mouse?
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }
        #endregion
    }
}
