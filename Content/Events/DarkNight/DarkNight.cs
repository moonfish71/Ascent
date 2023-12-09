using Ascent.Core;
using Ascent.Core.ModWorlds;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace Ascent.Content.Events.DarkNight
{
    public class DarkNight
    {
        public static void StartEvent()
        {
            EventWorld.DarkNightActive = true;
        }

        public static void UpdateEvent()
        {
            Lighting.GlobalBrightness = UpdatedLighting();

            if(Main.dayTime)
            {
                if (EventWorld.StarryNight)
                {
                    Warning(true);
                }

                EventWorld.DarkNightActive = false;
                EventWorld.StarryNight = false;
            }
        }

        public static float UpdatedLighting()
        {
            float brightness;

            if (Main.time < Main.nightLength / 2)
            {
                brightness = MathHelper.Lerp(1f, .8f, (float)Math.Clamp(Main.time / ModMath.SecondsToTicks(150), 0, 1));
            }
            else
            {
                brightness = MathHelper.Lerp(1f, .8f, (float)Math.Clamp((Main.nightLength - Main.time) / ModMath.SecondsToTicks(150), 0, 1));
            }

            return brightness;
        }

        public static void Warning(bool Ending = false)
        {
            string text = "The stars grow bright";

            if (Ending)
            {
                text = "The starlight wanes";
            }

            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(text, new Color(246, 121, 195));
            }
            if(Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 246, 121, 195, 0, 0, 0);
            }
        }
    }
}
