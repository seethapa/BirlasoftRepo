using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        private readonly VotingService _votingService;
        private readonly IVoterRepository _repo;

        public VoterController(IVoterRepository repo)
        {
            _repo = repo;
        }


        //[HttpPost("vote")]
        //public async Task<IActionResult> Vote(CastVoteRequest request)
        //{
        //    try
        //    {
        //        await _votingService.CastVote(
        //            request.CandidateId,
        //            request.VoterId
        //        );

        //        return Ok(new { message = "Vote cast successfully" });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { error = ex.Message });
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(new { error = ex.Message });
        //    }
        //}

        [HttpPost("/api/addvote")]
        public async Task<IActionResult> Add(Voter voter)
        {
            voter.HasVoted = false;
            await _repo.AddAsync(voter);
            return StatusCode(201, voter);
        }

        [HttpGet("/api/getvoters")]
        public async Task<IActionResult> GetAll()
        {
            var voters = await _repo.GetAllAsync();
            return Ok(voters);
        }

    }
}
