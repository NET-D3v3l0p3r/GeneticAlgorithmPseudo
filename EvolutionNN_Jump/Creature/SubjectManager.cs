using EvolutionNN_Jump.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.Creature
{
    public class SubjectManager
    {
        public List<Subject> Subjects { get; set; }
        public WorldManager WorldManager { get; set; }

        public SubjectManager()
        {
            Subjects = new List<Subject>();
        }

        public void AddSubject(Subject subject)
        {
            if (Subjects.Count > 25)
                return;

            Subjects.Add(subject);
        }

        public void RemoveSubjec(Subject subject)
        {
            Subjects.Remove(subject);
        }

        public void Update()
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Subjects[i].Update();
            }

            if(Subjects.Count == 0)
                AddSubject(new Subject(Color.Blue)
            {
                WorldManager = WorldManager,
                SubjectManager = this
            });

        }

        public void Render(SpriteBatch sBatch)
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Subjects[i].Render(sBatch);
            }

        }
    }
}
