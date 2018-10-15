using System;
using System.Linq;
using Verse;

namespace BloodTypes
{
    public enum ExpressedBloodTypes
    {
        A,
        B,
        O,
        AB
    }
    public enum BloodTypes
    {
        A,
        B,
        O
    }

    public enum Rh
    {
        Neg, 
        Pos
    }

    public static class Helper
    {
        public static BloodTypeHediffWithComps GetBloodType(this Pawn pawn)
        {
            return (BloodTypeHediffWithComps)pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodType);
        }
        
        public static void Clot(this Pawn pawn, int maxTends = 5)
        {
            foreach (var hediff in pawn.health.hediffSet.hediffs.Where(x=> x.Bleeding).OrderByDescending(x=>x.BleedRate))
            {
                hediff.Tended(Math.Min(Rand.Value+Rand.Value+Rand.Value, 1f));
                maxTends--;

                if (maxTends <= 0) return;
            }
        }
        
        public static void RemoveHediff(this Pawn pawn, HediffDef hediffDef)
        {
            if (hediffDef == null || pawn == null) return;
            var toxic = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
            if (toxic != null)
            {
                pawn.health.RemoveHediff(toxic);
            }
        }
    }
    public class BloodType: IExposable
    {
        public BloodTypes Primary, Secondary;
        public Rh RhPrimary, RhSecondary;


        public Rh RhExpressed()
        {
            if (RhPrimary == Rh.Pos || RhSecondary == Rh.Pos)
            {
                return Rh.Pos;
            }

            return Rh.Neg;
        }

        public ExpressedBloodTypes ExpressedBloodType()
        {
            ExpressedBloodTypes expressed = (ExpressedBloodTypes) Primary;
            if (Primary == Secondary) return expressed;
            if (Primary == BloodTypes.O)
                return (ExpressedBloodTypes) Secondary;
            if (Secondary == BloodTypes.O)
                return (ExpressedBloodTypes) Primary;
            return ExpressedBloodTypes.AB;
        }

        public bool CanGetBlood(BloodType other)
        {
            if (other == null) return true;
            var expressed = ExpressedBloodType();
            var otherExpressed = other.ExpressedBloodType();
            if (RhExpressed() == Rh.Neg)
            {
                if (other.RhExpressed() == Rh.Pos)
                {
                    return false;
                }
            }

            if (otherExpressed == ExpressedBloodTypes.O) 
                return true;

            if (expressed == ExpressedBloodTypes.AB)
                return true;

            return expressed == otherExpressed;


        }

        public override string ToString()
        {
            return ExpressedBloodType().ToString() + ((RhExpressed()==Rh.Pos)?"+":"-");
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref Primary, "ABO1");
            Scribe_Values.Look(ref Secondary, "ABO2");
            Scribe_Values.Look(ref RhPrimary, "RH1");
            Scribe_Values.Look(ref RhSecondary, "RH2");
        }

        public static BloodType Random()
        {
            return new BloodType()
            {
                Primary = (BloodTypes) (Rand.Value * 3),
                Secondary = (BloodTypes) (Rand.Value * 3),
                RhPrimary = (Rh) (Rand.Value * 2), 
                RhSecondary = (Rh) (Rand.Value * 2)
            };
        }
        
        public BloodType Child(BloodType other = null)
        {
            if (other == null)
            {
                other = Random();
            }
            return new BloodType()
            {
                Primary = new []{other.Primary, this.Primary}.RandomElement(),
                Secondary = new []{other.Secondary, this.Primary}.RandomElement(),
                RhPrimary = new []{other.RhPrimary, this.RhPrimary}.RandomElement(),  
                RhSecondary = new []{other.RhSecondary, this.RhSecondary}.RandomElement()
            };
        }
    }
}