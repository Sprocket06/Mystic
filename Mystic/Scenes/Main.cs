using Chroma.Graphics;
using Chroma.Input;
using Mystic.Entities;
using System.Numerics;

namespace Mystic.Scenes
{
    class Main : Scene
    {
        public Player player;
        public Main()
        {
            player = new Player(new Vector2(100, 100));
        }

        public override void Draw(RenderContext ctx)
        {
            player.Draw(ctx);
            ctx.DrawString(string.Format("Player position is {0}\nTime passed: {1}",player.Position, player.timePassed), new Vector2(0, 0));
        }
        public override void Update(float delta)
        {
            player.Update(delta);
        }
        public override void MousePressed(MouseButtonEventArgs e)
        {
            player.MoveTo(e.Position);
        }
    }
}
