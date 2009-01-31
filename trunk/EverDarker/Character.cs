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
        }
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public Texture2D texture { get; set; }
    }
}