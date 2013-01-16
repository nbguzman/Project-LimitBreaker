//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

public partial class Layer2Container : ObjectContext
{
    public const string ConnectionString = "name=Layer2Container";
    public const string ContainerName = "Layer2Container";

    #region Constructors

    public Layer2Container()
        : base(ConnectionString, ContainerName)
    {
        this.ContextOptions.LazyLoadingEnabled = true;
    }

    public Layer2Container(string connectionString)
        : base(connectionString, ContainerName)
    {
        this.ContextOptions.LazyLoadingEnabled = true;
    }

    public Layer2Container(EntityConnection connection)
        : base(connection, ContainerName)
    {
        this.ContextOptions.LazyLoadingEnabled = true;
    }

    #endregion

    #region ObjectSet Properties

    public ObjectSet<Exercise> Exercises
    {
        get { return _exercises  ?? (_exercises = CreateObjectSet<Exercise>("Exercises")); }
    }
    private ObjectSet<Exercise> _exercises;

    public ObjectSet<ScheduledExercise> ScheduledExercises
    {
        get { return _scheduledExercises  ?? (_scheduledExercises = CreateObjectSet<ScheduledExercise>("ScheduledExercises")); }
    }
    private ObjectSet<ScheduledExercise> _scheduledExercises;

    public ObjectSet<LoggedExercise> LoggedExercises
    {
        get { return _loggedExercises  ?? (_loggedExercises = CreateObjectSet<LoggedExercise>("LoggedExercises")); }
    }
    private ObjectSet<LoggedExercise> _loggedExercises;

    public ObjectSet<SetAttributes> SetAttributes
    {
        get { return _setAttributes  ?? (_setAttributes = CreateObjectSet<SetAttributes>("SetAttributes")); }
    }
    private ObjectSet<SetAttributes> _setAttributes;

    public ObjectSet<ExerciseGoal> ExerciseGoals
    {
        get { return _exerciseGoals  ?? (_exerciseGoals = CreateObjectSet<ExerciseGoal>("ExerciseGoals")); }
    }
    private ObjectSet<ExerciseGoal> _exerciseGoals;

    public ObjectSet<Routine> Routines
    {
        get { return _routines  ?? (_routines = CreateObjectSet<Routine>("Routines")); }
    }
    private ObjectSet<Routine> _routines;

    public ObjectSet<ScheduledRoutine> ScheduledRoutines
    {
        get { return _scheduledRoutines  ?? (_scheduledRoutines = CreateObjectSet<ScheduledRoutine>("ScheduledRoutines")); }
    }
    private ObjectSet<ScheduledRoutine> _scheduledRoutines;

    public ObjectSet<LevelFormula> LevelFormulas
    {
        get { return _levelFormulas  ?? (_levelFormulas = CreateObjectSet<LevelFormula>("LevelFormulas")); }
    }
    private ObjectSet<LevelFormula> _levelFormulas;

    public ObjectSet<ExperienceAtrophy> ExperienceAtrophies
    {
        get { return _experienceAtrophies  ?? (_experienceAtrophies = CreateObjectSet<ExperienceAtrophy>("ExperienceAtrophies")); }
    }
    private ObjectSet<ExperienceAtrophy> _experienceAtrophies;

    public ObjectSet<Notification> Notifications
    {
        get { return _notifications  ?? (_notifications = CreateObjectSet<Notification>("Notifications")); }
    }
    private ObjectSet<Notification> _notifications;

    public ObjectSet<LimitBreaker> LimitBreakers
    {
        get { return _limitBreakers  ?? (_limitBreakers = CreateObjectSet<LimitBreaker>("LimitBreakers")); }
    }
    private ObjectSet<LimitBreaker> _limitBreakers;

    public ObjectSet<Statistics> Statistics
    {
        get { return _statistics  ?? (_statistics = CreateObjectSet<Statistics>("Statistics")); }
    }
    private ObjectSet<Statistics> _statistics;

    public ObjectSet<ExerciseExp> ExerciseExps
    {
        get { return _exerciseExps  ?? (_exerciseExps = CreateObjectSet<ExerciseExp>("ExerciseExps")); }
    }
    private ObjectSet<ExerciseExp> _exerciseExps;

    #endregion

}
