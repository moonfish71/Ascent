using Ascent.Content.NPCs.Templates;
using Ascent.Content.Projectiles.Hostile.BossAttacks.Aster;
using Ascent.Core;
using Ascent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace Ascent.Content.NPCs.Bosses.Aster
{
    public partial class AsterBoss : AscentNPC
    {
        public Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            { char.Parse("A") , 1}, { char.Parse("a") , 1},
            { char.Parse("B") , 2}, { char.Parse("b") , 2},
            { char.Parse("C") , 3}, { char.Parse("c") , 3},
            { char.Parse("D") , 4}, { char.Parse("d") , 4},
            { char.Parse("E") , 5}, { char.Parse("e") , 5},
            { char.Parse("F") , 6}, { char.Parse("f") , 6},
            { char.Parse("G") , 7}, { char.Parse("g") , 7},
            { char.Parse("H") , 8}, { char.Parse("h") , 8},
            { char.Parse("I") , 9}, { char.Parse("i") , 9},
            { char.Parse("J") , 10}, { char.Parse("j") , 10},
            { char.Parse("K") , 11}, { char.Parse("k") , 11},
            { char.Parse("L") , 12}, { char.Parse("l") , 12},
            { char.Parse("M") , 13}, { char.Parse("m") , 13},
            { char.Parse("N") , 14}, { char.Parse("n") , 14},
            { char.Parse("O") , 15}, { char.Parse("o") , 15},
            { char.Parse("P") , 16}, { char.Parse("p") , 16},
            { char.Parse("Q") , 17}, { char.Parse("q") , 17},
            { char.Parse("R") , 18}, { char.Parse("r") , 18},
            { char.Parse("S") , 19}, { char.Parse("s") , 19},
            { char.Parse("T") , 20}, { char.Parse("t") , 20},
            { char.Parse("U") , 21}, { char.Parse("u") , 21},
            { char.Parse("V") , 22}, { char.Parse("v") , 22},
            { char.Parse("W") , 23}, { char.Parse("w") , 23},
            { char.Parse("X") , 24}, { char.Parse("x") , 24},
            { char.Parse("Y") , 25}, { char.Parse("y") , 25},
            { char.Parse("Z") , 26}, { char.Parse("z") , 26},
            { char.Parse(" ") , 27}
        };

        #region Attack Processing

        struct AttackCycle
        {
            public Attacks[] Attacks;
        }

        private AttackCycle P1 = new AttackCycle();
        private AttackCycle P2 = new AttackCycle();

        private enum Attacks
        {
            None,
            Crash,
            StarBarrage
        }

        private void SetUpPhases()
        {
            P1.Attacks = new Attacks[]
            {
                Attacks.StarBarrage
            };
        }

        private void DoAttack(Attacks attack, int phase)
        {
            AttackActive = true;

            timer[1]++;
            timer[2]++;

            if (phase == 0)
            {
                switch (attack)
                {
                    case Attacks.StarBarrage:
                        StarBarrage();
                        break;

                    case Attacks.Crash:
                        AsterCrash();
                        break;

                    default:
                        EndAttack();
                        break;
                }
            }
        }

        private void EndAttack()
        {
            timer[0] = 0;
            timer[1] = 0;
            timer[2] = 0;

            AttackActive = false;
            AttackIndex++;
            loopTracker = 0;
        }
        #endregion

        #region Phases

        private void Phase1()
        {
            if (timer[0] > 100)
            {
                DoAttack(P1.Attacks[AttackIndex], 0);

                if (AttackIndex >= P1.Attacks?.Length)
                {
                    AttackIndex = 0;
                }
            }
            else
            {
                Move(NPC.Center, ActivePlayer.Center, 1f, .9f);
            }
        }

        #endregion

        #region Attacks

        int loopTracker = 0;

        //Tracked Target Vector: (ActivePlayer.velocity * ModMath.Delta(NPC.Center, ActivePlayer.Center).Length() / *Speed*)

        //Lore time!!!! (I'll make better ones later)
        public static string[] AttackPhrases = new string[]
        {
            "Altae",
            "Let us in",
            "Kill Olothon",
            "The stars",
            "One truth",
            "Eternal",
            "Gilded cage",
            "Heaven says",
            "Angel",
            "Above",
            "You will know",
            "We wait above",
            "Our freedom",
            "False visage",
            "Rise with us",
            "We can see",
            "Greater",
            "Stronger",
            "Take control",
            "Ashes to ashes",
            "You live a farce",
            "Oblivion yearns",
            "You are trapped",
            "Ground to dust",
            "Rotting away",
            "Behold",
            "Heaven says"
        };

        private void StarBarrage()
        {
            String String = AttackPhrases[Main.rand.Next(AttackPhrases.Length)];
            for (int i = 0; i < String?.Length; i++)
            {
                int letterID = 0;

                if (LetterValues.ContainsKey(String.ElementAt(i)))
                {
                    letterID = LetterValues.ElementAt(i).Value;

                    Console.WriteLine(String.ElementAt(i));
                }
            }

            SoundStyle test = SoundID.DD2_ExplosiveTrapExplode;

            if (timer[2] >= 30)
            {
                NPC.netUpdate = true;

                Shoot(ModContent.ProjectileType<MadStar>(), NPC.Center, ActivePlayer.Center + (ActivePlayer.velocity * ModMath.Delta(NPC.Center, ActivePlayer.Center).Length() / 20), 20, false, NPC.damage, 1, NPC.target);
                timer[2] = 0;

                NPC.netUpdate = false;
            }

            Move(NPC.Center, ActivePlayer.Center, .6f, .9f);

            if (timer[1] > ModMath.SecondsToTicks(5))
            {
                SoundEngine.PlaySound(test);
                EndAttack();
            }
        }

        private void AsterCrash()
        {
            EndAttack();
        }

        #endregion
    }
}