using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz, Iidozito, Istatisztika
    {
        public double JelenlegiHomerseklet { get; private set; }
        public double CelHomerseklet { get; private set; }

        private int maradtIdo;
        private int beallitasokSzama;

        public Termosztat(
            string azonosito,
            string nev,
            double celHomerseklet)
            : base(azonosito, nev)
        {
            this.JelenlegiHomerseklet = 21.0;
            this.CelHomerseklet = celHomerseklet;
            this.maradtIdo = 0;
            this.beallitasokSzama = 0;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("BEALLIT_HOMERSEKLET:"))
            {
                string[] darabok = parancs.Split(':');

                double homerseklet;

                if (darabok.Length == 2 &&
                    double.TryParse(darabok[1],
                        System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out homerseklet))
                {
                    this.CelHomerseklet = homerseklet;
                    this.beallitasokSzama++;
                    this.UtolsoFrissites = DateTime.Now;
                }
            }
        }

        public override string AllapotJelentes()
        {
            return "Jelenlegi hőmérséklet: "
                + this.JelenlegiHomerseklet
                + " °C, célhőmérséklet: "
                + this.CelHomerseklet
                + " °C";
        }

        protected override bool OnTesztFuttatasa()
        {
            return this.CelHomerseklet >= 5.0
                && this.CelHomerseklet <= 35.0;
        }

        public void IdozitestBeallit(int perc)
        {
            if (perc < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(perc),
                    "Az idő nem lehet negatív.");
            }

            this.maradtIdo = perc;
        }

        public int GetMaradtIdo()
        {
            return this.maradtIdo;
        }

        public string StatisztikatKeszit()
        {
            return "Termosztát: " + this.Nev
                + ", jelenlegi hőmérséklet: "
                + this.JelenlegiHomerseklet
                + " °C, célhőmérséklet: "
                + this.CelHomerseklet
                + " °C, időzítő: "
                + this.maradtIdo
                + " perc, hőmérséklet-beállítások száma: "
                + this.beallitasokSzama;
        }
    }
}

