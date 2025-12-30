using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _repo;

        public CandidatesController(ICandidateRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("/api/addcandidate")]
        public async Task<IActionResult> Create(Candidate request)
        {
            request.Id = Guid.NewGuid().ToString();
            request.Type = request.Type.Trim().ToLowerInvariant();
        

            await _repo.AddAsync(request);

            return Ok(request);
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var candidate = await _repo.GetAsync(id);
            if (candidate == null)
                return NotFound();

            return Ok(candidate);
        }


        [HttpGet("/api/getcandidates")]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _repo.GetAllAsync();
            return Ok(candidates);
        }
    }

}
