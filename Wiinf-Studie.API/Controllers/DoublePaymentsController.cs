using Microsoft.AspNetCore.Mvc;

namespace Wiinf_Studie.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DoublePaymentsController : ControllerBase
{
    private readonly ILogger<DoublePaymentsController> _logger;

    public DoublePaymentsController(ILogger<DoublePaymentsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns all double payment candidates for the current client.
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        // Todo: Implement
        return Ok();
    }

    /// <summary>
    /// Returns a double payment candidate by id.
    /// </summary>
    /// <param name="pairId">The id of the pair to return.</param>
    [HttpGet("{pairId}")]
    public IActionResult GetById(string pairId)
    {
        // Todo: Implement
        return Ok();
    }

    /// <summary>
    /// Updates a pair of double payment candidates.
    /// </summary>
    /// <param name="pairId"></param>
    /// <returns></returns>
    [HttpPut("{pairId}")]
    public IActionResult Update(string pairId) // Todo: Add update form
    {
        // Todo: Implement
        return Ok();
    }

    // No POST or DELETE, as this is not the focus part of the workflow.
}
