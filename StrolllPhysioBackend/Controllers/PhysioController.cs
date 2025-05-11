using Microsoft.AspNetCore.Mvc;

namespace StrolllPhysioBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysioController : ControllerBase
    {
        private static readonly List<string> ExerciseRoutines = new();
    }
}
