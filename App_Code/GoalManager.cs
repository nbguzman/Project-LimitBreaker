﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoalManager
/// </summary>
public class GoalManager
{
	public GoalManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<ExerciseGoal> getUnachievedExerciseGoalsFromUser(string userName, int orderBy, string muscleGroup)
    {
        using (var context = new Layer2Container())
        {
            List<ExerciseGoal> goalSet;

            if (muscleGroup != "All Groups")
            {
                switch (orderBy)
                {
                    case 0:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false && s.Exercise.muscleGroups.Contains(muscleGroup)).OrderBy(o => o.Exercise.name).ToList();
                        break;
                    case 1:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false && s.Exercise.muscleGroups.Contains(muscleGroup)).OrderBy(o => o.id).ToList();
                        break;
                    default:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false && s.Exercise.muscleGroups.Contains(muscleGroup)).ToList();
                        break;
                }
            }

            else
            {
                switch (orderBy)
                {
                    case 0:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false).OrderBy(o => o.Exercise.name).ToList();
                        break;
                    case 1:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false).OrderBy(o => o.id).ToList();
                        break;
                    default:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == false).ToList();
                        break;
                }
            }

            foreach (ExerciseGoal eg in goalSet)
            {
                context.LoadProperty(eg, "Exercise");
            }

            return goalSet;
        }
    }

    public ExerciseGoal getUnachievedGoalByExerciseNameAndUserID(string name, int userID)
    {
        using (var context = new Layer2Container())
        {
            return context.ExerciseGoals.Where(s => s.Exercise.name == name && s.LimitBreaker.id == userID && s.achieved == false).FirstOrDefault();
        }
    }

    public ExerciseGoal getExerciseGoalByGoalID(int goalID)
    {
        using (var context = new Layer2Container())
        {
            return context.ExerciseGoals.Where(s => s.id == goalID).FirstOrDefault();
        }
    }

    public bool addNewExerciseGoal(int weight, double distance, int time, int reps, string userName, string exerciseName)
    {
        bool rc = false;

        using (var context = new Layer2Container())
        {
            try
            {
                ExerciseGoal newExerciseGoal = new ExerciseGoal();
                Exercise exercise = context.Exercises.Where(s => s.name == exerciseName).FirstOrDefault();
                LimitBreaker user = context.LimitBreakers.Where(s => s.username == userName).FirstOrDefault();

                newExerciseGoal.LimitBreaker = user;
                newExerciseGoal.Exercise = exercise;
                newExerciseGoal.weight = weight;
                newExerciseGoal.distance = distance;
                newExerciseGoal.time = time;
                newExerciseGoal.reps = reps;

                context.ExerciseGoals.AddObject(newExerciseGoal);
                context.SaveChanges();

                rc = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        return rc;
    }

    public string getExerciseNameWithinGoal(string userName, string exerciseName)
    {
        using (var context = new Layer2Container())
        {
            ExerciseGoal eg = context.ExerciseGoals.Where(s => s.Exercise.name == exerciseName && s.LimitBreaker.username == userName && s.achieved == false).FirstOrDefault();

            if (eg != null)
            {
                Exercise ex = eg.Exercise;
                return ex.name;
            }
            else
                return ""; 
        }
    }

    public Exercise getExerciseWithinGoal(int goalID)
    {
        using (var context = new Layer2Container())
        {
            ExerciseGoal eg = context.ExerciseGoals.Where(s => s.id == goalID).FirstOrDefault();

            if (eg != null)
            {
                Exercise ex = eg.Exercise;
                return ex;
            }
            else
                return null;
        }
    }

    public bool modifyGoalByGoalID(int goalID, int time, double distance, int weight, int reps)
    {
        bool rc = false;

        try
        {
            using (var context = new Layer2Container())
            {
                ExerciseGoal eg = context.ExerciseGoals.Where(s => s.id == goalID).FirstOrDefault();

                eg.time = time;
                eg.distance = distance;
                eg.weight = weight;
                eg.reps = reps;

                context.SaveChanges();
                rc = true;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
        }

        return rc;
    }

    public bool deleteGoalByGoalID(int goalID)
    {
        bool rc = false;

        try
        {
            using (var context = new Layer2Container())
            {
                ExerciseGoal eg = context.ExerciseGoals.Where(s => s.id == goalID).FirstOrDefault();

                context.ExerciseGoals.DeleteObject(eg);
                context.SaveChanges();
                rc = true;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
        }

        return rc;
    }

    public List<ExerciseGoal> getAchievedExerciseGoalsFromUser(string userName, int orderBy, string muscleGroup)
    {
        using (var context = new Layer2Container())
        {
            List<ExerciseGoal> goalSet;

            if (muscleGroup != "All Groups")
            {
                switch (orderBy)
                {
                    case 0:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true && s.Exercise.muscleGroups.Contains(muscleGroup)).OrderBy(o => o.Exercise.name).ToList();
                        break;
                    case 1:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true && s.Exercise.muscleGroups.Contains(muscleGroup)).OrderBy(o => o.id).ToList();
                        break;
                    default:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true && s.Exercise.muscleGroups.Contains(muscleGroup)).ToList();
                        break;
                }
            }

            else
            {
                switch (orderBy)
                {
                    case 0:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true).OrderBy(o => o.Exercise.name).ToList();
                        break;
                    case 1:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true).OrderBy(o => o.id).ToList();
                        break;
                    default:
                        goalSet = context.ExerciseGoals.Where(s => s.LimitBreaker.username == userName && s.achieved == true).ToList();
                        break;
                }
            }

            foreach (ExerciseGoal eg in goalSet)
            {
                context.LoadProperty(eg, "Exercise");
            }

            return goalSet;
        }
    }

    public bool achieveGoal(ExerciseGoal eg, int reps, int time, int weight, double distance)
    {       
        bool achieved = false;

        if (reps >= eg.reps && time >= eg.time && weight >= eg.weight && distance >= eg.distance)
        {
            using (var context = new Layer2Container())
            {
                ExerciseGoal saveGoal = context.ExerciseGoals.Where(s => s.id == eg.id).FirstOrDefault();
                saveGoal.achieved = true;
                context.SaveChanges();
                achieved = true;
            }
        }

        return achieved;
    }
}