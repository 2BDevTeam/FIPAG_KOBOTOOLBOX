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
        IKoboService _koboService;

        public KOBOController(IKoboService koboService)
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

        [HttpGet]
        [Route("registarCliente")]
        public async Task<ActionResult<ResponseDTO>> RegistarCliente(int id)
        {
            var response = _koboService.RegistarCliente(id);
            return Ok(response);
        }
    }
}

