using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar : OkosEszkoz, Ihangjelzo, Istatisztika
    {
        public bool ZartE { get; private set; }

        private string pinKod;
        private int nyitasokSzama;
        private int hibasPinProbalkozasok;
        private bool veszjelzesAktiv;

        public OkosZar(
            string azonosito,
            string nev,
            string pinKod)
            : base(azonosito, nev)
        {
            this.pinKod = pinKod;
            this.ZartE = true;
            this.nyitasokSzama = 0;
            this.hibasPinProbalkozasok = 0;
            this.veszjelzesAktiv = false;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("NYITAS:"))
            {
                string megadottPin = parancs.Substring("NYITAS:".Length);

                if (megadottPin == this.pinKod)
                {
                    this.ZartE = false;
                    this.nyitasokSzama++;
                    this.hibasPinProbalkozasok = 0;
                    this.UtolsoFrissites = DateTime.Now;
                }
                else
                {
                    this.hibasPinProbalkozasok++;
                    this.Csipog(200);

                    if (this.hibasPinProbalkozasok >= 3)
                    {
                        this.VeszjelzesInditasa();
                    }
                }
            }
            else if (parancs == "ZARAS")
            {
                this.ZartE = true;
                this.UtolsoFrissites = DateTime.Now;
            }
        }

        public override string AllapotJelentes()
        {
            return this.ZartE ? "ZÁRVA" : "NYITVA";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();

            this.pinKod = "0000";
            this.ZartE = true;
            this.hibasPinProbalkozasok = 0;
            this.veszjelzesAktiv = false;
        }

        public void Csipog(int hosszu)
        {
            if (hosszu < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hosszu));
            }

            Console.WriteLine(
                "A(z) " + this.Nev
                + " csipog (" + hosszu + " ms).");
        }

        public void VeszjelzesInditasa()
        {
            this.veszjelzesAktiv = true;

            Console.WriteLine(
                "RIASZTÁS! A(z) " + this.Nev
                + " vészjelzése aktív!");
        }

        public string StatisztikatKeszit()
        {
            return "Okoszár: " + this.Nev
                + ", sikeres nyitások: " + this.nyitasokSzama
                + ", hibás PIN-próbálkozások: "
                + this.hibasPinProbalkozasok
                + ", vészjelzés aktív: "
                + this.veszjelzesAktiv;
        }
    }
}