using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Project.Commands;

namespace Project.Controllers
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _commands;
        private Keys[] _stillPressed;
        private ICommand _defaultCommand;

        public KeyboardController()
        {
            this._commands = new Dictionary<Keys, ICommand>();
            this._stillPressed = new Keys[0];
        }

        public void RegisterKey(Keys key, ICommand command)
        {
            this._commands.Add(key, command);
        }

        public ICommand DefaultCommand
        {
            set { this._defaultCommand = value; }
        }

        public void Update()
        {
            Keys[] currentlyPressed = Keyboard.GetState().GetPressedKeys();

            if (currentlyPressed.Length == 0)
                this._defaultCommand.Execute();

            foreach (Keys key in currentlyPressed)
            {
                if (_commands.ContainsKey(key) && !_stillPressed.Contains<Keys>(key))
                {
                    _commands[key].Execute();
                }
            }

            // Keep an active log of currently pressed keys so that "holding down" a key
            //  is ignored.
            _stillPressed = currentlyPressed.Union<Keys>(_stillPressed).ToArray();
            _stillPressed = currentlyPressed.Intersect<Keys>(_stillPressed).ToArray();
        }
    }
}
