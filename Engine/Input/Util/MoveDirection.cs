using Hel.Engine.Input.GameComponent;
using Hel.Engine.Input.Model;
using Hel.Toolkit.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hel.Engine.Input.Util
{
    public static class MoveDirection
    {
        public static KeyboardState KeyState => InputHandler.KeyboardState;
        public static KeyboardState LastKeyState => InputHandler.LastKeyboardState;
        private static int _lastX;

        /// <summary>
        /// KeyboardDirection takes the current keyboard state, and calculates the required direction accordingly. 
        /// </summary>
        /// <returns>Direction to move based on keyboard input.</returns>
        public static Vector2 KeyboardDirection(DirectionalKeys directionalKeys)
        {
            int x = ConversionUtil.BoolToInt(KeyState.IsKeyDown(directionalKeys.Right)) - ConversionUtil.BoolToInt(KeyState.IsKeyDown(directionalKeys.Left));
            int y = ConversionUtil.BoolToInt(KeyState.IsKeyDown(directionalKeys.Down)) - ConversionUtil.BoolToInt(KeyState.IsKeyDown(directionalKeys.Up));

            if (KeyState.IsKeyDown(directionalKeys.Right) && KeyState.IsKeyDown(directionalKeys.Left))
            {
                x = LastKeyState.IsKeyDown(directionalKeys.Right) && LastKeyState.IsKeyDown(directionalKeys.Left) ? _lastX : -_lastX;
            }

            Vector2 dir = new Vector2(x, y);

            _lastX = (int)dir.X;

            if (!dir.Equals(Vector2.Zero)) dir.Normalize(); 

            return dir;
        }

    }
}
