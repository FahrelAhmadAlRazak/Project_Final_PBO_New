namespace MVC_ASP.Models
{
    public class cekOrder1
    {
        public int id_cekorder { get; set; }
        public int id_tiket { get; set; }
        public int no_penerbangan { get; set; }
        public string? maskapai { get; set; }
        public string? dari { get; set; }
        public string? ke { get; set; }
        public TimeSpan jam_keberangkatan { get; set; }
        public TimeSpan waktu_tiba { get; set; }
        public DateTime tanggal_berangkat { get; set; }
        public string? harga { get; set; }
        
        public string? username { get; set; }
        public string? bangku { get; set; }
    }
}
