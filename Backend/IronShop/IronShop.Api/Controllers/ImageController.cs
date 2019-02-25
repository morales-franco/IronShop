using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFileService _fileService;

        public ImageController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/image
        [HttpGet("{filename}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(string filename)
        {
            var file = await _fileService.GetFileFromFileSystem(filename);

            if (!file.Success)
                return BadRequest("Error getting file");

            return File(file.Metadata, file.ContentType);

        }
    }
}