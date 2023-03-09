using Microsoft.AspNetCore.Mvc.Razor;

namespace AccountTransaction.WebUI.Extensions
{
    public static class RazorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string FormatMoney(this RazorPage page, decimal valor)
        {
            return FormatMoney(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static string FormatMoney(decimal valor)
        {
            return string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);
        }
    }
}
