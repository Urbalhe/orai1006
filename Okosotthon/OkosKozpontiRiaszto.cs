using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    internal class OkosKozpontiRiaszto :
        OkosEszkoz, Iidozito, Ihangjelzo, Istatisztika
    {
        private int maradtIdo;
        private int csipogasokSzama;
        private int riasztasokSzama;
        private bool veszjelzesAktiv;

        public OkosKozpontiRiaszto(
            string azonosito,
            string nev)
            : base(azonosito, nev)
        {
            this.maradtIdo = 0;
            this.csipogasokSzama = 0;
            this.riasztasokSzama = 0;
            this.veszjelzesAktiv = false;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs == "RIASZTO_BE")
            {
                this.VeszjelzesInditasa();
            }
            else if (parancs == "RIASZTO_KI")
            {
                this.veszjelzesAktiv = false;
            }
        }

        public override string AllapotJelentes()
        {
            return this.veszjelzesAktiv
                ? "A központi riasztó AKTÍV."
                : "A központi riasztó inaktív.";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public void IdozitestBeallit(int perc)
        {
            if (perc < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(perc));
            }

            this.maradtIdo = perc;
        }

        public int GetMaradtIdo()
        {
            return this.maradtIdo;
        }

        public void Csipog(int hosszu)
        {
            if (hosszu < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hosszu));
            }

            this.csipogasokSzama++;

            Console.WriteLine(
                "Központi riasztó: csipogás ("
                + hosszu + " ms).");
        }

        public void VeszjelzesInditasa()
        {
            this.veszjelzesAktiv = true;
            this.riasztasokSzama++;

            Console.WriteLine(
                "FIGYELEM! Központi vészjelzés aktiválva!");
        }

        public string StatisztikatKeszit()
        {
            return "Központi riasztó: " + this.Nev
                + ", hátralévő idő: " + this.maradtIdo
                + " perc, csipogások: " + this.csipogasokSzama
                + ", riasztások: " + this.riasztasokSzama
                + ", aktív: " + this.veszjelzesAktiv;
        }
    }
}

    

