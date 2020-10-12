using Hel.Engine.Input.Model;
using Hel.Engine.Input.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hel.Engine.Input.GameComponent
{
    /// <summary>
    /// Basic inputhandler for executing commands linked to keybindings.
    ///
    /// Runs as as game component and checks if any keybindings are active. If so, runs all commands linked to the keybinding.
    /// <see cref="KeyBindingManager"/> for more details on keybindings
    /// </summary>
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {

        public static KeyboardState KeyboardState { get; private set; } = Keyboard.GetState();
        public static KeyboardState LastKeyboardState { get; private set; } = Keyboard.GetState();
        
        public static Vector2 MoveVector { get; private set; }

        public static KeyBindingManager KeyBindings { get; private set; } = new KeyBindingManager();

        public InputHandler(Game game) : base(game) { }
        
        public override void Initialize()
        {
            KeyBindings.LoadBindingsJSON();
            KeyBindings.UpdateBindings();

            base.Initialize();
        }

        public static bool IsPressed(Keys key)
        {
            if (KeyboardState.IsKeyDown(key) && !LastKeyboardState.IsKeyDown(key)) return true;
            return false;
        }
        
        public static bool IsHeld(Keys key)
        {
            if (KeyboardState.IsKeyDown(key)) return true;
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            foreach(var pressedKey in KeyboardState.GetPressedKeys())
            {
                var keyBinding = KeyBindings.GetKeyBinding(pressedKey);
                keyBinding?.Commands.ForEach(command => command.Execute());
            }

            MoveVector = MoveDirection.KeyboardDirection(new DirectionalKeys
            {
                Down = Keys.S,
                Up = Keys.W,
                Left = Keys.A,
                Right = Keys.D
            });

            base.Update(gameTime);
        }

    }
}
