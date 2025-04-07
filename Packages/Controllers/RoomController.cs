using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Commands;
using Project.Commands.RoomCommands;
using Project.Controllers;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Controllers
{
    public class RoomController : IController
    {
        IBlock doorBlock;
        List<ICommand> _commands;

        public RoomController()
        {
            _commands = new List<ICommand>();
            
        }

        public void AddRoomCommands(ICommand command)
        {
            this._commands.Add(command);
        }

        public void Update()
        {
            foreach(var c in _commands){
                c.Execute();
            }

        }
    }
}