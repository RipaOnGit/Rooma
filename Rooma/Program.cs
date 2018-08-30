using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooma
{
    public static class Program
    {
        static Dictionary<char, int> numeralToNumberLookup = new Dictionary<char, int>();
        static void Main(string[] args)
        {
            
            numeralToNumberLookup.Add('I', 1);
            numeralToNumberLookup.Add('V', 5);
            numeralToNumberLookup.Add('X', 10);
            numeralToNumberLookup.Add('L', 50);
            numeralToNumberLookup.Add('C', 100);
            numeralToNumberLookup.Add('D', 500);
            numeralToNumberLookup.Add('M', 1000);

            //Sovelluksen lopetus kontrollimerkki muuttuja.
            char stop = ' ';

            //Laskennan output.
            int result = 0;

            //Suoritetaan kunnes käyttäjä haluaa lopettaa.
            while( stop != 'y')
            {
                Console.WriteLine("Write roman numeral: ");
                Convert(Console.ReadLine(), out result);
                Console.WriteLine(result != 0 ? result.ToString() : "Input was not valid roman number, try again!");
                Console.WriteLine("Hit enter to continue else hit 'y' to stop.");
                stop = Console.ReadKey(true).KeyChar;
            }

            
        }

        static private void Convert(string numeral, out int result)
        {
            //Palautetaan nolla jos ei pystytty laskemaan numeraalin arvoa. 
            result = 0;

            //Alustetaan muuttuja, johon roomalaisen luvun arabialainen muoto summataan.
            var totalArabicvalue = 0;

            //Käännetään roomalainen numero kirjainlista, niin että pienin merkkaava merkki käsitellään ensin.
            var romanArray = numeral.ToCharArray().Reverse().ToArray();

            //Asetataan ensimmaisen vähiten merkitsevän numeraalin arvo summan alkuarvoksi.
            if (IsCharacterRomanNumeral(romanArray[0]))
            {
                totalArabicvalue = numeralToNumberLookup[romanArray[0]];
            }


            //Otetaan numeraalimerkki käsittelyyn pienemmästä merkitsevästä päästä ja tarkastetaan seuraava,
            //sitten joko summataan tai vähennetään seuraavan merkin arvo koko summasta laskenta hetkellä.
            for (int i = 0; i < romanArray.Length - 1; i++)
            {
                //Tarkastetaan onko myös käsiteltävää numeraalimerkkiä seuraava numeraalimerkki kelvollinen numeraalimerkki.
                if (IsCharacterRomanNumeral(romanArray[i+1]))
                {
                    //Jos käsiteltävää numeraalimerkkiä seuraava merkki on arvoltaan pienempi tarkastetaan onko se sallittu
                    //merkki käyttää kyseisessä järjestyksessä roomalaisessa numerossa.
                    if (numeralToNumberLookup[romanArray[i + 1]] < numeralToNumberLookup[romanArray[i]])
                    {
                        if (IsNumeralValidAsLouwerSubtractor(romanArray[i], romanArray[i + 1]))
                        {
                            //Jos seuraava merkki on kelvollinen vähennetään. 
                            totalArabicvalue = totalArabicvalue - numeralToNumberLookup[romanArray[i + 1]];
                        }
                        else
                        {
                            //Katkaistaan laskenta, jos numeraalit sisältävät ei sallittuja merkkejä.
                            //ja nollataan tulos.
                            totalArabicvalue = 0;
                            break;
                        }
                    }
                    else
                    { 
                        //Muuten lisätään seuraavan merkin arvo kokonaissummaan.
                        totalArabicvalue = totalArabicvalue + numeralToNumberLookup[romanArray[i + 1]];
                    }
                }
                else
                {
                    //Katkaistaan laskenta, jos numeraalit sisältävät ei sallittuja merkkejä.
                    //ja nollataan tulos.
                    totalArabicvalue = 0;
                    break;
                }
            }

            result = totalArabicvalue;
        }

        /// <summary>
        /// Tarkastetaan kuuluuko merkki Roomalaisiin numeraaleihin
        /// </summary>
        /// <param name="character">Tarkastettava numeraali kirjain.</param>
        /// <returns></returns>
        private static bool IsCharacterRomanNumeral(char character)
        {
            var result = numeralToNumberLookup.ContainsKey(character);
            return result;
        }

        /// <summary>
        /// Tarkastetaan jos käsittelyssä olevaa merkkiä seuraava merkki on pienenmpi kuin käsiteltävä,
        /// onko merkki sallittu vähennettävä suhteessa käsiteltävään merkkiin. 
        /// </summary>
        /// <param name="charTohandle">Käsittelyssä oleva merkki.</param>
        /// <param name="toCheck">Seuraava merkki.</param>
        /// <returns>Arvo on true, jos merkki seuraava merkki on sallittu, arvoltaan pienempi edeltäjäänsä nähden.</returns>
        private static bool IsNumeralValidAsLouwerSubtractor(char charTohandle, char toCheck)
        {
            var result = false;

            //So I can only be used in front of V and X
            if ((charTohandle == 'V' || charTohandle == 'X') && toCheck == 'I')
            {
                result = true; 
            }

            //X in front of L and C
            if ((charTohandle == 'L' || charTohandle == 'C') && toCheck == 'X')
            {
                result = true;
            }

            //C in front of D and M
            if ((charTohandle == 'D' || charTohandle == 'M') && toCheck == 'C')
            {
                result = true;
            }

            return result;
        }

    }
}
