using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chroma;
using Chroma.Graphics;
using System.Drawing;
using EasingSharp;
using Color = Chroma.Graphics.Color;

namespace Mystic.UI
{
    class StatusBar
    {
        public Vector2 Position;
        public int MaxHealth;
        public int Health;
        public int MaxMana;
        public int Mana;

        private Texture statusBarBase;
        private Texture statusBarRed;
        private Texture statusBarBlue;
        private Rectangle healthScissor;
        private Rectangle manaScissor;
        private int blueMax;
        private int redMax;
        public StatusBar(Vector2 pos)
        {
            Position = pos;
            MaxHealth = 500;
            Health = 500;
            Mana = 500;
            MaxMana = 500;
            statusBarBase = GameCore.Content.Load<Texture>("sprites/statusEmpty.png");
            statusBarRed = GameCore.Content.Load<Texture>("sprites/statusRed.png");
            statusBarBlue = GameCore.Content.Load<Texture>("sprites/statusBlue.png");
            redMax = statusBarBase.Width - 49;  //weird offsets here avoid weird offsets in the percentage math
            blueMax = statusBarBase.Width - 50;
            healthScissor = new Rectangle((int)pos.X + 40, (int)pos.Y, redMax, statusBarBase.Height);
            manaScissor = new Rectangle((int)pos.X + 39, (int)pos.Y, blueMax, statusBarBase.Height); 
        }

        public void Draw(RenderContext ctx)
        {
            ctx.DrawTexture(statusBarBase, Position, new Vector2(1), new Vector2(0,0), 0);
            decimal hpPercent = Health / (decimal)MaxHealth;
            healthScissor.Width = (int)(hpPercent * redMax);
            RenderSettings.Scissor = healthScissor;
            ctx.DrawTexture(statusBarRed, Position, new Vector2(1), new Vector2(0, 0), 0);
            decimal mpPercent = Mana / (decimal)MaxMana;
            manaScissor.Width = (int)(mpPercent * blueMax);
            RenderSettings.Scissor = manaScissor;
            ctx.DrawTexture(statusBarBlue, Position, new Vector2(1), new Vector2(0, 0), 0);
            RenderSettings.Scissor = Rectangle.Empty;
        }

        public void setHP(int newHP)
        {
            Easing.easeOutCirc(p => Health = (int)p, Health, newHP, 1000);
        }

    }
}
