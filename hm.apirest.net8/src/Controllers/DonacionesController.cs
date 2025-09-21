using Microsoft.AspNetCore.Mvc;
using ApiTrans.Services.ContextBilleterasVirtuales;
using ApiTrans.Services;


namespace ApiTrans.Controllers {
    [Route("[controller]")]
    [ApiController]
    
    public class Donaciones: ControllerBase {
        private readonly DonacionService _donacionService;

        public Donaciones(DonacionService donacionService){
            _donacionService = donacionService;
        }
        [HttpGet]
        public async Task<ActionResult> Get() {
            await Task.Delay(1);
            return Ok(_donacionService.consulta());
        }
    
    }}