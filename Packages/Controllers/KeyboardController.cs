using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _commands;
        private IEnumerable<Keys> _stillPressed;

        public KeyboardController()
        {
            this._commands = new Dictionary<Keys, ICommand>();
            this._stillPressed = new Keys[0];
        }

        public void RegisterKey(Keys key, ICommand command)
        {
            this._commands.Add(key, command);
        }

        public void Update()
        {
            Keys[] currentlyPressed = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in currentlyPressed)
            {
                /*System.Console.Write(key.ToString());*/
                if (_commands.ContainsKey(key) && !_stillPressed.Contains<Keys>(key))
                {
                    _commands[key].Execute();
                }
            }

            _stillPressed = currentlyPressed.Union<Keys>(_stillPressed);
            _stillPressed = currentlyPressed.Intersect<Keys>(_stillPressed);
        }

        /// <summary>Used for debugging, leaving in just in case.</summary>
        private void printKeys(IEnumerable<Keys> enumerable)
        {
            foreach (Keys key in enumerable)
                System.Console.Write(key.ToString());
            System.Console.WriteLine();
        }
    }
}
