﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;


/// <summary>
/// Summary description for Layer2Manager
/// </summary>
public class SystemExerciseManager
{
    System.Web.HttpApplication _context;

    public SystemExerciseManager()
    {
        //Reference to curent application instance
        _context = System.Web.HttpContext.Current.ApplicationInstance;
    }

    public List<Exercise> getAllExercises()
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.OrderBy(o => o.id).ToList();
        }
    }

    public List<Juice.AutocompleteItem> getExerciseNamesAC(bool admin)
    {
        using (var context = new Layer2Container())
        {
            List<Juice.AutocompleteItem> rc = new List<Juice.AutocompleteItem>();
            // to make a new Juice.AutocompleteItem:
            // rc = new List<Juice.AutocompleteItem>(){new Juice.AutocompleteItem { Label = "", Value = "" }};

            // label is what shows up in the drop down list of names, value is what shows up on the textbox when a name is selected
            // return null if no entry in the database, otherwise display all exercises
            // did not check if it would be in ascending/descending order
            if (context.Exercises.Count() == 0)
            {
                rc = null;
            }
            else
            {
                if (admin)
                {
                    rc = context.Exercises.Select(x => new Juice.AutocompleteItem { Label = x.name, Value = x.name }).ToList();
                }
                else
                {
                    rc = context.Exercises.Where(e => e.enabled == true).Select(x => new Juice.AutocompleteItem { Label = x.name, Value = x.name }).ToList();
                }
            }

            return rc;
        }
    }

    public Int32 getExerciseID(String name)
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.Where(x => x.name == name).Select(x => x.id).FirstOrDefault();
        }
    }

    public Exercise getExerciseInfo(Int32 id)
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.Where(x => x.id == id).FirstOrDefault();
        }
    }

    public String[] splitMuscleGroups(String muscleGroups)
    {
        String[] rc = new String[0];

        rc = muscleGroups.Split(null);

        for (int i = 0; i < rc.Length; i++)
            rc[i] = rc[i].Trim();

        return rc;
    }

    public Boolean modifyExercise(Int32 id, string exerciseName, string muscleGroups, string equipment, string videoLink, bool rep, bool weight, bool distance, bool time, string desc, bool nameChanged)
    {
        Boolean rc = false;
        Exercise exercise = null;
        using (var context = new Layer2Container())
        {
            exercise = context.Exercises.Where(x => x.id == id).FirstOrDefault();
            
            try
            {
                if (nameChanged) { 
                    if (context.Exercises.Where(e => e.name == exerciseName).FirstOrDefault() != null)
                    {
                        rc = false;
                    }
                    else
                    {
                        exercise.name = exerciseName.Trim();
                        exercise.equipment = equipment.Trim();
                        exercise.videoLink = videoLink.Trim();
                        exercise.rep = rep;
                        exercise.weight = weight;
                        exercise.distance = distance;
                        exercise.time = time;
                        exercise.muscleGroups = muscleGroups;
                        exercise.description = desc;
                        context.Exercises.ApplyCurrentValues(exercise);
                        context.SaveChanges();
                        rc = true;
                    }
                }
                else
                {
                    exercise.equipment = equipment.Trim();
                    exercise.videoLink = videoLink.Trim();
                    exercise.rep = rep;
                    exercise.weight = weight;
                    exercise.distance = distance;
                    exercise.time = time;
                    exercise.muscleGroups = muscleGroups;
                    exercise.description = desc;
                    context.Exercises.ApplyCurrentValues(exercise);
                    context.SaveChanges();
                    rc = true;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return rc;
    }

    public bool createNewExercise(string exerciseName, string muscleGroups, string equipment, string videoLink, bool rep, bool weight, bool distance, bool time, bool enabled, string desc)
    {
        bool rc = false;

        using (var context = new Layer2Container())
        {
            Exercise newExercise = new Exercise();
            ExperienceManager expMngr = new ExperienceManager();
            try
            {
                if ((context.Exercises.FirstOrDefault(exercise => exercise.name == exerciseName).name == exerciseName))
                    rc = false;
            }
            catch (NullReferenceException e)
            {
                newExercise.name = exerciseName;
                newExercise.muscleGroups = muscleGroups;
                newExercise.equipment = equipment;
                newExercise.videoLink = videoLink;
                newExercise.rep = rep;
                newExercise.weight = weight;
                newExercise.distance = distance;
                newExercise.time = time;
                newExercise.enabled = enabled;
                newExercise.description = desc;

                context.Exercises.AddObject(newExercise);
                context.SaveChanges();

                expMngr.createNewExerciseExp(exerciseName, 100, weight ? 1 : 0, rep ? 1 : 0, distance ? 1 : 0, time ? 1 : 0);

                rc = true;
            }
            return rc;
        }
    }

    public List<Exercise> getExercisesByName(string exerciseName, bool isAdmin)
    {
        using (var context = new Layer2Container())
        {
            //context.ContextOptions.LazyLoadingEnabled = false;
            if (isAdmin)
            {
                var query = (from exercise in context.Exercises
                             where exercise.name.Contains(exerciseName)
                             select exercise);
                return query.OrderBy(exercise => exercise.name).ToList();
            }
            else
            {
                var query = (from exercise in context.Exercises
                             where exercise.name.Contains(exerciseName)
                             where exercise.enabled == true
                             select exercise);
                return query.OrderBy(exercise => exercise.name).ToList();
            }


            //context.LoadProperty(query, "MuscleGroups");
            
        }
    }

    public List<Exercise> getExercisesByMuscleGroup(string muscleGroup)
    {
        using (var context = new Layer2Container())
        {
            if (muscleGroup == "ALL")
            {
                return context.Exercises.OrderBy(o => o.id).ToList();
            }
            else { 
            //context.ContextOptions.LazyLoadingEnabled = false;
            var query = (from exercise in context.Exercises
                         where exercise.muscleGroups.Contains(muscleGroup)
                         && exercise.enabled == true
                         select exercise);

            return query.OrderBy(exercise => exercise.name).ToList();
            }
            //context.LoadProperty(query, "MuscleGroups");
            
        }
    }

    public Exercise getExercise(string exerciseName)
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.FirstOrDefault(exercise => exercise.name == exerciseName);
        }
    }

    public Exercise getExerciseById(int ID)
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.Where(e => e.id == ID).FirstOrDefault();
        }
    }

    public Exercise getFirstExercise()
    {
        using (var context = new Layer2Container())
        {
            return context.Exercises.FirstOrDefault();
        }
    }
}