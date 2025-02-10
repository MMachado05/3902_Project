using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _commands;
        private Game1 _game;


        public KeyboardController(Game1 game)
        {
            _game = game;
            _commands = new Dictionary<Keys, ICommand>();
            _commands.Add(Keys.W, new MoveCommand(_game, "Up"));
            _commands.Add(Keys.A, new MoveCommand(_game, "Left"));
            _commands.Add(Keys.S, new MoveCommand(_game, "Down"));
            _commands.Add(Keys.D, new MoveCommand(_game, "Right"));
            
            _commands.Add(Keys.Z, new AttackCommand(_game));
            _commands.Add(Keys.N, new AttackCommand(_game));


            //_commands.Add(Keys.D0, new QuitCommand(game));
            //_commands.Add(Keys.D1, new SetSpritesCommand(game, Game1.LuigiSpriteNames.Static));
            //_commands.Add(Keys.D2, new SetSpritesCommand(game, Game1.LuigiSpriteNames.Animated));
            //_commands.Add(Keys.D3, new SetSpritesCommand(game, Game1.LuigiSpriteNames.MovingStatic));
            //_commands.Add(Keys.D4, new SetSpritesCommand(game, Game1.LuigiSpriteNames.MovingAnimated));
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] currentlyPressed = state.GetPressedKeys();

            foreach (var key in currentlyPressed)
            {
                if (_commands.ContainsKey(key))
                {
                    _commands.GetValueOrDefault(key).Execute(); // Maybe: _commands[key].Execute();
                }

            }
        }
    }
}
