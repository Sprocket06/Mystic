using System.Numerics;
using Chroma.Graphics;
using static Mystic.cute_c2;
using Chroma.Input;
using Mystic.Physics;
using System.Collections.Generic;

namespace Mystic.Scenes
{
    class CollisionTest : Scene
    {
        private RectangleCollider testBoxOne;
        private RectangleCollider testBoxTwo;

        public CollisionTest()
        {

            testBoxOne = new RectangleCollider(new Vector2(100, 100), new Vector2(50, 50));
            testBoxTwo = new RectangleCollider(new Vector2(125, 125), new Vector2(50, 50));

            testBoxOne.Collision += OnCollision;
        }

        public override void Draw(RenderContext ctx)
        {
            var collisionInfo = new Manifold();

            bool collisionTest = testBoxOne.IsIntersecting(testBoxTwo, ref collisionInfo);
            Color boxColor;
            boxColor = collisionTest ? Color.Red : Color.White;
            ctx.Rectangle(ShapeMode.Stroke, testBoxOne.Position, 50f, 50f, boxColor);
            ctx.Rectangle(ShapeMode.Stroke, testBoxTwo.Position, 50f, 50f, boxColor);

            if (collisionTest)
            {
                ctx.Line(collisionInfo.contact_points[0], collisionInfo.contact_points[0] + (-collisionInfo.normal * collisionInfo.depths[0]), Color.Blue);
            }
        }

        public override void Update(float delta)
        {
            Vector2 mousePos = Mouse.GetPosition();
            testBoxOne.Move(mousePos);
        }

        private void OnCollision(object ColliderObject, CollisionEventArgs e)
        {
            Collider collider = (Collider)ColliderObject;
            foreach(Collision c in e.collisions)
            {
                collider.Move(collider.Position + (-c.collisionInfo.normal * c.collisionInfo.depths[0]));
            }
        }
    }
}
