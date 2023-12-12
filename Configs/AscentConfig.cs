using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace Ascent.Configs
{
    public class AscentServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(255)]
        [Range(0f, 8192f)]
        public float ParticleLimit;

        public enum talkInteraction
        {
            World = 0,
            Chat,
            Both
        }

        [DefaultValue(talkInteraction.World)]
        [Label("Talk Interaction")]
        [Tooltip("Controls whether certain things talk using in-world text, the chatbox, or both.")]
        public talkInteraction TalkTextSetting;
    }
}
