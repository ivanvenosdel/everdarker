using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverDarker
{
    class Character
    {
        public int xposition { get; set; }
        public int yposition { get; set; }
        public Orientations orientation { get; set; }
    }
}
enum Orientations
{
    up,
    down,
    left,
    right
}
