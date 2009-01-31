﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EverDarker
{
    class Character
    {
        public Character()
        {
            this.xPosition = 50;
            this.yPosition = 50;
            this.textures = new Dictionary<Orientations, string>();
            this.textures[Orientations.up] = "characterup.png";
            this.textures[Orientations.left] = "characterleft.png";
            this.textures[Orientations.right] = "characterright.png";
            this.textures[Orientations.down] = "characterdown.png";
        }
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public Dictionary<Orientations, string> textures { get; set; }
    }
}
enum Orientations
{
    up,
    down,
    left,
    right
}