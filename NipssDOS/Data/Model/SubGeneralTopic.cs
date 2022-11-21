namespace NipssDOS.Data.Model
{
    public class SubGeneralTopic
    {
        public long Id { get; set; }
        public string SunTheme { get; set; }
        public int SortOrder { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
    }
}
