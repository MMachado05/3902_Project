﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Project.Commands;

namespace Project.Controllers
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _onPress;
        private Dictionary<Keys, ICommand> _onRelease;
        private Keys[] _stillPressed;
        private ICommand _defaultCommand;

        public KeyboardController()
        {
            this._onPress = new Dictionary<Keys, ICommand>();
            this._onRelease = new Dictionary<Keys, ICommand>();
            this._stillPressed = new Keys[0];
        }

        public void RegisterOnPress(Keys key, ICommand command)
        {
            this._onPress.Add(key, command);
        }

        public void RegisterOnRelease(Keys key, ICommand command)
        {
            this._onRelease.Add(key, command);
        }

        public ICommand DefaultCommand
        {
            set { this._defaultCommand = value; }
        }

        public void Update()
        {
            IEnumerable<Keys> justReleased = new Keys[0];

            Keys[] currentlyPressed = Keyboard.GetState().GetPressedKeys();

            justReleased = _stillPressed.Except<Keys>(currentlyPressed);
            foreach (Keys key in justReleased)
            {
                if (this._onRelease.ContainsKey(key)) _onRelease[key].Execute();
            }

            foreach (Keys key in currentlyPressed)
            {
                if (this._onPress.ContainsKey(key))
                {
                    if (!this._stillPressed.Contains(key))
                        _onPress[key].Execute();
                }
            }

            // Keep an active log of currently pressed keys so that "holding down" a key
            //  is ignored.
            _stillPressed = currentlyPressed.Union<Keys>(_stillPressed).ToArray();
            _stillPressed = currentlyPressed.Intersect<Keys>(_stillPressed).ToArray();
        }
    }
}
