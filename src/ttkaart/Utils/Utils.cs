﻿using System;
using System.Collections.Generic;

namespace ttkaart.Utils
{
    public static class CrawlerDetection
    {
        private static readonly List<string> Crawlers = new List<string>()
            {
                "bot","crawler","spider","80legs","baidu","yahoo! slurp","ia_archiver","mediapartners-google",
                "lwp-trivial","nederland.zoek","ahoy","anthill","appie","arale","araneo","ariadne",            
                "atn_worldwide","atomz","bjaaland","ukonline","calif","combine","cosmos","cusco",
                "cyberspyder","digger","grabber","downloadexpress","ecollector","ebiness","esculapio",
                "esther","felix ide","hamahakki","kit-fireball","fouineur","freecrawl","desertrealm",
                "gcreep","golem","griffon","gromit","gulliver","gulper","whowhere","havindex","hotwired",
                "htdig","ingrid","informant","inspectorwww","iron33","teoma","ask jeeves","jeeves",
                "image.kapsi.net","kdd-explorer","label-grabber","larbin","linkidator","linkwalker",
                "lockon","marvin","mattie","mediafox","merzscope","nec-meshexplorer","udmsearch","moget",
                "motor","muncher","muninn","muscatferret","mwdsearch","sharp-info-agent","webmechanic",
                "netscoop","newscan-online","objectssearch","orbsearch","packrat","pageboy","parasite",
                "patric","pegasus","phpdig","piltdownman","pimptrain","plumtreewebaccessor","getterrobo-plus",
                "raven","roadrunner","robbie","robocrawl","robofox","webbandit","scooter","search-au",
                "searchprocess","senrigan","shagseeker","site valet","skymob","slurp","snooper","speedy",
                "curl_image_client","suke","www.sygol.com","tach_bw","templeton","titin","topiclink","udmsearch",
                "urlck","valkyrie libwww-perl","verticrawl","victoria","webscout","voyager","crawlpaper",
                "webcatcher","t-h-u-n-d-e-r-s-t-o-n-e","webmoose","pagesinventory","webquest","webreaper",
                "webwalker","winona","occam","robi","fdse","jobo","rhcs","gazz","dwcp","yeti","fido","wlm",
                "wolp","wwwc","xget","legs","curl","webs","wget","sift","cmc"
            };

        public static bool IsCrawler(string UserAgent)
        {
            if (String.IsNullOrEmpty(UserAgent))
                return false;

            string ua = UserAgent.ToLower();

            return Crawlers.Exists(x => ua.Contains(x));
        }
    }
}