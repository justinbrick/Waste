using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Waste
{

    public class Container : WasteItem
    {
        
        public List<WasteItem> Items { get; protected set; } // List of all the items currently in the container.

        public Container(int sizeX, int sizeY)  
        {
            Size = new(sizeX, sizeY);
            Items = new();
        }

        public bool CanAddItem(WasteItem item)
        {
            return false;
        }
    }
}
