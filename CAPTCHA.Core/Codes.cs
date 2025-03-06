namespace CAPTCHA.Core
{
    /// <summary>
    /// All captcha response codes
    /// </summary>
    public static class Codes
    {
        public static string WRONG_ANSWER = "WRONG_ANSWER";

        public static string NOT_FOUND = "NOT_FOUND";

        public static string EXPIRED = "EXPIRED";

        public static string MAX_ATTEMPTS = "MAX_ATTEMPTS";

        public static string USED = "USED";

        public static string INTERNAL_CAPTCHA_ISSUE = "INTERNAL_CAPTCHA_ISSUE";
    }
}
