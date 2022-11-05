using System.Net;
using Microsoft.AspNetCore.Mvc;
using Wiinf_Studie.API.Data;
using Wiinf_Studie.API.Models;

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
    [ProducesResponseType(typeof(IEnumerable<DoublePaymentPair>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _candidatesRepository.GetDoublePaymentPairsIncludingCandidates();

        return Ok(result);
    }

    /// <summary>
    /// Returns a double payment candidate by id.
    /// </summary>
    /// <param name="pairId">The id of the pair to return.</param>
    [HttpGet("{pairId}")]
    [ProducesResponseType(typeof(DoublePaymentPair), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int pairId)
    {
        var result = await _candidatesRepository.GetDoublePaymentPairByIdIncludingCandidates(pairId);
        return Ok(result);
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
