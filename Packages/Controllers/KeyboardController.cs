using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands;
using Project.Commands.CommandClasses;
using Project.Enemies;

namespace Project
{
    public class KeyboardController : IController
    {
        // ---- test ---- //
        private Dictionary<Keys, ICommand> _movementCommands;
        private Keys[] _previouslyPressed = new Keys[0];
        // ---- test ---- //

        private Dictionary<Keys, ICommand> _commands;

        private Player _player;
        private Game1 _game;
        private KeyboardState previousKeyboardState;

        public KeyboardController(Player player, Game1 game1, SolidBlockManager blockManager, EnemyManager enemyManager)
        {
            _player = player;
            _game = game1;

            _movementCommands = new Dictionary<Keys, ICommand>();
            _movementCommands.Add(Keys.W, new MoveCommand(_player, "Up"));
            _movementCommands.Add(Keys.A, new MoveCommand(_player, "Left"));
            _movementCommands.Add(Keys.S, new MoveCommand(_player, "Down"));
            _movementCommands.Add(Keys.D, new MoveCommand(_player, "Right"));

            _movementCommands.Add(Keys.Up, new MoveCommand(_player, "Up"));
            _movementCommands.Add(Keys.Left, new MoveCommand(_player, "Left"));
            _movementCommands.Add(Keys.Down, new MoveCommand(_player, "Down"));
            _movementCommands.Add(Keys.Right, new MoveCommand(_player, "Right"));

            _movementCommands.Add(Keys.Z, new AttackCommand(_player));
            _movementCommands.Add(Keys.N, new AttackCommand(_player));

            _movementCommands.Add(Keys.Q, new QuitCommand(_game));

            _commands = new Dictionary<Keys, ICommand>();
            _commands.Add(Keys.E, new DamageCommand(_player));
            _commands.Add(Keys.R, new RestartGameCommand(_game));
            _commands.Add(Keys.T, new NextBlockCommand(blockManager));
            _commands.Add(Keys.Y, new PreviousBlockCommand(blockManager));
            _commands.Add(Keys.O, new CommandPreviousEnemy(_game, enemyManager));
            _commands.Add(Keys.P, new CommandNextEnemy(_game, enemyManager));






        }

        public void Update()
        {

            KeyboardState state = Keyboard.GetState();
            Keys[] currentlyPressed = state.GetPressedKeys();

            foreach (var key in currentlyPressed)
            {
                if (_movementCommands.ContainsKey(key))
                {
                    _movementCommands[key].Execute(); // Was: _commands.GetValueOrDefault(key).Execute()
                }

            }

            // For _commands
            foreach (var key in currentlyPressed)
            {
                if (_commands.ContainsKey(key) && !(_previouslyPressed.Contains(key)))
                {
                    _commands.GetValueOrDefault(key).Execute();
                }
            }

            _previouslyPressed = currentlyPressed;
        }
    }
}
