using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikovaKostkaSimulace.Models
{

    public class Cube
    {
        public Dictionary<FaceType, Face> Faces { get; private set; }
        private readonly Random _random = new Random();

        public Cube()
        {
            Faces = new Dictionary<FaceType, Face>();
            Reset();
        }


        public void Reset()
        {
            Faces[FaceType.Up] = new Face(FaceType.Up, CubeColor.White);
            Faces[FaceType.Down] = new Face(FaceType.Down, CubeColor.Yellow);
            Faces[FaceType.Front] = new Face(FaceType.Front, CubeColor.Green);
            Faces[FaceType.Back] = new Face(FaceType.Back, CubeColor.Blue);
            Faces[FaceType.Left] = new Face(FaceType.Left, CubeColor.Orange);
            Faces[FaceType.Right] = new Face(FaceType.Right, CubeColor.Red);
        }

        public void ApplyMove(Move move)
        {
            if (move == null)
                throw new ArgumentNullException(nameof(move));

            int repetitions = move.IsClockwise ? 1 : 3;

            for (int i = 0; i < repetitions; i++)
            {
                RotateFaceClockwise(move.Face);
            }
        }

        private void RotateFaceClockwise(FaceType faceType)
        {
            Faces[faceType].RotateClockwise();

            switch (faceType)
            {
                case FaceType.Front:
                    RotateFrontEdgesCW();
                    break;
                case FaceType.Back:
                    RotateBackEdgesCW();
                    break;
                case FaceType.Up:
                    RotateUpEdgesCW();
                    break;
                case FaceType.Down:
                    RotateDownEdgesCW();
                    break;
                case FaceType.Left:
                    RotateLeftEdgesCW();
                    break;
                case FaceType.Right:
                    RotateRightEdgesCW();
                    break;
            }
        }

        private void RotateFrontEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Up].Grid[2, i].Color;
                Faces[FaceType.Up].Grid[2, i].Color = Faces[FaceType.Left].Grid[2 - i, 2].Color;
                Faces[FaceType.Left].Grid[2 - i, 2].Color = Faces[FaceType.Down].Grid[0, 2 - i].Color;
                Faces[FaceType.Down].Grid[0, 2 - i].Color = Faces[FaceType.Right].Grid[i, 0].Color;
                Faces[FaceType.Right].Grid[i, 0].Color = temp[i];
            }
        }

        private void RotateBackEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Up].Grid[0, i].Color;
                Faces[FaceType.Up].Grid[0, i].Color = Faces[FaceType.Right].Grid[i, 2].Color;
                Faces[FaceType.Right].Grid[i, 2].Color = Faces[FaceType.Down].Grid[2, 2 - i].Color;
                Faces[FaceType.Down].Grid[2, 2 - i].Color = Faces[FaceType.Left].Grid[2 - i, 0].Color;
                Faces[FaceType.Left].Grid[2 - i, 0].Color = temp[i];
            }
        }

        private void RotateUpEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Front].Grid[0, i].Color;
                Faces[FaceType.Front].Grid[0, i].Color = Faces[FaceType.Right].Grid[0, i].Color;
                Faces[FaceType.Right].Grid[0, i].Color = Faces[FaceType.Back].Grid[0, i].Color;
                Faces[FaceType.Back].Grid[0, i].Color = Faces[FaceType.Left].Grid[0, i].Color;
                Faces[FaceType.Left].Grid[0, i].Color = temp[i];
            }
        }

        private void RotateDownEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Front].Grid[2, i].Color;
                Faces[FaceType.Front].Grid[2, i].Color = Faces[FaceType.Left].Grid[2, i].Color;
                Faces[FaceType.Left].Grid[2, i].Color = Faces[FaceType.Back].Grid[2, i].Color;
                Faces[FaceType.Back].Grid[2, i].Color = Faces[FaceType.Right].Grid[2, i].Color;
                Faces[FaceType.Right].Grid[2, i].Color = temp[i];
            }
        }

        private void RotateLeftEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Up].Grid[i, 0].Color;
                Faces[FaceType.Up].Grid[i, 0].Color = Faces[FaceType.Back].Grid[2 - i, 2].Color;
                Faces[FaceType.Back].Grid[2 - i, 2].Color = Faces[FaceType.Down].Grid[i, 0].Color;
                Faces[FaceType.Down].Grid[i, 0].Color = Faces[FaceType.Front].Grid[i, 0].Color;
                Faces[FaceType.Front].Grid[i, 0].Color = temp[i];
            }
        }

        private void RotateRightEdgesCW()
        {
            CubeColor[] temp = new CubeColor[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = Faces[FaceType.Up].Grid[i, 2].Color;
                Faces[FaceType.Up].Grid[i, 2].Color = Faces[FaceType.Front].Grid[i, 2].Color;
                Faces[FaceType.Front].Grid[i, 2].Color = Faces[FaceType.Down].Grid[i, 2].Color;
                Faces[FaceType.Down].Grid[i, 2].Color = Faces[FaceType.Back].Grid[2 - i, 0].Color;
                Faces[FaceType.Back].Grid[2 - i, 0].Color = temp[i];
            }
        }

        public List<Move> Scramble(int moveCount = 20)
        {
            List<Move> scrambleMoves = new List<Move>();
            FaceType[] allFaces = (FaceType[])Enum.GetValues(typeof(FaceType));

            for (int i = 0; i < moveCount; i++)
            {
                FaceType randomFace = allFaces[_random.Next(allFaces.Length)];
                bool randomDirection = _random.Next(2) == 0;

                Move move = new Move(randomFace, randomDirection);
                ApplyMove(move);
                scrambleMoves.Add(move);
            }

            return scrambleMoves;
        }

        public bool IsSolved()
        {
            foreach (Face face in Faces.Values)
            {
                if (!face.IsFaceSolved())
                {
                    return false;
                }
            }
            return true;
        }
    }
}