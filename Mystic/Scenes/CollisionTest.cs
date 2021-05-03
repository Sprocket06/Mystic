using System.Numerics;
using Chroma.Graphics;
using static Mystic.cute_c2;
using Chroma.Input;
using Chroma.Diagnostics.Logging;
using Mystic.Physics;

namespace Mystic.Scenes
{
    class CollisionTest : Scene
    {
        private RectangleCollider testBoxOne;
        private RectangleCollider testBoxTwo;

        public CollisionTest()
        {

            testBoxOne = new RectangleCollider(new Vector2(100,100), new Vector2(50,50));
            testBoxTwo = new RectangleCollider(new Vector2(200,150), new Vector2(50,50));

            testBoxOne.Collision += OnCollision;
        }

        public override void Draw(RenderContext ctx)
        {
            bool collisionTest = testBoxOne.IsIntersecting(testBoxTwo);
            Color boxColor;
            boxColor = collisionTest ? Color.Red : Color.White;
            ctx.Rectangle(ShapeMode.Fill, testBoxOne.Position, 50f, 50f, boxColor);
            ctx.Rectangle(ShapeMode.Fill, testBoxTwo.Position, 50f, 50f, boxColor);
        }

        public override void Update(float delta)
        {
            Vector2 mousePos = Mouse.GetPosition();
            testBoxOne.Move(mousePos);
        }

        private void OnCollision(object collider, CollisionEventArgs e)
        {
            GameCore.Log.Info("Collision!");
        }
    }
}
