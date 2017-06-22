using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.World
{
    public interface IObject
    {
        Color Texture { get; set; }
        Rectangle Rectangle { get; set; }
    }
}
