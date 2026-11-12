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
            string prefix;
            switch (Face)
            {
                case FaceType.Up:
                    prefix = "U";
                    break;
                case FaceType.Down:
                    prefix = "D";
                    break;
                case FaceType.Front:
                    prefix = "F";
                    break;
                case FaceType.Back:
                    prefix = "B";
                    break;
                case FaceType.Left:
                    prefix = "L";
                    break;
                case FaceType.Right:
                    prefix = "R";
                    break;
                default:
                    prefix = "?";
                    break;
            }

            return IsClockwise ? prefix : prefix + "'";
        }
    }
}