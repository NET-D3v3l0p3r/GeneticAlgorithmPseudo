using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvolutionNN_Jump.World
{
    public class WorldManager
    {
        public static int Width, Height;

        public Texture2D Map { get; private set; }
        public int[] Map2D { get; private set; }

        public List<IObject> GameObjects { get; private set; }

        public static Texture2D _vertexColor;

        public WorldManager(Texture2D map)
        {
            Width = Height = 15;

            Map = map;
            GameObjects= new List<IObject>();

            _vertexColor = new Texture2D(Game1.Device, 1, 1);
            _vertexColor.SetData<Color>(new Color[] { new Color(255, 255, 255, 255) });
        }

        public void LoadMap()
        {
            int w = Map.Width;
            int h = Map.Height;

            Map2D = new int[w * h];

            Color[] _data = new Color[w * h];
            Map.GetData<Color>(_data);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int index = x + y * w;
                    Color __pixel = _data[index];

                    if (__pixel == new Color(0, 0, 0, 255))
                    {
                        GameObjects.Add(new Obstacle(new Vector3(x, y, 0), Color.Black));
                        Map2D[index] = 1;
                    }
                    else if (__pixel == new Color(0, 255, 0, 255))
                    {
                        GameObjects.Add(new SafeZone(new Vector3(x, y, 0), Color.Green));
                        Map2D[index] = 2;
                    }
                    else if (__pixel == new Color(255, 0, 0, 255))
                    {
                        GameObjects.Add(new JumpObstacle(new Vector3(x, y, 0), Color.Red));
                        Map2D[index] = -1;
                    }
                    else if (__pixel == new Color(255, 255, 0, 255))
                    {
                        GameObjects.Add(new Indicator(new Vector3(x, y, 0), Color.Yellow));
                        Map2D[index] = 3;
                    }
                }
            }

        }

        public void Render(SpriteBatch sbatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                IObject go = GameObjects[i];
                sbatch.Draw(_vertexColor, go.Rectangle, go.Texture);
            }

        }

    }
}
