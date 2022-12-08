using Microsoft.AspNetCore.Mvc;
using MVC_ASP.Models;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace MVC_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Helper helper;
        // Object DataSet
        DataSet ds;
        // Object List of NpgsqlParameter
        NpgsqlParameter[] param;
        // Query ke Database
        string query;

        public HomeController(ILogger<HomeController> logger)
        {
            // Inisialisasi Object
            helper = new Helper();
            ds = new DataSet();
            param = new NpgsqlParameter[] { };
            query = "";
            _logger = logger;
        }


        //public IActionResult Login(UserModel user)
        //{
        //    ds = new DataSet();
        //    param = new NpgsqlParameter[] {
        //    // Parameter untuk id dan username
        //    //new NpgsqlParameter("@tiket_id", user.tiket_id),
        //    new NpgsqlParameter("@username", user.username),
        //    new NpgsqlParameter("@password", user.password)
        //};


        //    query = "select username, password from akun where username= '@username' and password='@password';";
        //    if (user.username != null)
        //    {
        //        RedirectToAction("SignUp");
        //    }
        //    else
        //    {
        //        return RedirectToAction("SignUp");
        //    }
        //    helper.DBConn(ref ds, query, param);



        //    return View("SignUp");
        //}

    
        public IActionResult LoginAdmin(string username, string password)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM akun;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            List<UserModel> admins = new List<UserModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                UserModel admin = new UserModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                admin.id_akun = u.Field<Int32>(data.Columns[0]);
                admin.username = u.Field<string>(data.Columns[1]);
                admin.password = u.Field<string>(data.Columns[2]);
                // Menambahkan user ke users (List of User)
                admins.Add(admin);
            }

            ViewData["data"] = admins;

            bool berhasil = true;
            foreach (var admin in admins) 
            {
                if (admin.username == username && admin.password == password)
                {
                    Console.WriteLine("Login Berhasil");
                    berhasil = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Cek Kembali username dan password Anda!");
                    berhasil = false;
                }
            }

            switch (berhasil)
            {
                case true:
                    return RedirectToAction("MainDashboard");
                case false:
                    return RedirectToAction("Index");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignUpAdmin(SignUp user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@username", user.username),
            new NpgsqlParameter("@email", user.email),
            new NpgsqlParameter("@password", user.password)
        };

            query = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password);";
            helper.DBConn(ref ds, query, param);



            return RedirectToAction("MainDashboard");
        }

        
        public IActionResult PilihTiket(tiket user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@dari", user.dari),
            new NpgsqlParameter("@ke", user.ke),
            new NpgsqlParameter("@tanggal", user.tanggal_berangkat),
            //new NpgsqlParameter("@tanggal", user.tanggal_berangkat),



        };

            //query = "INSERT INTO dari () VALUES (@dari, @ke, @jumlah, @tanggal, @kelas);";
            query = "select dari, ke, tanggal_berangkat from tiket where dari = '@dari' and ke = '@ke' and tanggal_berangkat = '@tanggal'";
            //query = "select t.id_tiket, m.nama_maskapai, h.dari_kota, h.ke_kota, d.durasi, d.tiba, p.tanggal_terbang, h.harga from tiket t, maskapai m, harga h, durasi_terbang d, pilih_tiket p";
            helper.DBConn(ref ds, query, param);



            return RedirectToAction("cariTiket1");
        }

        public IActionResult Index()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM akun;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            List<UserModel> users = new List<UserModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                UserModel user = new UserModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                //user.tiket_id = u.Field<Int32>(data.Columns[0]);
                //user.user_nama = u.Field<string>(data.Columns[1]);
                //user.user_age = u.Field<Int32>(data.Columns[2]);
                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;

            return View();
        }
        //public IActionResult MainDashboard(pemesanan_tiket user)
        //{
        //    // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
        //    ds = new DataSet();
        //    param = new NpgsqlParameter[] { };

        //    // Query Select
        //    query = "SELECT * FROM pemesanan_tiket;";
        //    // Panggil DBConn untuk eksekusi Query
        //    helper.DBConn(ref ds, query, param);

        //    // List of User untuk menampung data user
        //    List<UserModel> users = new List<UserModel>();
        //    // Mengambil value dari tabel di index 0
        //    var data = ds.Tables[0];

        //    // Perulangan untuk mengambil instance tiap baris dari tabel
        //    foreach (DataRow u in data.Rows)
        //    {
        //        // Membuat object User baru
        //        UserModel user = new UserModel();
        //        // Mengisi id dan username dari object user dengan nilai dari database
        //        //user.tiket_id = u.Field<Int32>(data.Columns[0]);
        //        //user.user_nama = u.Field<string>(data.Columns[1]);
        //        //user.user_age = u.Field<Int32>(data.Columns[2]);
        //        // Menambahkan user ke users (List of User)
        //        users.Add(user);
        //    }

        //    ViewData["data"] = users;

        //    return View();
        //}
        public IActionResult cariTiket(cekOrder1 user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@id_tiket", user.id_tiket),
            new NpgsqlParameter("@username", user.username),
            new NpgsqlParameter("@bangku", user.bangku)
        };

            query = "INSERT INTO cekorder (id_tiket, username, bangku) VALUES (@id_tiket, @username, @bangku);";
            helper.DBConn(ref ds, query, param);



            return RedirectToAction("beliTiket");
        }



        public IActionResult cariTiket1()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM tiket;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            List<tiket> users = new List<tiket>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                tiket user = new tiket();
                // Mengisi id dan username dari object user dengan nilai dari database
                user.id_tiket = u.Field<int>(data.Columns[0]);
                user.no_penerbangan = u.Field<int>(data.Columns[1]);
                user.maskapai = u.Field<string>(data.Columns[2]);
                user.dari = u.Field<string>(data.Columns[3]);
                user.ke = u.Field<string>(data.Columns[4]);
                user.jam_keberangkatan = u.Field<TimeSpan>(data.Columns[5]);
                user.waktu_tiba = u.Field<TimeSpan>(data.Columns[6]);
                user.tanggal_berangkat = u.Field<DateTime>(data.Columns[7]);
                user.harga = u.Field<string>(data.Columns[8]);


                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;

            return View();
        }

        //public IActionResult pilih_tiket(int id)
        //{
        //    ds = new DataSet();
        //    param = new NpgsqlParameter[] {
        //    new NpgsqlParameter("@id_tiket", id)
        //};

        //    query = "DELETE FROM tiket WHERE tiket_id = @tiket_id;";
        //    helper.DBConn(ref ds, query, param);

        //    return RedirectToAction("Index");
        //}

        public IActionResult beliTiketAdmin(cekOrder1 user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@id_tiket", user.id_tiket),
            new NpgsqlParameter("@username", user.username),
            new NpgsqlParameter("@bangku", user.bangku)
        };

            query = "SELECT tiket.id_tiket, maskapai, dari, ke, jam_keberangkatan, waktu_tiba, tanggal_berangkat, harga, username, bangku FROM tiket INNER JOIN cekorder ON cekorder.id_tiket = tiket.id_tiket;";
            helper.DBConn(ref ds, query, param);



            return RedirectToAction("beliTiket");
        }

        public IActionResult MainDashboard()
        {
            return View();
        }
        public IActionResult editTiket(cekOrder1 user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            new NpgsqlParameter("@id_cekorder", user.id_cekorder),
            new NpgsqlParameter("@username", user.username),
            new NpgsqlParameter("@bangku", user.bangku)
        };

            query = "UPDATE cekorder SET username = @username, bangku = @bangku WHERE id_cekorder = @id_cekorder;";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("edit");

        }
        public IActionResult edit()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT tiket.id_tiket,id_cekorder, maskapai, dari, ke, jam_keberangkatan, waktu_tiba, tanggal_berangkat, harga, username, bangku FROM tiket INNER JOIN cekorder ON cekorder.id_tiket = tiket.id_tiket;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            //List<tiket> users = new List<tiket>();
            //List<cekOrder1> admins = new List<cekOrder1>();
            List<cekOrder1> users = new List<cekOrder1>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                //tiket user = new tiket();
                //cekOrder1 admin = new cekOrder1();
                cekOrder1 user = new cekOrder1();
                // Mengisi id dan username dari object user dengan nilai dari database
                user.id_tiket = u.Field<int>(data.Columns[0]);
                //user.no_penerbangan = u.Field<int>(data.Columns[1]);
                user.id_cekorder = u.Field<int>(data.Columns[1]);
                user.maskapai = u.Field<string>(data.Columns[2]);
                user.dari = u.Field<string>(data.Columns[3]);
                user.ke = u.Field<string>(data.Columns[4]);
                user.jam_keberangkatan = u.Field<TimeSpan>(data.Columns[5]);
                user.waktu_tiba = u.Field<TimeSpan>(data.Columns[6]);
                user.tanggal_berangkat = u.Field<DateTime>(data.Columns[7]);
                user.harga = u.Field<string>(data.Columns[8]);
                user.username = u.Field<string>(data.Columns[9]);
                user.bangku = u.Field<string>(data.Columns[10]);



                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;

            return View();
        }


        public IActionResult delete()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT tiket.id_tiket, maskapai, dari, ke, jam_keberangkatan, waktu_tiba, tanggal_berangkat, harga, username, bangku FROM tiket INNER JOIN cekorder ON cekorder.id_tiket = tiket.id_tiket;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            //List<tiket> users = new List<tiket>();
            //List<cekOrder1> admins = new List<cekOrder1>();
            List<cekOrder1> users = new List<cekOrder1>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                //tiket user = new tiket();
                //cekOrder1 admin = new cekOrder1();
                cekOrder1 user = new cekOrder1();
                // Mengisi id dan username dari object user dengan nilai dari database
                user.id_tiket = u.Field<int>(data.Columns[0]);
                //user.no_penerbangan = u.Field<int>(data.Columns[1]);
                user.maskapai = u.Field<string>(data.Columns[1]);
                user.dari = u.Field<string>(data.Columns[2]);
                user.ke = u.Field<string>(data.Columns[3]);
                user.jam_keberangkatan = u.Field<TimeSpan>(data.Columns[4]);
                user.waktu_tiba = u.Field<TimeSpan>(data.Columns[5]);
                user.tanggal_berangkat = u.Field<DateTime>(data.Columns[6]);
                user.harga = u.Field<string>(data.Columns[7]);
                user.username = u.Field<string>(data.Columns[8]);
                user.bangku = u.Field<string>(data.Columns[9]);



                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;

            return View();
        }

        public IActionResult deleteTiket(cekOrder1 user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            new NpgsqlParameter("@id_tiket", user.id_tiket)
        };

            query = "DELETE FROM cekorder WHERE id_tiket = @id_tiket;";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("delete");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BeliTiketAdmin(tiket user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@dari", user.dari),
            new NpgsqlParameter("@ke", user.ke),
            new NpgsqlParameter("@tanggal", user.tanggal_berangkat),
            //new NpgsqlParameter("@tanggal", user.tanggal_berangkat),



        };

            //query = "INSERT INTO dari () VALUES (@dari, @ke, @jumlah, @tanggal, @kelas);";
            query = "select dari, ke, tanggal_berangkat from tiket where dari = '@dari' and ke = '@ke' and tanggal_berangkat = '@tanggal'";
            //query = "select t.id_tiket, m.nama_maskapai, h.dari_kota, h.ke_kota, d.durasi, d.tiba, p.tanggal_terbang, h.harga from tiket t, maskapai m, harga h, durasi_terbang d, pilih_tiket p";
            helper.DBConn(ref ds, query, param);



            return RedirectToAction("cariTiket1");
        }
        public IActionResult beliTiket()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT tiket.id_tiket, maskapai, dari, ke, jam_keberangkatan, waktu_tiba, tanggal_berangkat, harga, username, bangku FROM tiket INNER JOIN cekorder ON cekorder.id_tiket = tiket.id_tiket;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            //List<tiket> users = new List<tiket>();
            //List<cekOrder1> admins = new List<cekOrder1>();
            List<cekOrder1> users = new List<cekOrder1>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                //tiket user = new tiket();
                //cekOrder1 admin = new cekOrder1();
                cekOrder1 user = new cekOrder1();
                // Mengisi id dan username dari object user dengan nilai dari database
                user.id_tiket = u.Field<int>(data.Columns[0]);
                //user.no_penerbangan = u.Field<int>(data.Columns[1]);
                user.maskapai = u.Field<string>(data.Columns[1]);
                user.dari = u.Field<string>(data.Columns[2]);
                user.ke = u.Field<string>(data.Columns[3]);
                user.jam_keberangkatan = u.Field<TimeSpan>(data.Columns[4]);
                user.waktu_tiba = u.Field<TimeSpan>(data.Columns[5]);
                user.tanggal_berangkat = u.Field<DateTime>(data.Columns[6]);
                user.harga = u.Field<string>(data.Columns[7]);
                user.username = u.Field<string>(data.Columns[8]);
                user.bangku = u.Field<string>(data.Columns[9]);



                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;

            return View();
        }


    }



    }


    // Helper untuk koneksi ke DB
    class Helper
    {
        public void DBConn(ref DataSet ds, string query, NpgsqlParameter[] param)
        {
            // Data Source Name berisi credential dari database
            string dsn = "Host=localhost;Username=postgres;Password=0110;Database=Tiket_Pesawat;Port=5432";
            // Membuat koneksi ke db
            var conn = new NpgsqlConnection(dsn);
            // Command untuk eksekusi query
            var cmd = new NpgsqlCommand(query, conn);

            try
            {
                // Perulangan untuk menyisipkan nilai yang ada pada parameter ke query
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }
                // Membuka koneksi ke database
                cmd.Connection!.Open();
                // Mengisi ds dengan data yang didapatkan dari database
                new NpgsqlDataAdapter(cmd).Fill(ds);
                Console.WriteLine("Query berhasil dieksekusi");
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // Menutup koneksi ke database
                cmd.Connection!.Close();
            }

        }
    }