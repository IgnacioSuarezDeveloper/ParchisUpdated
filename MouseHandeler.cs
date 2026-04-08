using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ParchisFresh
{
    internal static class MouseHandeler
    {
        //estado del mouse.
        static MouseState mouseState = new MouseState();

        //posicion del mouse.
        static Vector2 position = new Vector2();

        //obtiene la posicion del raton en x e y.
        static public Vector2 GetPos()
        {
            mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        //posicion del mouse.
        static public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        //click mouse?
        static public bool GetClick()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }
    }
}
