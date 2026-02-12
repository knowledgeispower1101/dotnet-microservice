using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Upload.Commands.UploadMedia;
namespace Shop.Api.Controllers;

[ApiController]
[Route("api/media")]
public class UploadFileControllers(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;

    [HttpPost("image")]
    public async Task<ActionResult> UploadImage([FromForm] UploadFormRequest request)
    {
        await using var stream = request.File.OpenReadStream();
        // var command = new UploadMediaCommand(
        //    stream,
        //    request.File.FileName,
        //    request.File.ContentType,
        //    request.File.Length
        // );
        // var result = await _mediator.Send(command);
        return Ok();
    }
}

public record UploadFormRequest(IFormFile File);
