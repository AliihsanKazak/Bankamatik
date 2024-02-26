using System;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Threading;

namespace Bankamatik
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string sifre = "ab18";
            int fatura = 0;
            double bakiye = 250;

        GIRISMENU:
            Console.WriteLine("\tKartli Islem\t1\n\tKartsız Islem\t2");
            string giris = Console.ReadLine();

            switch (giris)
            {
                case "1":
                    string kartsıfre = null;
                    int hak = 0;
                    while (kartsıfre != sifre && hak <3)
                    {
                        hak++;
                        Console.WriteLine("Kart sıfresini giriniz: ");
                        kartsıfre = Console.ReadLine();
                        if (kartsıfre != sifre)
                        {
                            Console.WriteLine(hak + ". hakkınız. Sifre hatalıdır.... ");
                        }
                    }
                    if (kartsıfre == sifre)
                    {
                    ANAMENU:
                        Console.WriteLine("*********** Ana Menu ************\n\tPara Cekmek 1\n\tPara Yatirmak 2\n\tPara Transferleri 3\n\tEgitim Odemeleri 4\n\tOdemeler 5\n\tBilgi Guncelleme 6");
                        string menuSecim = Console.ReadLine();
                        switch (menuSecim)
                        {
                            #region Para Cekme Alani
                            case "1":
                            CEK:
                                Console.WriteLine("Cekmek istediginiz miktari giriniz");
                                double cekilecekMiktar  = Convert.ToDouble(Console.ReadLine());
                                if (bakiye >= cekilecekMiktar)
                                {
                                    bakiye = bakiye - cekilecekMiktar;
                                    Console.WriteLine("\tCekilen Miktar:{0}\n\tYeni bakiyeniz:{1}", cekilecekMiktar, bakiye);
                                }
                                else
                                {
                                    Console.WriteLine("Yetersiz Bakiye");
                                    Console.WriteLine("\tYeni Bakiye girisi icin\t1");
                                    Console.WriteLine("\tAna Menu icin \t9\n\tCikmak icin herhangibir tusa basin");
                                    string secimBakiye = Console.ReadLine();
                                    if (secimBakiye == "9")
                                    {
                                        goto ANAMENU;
                                    }
                                    else if (secimBakiye == "1")
                                    {
                                        goto CEK;
                                    }
                                    
                                }
                            break;
                            #endregion

                            #region Para Yatırma Alani
                            case "2":
                                Console.WriteLine("\tKredi Kartina  1\n\t\n Kendi Hesabiniza Yatirmak Icın  2\n\t Cikmak Icın  0");
                                string secimParaYatirma = Console.ReadLine();
                                if (secimParaYatirma == "1")
                                {
                                KARTNO:
                                    Console.WriteLine("Kredi karti icin 12 haneli kart numarasi girin");
                                    long krediKartiNo = Convert.ToInt64(Console.ReadLine());
                                    if (krediKartiNo >= 100000000000 && krediKartiNo <= 999999999999)
                                    {
                                        Console.WriteLine("Kredi kartina yatirmak istediğiniz miktari giriniz: ");
                                        double krediKartinaYatirilicakMiktar = Convert.ToDouble(Console.ReadLine());
                                        if (bakiye >= krediKartinaYatirilicakMiktar)
                                        {
                                            bakiye = bakiye - krediKartinaYatirilicakMiktar;
                                            Console.WriteLine("\tKredi kartina yatitilan miktar: {0}\n\tYeni bakiyeniz:{1}", krediKartinaYatirilicakMiktar, bakiye);

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Kart numarasi eksik/hatali girdiniz");
                                        Console.WriteLine("\tKredi karti numarasini tekrar girmek icin \t1");
                                        Console.WriteLine("\tAna menu icin \t9\n\tCikmak icin herhangi bir tusa basiniz");
                                        string secimTekrarKrediKartiNo = Console.ReadLine();
                                        if (secimTekrarKrediKartiNo == "9")
                                        {
                                            goto ANAMENU;
                                        }
                                        else if (secimTekrarKrediKartiNo == "1")
                                        {
                                            goto KARTNO;
                                        }
                                    }
                                }
                                else if (secimParaYatirma == "2")
                                {
                                    Console.WriteLine("YatirilacaK Miktari Giriniz");
                                    bakiye = bakiye + Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("\tYeni bakiyeniz: " + bakiye);
                                }
                                break;
                            #endregion

                            #region Para Transfer Alani
                            case "3":
                                Console.WriteLine("********* Para Transfer Menusu***********\n\tEFT yapmak icin 1\n\tHavale yapmak için 2");
                                int secim = int.Parse(Console.ReadLine());
                                if (secim == 1)
                                {
                                EFT:
                                    Console.WriteLine("Yatirmak istediginiz IBAN basinda TR olacak sekilde giriniz....");
                                    string gelen = Console.ReadLine();
                                    long trIban = long.Parse(gelen.Substring(2));
                                    string tr = gelen.Substring(0, 2).ToLower();
                                    if (tr == "tr")
                                    {
                                        if (trIban >= 100000000000 && trIban <= 999999999999)
                                        {
                                        TRMIKTAR:
                                            Console.WriteLine("Yatirilacak Miktari Giriniz..");
                                            double trMiktar = Convert.ToDouble(Console.ReadLine());
                                            if (bakiye >= trMiktar)
                                            {
                                                bakiye = bakiye - trMiktar;
                                                Console.WriteLine("Belirtilen " + trIban + " numarasina " + trMiktar + " lira gönderi yapilmistir.");
                                                Console.WriteLine("\tYeni Bakiyeniz: " + bakiye);

                                            }
                                            else
                                            {
                                                Console.WriteLine("\tBakiyeniz yetersiz. Lütfen tekrar giriniz. \t\nMevcut bakiyeniz: " + bakiye);
                                                goto TRMIKTAR;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("IBAN numarasini eksik/hatali girdiniz");
                                            Console.WriteLine("\tIBAN numarasini tekrar girmek icin \t1");
                                            Console.WriteLine("\tAna menu icin \t9\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimTekrarKrediNo = Console.ReadLine();
                                            if (secimTekrarKrediNo == "9")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimTekrarKrediNo == "1")
                                            {
                                                goto EFT;
                                            }
                                        }

                                    }
                                }
                                if (secim == 2)
                                {
                                HAVALE:
                                    Console.WriteLine("Yatirmak istediginiz IBAN basinda TR olacak sekilde giriniz....");
                                    string gelen = Console.ReadLine();
                                    long trIban = long.Parse(gelen.Substring(2));
                                    string tr = gelen.Substring(0, 2).ToLower();
                                    if (tr == "tr")
                                    {
                                        if (trIban >= 100000000000 && trIban <= 999999999999)
                                        {
                                        TRMIKTAR:
                                            Console.WriteLine("Yatirilacak Miktari Giriniz..");
                                            double trMiktar = Convert.ToDouble(Console.ReadLine());
                                            if (bakiye >= trMiktar)
                                            {
                                                bakiye = bakiye - trMiktar;
                                                Console.WriteLine("Belirtilen " + trIban + " numarasina " + trMiktar + " lira gönderi yapilmistir.");
                                                Console.WriteLine("\tYeni Bakiyeniz: " + bakiye);

                                            }
                                            else
                                            {
                                                Console.WriteLine("\tBakiyeniz yetersiz. Lütfen tekrar giriniz. \t\nMevcut bakiyeniz: " + bakiye);
                                                goto TRMIKTAR;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("IBAN numarasini eksik/hatali girdiniz");
                                            Console.WriteLine("\tIBAN numarasini tekrar girmek icin \t1");
                                            Console.WriteLine("\tAna menu icin \t9\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimTekrarKrediNo = Console.ReadLine();
                                            if (secimTekrarKrediNo == "9")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimTekrarKrediNo == "1")
                                            {
                                                goto HAVALE;
                                            }
                                        }

                                    }
                                }
                                break;
                                
                        

                            #endregion

                            #region Eğitim Ödemeleri

                            case "4":
                                Console.WriteLine("Eğitim ödemeleri sistemi arizali. Lütfen daha sonra tekrar deneyiniz");
                                break;

                                #endregion

                            #region Fatura ödeme Alanı
                            case "5":
                                FATURA:
                                Console.WriteLine("******** Faura Ödeme Alanı *********");
                                Console.WriteLine("Elektirik faturasi icin 1");
                                Console.WriteLine("Telefon Faturasi icin 2");
                                Console.WriteLine("Internet Faturasi icin 3");
                                Console.WriteLine("Su Faturasi icin 4");
                                Console.WriteLine("OGS Faturasi icin 5");
                                Console.WriteLine("--------------------------");
                                Console.WriteLine("Ana menu icin 9");
                                string odeme = Console.ReadLine();

                                switch (odeme)
                                {
                                    case "1":
                                        Console.WriteLine("Elektrik fatura numarasi giriniz");
                                        fatura = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Fatura bedeliniz: " + 40.00);
                                        Console.WriteLine("Odeme yapmak istiyor musunuz? (E/H)");
                                        string eCevap = Console.ReadLine().ToUpper();
                                        if (eCevap == "E"|| eCevap == "Evet")
                                        {
                                            bakiye = bakiye - 40;
                                            Console.WriteLine("Odeme yapildi. Yeni Bakiyeniz {0}", bakiye);
                                            Console.WriteLine("\tAna menu icin 9\t\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimFatura = Console.ReadLine();
                                            if (secimFatura == "0")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimFatura == "1")
                                            {
                                                goto FATURA;
                                            }
                                        }
                                        else if (eCevap == "H" || eCevap == "HAYIR")
                                        {
                                            goto FATURA;
                                        }
                                        break;

                                    case "2":
                                        Console.WriteLine("Telefon fatura numarasi giriniz");
                                        fatura = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Fatura bedeliniz: " + 60.00);
                                        Console.WriteLine("Odeme yapmak istiyor musunuz? (E/H)");
                                        string tCevap = Console.ReadLine().ToUpper();
                                        if (tCevap == "E" || tCevap == "Evet")
                                        {
                                            bakiye = bakiye - 40;
                                            Console.WriteLine("Odeme yapildi. Yeni Bakiyeniz {0}", bakiye);
                                            Console.WriteLine("\tAna menu icin 9\t\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimFatura = Console.ReadLine();
                                            if (secimFatura == "0")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimFatura == "1")
                                            {
                                                goto FATURA;
                                            }
                                        }
                                        else if (tCevap == "H" || tCevap == "HAYIR")
                                        {
                                            goto FATURA;
                                        }
                                        break;

                                    case "3":
                                        Console.WriteLine("Internet fatura numarasi giriniz");
                                        fatura = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Fatura bedeliniz: " + 55.00);
                                        Console.WriteLine("Odeme yapmak istiyor musunuz? (E/H)");
                                        string iCevap = Console.ReadLine().ToUpper();
                                        if (iCevap == "E" || iCevap == "Evet")
                                        {
                                            bakiye = bakiye - 40;
                                            Console.WriteLine("Odeme yapildi. Yeni Bakiyeniz {0}", bakiye);
                                            Console.WriteLine("\tAna menu icin 9\t\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimFatura = Console.ReadLine();
                                            if (secimFatura == "0")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimFatura == "1")
                                            {
                                                goto FATURA;
                                            }
                                        }
                                        else if (iCevap == "H" || iCevap == "HAYIR")
                                        {
                                            goto FATURA;
                                        }
                                        break;

                                    case "4":
                                        Console.WriteLine("Su fatura numarasi giriniz");
                                        fatura = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Fatura bedeliniz: " + 25.00);
                                        Console.WriteLine("Odeme yapmak istiyor musunuz? (E/H)");
                                        string sCevap = Console.ReadLine().ToUpper();
                                        if (sCevap == "E" || sCevap == "Evet")
                                        {
                                            bakiye = bakiye - 40;
                                            Console.WriteLine("Odeme yapildi. Yeni Bakiyeniz {0}", bakiye);
                                            Console.WriteLine("\tAna menu icin 9\t\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimFatura = Console.ReadLine();
                                            if (secimFatura == "0")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimFatura == "1")
                                            {
                                                goto FATURA;
                                            }
                                        }
                                        else if (sCevap == "H" || sCevap == "HAYIR")
                                        {
                                            goto FATURA;
                                        }
                                        break;

                                    case "5":
                                        Console.WriteLine("OGS fatura numarasi giriniz");
                                        fatura = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Fatura bedeliniz: " + 60.00);
                                        Console.WriteLine("Odeme yapmak istiyor musunuz? (E/H)");
                                        string ogsCevap = Console.ReadLine().ToUpper();
                                        if (ogsCevap == "E" || ogsCevap == "Evet")
                                        {
                                            bakiye = bakiye - 40;
                                            Console.WriteLine("Odeme yapildi. Yeni Bakiyeniz {0}", bakiye);
                                            Console.WriteLine("\tAna menu icin 9\t\n\tCikmak icin herhangi bir tusa basiniz");
                                            string secimFatura = Console.ReadLine();
                                            if (secimFatura == "0")
                                            {
                                                goto ANAMENU;
                                            }
                                            else if (secimFatura == "1")
                                            {
                                                goto FATURA;
                                            }
                                        }
                                        else if (ogsCevap == "H" || ogsCevap == "HAYIR")
                                        {
                                            goto FATURA;
                                        }
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            #endregion

                            #region Sifre Degistirme
                            //string temp = Console.ReadLine();
                            //temp.Equals(sifre);

                            case "6":
                            SIFRE:
                                Console.WriteLine("******** Sifre Degistirme Alani **********");
                                Console.WriteLine("Eski Sifrenizi Giriniz");
                                string temp = Console.ReadLine();
                                temp.Equals(sifre);
                                if (temp.Equals(sifre))
                                {
                                    string sif1;
                                    string sif2;
                                    do
                                    {
                                        Console.WriteLine("Yeni Sifrenizi Giriniz...");
                                        sif1 = Console.ReadLine();
                                        Console.WriteLine("Yeni Sifrenizi Tekrar Giriniz...");
                                        sif2 = Console.ReadLine();
                                    }
                                    while (sif1 != sif2);

                                    sifre = sif1;
                                    Console.WriteLine("Sifreniz basariyla degistirildi.....");
                                    goto GIRISMENU;
                                }
                                else
                                {
                                    Console.WriteLine("Sifrenizi yanlis girdiniz. Tekrar deneyiniz");
                                    goto SIFRE;
                                }

                                break;
                            #endregion

                            default:
                                Console.WriteLine("Ana menuden hatalı secim yaptınız");
                                Console.WriteLine("\tAna menu icin \t0\n\tCikmak icin herhangi bir tusa basiniz");
                                if (Console.ReadLine() == "0")
                                {
                                    goto ANAMENU;
                                }
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Hakkniz bitti");
                    }
                    break;
                default:
                    Console.WriteLine("Hatali Secim Yaptiniz");
                    goto GIRISMENU;
                    break;
            }
            Console.ReadKey();


        }
    }
}