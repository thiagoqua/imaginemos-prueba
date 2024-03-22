using backend.Models.DTOs;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
    [Route("api/ventas/{sellId}/detalles")]
    [ApiController]
    public class SellDetailController : ControllerBase {
        private readonly ISellDetailService _service;

        public SellDetailController(ISellDetailService service){
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] long sellId) {
            try {
                List<SellDetailDTO>? res = await _service.Get(sellId);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(
            [FromRoute] long sellId,
            [FromRoute] long id
        ) {
            try {
                SellDetailDTO? res = await _service.GetById(id,sellId);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            [FromRoute] long sellId,
            [FromRoute] long id,
            [FromBody] SellDetailDTO dto
        ) {
            try {
                SellDetailDTO? res = await _service.Update(id, sellId, dto);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(
            [FromRoute] long sellId,
            [FromRoute] long id
        ) {
            try {
                await _service.Delete(id, sellId);

                return Ok();
            } catch(ArgumentException) {
                return BadRequest();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
