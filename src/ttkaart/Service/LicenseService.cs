using System;

namespace ttkaart.Service
{
    public static class LicenseService
    {
        public static string GetLicense(int rating, string region, int category)
        {
            string license = "-";

            if (category == 2)
                return license;

            if (rating == -9999)
                return license;
            
            int[] licenseRatings;

            if ( region.Equals("Landelijk Dames") )
			    licenseRatings = new int[] {505,415,345,255,185,-10000};
            else
			    licenseRatings = new int[] {660,560,505,415,345,255,185,-10000};

            int i = 0;

            while (licenseRatings[i] > rating)
                i++;

            license = Char.ConvertFromUtf32('A' + i);
            
            return license;
        }
    }
}