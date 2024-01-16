using System.Diagnostics;

namespace HtmlToPdfHeadless
{
    /// <summary>
    /// the constructor for this class, takes the chrome executable path as a string parameter.
    /// the path looks like this: C:\Program Files\Google\Chrome\Application\chrome.exe
    /// </summary>
    public class Util
    {
        private  string _pathToChromeExecutable;

        public Util(string pathToChromeExecutable)
        {
            _pathToChromeExecutable = pathToChromeExecutable;
        }
    
        /// <summary>
        /// this method generates pdf asynchronously
        /// </summary>
        /// <param name="outputPath">
        ///this is the folder path where the generated pdf is saved
        /// </param>
        /// <param name="htmlText">
        ///the html text you want to convert to pdf
        /// </param>
        /// <returns>byte[]</returns>
        public async Task<byte[]> GeneratePdfFromHtmlTextAsync(string outputPath,string htmlText)
        {
            try
            {
                var fileName_ = AlphaNumeric(9);
                var htmlPath_ = Path.Combine(outputPath, $"{fileName_}.html");

                File.WriteAllText(htmlPath_, htmlText);

                var output = Path.Combine(outputPath, $"{fileName_}.pdf");

                using (var p = new Process())
                {
                  
                    //p.StartInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                    p.StartInfo.FileName = _pathToChromeExecutable;
                    p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} --no-pdf-header-footer --print-to-pdf-no-footer {htmlPath_}";
                    p.Start();
                    await p.WaitForExitAsync();
                }

                var count = 0;
                while (!File.Exists(output))
                {
                    count++;
                    if (count > 3)
                    {
                        return null;
                    }
                    await Task.Delay(100);
                }
                return await File.ReadAllBytesAsync(output);
            }
            catch (Exception e)
            {
                return null;
            }
        }
     
        /// <summary>
        /// this method generates pdf asynchronously
        /// </summary>
        /// <param name="outputPath">
        ///this is the folder path where the generated pdf is saved
        /// </param>
        /// <param name="pathToHtmlFile">
        ///this is the path to the html file you want to convert to pdf
        /// </param>
        /// <returns>byte[]</returns>
        public async Task<byte[]> GeneratePdfFromHtmlFileAsync(string outputPath,string pathToHtmlFile)
        {
            try
            {
                var fileName_ = AlphaNumeric(9);
                var output = Path.Combine(outputPath, $"{fileName_}.pdf");

                using (var p = new Process())
                {
                    var chromeExePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "chrome.exe");
                    //p.StartInfo.FileName = chromeExePath;
                    p.StartInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                    p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} --no-pdf-header-footer --print-to-pdf-no-footer {pathToHtmlFile}";
                    p.Start();
                    await p.WaitForExitAsync();
                }

                var count = 0;
                while (!File.Exists(output))
                {
                    count++;
                    if (count > 3)
                    {
                        return null;
                    }
                    await Task.Delay(100);
                }
                return await File.ReadAllBytesAsync(output);
            }
            catch (Exception e)
            {
                return null;
            }
        }
       
        /// <summary>
        /// this method generates pdf synchronously
        /// </summary>
        /// <param name="outputPath">
        ///this is the folder path where the generated pdf is saved
        /// </param>
        /// <param name="htmlText">
        ///the html text you want to convert to pdf
        /// </param>
        /// <returns>byte[]</returns>
        public byte[] GeneratePdfFromHtmlText(string outputPath,string htmlText)
        {
            try
            {
                var fileName_ = AlphaNumeric(9);
                var htmlPath_ = Path.Combine(outputPath, $"{fileName_}.html");

                File.WriteAllText(htmlPath_, htmlText);

                var output = Path.Combine(outputPath, $"{fileName_}.pdf");

                using (var p = new Process())
                {
                  
                    //p.StartInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                    p.StartInfo.FileName = _pathToChromeExecutable;
                    p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} --no-pdf-header-footer --print-to-pdf-no-footer {htmlPath_}";
                    p.Start();
                     p.WaitForExit();
                }

                var count = 0;
                while (!File.Exists(output))
                {
                    count++;
                    if (count > 3)
                    {
                        return null;
                    }
                     Task.Delay(100);
                }
                return  File.ReadAllBytes(output);
            }
            catch (Exception e)
            {
                return null;
            }
        }
      
        /// <summary>
        /// this method generates pdf synchronously
        /// </summary>
        /// <param name="outputPath">
        ///this is the folder path where the generated pdf is saved
        /// </param>
        /// <param name="pathToHtmlFile">
        ///this is the path to the html file you want to convert to pdf
        /// </param>
        /// <returns>byte[]</returns>
        public byte[] GeneratePdfFromHtmlFile(string outputPath,string pathToHtmlFile)
        {
            try
            {
                var fileName_ = AlphaNumeric(9);
                var output = Path.Combine(outputPath, $"{fileName_}.pdf");

                using (var p = new Process())
                {
                    var chromeExePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "chrome.exe");
                    //p.StartInfo.FileName = chromeExePath;
                    p.StartInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
                    p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} --no-pdf-header-footer --print-to-pdf-no-footer {pathToHtmlFile}";
                    p.Start(); 
                    p.WaitForExit();
                }

                var count = 0;
                while (!File.Exists(output))
                {
                    count++;
                    if (count > 3)
                    {
                        return null;
                    }
                    Task.Delay(100);
                }
                return  File.ReadAllBytes(output);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static string AlphaNumeric(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}