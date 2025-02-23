﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Project.Commands;
using Project.Packages.Items;

namespace Project
{
    public class ItemController : IController
    {

        private Keys[] _previouslyPressed = new Keys[0];
        // ---- test ---- //

        private Dictionary<Keys, ICommand> _commands;

        private Game1 _game;
        private ItemManager _itemManager;


        public ItemController(ItemManager itemManager, Game1 game1)
        {
            
            _game = game1;
            _itemManager = itemManager;
            _commands = new Dictionary<Keys, ICommand>();
            _commands.Add(Keys.D1, new InventoryCommand(_itemManager, "1"));
            _commands.Add(Keys.D2, new InventoryCommand(_itemManager, "2"));
            _commands.Add(Keys.D3, new InventoryCommand(_itemManager, "3"));
            _commands.Add(Keys.U, new InventoryCommand(_itemManager, "Next"));
            _commands.Add(Keys.I, new InventoryCommand(_itemManager, "Previous"));
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
