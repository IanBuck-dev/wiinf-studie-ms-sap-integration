using Microsoft.AspNetCore.Mvc;
using Wiinf_Studie.API.Data;

namespace Wiinf_Studie.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DoublePaymentsController : ControllerBase
{
    private readonly ILogger<DoublePaymentsController> _logger;
    private readonly CandidatesRepository _candidatesRepository;

    public DoublePaymentsController(
        ILogger<DoublePaymentsController> logger,
        CandidatesRepository candidatesRepository)
    {
        _logger = logger;
        _candidatesRepository = candidatesRepository;
    }

    /// <summary>
    /// Returns all double payment candidates for the current client.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _candidatesRepository.GetDoublePaymentPairs();
        // Todo: Implement
        return Ok(result);
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
