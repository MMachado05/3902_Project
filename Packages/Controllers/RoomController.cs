using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Commands;
using Project.Controllers;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Controllers
{
    public class RoomController : IController
{
    IBlock doorBlock;
    List<ICommand> _commands;

     public void AddRoomCommands(ICommand command)
        {
            this._commands.Add( command);
        }

    public void Update()
    {
        if(doorBlock.SwitchRoom){

            _commands[0].Execute();
        }
    }
}
}