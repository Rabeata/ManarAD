using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreServer.Settings
{

    public static class ServerSettings
    {

        private static readonly string _AuthUrl = "http://ischool.org.il/auth/";// "http://192.168.1.3:5002";
        private static readonly string _MyUrl = "http://ischool.org.il";
        private static readonly string _ClientId = "mvc";
        private static readonly string _ClientSecret = "secret";
       // private static readonly string _EncDecKey = @"0@rMsL8!9DtV]x6A2$f#";

        private static readonly string _EncDecKey = @"0@rMsL8!";

        public static string AuthUrl
        {
            get
            {
                return _AuthUrl;
            }
        }
        public static string MyUrl
        {
            get
            {
                return _MyUrl;
            }
        }
        public static string ClientId
        {
            get
            {
                return _ClientId;
            }
        }
        public static string ClientSecret
        {
            get
            {
                return _ClientSecret;
            }
        }
        public static string EncDecKey
        {
            get
            {
                return _EncDecKey;
            }
        }
    }
}
