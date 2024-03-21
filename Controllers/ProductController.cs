using backend.Models.DTOs;
using backend.Models.Requests;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
    [Route("api/productos")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductService _service;

        public ProductController(IProductService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? filter) {
            try {
                var res = await _service.Get(filter);

                return Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id) {
            try {
                ProductDTO? res = await _service.GetById(id);

                return res == null
                    ? BadRequest()
                    : Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductRequest req
        ) {
            try {
                ProductDTO res = await _service.Create(req);

                return Ok(res);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            [FromRoute] long id,
            [FromBody] ProductDTO dto
        ) {
            try {
                await _service.Update(id, dto);
                
                return Ok();
            } catch(ArgumentException) {
                return BadRequest();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id) {
            try {
                await _service.Delete(id);

                return Ok();
            } catch(ArgumentException) {
                return BadRequest();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
