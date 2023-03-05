using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientLourdBloc.Utils
{
    internal static class ColorUtils
    {
        public static string GrayBackColor = "#42414d";

        public static Color ToArgb(this string color)
        {
            try
            {
                // Séparation des valeurs rgb
                string hexaRed = color.Substring(1, 2);
                string hexaGreen = color.Substring(3, 2);
                string hexaBlue = color.Substring(5, 2);

                // Convert.ToInt32() pour changer les valeurs hexadécimales en décimales.
                // Source : https://www.includehelp.com/dot-net/convert-a-hexadecimal-value-to-decimal-in-c-sharp.aspx
                return Color.FromArgb(Convert.ToInt32(hexaRed, 16), Convert.ToInt32(hexaGreen, 16), Convert.ToInt32(hexaBlue, 16));
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur", "Erreur lors du passage de la couleur en format décimal : " + e.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Gray;
            }
        }
    }
}
