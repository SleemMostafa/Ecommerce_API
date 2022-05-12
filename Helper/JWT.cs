namespace Ecommerce_API.Helper
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public int DurationInDays { get; set; }

    }
}
