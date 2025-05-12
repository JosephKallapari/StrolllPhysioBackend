namespace StrolllPhysioBackend.Common
{
    public record ExerciseRoutine(
     int Id,
     string PatientId,
     int Age,
     string Condition,
     List<Exercises> PrescribedExercises
    );
    public record Exercises(
                 string ExerciseName,
     string Description,
     int Repetitions,
     int Sets
        );
    public record SessionResult (
    
        string SessionId,
        string PatientId,
        string Date,
        int Duration,
        List<Exercise> Exercises,
        List<MissedExercise> MissedExercises,
        HeartRate HeartRateBPM,
        double ConsistencyScore,
        int CaloriesBurned
    );

    public record Exercise (
        string ExerciseName,
        int Repetitions,
        int Sets,
        int MissedReps
     );

    public record MissedExercise(string ExerciseName,string Reason);

    public record HeartRate(int Average,int Max);


}
