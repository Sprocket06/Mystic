using System.Numerics;
using Chroma.Graphics;
using Chroma.Input;
using Mystic.Entities;
using Mystic.UI;


namespace Mystic.Scenes
{
    class Main : Scene
    {
        public Player player;
        public StatusBar sBar;
        public Main()
        {
            player = new Player(new Vector2(150, 150));
            sBar = new StatusBar(new Vector2(0,50));
            sBar.Health = 300;
            sBar.Mana = 450;
        }

        public override void Draw(RenderContext ctx)
        {
            player.Draw(ctx);
            ctx.DrawString(string.Format("Player position is {0}\nTime passed: {1}",player.Position, player.timePassed), new Vector2(0, 0));
            sBar.Draw(ctx);
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
