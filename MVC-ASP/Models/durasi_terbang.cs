namespace MVC_ASP.Models
{
    public class durasi_terbang
    {
        public int id_durasi { get; set; }
        public TimeSpan durasi { get; set; }
        public TimeSpan tiba { get; set; }
        public string? ke_kota {get; set;}
    }
}
