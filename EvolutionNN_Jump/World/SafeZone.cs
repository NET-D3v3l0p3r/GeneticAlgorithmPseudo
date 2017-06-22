using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.World
{
    public class SafeZone : IObject
    {
        public Color Texture { get; set; }
        public Rectangle Rectangle { get; set; }


        public SafeZone(Vector3 pos, Color color)
        {
            Texture = color;
            pos = pos * new Vector3(WorldManager.Width, WorldManager.Height, 0);
            Rectangle = new Rectangle(new Point((int)pos.X, (int)pos.Y), new Point(WorldManager.Width, WorldManager.Height));
        }

    }
}