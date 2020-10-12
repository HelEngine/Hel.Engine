using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hel.Engine.Commander;
using Hel.Engine.Commander.Model;
using Hel.Engine.Input.Model;
using Hel.Toolkit.Serializer;
using Microsoft.Xna.Framework.Input;

namespace Hel.Engine.Input
{
    /// <summary>
    /// The keybinding manager handles assigning, loading and saving keybindings. It is the only place where keybindings can
    /// be mutated or fetched.
    /// </summary>
    internal interface IKeyBindingManager
    {
        /// <summary>
        /// Uses the provided key to return the keybinding associated with a specific key.
        /// </summary>
        /// <param name="key">The key you're trying to get the action of.</param>
        KeyBinding GetKeyBinding(Keys key);
        /// <summary>
        /// Uses the provided key to return the keybinding associated with a specific key.
        /// </summary>
        /// <param name="key">The key you're trying to get the action of.</param>
        KeyBinding GetKeyBinding(KeyBinding key);
        /// <summary>
        /// Adds a keybinding to the bindings hashset. If key is already in use, replaces the associated action. 
        /// </summary>
        /// <param name="key">The key to bind</param>
        /// <param name="action">The action to bind to the key</param>
        void SetKeyBinding(KeyBinding binding);
        /// <summary>
        /// Adds multiple keybindings to the bindings hashset. If key is already in use, replaces the associated action. 
        /// </summary>
        /// <param name="key">The key to bind</param>
        /// <param name="action">The action to bind to the key</param>
        void SetKeyBindings(HashSet<KeyBinding> bindings);
        /// <summary>
        /// Removes a key from the hashset entirely. 
        /// </summary>
        /// <param name="key">Key to remove from the hashset.</param>
        void RemoveKey(Keys key);
        /// <summary>
        /// Removes a key from the hashset using a keybinding object
        /// </summary>
        /// <param name="key"></param>
        void RemoveKey(KeyBinding key);
        /// <summary>
        /// Saves temporary bindings. All binding changes are temporary until this method is called.
        /// </summary>
        void UpdateBindings();
        /// <summary>
        /// Reverts the temporary bindings to how they were prior to being changed.
        /// </summary>
        void UndoBindings();
        /// <summary>
        /// Saves bindings to a JSON file.
        /// </summary>
        void SaveBindingsJSON();
        /// <summary>
        /// Loads bindings from the JSON file. UpdateBindings() must be called to save the bindings.
        /// </summary>
        void LoadBindingsJSON();
    }
    public class KeyBindingManager : EventChange<HashSet<KeyBinding>>, IKeyBindingManager
    {
        /// <summary>
        /// Permanent key bindings used by Get methods
        /// </summary>
        private HashSet<KeyBinding> _bindings;
        /// <summary>
        /// Temporary key bindings used to safely modify bindings, and revert if needed. 
        /// </summary>
        private HashSet<KeyBinding> _tempBindings;

        public KeyBindingManager(HashSet<KeyBinding> _bindingSet)
        {
            _bindings = new HashSet<KeyBinding>();
            _tempBindings = new HashSet<KeyBinding>();

            foreach (var entry in _bindingSet)
            {
                _bindings.Add(entry);
                _tempBindings.Add(entry);
            }
        }

        public KeyBindingManager() : this(new HashSet<KeyBinding>()) {}

        public KeyBinding GetKeyBinding(Keys key) =>
            GetKeyBinding(new KeyBinding(new HashSet<Keys>() { key }, null));
        
        public HashSet<KeyBinding> GetAllBindings() => new HashSet<KeyBinding>(_bindings);
        
        public KeyBinding GetKeyBinding(KeyBinding key) => 
            _bindings.FirstOrDefault(binding => binding.Equals(key));

        public void RemoveKey(Keys key) =>
            RemoveKey(new KeyBinding(new HashSet<Keys>() { key }, null));

        public void RemoveKey(KeyBinding key) =>
            _tempBindings.Remove(key);

        public void SetKeyBinding(KeyBinding binding)
        {
            if (GetKeyBinding(binding) != null)
                _tempBindings.Remove(binding);
            _tempBindings.Add(binding);
        }

        public void SetKeyBindings(HashSet<KeyBinding> bindings)
        {
            foreach (var binding in bindings)
            {
                SetKeyBinding(binding);
            }
        }

        public void UpdateBindings()
        {
            _bindings = new HashSet<KeyBinding>(_tempBindings);
            OnChangeEvents(_bindings);
        }

        public void UndoBindings() =>
            _tempBindings = new HashSet<KeyBinding>(_bindings);

        public void SaveBindingsJSON()
        {
            var output = ByteSerializer.ObjectToByteArray(_bindings);//JsonConvert.SerializeObject(_bindings);
            File.WriteAllBytes($"{Hel.Engine.Engine.FileRoot}/Bindings", output);
        }
        
        public void LoadBindingsJSON()
        {
            string filePath = $"{Hel.Engine.Engine.FileRoot}/Bindings";
            
            if (!File.Exists(filePath))
            {
                SetKeyBindings(new HashSet<KeyBinding>()
                {
                    new KeyBinding(
                        new HashSet<Keys> {Keys.Enter},
                        new List<ICommand> {new ExitCommand()})
                });
                UpdateBindings();
                SaveBindingsJSON();
            }

            var input = File.ReadAllBytes(filePath);
            
            
            _tempBindings = (HashSet<KeyBinding>) ByteSerializer.ByteArrayToObject(input);//JsonConvert.DeserializeObject<HashSet<KeyBinding>>(input);
        }
    }
}
