﻿using static Ascent.Core.QuickDirectory;
using Ascent.Core.Systems.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ascent.Content.NPCs.Templates;
using Terraria.ID;
using Ascent.Core;
using Mono.Cecil;
using Terraria.Graphics.Renderers;

namespace Ascent.Content.NPCs.Bosses.Aster
{
    public partial class AsterBoss : AscentNPC
    {
        Particle spr;

        #region Textures

        public Texture2D speedTex = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/Projectile_" + ProjectileID.StarWrath);

        public Texture2D glowTex = (Texture2D)ModContent.Request<Texture2D>(BossTex + "Aster/AsterGlowmask");

        public Texture2D eyeTex = (Texture2D)ModContent.Request<Texture2D>(BossTex + "Aster/AsterEye");

        #endregion

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void ModifyTypeName(ref string typeName)
        {
            if(Z > 50 || Z < -50)
            {
                typeName = null;
                NPC.dontTakeDamage = true;
            }
            else
            {
                typeName = "Fell Aster";
                NPC.dontTakeDamage = false;
            }
        }
    }

    public class AsterSprite : Particle
    {
        public override string TexturePath => BossTex + "Aster/Aster";

        public NPC parent;
        public NPC oldParent;

        public float timer = 0;
        public float[] animTimer = new float[4];

        public override void Update()
        {
            if (parent != null && Main.npc.Contains(parent))
            {
                if (parent.ModNPC is AsterBoss aster)
                {
                    TimeLeft = 10;

                    position.X = parent.Center.X;
                    position.Y = parent.Center.Y;
                    position.Z = aster.Z;

                    velocity.X = parent.velocity.X;
                    velocity.Y = parent.velocity.Y;

                    rotation += velocity.X / 30;

                    if(Math.Abs(rotation) > Math.Tau / 5)
                    {
                        rotation = 0;
                    }
                }
                else
                {
                    TimeLeft = 0;
                }

                if (oldParent != parent)
                {
                    TimeLeft = 0;
                }

                if (!Main.npc.Contains(parent))
                {
                    TimeLeft = 0;
                }
            }
            else
            {
                TimeLeft = 0;
            }

            oldParent = parent;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            #region Trail

            if (parent.ModNPC is AsterBoss aster)
            {

                Vector2 flatVel = new Vector2(velocity.X, velocity.Y);

                float speedScale = (float)(2 / (1 + Math.Pow(Math.E, -flatVel.Length() / 4)) - 1);

                animTimer[0]++;

                if (animTimer[0] / 7 > MathHelper.TwoPi)
                {
                    animTimer[0] = 0;
                }


                Rectangle speedSource = new Rectangle(0, 0, (int)aster.speedTex.Size().X, (int)aster.speedTex.Size().Y);

                Vector2 drawPosition2 = drawPosition + new Vector2(0, 150 * scale * speedScale).RotatedBy(flatVel.ToRotation() + (MathHelper.PiOver2));

                for (int i = 0; i < 3; i++)
                {
                    spriteBatch.Draw
                        (
                            aster.speedTex,
                            drawPosition2 - Main.screenPosition + new Vector2(20 * scale * speedScale, 0).RotatedBy((animTimer[0] / 7) + (MathHelper.TwoPi / 3 * i)),
                            speedSource,
                            Color.Magenta * (parent.Opacity / 20 * (flatVel.Length() / 10)),
                            flatVel.ToRotation() + (MathHelper.PiOver2 * 3),
                            aster.speedTex.Size() / 2,
                            scale * 4f * speedScale,
                            SpriteEffects.None,
                            0f
                        );
                }

                spriteBatch.Draw
                    (
                        aster.speedTex,
                        drawPosition2 - Main.screenPosition - new Vector2(0, 50 * scale * speedScale).RotatedBy(flatVel.ToRotation() + (MathHelper.PiOver2)),
                        speedSource,
                        Color.White * (parent.Opacity / 16 * (flatVel.Length() / 2)),
                        flatVel.ToRotation() + (MathHelper.PiOver2 * 3),
                        aster.speedTex.Size() / 2,
                        (float)(scale * 2f * speedScale + (0.1 * Math.Sin(animTimer[0] / 7 * MathHelper.PiOver2))),
                        SpriteEffects.None,
                        0f
                    );
            }
            #endregion

            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (parent.ModNPC is AsterBoss aster)
            {
                //Glowmask
                Main.spriteBatch.Draw
                    (
                        aster.glowTex,
                        drawPosition - Main.screenPosition,
                        frame,
                        Color.White,
                        (float)rotation,
                        frame.Size() / 2,
                        (float)(scale * Math.Pow(ModMath.ZToParallax(position.Z), 2)),
                        SpriteEffects.None,
                        0f
                    );

                #region Eye

                Vector2 delta = ModMath.Delta(drawPosition, Main.player[parent.target].Center);

                Vector2 EyeOffset = 4 * Vector2.Normalize(delta) * Math.Clamp(delta.Length() / 1000f, 0, 1);

                spriteBatch.Draw
                    (
                        aster.eyeTex,
                        drawPosition + EyeOffset - Main.screenPosition,
                        frame,
                        Color.White,
                        0f,
                        frame.Size() / 2,
                        (float)(scale * Math.Pow(ModMath.ZToParallax(position.Z), 2)),
                        SpriteEffects.None,
                        0f
                    );

                #endregion
            }
        }
    }
}
