using Ascent.Core;
using Ascent.Core.Systems.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Ascent.Content.Particles
{
    public class CUBE : Particle
    {
        Vector3[] vertices = new Vector3[8] 
        {
            new Vector3(1, 1, 1), 
            new Vector3(-1, 1, 1), 
            new Vector3(1, -1, 1), 
            new Vector3(-1, -1, 1), 
            new Vector3(1, 1, -1), 
            new Vector3(-1, 1, -1), 
            new Vector3(1, -1, -1), 
            new Vector3(-1, -1, -1) 
        };

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            for(int i = 0; i < vertices?.Length; i++)
            {
                Vector3 Vertex = vertices[i] * 100;

                float Parallax = ModMath.ZToParallax(Vertex.Z);

                spriteBatch.Draw
                    (
                        texture.Value,
                        drawPosition + DrawPosition(Vertex, Parallax) - Main.screenPosition,
                        frame,
                        Color.White * Opacity,
                        (float)rotation,
                        frame.Size() / 2,
                        scale * Parallax * Parallax,
                        SpriteEffects.None,
                        0f
                    );
            }

            return true;
        }

        public Vector2 DrawPosition(Vector3 position, float parallax)
        {
            Vector2 pos2D = new Vector2(position.X, position.Y) - new Vector2(texture.Width() / 2, texture.Height() / 2);

            if (parallax < 0f)
            {
                parallax = 0f;
            }

            float scale3D = parallax * parallax;

            Vector2 playerPos = Main.LocalPlayer.MountedCenter;
            Vector2 offset = (playerPos + pos2D) * -(scale3D / 2 - 0.5f);

            return offset;
        }
    }
}
