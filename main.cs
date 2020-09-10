using HtmlAgilityPack;
static void Main(string[] args)
{
 
    StreamReader accs_uw_file = new StreamReader(@"E:\Temp\accs.txt");
    var accs_uw = new List<string>();
    accs_uw.Add(accs_uw_file.ReadToEnd());
    sw = Stopwatch.StartNew();
    
    ThreadPool.SetMinThreads(30, 30);
    Parallel.ForEach(donor_accs, item =>
        {
            check_posts_count3(item);
        }
        );
        Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", sw.Elapsed.TotalSeconds);
}
 
public static void check_posts_count(string donor_acc)
{
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.DefaultConnectionLimit = 100000;
 
            
 
            string subStringStart = "edge_owner_to_timeline_media:{count:";
            string subStringEnd = ",page_info:{has_next_page:true,end_cursor";
            int indexOfSubstring = 0;
            int UnixTimeCurrent = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create($"https://www.instagram.com/{donor_acc}/");
        request.CookieContainer = new CookieContainer();
 
            request.Method = WebRequestMethods.Http.Get;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 100000;
            request.ContentType = "application/x-www-form-urlencoded";
 
            WebResponse myResponse = request.GetResponse();
 
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.Load(myResponse.GetResponseStream(), Encoding.GetEncoding(1251));
            string text = htmlDoc.DocumentNode.InnerHtml;
            string sText = Regex.Replace(text, "\"", "");
            indexOfSubstring = sText.IndexOf(subStringStart);
            sText = sText.Substring(indexOfSubstring);
            indexOfSubstring = sText.IndexOf(subStringEnd);
            sText = sText.Substring(0, indexOfSubstring);
            sText = sText.Substring(subStringStart.Length);
            UnixTimeCurrent = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            Console.WriteLine($"--{donor_acc}-----------{sText}---------");
            TimeSpend = DateTime.Now - TimeNow;
            TotalSecondes = TimeSpend.TotalSeconds;
}
