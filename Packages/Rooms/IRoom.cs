using System;
using System.Collections.Generic;

namespace Project.rooms
{
    public interface IRoom
    {
    public List<Object> roomMap();
     public int getPlayerIndex();
    public List<object> BlocksList{get;set;}

    }
}
