using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.rooms
{
    public interface IRoom
    {
    public List<Object> roomMap();
    public void Draw();
        
    }
}