using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ascent.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class CodHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("LunarCod's Vacuum Helmet");

			// Tooltip.SetDefault("Great for impersonating developers!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
                                                                // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
                                                                // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
                                                                // ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
                                                                // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
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