using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VATValidator.Models
{
    
    internal class VatUtils
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int[] Digits { get; set; }

        public static List<VatUtils> allCountries { get; set; } = new List<VatUtils>();

        public VatUtils(string countryCode, string countryName, int[] digits)
        {
            this.CountryCode = countryCode;
            this.Digits = digits;
            this.CountryName = countryName;
        }

        /// <summary>
        /// Creates a List of all EU Countries with Country code, Country and allowed digits Length after the Country code
        /// </summary>
        /// <returns>List of all EU Countries</returns>
        public static List<VatUtils> createAllCountriesList()
        {
            allCountries.Add(new VatUtils("BE", "Belgien", new int[] { 10 }));
            allCountries.Add(new VatUtils("BG", "Bulgarien", new int[] { 9, 10 }));
            allCountries.Add(new VatUtils("DK", "Dänemark", new int[] { 8 }));
            allCountries.Add(new VatUtils("DE", "Deutschland", new int[] { 9 }));
            allCountries.Add(new VatUtils("EE", "Estland", new int[] { 9 }));
            allCountries.Add(new VatUtils("FI", "Finnland", new int[] { 8 }));
            allCountries.Add(new VatUtils("FR", "Frankreich", new int[] { 11 }));
            allCountries.Add(new VatUtils("EL", "Griechenland", new int[] { 9 }));
            allCountries.Add(new VatUtils("IE", "Irland", new int[] { 8, 9 }));
            allCountries.Add(new VatUtils("IT", "Italien", new int[] { 11 }));
            allCountries.Add(new VatUtils("HR", "Kroatien", new int[] { 11 }));
            allCountries.Add(new VatUtils("LV", "Lettland", new int[] { 11 }));
            allCountries.Add(new VatUtils("LT", "Litauen", new int[] { 9, 12 }));
            allCountries.Add(new VatUtils("LU", "Luxemburg", new int[] { 8 }));
            allCountries.Add(new VatUtils("MT", "Malta", new int[] { 8 }));
            allCountries.Add(new VatUtils("NL", "Niederlande", new int[] { 12 }));
            allCountries.Add(new VatUtils("XI", "Nordirland", new int[] { 9 }));
            allCountries.Add(new VatUtils("ATU", "Österreich", new int[] { 8 }));
            allCountries.Add(new VatUtils("PL", "Polen", new int[] { 10 }));
            allCountries.Add(new VatUtils("PT", "Portugal", new int[] { 9 }));
            allCountries.Add(new VatUtils("RO", "Rumänien", new int[] { 10 }));
            allCountries.Add(new VatUtils("SE", "Schweden", new int[] { 12 }));
            allCountries.Add(new VatUtils("SK", "Slowakei", new int[] { 9, 10 }));
            allCountries.Add(new VatUtils("SI", "Slowenien", new int[] { 8 }));
            allCountries.Add(new VatUtils("ES", "Spanien", new int[] { 9 }));            
            allCountries.Add(new VatUtils("CZ", "Tschechische Republik", new int[] { 8, 9, 10 }));
            allCountries.Add(new VatUtils("HU", "Ungarn", new int[] { 8 }));
            allCountries.Add(new VatUtils("CY", "Zypern", new int[] { 9 }));

            return allCountries;
        }

        /// <summary>
        /// creates a List of all Countries, splits the VAT into countryCode / digits after countryCode and checks if the VAT is valid
        /// DOES NOT WORK CURRENTLY FOR Countries where countryCode length is > 2 , except ATU for AUSTRIA
        /// </summary>
        /// <param name="vat"></param>
        /// <returns>Vat object with Country code, Country name, VatNumber and boolean if it is valid</returns>
        public static Vat checkVat(string vat)
        {
            var allCountries = VatUtils.createAllCountriesList();            

            var responseObject = new Vat();

            string countryCode = "";
            string digits = "";

            foreach (char c in vat)
            {
                if (Char.IsDigit(c))
                {
                    break;
                }
                countryCode += c;
            }

            digits = vat.Substring(countryCode.Length);
            digits = RemoveSpecialCharacters(digits);

            foreach (var country in allCountries)
            {
                if (country.CountryCode.Contains(countryCode) && country.Digits.Contains(digits.Length))
                {
                    responseObject.CountryCode = countryCode;
                    responseObject.CountryName = country.CountryName;
                    responseObject.VatNumber = vat;
                    responseObject.isVatValid = true;
                }
                
                if(!responseObject.isVatValid)
                {
                    responseObject.CountryCode = countryCode;
                    if (country.CountryCode.Contains(countryCode))
                    {
                        responseObject.CountryName = country.CountryName;
                    }
                    responseObject.VatNumber = vat;
                }
            }

            return responseObject;
        }

        /// <summary>
        /// removes all special characters from the vat
        /// </summary>
        /// <param name="str"></param>
        /// <returns>vat digits after Country code without special characters</returns>
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}