using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascent.Core
{
    public static class ModMath
    {
        /// <summary>
        /// Checks if a float "value" is a multiple of the float denominator.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="denomenator"></param>
        /// <returns></returns>
        public static bool IsMultipleOf(float value, float denomenator)
        {
            if(value / denomenator == Math.Round(value / denomenator))
            {
                return true;
            }

            return false;
        }

        public static int ToThePower(int Base, int power)
        {
            int mult = Base;

            for(int i = 0; i < power; i++)
            {
                mult *= Base;
            }

            return mult;
        }

        public static float ToThePower(float Base, float power)
        {
            float mult = Base;

            for (int i = 0; i < power; i++)
            {
                mult *= Base;
            }

            return mult;
        }

        public static double ToThePower(double Base, double power)
        {
            double mult = Base;

            for (int i = 0; i < power; i++)
            {
                mult *= Base;
            }

            return mult;
        }

        public static float TicksToSeconds(int ticks)
        {
            return ticks / 60;
        }

        public static int SecondsToTicks(float seconds)
        {
            return (int)(seconds * 60);
        }

        public static Vector2 Delta(Vector2 Start, Vector2 End)
        {
            return End - Start;
        }

        public static float ZToParallax(float Z)
        {
            return ModMath.ToThePower((Z + 5500) / 5500f, 2);
        }

        public static float ParallaxToZ(float parallax)
        {
            return (float)(Math.Sqrt(parallax) * 5500f - 5500);
        }

        public static float QuadEase(float easeValue)
        {
            float result = Math.Clamp(-4f * easeValue * (easeValue - 1), 0f, 1f);

            return result;
        }
    }
}
