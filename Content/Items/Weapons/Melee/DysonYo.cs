using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascent.Core;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Net;

namespace Ascent.Content.Items.Weapons.Melee
{
    public class DysonYo : ModItem
    {
        public override string Texture => QuickDirectory.PlaceHolderTx;

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 5;
            Item.useTime = Item.useAnimation = 5;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
        }
    }
}
