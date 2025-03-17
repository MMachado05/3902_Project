using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace Project.rooms
{
    public class RoomParser
    {         
         public Dictionary <Vector2,String> loadRoom(string filepath){
            Dictionary<Vector2,String> result = new ();
            StreamReader reader = new (filepath);
            string line;
            int y=0;
            while((line = reader.ReadLine())!=null){
                string [] items = line.Split(',');
                for(int x=0;x<items.Length;x++){
                    switch(items[x]){
                        case "bl":
                        result[new Vector2(x,y)] = items[x];
                        break;
                        case "ob":
                        result[new Vector2(x,y)] = items[x];
                        break;
                        case "dr":
                        result[new Vector2(x,y)] = items[x];
                        break;
                        case "pl":
                        result[new Vector2(x,y)] = items[x];
                        break;
                        case "en":
                        result[new Vector2(x,y)] = items[x];
                        break;

                        default:
                        break;
                    }
                }
                y++;

            }
            
            
            return result;
        }

        
    }
}
