using System;
using System.Collections.Generic;
using Hel.Engine.Commander.Model;
using Microsoft.Xna.Framework.Input;

namespace Hel.Engine.Input.Model
{
    /// <summary>
    /// A single keybinding. A keybinding is the concept or a key or series of keys that result in one or more events to occur.
    /// All keys in the hashset must be pressed to activate the keybinding. All commands in the list will be executed when the
    /// keybinding is activated.
    /// </summary>
    [Serializable]
    public class KeyBinding : IEquatable<KeyBinding>
    {
        /// <summary>
        /// The key combination that must be pressed
        /// </summary>
        public HashSet<Keys> Keys { get; }
        
        /// <summary>
        /// The commands which will run as as result of the keys being pressed
        /// </summary>
        public List<ICommand> Commands { get; set; }

        public KeyBinding(HashSet<Keys> keys, List<ICommand> commands)
        {
            Keys = keys;
            Commands = commands;
        }

        public override bool Equals(object other)
        {
            if (!(other is KeyBinding)) return false;
            var obj = (KeyBinding)other;
            return Keys.SetEquals(obj.Keys);
        }

        public bool Equals(KeyBinding other) =>
            other != null && Keys.SetEquals(other.Keys);

        public override int GetHashCode() =>
            Keys.GetHashCode();
    }
}
