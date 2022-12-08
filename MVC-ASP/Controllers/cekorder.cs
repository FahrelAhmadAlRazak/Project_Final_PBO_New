using System.Data;
using Microsoft.AspNetCore.Mvc;
using MVC_ASP.Models;
using Npgsql;

namespace MVC_ASP.Controllers
{
    public class cekorder : Controller
    {
        private readonly ILogger<cekorder> _logger;
        Helper helper;
        // Object DataSet
        DataSet ds;
        // Object List of NpgsqlParameter
        NpgsqlParameter[] param;
        // Query ke Database
        string query;

        public cekorder(ILogger<cekorder> logger)
        {
            // Inisialisasi Object
            helper = new Helper();
            ds = new DataSet();
            param = new NpgsqlParameter[] { };
            query = "";
            _logger = logger;
        }
        public IActionResult cekOrder(UserModel user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            //new NpgsqlParameter("@tiket_id", user.tiket_id),
            new NpgsqlParameter("@username", user.username),
            new NpgsqlParameter("@password", user.password)
        };


            query = "select username, password from akun where username= '@username' and password='@password';";
            if (user.username != null)
            {
                RedirectToAction("SignUp");
            }
            else
            {
                return RedirectToAction("MainDashboard");
            }
            helper.DBConn(ref ds, query, param);



            return View("MainDashboard");
        }
    }
}
