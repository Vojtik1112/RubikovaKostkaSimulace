using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikovaKostkaSimulace.Models
{
    public class Face
    {
        public FaceType Type { get; private set; }
        public Sticker[,] Grid { get; private set; }

        public Face(FaceType type, CubeColor initialColor)
        {
            Type = type;
            Grid = new Sticker[3, 3];
            Reset(initialColor);
        }

        public void Reset(CubeColor color)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Grid[row, col] = new Sticker(color);
                }
            }
        }

        public void RotateClockwise()
        {
            CubeColor[,] temp = new CubeColor[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    temp[col, 2 - row] = Grid[row, col].Color;
                }
            }

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Grid[row, col].Color = temp[row, col];
                }
            }
        }

        public bool IsFaceSolved()
        {
            CubeColor centerColor = Grid[1, 1].Color;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Grid[row, col].Color != centerColor)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
