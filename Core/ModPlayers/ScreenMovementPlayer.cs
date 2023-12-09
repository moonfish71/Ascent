using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Ascent.Core.ModPlayers
{
    public class ScreenMovementPlayer : ModPlayer
    {
        public float ScreenShakeStrength;
        public Vector2 ScreenCenter;
        public Vector2 OldScreenCenter;

        public bool ScreenPosModified;

        public bool ScreenMoving;
        public float Counter;

        public override void Initialize()
        {
            ScreenShakeStrength = 0;
            ScreenCenter = Vector2.Zero;
            ScreenPosModified = false;

            ScreenMoving = false;
            Counter = 0;
        }

        public override void ResetEffects()
        {
            ScreenCenter = Vector2.Zero;
            ScreenMoving = true;

            if (!ScreenMoving)
            {
                Counter = 0;
            }
        }

        public override void ModifyScreenPosition()
        {
            if (ScreenShakeStrength > 0)
            {
                Main.screenPosition += Main.rand.NextVector2Circular(ScreenShakeStrength, ScreenShakeStrength);
                ScreenShakeStrength = Math.Clamp(ScreenShakeStrength, 0, 40);

                ScreenShakeStrength -= 1.95f;
            }

            OldScreenCenter = ScreenCenter;
        }
    }
}
