using System.Text.RegularExpressions;

namespace shared.StockManager.Shered
{
    public static class RegexUtils
    {
        public static bool IsValidImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            // Regex: começa com https:// e termina com .jpg ou .png
            string pattern = @"^https:\/\/.+\.(jpg|png)$";

            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidBrazilPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Regex: (XX) XXXX-XXXX ou (XX) 9XXXX-XXXX
            string pattern = @"^\(?\d{2}\)?\s9?\d{4}-\d{4}$";

            return Regex.IsMatch(phone, pattern);
        }
    }
}