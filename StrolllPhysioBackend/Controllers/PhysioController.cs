using Microsoft.AspNetCore.Mvc;
using StrolllPhysioBackend.Common;

namespace StrolllPhysioBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysioController : ControllerBase
    {
        private static readonly List<ExerciseRoutine> ExerciseRoutines = new();
        private static readonly List<SessionResult> SessionResults = new();

        [HttpPost("prescribe")]
        public IActionResult PrescribeExercise([FromBody] ExerciseRoutine exerciseRoutine)
        {
            var existingRoutine=ExerciseRoutines.FirstOrDefault(r=>r.PatientId==exerciseRoutine.PatientId);
            if (existingRoutine!=null)
            {
                ExerciseRoutines.Remove(existingRoutine);
                var updatedRoutine = existingRoutine with
                {
                    PrescribedExercises = exerciseRoutine.PrescribedExercises,
                    Condition = exerciseRoutine.Condition,
                    Age = exerciseRoutine.Age
                };
                ExerciseRoutines.Add(updatedRoutine);
                return Ok(new { Message = "Exercise routine updated successfully", exerciseRoutine = updatedRoutine });
               
            }
            exerciseRoutine=exerciseRoutine with { Id = ExerciseRoutines.Count+1 };
            ExerciseRoutines.Add(exerciseRoutine);

            return Ok( exerciseRoutine );


        }

        [HttpGet("physiosessionresults/{patientId}")]
        public IActionResult GetSessionResultsByPatientId(string patientId)
        {
            //Retrieve session results for the given patient Id
            var resultsForPatient = SessionResults.Where(r => r.PatientId == patientId).ToList();

            //Retrieve exercise routines for the given patient Id
            var routinesForPatient = ExerciseRoutines.Where(r => r.PatientId == patientId).ToList();

            //Retrieve session results for the given patient Id
            if(!resultsForPatient.Any() && routinesForPatient.Any())
            {
                var random=new Random();
                foreach(var routine in routinesForPatient)
                {
                    var exercises=routine.PrescribedExercises.Select(p=>new Exercise(
                        ExerciseName:p.ExerciseName,
                        Repetitions:p.Repetitions-1,
                        Sets:p.Sets,
                        MissedReps:1 //Default Value
                        )).ToList();
                    var sessionResult = new SessionResult(
                        SessionId: Guid.NewGuid().ToString(),
                        PatientId: routine.PatientId,
                        Date: DateTime.Now.ToString("yyyy-MM-dd"),
                        Duration: 30,
                        Exercises: exercises,
                        MissedExercises: new List<MissedExercise>(),
                        HeartRateBPM:new HeartRate(random.Next(70,100),random.Next(100,140)),
                        ConsistencyScore:Math.Round(random.NextDouble()*10,1),
                        CaloriesBurned:random.Next(100,500)
                        );
                    SessionResults.Add(sessionResult);
                    resultsForPatient.Add(sessionResult);
                }
            }
            if (!resultsForPatient.Any())
            {
                return NotFound($"No session results or exercise routines found for patient ID:{patientId}");
            }
            return Ok(resultsForPatient);
        }

    }
}
