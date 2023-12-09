using Ascent.Core.Systems;
using Ascent.Core.Systems.Particles;
using rail;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Ascent
{
    public class Ascent : Mod
	{
        public static Mod Instance;

        public int ParticleLimit = 255;

        public Ascent() 
        {
            Instance = this;
        }

        public override void Load()
        {
            ParticleHandler.SetHooks();
            HordeManager.Load();
        }

        public override void Unload()
        {
            ParticleHandler.Unload();
            HordeManager.Unload();
        }
    }
}