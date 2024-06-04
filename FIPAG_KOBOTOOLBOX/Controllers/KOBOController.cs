using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.DTOs;

namespace FIPAG_KOBOTOOLBOX.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KOBOController : ControllerBase
    {
        IKOBOService _koboService;

        public KOBOController(IKOBOService koboService)
        {
            _koboService = koboService;
        }

        [HttpGet]
        [Route("getData")]
        public async Task<ActionResult<ResponseDTO>> GetResult(string nome)
        {
            var response = _koboService.GetResult(nome);
            return Ok(response);
        }
    }
}
