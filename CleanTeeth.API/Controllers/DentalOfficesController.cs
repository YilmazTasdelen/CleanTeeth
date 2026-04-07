using CleanTeeth.API.DTOs.DentalOffices;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers
{
    [ApiController]
    [Route("api/dentaloffices")]
    public class DentalOfficesBaseontroller : ControllerBase
    {
        private readonly IMediator mediator;
        public DentalOfficesBaseontroller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDentalOfficeDTO dto)
        {
            var command = new CreateDentalOfficeCommand
            {
                Name = dto.Name
            };

            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetDentalOfficeDetailQuery { Id = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
