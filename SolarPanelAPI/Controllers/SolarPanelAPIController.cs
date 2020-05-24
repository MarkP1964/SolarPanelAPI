using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SolarPanelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SolarPanelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SolarPanelAPIController : Controller
    {
        private readonly SolarPanelAPIContext _context;

        public SolarPanelAPIController(SolarPanelAPIContext context)
        {
            _context = context;
        }

        [HttpGet, Route("DailyReadings")]
        public JsonResult DailyReadings()
        {
            return Json(_context.DailyReadings);
        }
        [HttpGet, Route("MonthlyView")]
        public JsonResult MonthlyView()
        {
            return Json(GetMonthlyView());
        }

        [HttpGet, Route("GetAnnualCumulativeView")]
        public JsonResult GetAnnualCumulativeView()
        {
            return Json(_context.MonthlyAnnualCumulativePivot);
        }

        [HttpGet, Route("MonthlyCumulativeView")]
        public JsonResult MonthlyCumulativeView()
        {
            List<DailyReadings> readings = GetMonthlyView().OrderBy(d => d.Date).ToList();
            List<DailyReadings> cumReadings = new List<DailyReadings>();

            double? power = 0.0;
            decimal? rebate = 0.0m;
            readings.ForEach(dr =>
            {
                if (dr.Date.Month == 1)
                {
                    power = dr.PowerGenerated;
                    rebate = dr.RebateGenerated;
                }
                else
                {
                    power = power + dr.PowerGenerated;
                    rebate = rebate + dr.RebateGenerated;
                }
                cumReadings.Add(new DailyReadings
                {
                    Date = dr.Date,
                    PowerGenerated = power,
                    RebateGenerated = rebate
                });
            });

            return Json(cumReadings);
        }

        [HttpGet, Route("AnnualView")]
        public JsonResult AnnualView()
        {
            return Json(GetAnnualView());
        }


        [HttpGet, Route("AnnualCumulativeView")]
        public JsonResult AnnualCumulativeView()
        {
            List<DailyReadings> readings = GetAnnualView().OrderBy(d => d.Date).ToList();
            List<DailyReadings> cumReadings = new List<DailyReadings>();

            double? power = 0.0;
            decimal? rebate = 0.0m;
            readings.ForEach(dr =>
            {
                power += dr.PowerGenerated;
                rebate += dr.RebateGenerated;
                cumReadings.Add(new DailyReadings
                {
                    Date = dr.Date,
                    PowerGenerated = power,
                    RebateGenerated = rebate
                });
            });

            return Json(cumReadings);

        }
        [HttpPost, Route("SetReading")]
        public ActionResult SetReading([FromBody] JObject reading)
        {
            try
            {
                dynamic newReading = reading;
                DateTime date = newReading.Date;
                decimal value = newReading.Reading;
                SqlParameter[] sqlParameters = { new SqlParameter("@reading", value), new SqlParameter("@currDate", date) };

                _context.Database.ExecuteSqlRaw($"exec SetDailyReadingAndFillInBlanks @reading, @currDate", sqlParameters);
            }
            catch (Exception ex)
            {
                return Json(new JObject { { "Error", ex.Message } });
            }
            return Json(reading);
        }

        #region get data as rollups
        private List<DailyReadings> GetMonthlyView()
        {
            return _context.DailyReadings
            .GroupBy(d => new { Year = d.Date.Year, Month = d.Date.Month })
            .ToList()
            .Select(d => new DailyReadings
            {
                Date = new DateTime(d.Key.Year, d.Key.Month, 1),
                Reading = null,
                PowerGenerated = d.Sum(x => x.PowerGenerated),
                RebateGenerated = d.Sum(x => x.RebateGenerated)
            })
            .OrderBy(d => d.Date)
            .ToList<DailyReadings>();
        }

        private List<DailyReadings> GetAnnualView()
        {
            return _context.DailyReadings
            .GroupBy(d => new { Year = d.Date.Year })
            .ToList()
            .Select(d => new DailyReadings
            {
                Date = new DateTime(d.Key.Year, 1, 1),
                Reading = null,
                PowerGenerated = d.Sum(x => x.PowerGenerated),
                RebateGenerated = d.Sum(x => x.RebateGenerated)
            })
            .OrderBy(d => d.Date)
            .ToList<DailyReadings>();
        }
        #endregion

    }
}
