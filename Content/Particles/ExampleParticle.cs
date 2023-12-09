using Ascent.Core;
using Ascent.Core.Systems.Particles;
using System;

namespace Ascent.Content.Particles
{
    public class ExampleParticle : Particle
    {
        float timer = 0;
        public override void SetDefaults()
        {
            TimeLeft = ModMath.SecondsToTicks(25);
        }

        public override void OnSpawn()
        {

        }

        public override void Update()
        {
            timer++;
            velocity *= .95f;
            //position.Z = (float)(500 * Math.Sin(timer / 15));
        }
    }
}
