namespace MVC_ASP.Models
{
    public class UserModel
    {
        public int id_akun { get; set; }
        public string? username { get; set; }

        public string? password { get; set; }


    }

  

    //public class data_pemesanan
    //{
    //    public int id_dataPemesanan { get; set; }
    //    public string nama_depan { get; set; }
    //    public string nama_belakang { get; set; }
    //    public string no_hp { get; set; }
    //    public string email { get; set; }

    //}

    public class pemesanan_tiket
    {
        public int id_pemesananTiket { get; set; }
        public string? dari { get; set; }
        public string? ke { get; set; }
        public int jumlah_penumpang { get; set; }
        public DateOnly? tanggal_pergi { get; set; }
        public string? kelas_penerbangan { get; set; }
        public string? bangku { get; set; }
        public string? username { get; set; }

    }
  
}


