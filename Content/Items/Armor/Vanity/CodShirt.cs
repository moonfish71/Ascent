using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Ascent.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Body)]
	public class CodShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("LunarCod's Spacesuit");
			// Tooltip.SetDefault("Great for impersonating developers!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			//int equipSlotBody = Mod.GetEquipSlot(Name, EquipType.Body); 
			//ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.rare = 5;
			Item.vanity = true;
		}
    }
}
