using Ascent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Terraria.DataStructures;

namespace Ascent.Content.Items.Weapons.Melee
{
    public class FectoSpear : ModItem
    {
        public override string Texture => QuickDirectory.MeleeTex + "FectoSpear";

        public override void SetDefaults()
        {
            Item.Size = new Vector2(64);
            Item.damage = 10;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<FectoSpearProj>();
            Item.shootSpeed = 1;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            base.UseStyle(player, heldItemFrame);
        }
    }

    public class FectoSpearProj : ModProjectile
    {
        public override string Texture => $"{QuickDirectory.MeleeTex}FectoSpearProj";

        public Texture2D tex = (Texture2D)ModContent.Request<Texture2D>($"{QuickDirectory.MeleeTex}FectoSpearProj");

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(32);
            Projectile.damage = 10;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        float length = 100;
        float rotation = 0;

        bool SetInitCons;
        int InMouseDir = 1;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 armCenter = player.Center - new Vector2(player.width-2,player.height/2);

            if (!SetInitCons)
            {
                Vector2 MouseDelta = player.Center - Main.MouseWorld;

                rotation = MouseDelta.ToRotation();
                InMouseDir = Math.Sign(MouseDelta.X);
                SetInitCons = true;
            }

            if (InMouseDir < 0)
            {
                rotation += (float)(.25 / Math.Tau);
            }
            else
            {
                rotation -= (float)(.25 / Math.Tau);
            }

            length += 0.5f;

            Vector2 spearPath = armCenter + new Vector2(0,-length).RotatedBy(rotation);

            Projectile.position = spearPath;
            Projectile.rotation = armCenter.AngleTo(Projectile.position) + (float)(Math.PI/4);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.Draw
            (
                tex,
                Projectile.Center - Main.screenPosition, 
                new Rectangle(0,0,tex.Width,tex.Height), 
                lightColor, Projectile.rotation, 
                new Vector2(tex.Width, 0), 
                Projectile.scale, 
                SpriteEffects.None, 
                0f
            );

            return false;
        }

        private void UpdatePlayer(Player player)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            int dir = Projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = Projectile.whoAmI;
            player.itemRotation = Projectile.rotation;
        }
    }
}
