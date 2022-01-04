using OsigDrustvo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace OsigDrustvo.Controllers
{
    public class DefaultController : Controller
    {
        //public SqlConnection con;
        string connString = ConfigurationManager.ConnectionStrings["myConnStr"].ToString();


        // GET: Default
        public ActionResult Index()
        {
            List<Partner> partners = new List<Partner>();
            using (IDbConnection db = new SqlConnection(connString))
            {
                partners = db.Query<Partner>("SELECT * FROM[OsigDrustvo].[dbo].[Partners] ORDER BY CreatedAtUtc DESC, PartnerId DESC").ToList();
            }
            return View(partners);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        private bool IsExternalCodeExist(Partner partner)
        {
            bool exist;

            try
            {
                using (IDbConnection db = new SqlConnection(connString))
                {
                    string upit = "SELECT COUNT (ExternalCode) ExternalCode FROM [dbo].[Partners] WHERE ExternalCode=@ExternalCode";
                    exist = db.ExecuteScalar<int>(upit, new { partner.ExternalCode }) > 0;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return exist;

        }
        [HttpPost]
        public ActionResult Create(Partner partner)
        {
            if (IsExternalCodeExist(partner))
            {
                ModelState.AddModelError("ExternalCode", "External code already exist");
            }

            if (ModelState.IsValid)
            {
                try
                {

                    using (IDbConnection db = new SqlConnection(connString))
                    {
                        string sql = @"
INSERT INTO [dbo].[Partners]
(FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender) Values
(@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, CAST(GETUTCDATE() as date), @CreateByUser, @IsForeign, @ExternalCode, @Gender)";
                        var result = db.Execute(sql, new
                        {
                            partner.FirstName,
                            partner.LastName,
                            partner.Address,
                            partner.PartnerNumber,
                            partner.CroatianPin,
                            partner.PartnerTypeId,
                            partner.CreateByUser,
                            partner.IsForeign,
                            partner.ExternalCode,
                            partner.Gender
                        });
                        TempData["NewRow"] = partner.ExternalCode;
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                return View();
            }

        }

    }
}