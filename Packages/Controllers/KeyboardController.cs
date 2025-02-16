using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _commands;
        private Player _player;
        private Game1 _game;


        public KeyboardController(Player player, Game1 game1)
        {
            
            _player = player;
            _game = game1;

            _commands = new Dictionary<Keys, ICommand>();
            _commands.Add(Keys.W, new MoveCommand(_player, "Up"));
            _commands.Add(Keys.A, new MoveCommand(_player, "Left"));
            _commands.Add(Keys.S, new MoveCommand(_player, "Down"));
            _commands.Add(Keys.D, new MoveCommand(_player, "Right"));

            _commands.Add(Keys.Up, new MoveCommand(_player, "Up"));
            _commands.Add(Keys.Left, new MoveCommand(_player, "Left"));
            _commands.Add(Keys.Down, new MoveCommand(_player, "Down"));
            _commands.Add(Keys.Right, new MoveCommand(_player, "Right"));

            _commands.Add(Keys.Z, new AttackCommand(_player));
            _commands.Add(Keys.N, new AttackCommand(_player));

            _commands.Add(Keys.E, new DamageCommand(_player));

            _commands.Add(Keys.Q, new QuitCommand(_game));
        }

        public void Update()
        {

            KeyboardState state = Keyboard.GetState();
            Keys[] currentlyPressed = state.GetPressedKeys();

            Console.WriteLine("currentlyPressed" + currentlyPressed);

            foreach (var key in currentlyPressed)
            {
                if (_commands.ContainsKey(key))
                {
                    _commands[key].Execute(); // Was: _commands.GetValueOrDefault(key).Execute()
                }

            }
        }
    }
}
