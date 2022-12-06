using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Engine.Models.Character
{
    public class CharacterMoveDirection
    {
        public float Horizontal { get; set; }
        public float Vertical { get; set; }
        public float Speed { get; set; }

        public CharacterMoveDirection()
        { 
            Horizontal = 0;
            Vertical = 0;
            Speed = 0;
        }
    }
}
