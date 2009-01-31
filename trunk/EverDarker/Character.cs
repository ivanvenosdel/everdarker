using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EverDarker
{
    class Character : Sprite
    {
        public void LoadContent(ContentManager theContentManager, string theAssetName, Viewport viewPort)
        {
            base.LoadContent(theContentManager, theAssetName);
            this.Position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
        }
    }
}
