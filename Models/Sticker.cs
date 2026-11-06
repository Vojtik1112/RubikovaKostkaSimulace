using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikovaKostkaSimulace.Models
{
    public class Sticker
    {
        public CubeColor Color { get; set; }

        public Sticker(CubeColor color)
        {
            Color = color;
        }
    }
}