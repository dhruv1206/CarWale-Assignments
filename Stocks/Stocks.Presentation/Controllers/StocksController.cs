using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mysqlx;
using Stocks.Application.Dtos;
using Stocks.Application.Interfaces.IServices;
using Stocks.Presentation.Helpers;

namespace Stocks.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FiltersDto filters)
        {
            if (ModelState.IsValid)
            {


                var stocks = await _stockService.GetAll(filters);
                return Ok(new ServerResponseDto<IEnumerable<StockDto>>
                {
                    Data = stocks
                });
            }
            return BadRequest(
                new ServerResponseDto<IEnumerable<StockDto>>
                {
                    Error = ModelStateHelper.GetErrors(ModelState)
                }
            );
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {

                var stock = await _stockService.GetById(id);
                if (stock == null)
                {
                    return NotFound(
                        new ServerResponseDto<StockDto>
                        {
                            Error = $"Stock with id {id} not found"
                        }
                    );
                }
                return Ok(new ServerResponseDto<StockDto>
                {
                    Data = stock
                });
            }
            else
            {
                return BadRequest(
                new ServerResponseDto<StockDto>
                {
                    Error = ModelStateHelper.GetErrors(ModelState)
                }
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockCreateDto stock)
        {
            if (ModelState.IsValid)
            {

                var createdStock = await _stockService.Create(stock);
                return Ok(new ServerResponseDto<StockDto>
                {
                    Data = createdStock,
                });
            }
            else
            {
                return BadRequest(
                new ServerResponseDto<StockDto>
                {
                    Error = ModelStateHelper.GetErrors(ModelState)
                }
                );
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StockUpdateDto stock)
        {
            if (ModelState.IsValid)
            {


                if (await _stockService.Exists(id))
                {
                    return NotFound(
                        new ServerResponseDto<StockDto>
                        {
                            Error = $"Stock with id {id} not found"
                        }
                    );
                }
                var updatedStock = await _stockService.Update(id, stock);
                return Ok(new ServerResponseDto<StockDto>
                {
                    Data = updatedStock,
                });
            }
            else
            {
                return BadRequest(
                new ServerResponseDto<StockDto>
                {
                    Error = ModelStateHelper.GetErrors(ModelState)
                }
                );
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {

                if (await _stockService.Exists(id))
                {
                    return NotFound(
                        new ServerResponseDto<StockDto>
                        {
                            Error = $"Stock with id {id} not found"
                        }
                    );
                }
                await _stockService.DeleteById(id);
                return Ok(new ServerResponseDto<StockDto>
                {
                });
            }
            else
            {
                return BadRequest(
                new ServerResponseDto<StockDto>
                {
                    Error = ModelStateHelper.GetErrors(ModelState)
                }
                );
            }
        }
    }
}