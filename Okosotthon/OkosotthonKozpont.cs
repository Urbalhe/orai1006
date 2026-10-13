using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosotthonKozpont
    {
        private readonly List<OkosEszkoz> eszkozok;

        public OkosotthonKozpont()
        {
            this.eszkozok = new List<OkosEszkoz>();
        }

        public void EszkozHozzaadasa(OkosEszkoz eszkoz)
        {
            if (eszkoz == null)
            {
                throw new ArgumentNullException(nameof(eszkoz));
            }

            this.eszkozok.Add(eszkoz);
        }

        public void OsszesCsatlakoztatasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                eszkoz.Csatlakozas();
            }
        }

        public int RendszerDiagnosztikaFuttatasa()
        {
            int sikeresTesztek = 0;

            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz.DiagnosztikaFuttatasa())
                {
                    sikeresTesztek++;
                }
            }

            return sikeresTesztek;
        }

        public void OsszesVeszjelzesInditasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is Ihangjelzo h)
                {
                    h.VeszjelzesInditasa();
                }
            }
        }

        public void OsszesIdozitoBeallitasa(int perc)
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is Iidozito i)
                {
                    i.IdozitestBeallit(perc);
                }
            }
        }

        public void OsszesStatisztikaKiirasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is Istatisztika s)
                {
                    Console.WriteLine(s.StatisztikatKeszit());
                }
            }
        }
    }
}



