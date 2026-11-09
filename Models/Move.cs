using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikovaKostkaSimulace.Models
{
    public class Move
    {
        public FaceType Face { get; set; }
        public bool IsClockwise { get; set; }

        public Move(FaceType face, bool isClockwise = true)
        {
            Face = face;
            IsClockwise = isClockwise;
        }
        
        public Move GetInverseMove()
        {
            return new Move(Face, !IsClockwise);
        }

        public override string ToString()
        {
            string prefix = Face switch
            {
                FaceType.Up => "U",
                FaceType.Down => "D",
                FaceType.Front => "F",
                FaceType.Back => "B",
                FaceType.Left => "L",
                FaceType.Right => "R",
                _ => "?"
            };

            return IsClockwise ? prefix : prefix + "'";
        }
    }
    
}
