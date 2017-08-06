
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PHS.DB.ViewModels.Form
{
    public class AddressViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Blk")]
        public virtual string Blk { get; set; }

        [Display(Name = "Unit")]
        public virtual string Unit { get; set; }

        [Display(Name = "StreetAddress")]
        public virtual string StreetAddress { get; set; }

        [Display(Name = "Zip Code")]
        public virtual string ZipCode { get; set; }

        public static AddressViewModel Initialize()
        {
            return new AddressViewModel();
        }

        public static AddressViewModel Initialize(string oneLineOfAddress)
        {
            AddressViewModel result = new AddressViewModel();

            result.Blk = getValue(oneLineOfAddress, "Blk: ", ", Street: ");
            result.StreetAddress = getValue(oneLineOfAddress, ", Street: ", ", Unit: ");
            string[] array = Regex.Split(oneLineOfAddress, ", Unit: ");

            if (array.Length > 1)
            {
                result.Unit = array[1];
            }

            if (string.IsNullOrEmpty(result.Blk) 
                && string.IsNullOrEmpty(result.Unit)
                && string.IsNullOrEmpty(result.StreetAddress))
            {
                result.StreetAddress = oneLineOfAddress;
            }
            

            return result;
        }

        public string ConvertToOneLineAddress()
        {
            return "Blk: " + Blk + ", Street: " + StreetAddress + ", Unit: " + Unit;
        }

        private static string getValue(string stringmessage, string startWord, string endWord)
        {
            int pFrom = stringmessage.IndexOf(startWord) + startWord.Length;
            int pTo = stringmessage.LastIndexOf(endWord);

            return pFrom != -1 && pTo != -1 ? stringmessage.Substring(pFrom, pTo - pFrom) : null;
        }

    }
}