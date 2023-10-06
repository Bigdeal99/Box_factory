using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;


public class BoxController : ControllerBase
{

    private readonly ILogger<BoxController> _logger;
    private readonly BoxService _boxService;

    public BoxController(ILogger<BoxController> logger,
        BoxService boxService)
    {
        _logger = logger;
        _boxService = boxService;
    }



    [HttpGet]
    [Route("/api/box")]
    public ResponseDto Get()
    {
        HttpContext.Response.StatusCode = 200;
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _boxService.GetBoxForFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/box")]
    public ResponseDto Post([FromBody] CreateBoxRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a box",
            ResponseData = _boxService.CreateBox(dto.BoxName, dto.BoxWeight)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/box/{boxId}")]
    public ResponseDto Put([FromRoute] int boxId,
        [FromBody] UpdateBoxRequestDto dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated",
            ResponseData =
                _boxService.UpdateBox(dto.BoxId, dto.BoxName, dto.BoxWeight)
        };

    }

    [HttpDelete]
    [Route("/api/box/{boxId}")]
    public ResponseDto Delete([FromRoute] int boxId)
    {
        _boxService.DeleteBox(boxId);
        return new ResponseDto()
        {
            MessageToClient = "Succesfully deleted"
        };

    }
}


