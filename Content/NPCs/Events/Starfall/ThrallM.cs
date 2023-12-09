﻿using Ascent.Content.NPCs.Templates;
using Ascent.Core.ModPlayers;
using Humanizer.Localisation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Ascent.Core.QuickDirectory;

namespace Ascent.Content.NPCs.Events.Starfall
{
    public class ThrallM : AscentNPC
    {
        public override string Texture => PlaceHolderTx;

        public enum States
        {
            Walking,
            Slash,
            Dashing
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 48;
            NPC.lifeMax = 100;
            NPC.aiStyle = -1;
            NPC.damage = 60;
            NPC.knockBackResist = 0.3f;
            NPC.defense = 1;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Meteor,
                new FlavorTextBestiaryInfoElement("A thrall of the Astral Gestalt. This thrall has developed a pair of knife-like limbs, giving it vastly increased reach and damage.")
            }); ;
        }

        public float[] Timer = new float[4] { 0, 0, 0, 0 };

        public ref float State => ref NPC.ai[2];

        float oldState = 0;

        public Vector2 delta = Vector2.Zero;

        public override void AI()
        {
            NPC.TargetClosestUpgraded();
            Player player = Main.player[NPC.target];

            oldState = State;

            delta = player.Center - NPC.Center;

            Timer[0]++;

            switch (State)
            {
                case (float)States.Walking:
                    NPC.aiStyle = NPCAIStyleID.Fighter;
                    NPC.velocity.X = NPC.direction * 1.2f;
                    break;
                case (float)States.Slash:
                    NPC.velocity.X *= .95f;
                    NPC.aiStyle = -1;
                    break;
                case (float)States.Dashing:
                    NPC.velocity.X *= .95f;
                    NPC.aiStyle = -1;
                    break;
            }

            bool dashCheck = false;

            switch (State)
            {
                case (float)States.Walking:

                    if(delta.Length() < 200 && !dashCheck)
                    {
                        NPC.netUpdate = true;
                        if (Main.rand.NextBool())
                        {
                            State = (float)States.Dashing;
                        }
                        else
                        {
                            Timer[0] = 0;
                        }

                        dashCheck = true;

                        NPC.netUpdate = false;
                    }
                    else if (dashCheck && Timer[0] > 300)
                    {
                        State = (float)States.Dashing;
                        dashCheck = false;
                    }

                    if (delta.Length() < 50f)
                    {
                        Timer[0] = 0;
                        State = 1;
                    }
                    break;
                case (float)States.Slash:
                    if (Timer[0] > 60)
                    {
                        Timer[0] = 0;
                        State = 0;
                    }
                    break;
                case (float)States.Dashing:
                    if (Timer[0] > 180 && NPC.velocity.Y == 0f)
                    {
                        State = (float)States.Walking;
                    }
                    break;
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
    }
}