namespace PMApp.Helper
{
    using System;
    using System.IO;

    public class FilesHelper
    {
        public static string StreamToBase64(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                
                input.CopyTo(ms);
                byte[] array = ms.ToArray();
                string img64 = Convert.ToBase64String(array);
                return img64;
            }
        }
    }

}
