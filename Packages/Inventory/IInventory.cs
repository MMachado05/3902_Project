using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Project.Items;

namespace Project.Inventory
{
    public interface IInventory
    {
        List<(IItem,int)> Items{get; set;}
        bool Add(IItem item);
        bool Remove(IItem item);
        IItem GetItem();
    }
}