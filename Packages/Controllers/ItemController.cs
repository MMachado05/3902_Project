using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Project.Commands.CommandClasses;
using Project.Commands.PlayerCommands;
using Project.Commands;
using Project.Items;

namespace Project.Controllers
{
    public class ItemController : IController
    {

        private Keys[] _previouslyPressed = new Keys[0];
        // ---- test ---- //

        private Dictionary<Keys, ICommand> _commands;

        private Game1 _game;
        private ItemManager _itemManager;

        // TODO: Get rid of this class entirely; it should all be taken care of by the
        //  KeyboardController, based on this current implementation, anyways.

        public ItemController(ItemManager itemManager, Game1 game1)
        {

            _game = game1;
            _itemManager = itemManager;
            _commands = new Dictionary<Keys, ICommand>
            {
                { Keys.D1, new InventoryCommand(_itemManager, 0) },
                { Keys.D2, new InventoryCommand(_itemManager, 1) },
                { Keys.D3, new InventoryCommand(_itemManager, 2) },
                { Keys.Z, new ProjectileCommand(_itemManager) }
            };
        }

        public void Update()
        {

            KeyboardState state = Keyboard.GetState();
            Keys[] currentlyPressed = state.GetPressedKeys();

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
