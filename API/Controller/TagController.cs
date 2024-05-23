using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagCommands _tagCommands;
        private readonly ITagQuieries _tagQueries;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagCommands tagCommands, ITagQuieries tagQueries, ILogger<TagController> logger)
        {
            _tagCommands = tagCommands;
            _tagQueries = tagQueries;
            _logger = logger;
        }
    }
}
