namespace Okosotthon
{
    public class Program
    {
        static void Main(string[] args)
        {
            Termosztat termosztat = new Termosztat(
                "T001",
                "Nappali termosztát",
                22.0
            );

            OkosZar zar = new OkosZar(
                "Z001",
                "Bejárati ajtó",
                "1234"
            );


            Console.WriteLine(termosztat.AllapotJelentes());
            Console.WriteLine(zar.AllapotJelentes());


            OkosotthonKozpont kozpont = new OkosotthonKozpont();

            kozpont.EszkozHozzaadasa(termosztat);
            kozpont.EszkozHozzaadasa(zar);


            kozpont.OsszesCsatlakoztatasa();


            Console.WriteLine("Termosztát online: " + termosztat.OnlineE);
            Console.WriteLine("Zár online: " + zar.OnlineE);


            Console.WriteLine(
                "Sikeres diagnosztikák: "
                + kozpont.RendszerDiagnosztikaFuttatasa()
            );


            termosztat.ParancsVegrehajtasa("BEALLIT_HOMERSEKLET:25");

            Console.WriteLine(termosztat.AllapotJelentes());


            zar.ParancsVegrehajtasa("NYITAS:1234");

            Console.WriteLine(zar.AllapotJelentes());


            zar.ParancsVegrehajtasa("ZARAS");

            Console.WriteLine(zar.AllapotJelentes());


            Console.ReadLine();
        }
    }
}
    

