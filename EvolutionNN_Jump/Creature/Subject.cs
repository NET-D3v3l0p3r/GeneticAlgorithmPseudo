using EvolutionNN_Jump.NN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EvolutionNN_Jump.World;

namespace EvolutionNN_Jump.Creature
{
    public class Subject
    {
        public SubjectManager SubjectManager { get; set; }
        public WorldManager WorldManager { get; set; }
        public NeuralNetwork NN = new NeuralNetwork();

        public int X { get; set; }
        public int Y { get; set; }

        public BoundingBox BoundingBox { get; set; }

        public Color Color { get; set; }
        public double Life { get; private set; }

        private bool isJumping;

        public Subject(Color color)
        {
            Y = 1;
            X = 5;

            jumpForbidden = true;

            Color = color;
            Life = 50 * 2;

            NN.CreateInputLayer(8, 1.0);
            NN.CreateHiddenLayer(15, 1.0);
            NN.CreateOutputLayer(3);

            NN.CreateMesh();

            CostTable.Initialize();
        }

        public Subject(Subject mother)
        {
            Y = 1;
            X = 5;

            jumpForbidden = true;

            Color = mother.Color * 0.1f;
            Life = 50 * 2;

            WorldManager = mother.WorldManager;
            SubjectManager = mother.SubjectManager;

            List<INeurone[]> layers = mother.NN.CloneLayers();


            NN.CreateInputLayer(8, 1.0);
            NN.CreateHiddenLayer(15, 1.0);
            NN.CreateOutputLayer(3);

            NN.CreateMesh(layers[0], layers[1]);
        }

        public void Perceive()
        {
            try
            {
                int down = WorldManager.Map2D[X + (Y + 2) * WorldManager.Map.Width];
                int up = WorldManager.Map2D[X + (Y - 1) * WorldManager.Map.Width];

                int left = WorldManager.Map2D[(X - 1) + (Y) * WorldManager.Map.Width];
                int right = WorldManager.Map2D[(X + 1) + (Y) * WorldManager.Map.Width];

                int downLeft = WorldManager.Map2D[(X - 1) + (Y + 2) * WorldManager.Map.Width];
                int downRight = WorldManager.Map2D[(X + 1) + (Y + 2) * WorldManager.Map.Width];

                int upLeft = WorldManager.Map2D[(X - 1) + (Y - 1) * WorldManager.Map.Width];
                int upRight = WorldManager.Map2D[(X + 1) + (Y - 1) * WorldManager.Map.Width];

                var outputs = NN.StimulateL0(new float[] { down, up, left, right, downLeft, downRight, upLeft, upRight });

                if (outputs[0] > .55)
                    Jump();
                if (outputs[1] > .55)
                    MoveLeft();
                if (outputs[2] > .55)
                    MoveRight();

                for (int i = 0; i < NN.L0.Length; i++)
                {
                    NN.L0[i].Value = 0;
                }
            }
            catch
            {
                SubjectManager.RemoveSubjec(this);
            }
        }

        bool jumpForbidden;

        public void Update()
        {
            int tile = WorldManager.Map2D[X + (Y + 2) * WorldManager.Map.Width];

            if (WorldManager.Map2D[X + (Y) * WorldManager.Map.Width] == -1)
                Life = 0;

            if (isJumping)
            {
                if (internalTimerUp++ < 0)
                    return;
                Y--;
                if (Y == 2)
                    isJumping = false;
                internalTimerUp = 0;
            }
            else
            {
                if (internalTimerUp++ < 0)
                    return;
                if (tile == 0)
                    Y++;
                else jumpForbidden = false;

                internalTimerUp = 0;
            }

            Perceive();

            Life += CostTable.Costs[tile];

            if (Life < 0)
                SubjectManager.RemoveSubjec(this);

            if (Life > 50*2)
                GiveBirth();
        }

        public void Jump()
        {
            if (jumpForbidden)
                return;

            isJumping = true;
            jumpForbidden = true;

            Life -= 5;

        }

        int internalTimerLeft = 0;
        int internalTimerRight = 0;
        int internalTimerUp = 0;

        public void MoveLeft()
        {
            if (internalTimerLeft++ < 0)
                return;
            if (X - 1 < 0)
                SubjectManager.RemoveSubjec(this);
            if (WorldManager.Map2D[(X - 1) + (Y) * WorldManager.Map.Width] == -1)
                SubjectManager.RemoveSubjec(this);
            X--;
            internalTimerLeft = 0;
            Life--;
        }
        public void MoveRight()
        {
            if (internalTimerRight++ < 0)
                return;
            if (X + 1 >= WorldManager.Map.Width)
                SubjectManager.RemoveSubjec(this);
            if (WorldManager.Map2D[(X + 1) + (Y) * WorldManager.Map.Width] == -1)
                SubjectManager.RemoveSubjec(this);

            X++;
            internalTimerRight = 0;
            Life--;
        }

        public void GiveBirth()
        {
            Subject child = new Subject(this);
            SubjectManager.AddSubject(child);
            Life = 0;
        }


        public void Render(SpriteBatch sbatch)
        {
            sbatch.Draw(WorldManager._vertexColor, new Rectangle(new Point(X * WorldManager.Width, Y * WorldManager.Height), new Point(15, 30)), Color.Blue);
        }
    }
}
