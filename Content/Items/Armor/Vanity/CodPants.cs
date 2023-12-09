using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ascent.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class CodPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
