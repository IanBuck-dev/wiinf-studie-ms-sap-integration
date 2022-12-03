using System.Net;
using Microsoft.AspNetCore.Mvc;
using Wiinf_Studie.API.Data;
using Wiinf_Studie.API.Models;
using Wiinf_Studie.API.Utils;

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
    public async Task<IActionResult> Get([FromQuery] string? filterBy, [FromQuery] string? orderBy = "PairId asc")
    {
        var requestContext = HttpContext.GetRequestContext();

        var result = await _candidatesRepository.GetDoublePaymentPairsIncludingCandidates(requestContext);

        HttpContext.AddPagedContextInfo(requestContext);

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
    [HttpPost("{pairId}/changeJudgement")]
    public async Task<IActionResult> ChangeJudgement(int pairId, [FromBody] ChangeJudgementForm form)
    {
        // Check if pair exists.
        var pair = await _candidatesRepository.GetDoublePaymentPairByIdIncludingCandidates(pairId);

        if (pair is null) return NotFound();

        var result = await _candidatesRepository.ChangeJudgementOfDoublePaymentPair(pairId, form.Judgement!);
        return Ok(result);
    }

    // No POST or DELETE, as this is not the focus part of the workflow.
}
