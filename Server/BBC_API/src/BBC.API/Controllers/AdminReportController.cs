using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.API.Helper.Attribute;
using BBC.Services.Services.AdminService;
using BBC.Services.Services.AdminService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredAuth]
    public class AdminReportController : ControllerBase
    {
        private readonly IAdminReportService _adminReportService;
        public AdminReportController(IAdminReportService adminReportService)
        {
            _adminReportService = adminReportService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(AdminReportWidget), 200)]
        [Route("AdminReport")]
        public async Task<IActionResult> AdminReport()
        {
            var result = await _adminReportService.Report();
            return Ok(result);
        }

    }
}