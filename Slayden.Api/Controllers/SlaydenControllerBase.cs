using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Slayden.Api.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Consumes("application/json")]
public class SlaydenControllerBase : ControllerBase { }
