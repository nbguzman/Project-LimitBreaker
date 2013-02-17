﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ui_uc_ucViewExercise : System.Web.UI.UserControl
{
    SystemExerciseManager manager = new SystemExerciseManager();
    ExerciseManager exerciseManager = new ExerciseManager();
    public event EventHandler userControlEventHappened;

    private void OnUserControlEvent()
    {
        if (userControlEventHappened != null)
        {
            userControlEventHappened(this, EventArgs.Empty);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        exerciseAutoComplete.SourceList = manager.getExerciseNamesAC();
        
        if (!IsPostBack)
        {
            populateExiseList();
            populateExerciseInfo();
        }
    }

    protected void exerciseSearchButton_Click(object sender, EventArgs e)
    {
        //lblResult.Text = "";
        List<Exercise> foundExercises = manager.getExercisesByName(exerciseSearchBox.Text.Trim());
        ExerciseDDL.Items.Clear();
        if (foundExercises.Count != 0)
        {

            ExerciseDDL.DataSource = foundExercises;
            ExerciseDDL.DataBind();
           // foreach (Exercise name in foundExercises)
           // {
             //   ExerciseDDL.Items.Add(name.name);
                //    if (name.enabled)
                //        rblEnaber.Items[0].Selected = true;
                //    else
                //        rblEnaber.Items[1].Selected = false;
            //}
            //rblEnaber.Visible = true;

            exceriseNotFound.Visible = false;
            ExerciseDDL_SelectedIndexChanged(sender, e);
            ExerciseDDL.Visible = true;
        }
        else
            exerciesNotFound();

        OnUserControlEvent();
    }

    protected void MuscleGroupDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        //lblResult.Text = "";
        populateExiseList();
        OnUserControlEvent();
    }

    protected void ExerciseDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        populateExerciseInfo();
        /*
        Exercise exercise = manager.getExercise(ExerciseDDL.SelectedValue);
        exerciseName.Visible = true;
        exerciseName.Text = exercise.name;
        exerciseEquipment.Visible = true;
        exerciseEquipment.Text = exercise.equipment;
        exerciseVideo.Visible = true;
        exerciseVideo.Text = exercise.videoLink;
        exerciseAttributes.Visible = true;
        exerciseAttributes.Text = "";
        if (exercise.weight)
            exerciseAttributes.Text += "Weight\n";
        if (exercise.rep)
            exerciseAttributes.Text += "Reps\n";
        if (exercise.time)
            exerciseAttributes.Text += "Time\n";
        if (exercise.distance)
            exerciseAttributes.Text += "Distance\n";
        exerciseEnabled.Visible = true;
        exerciseEnabled.Text = exercise.enabled.ToString();
        //populateForm();
        */
        OnUserControlEvent();
    }

    protected void exerciesNotFound()
    {
        //exerciseName.Visible = false;
        //exerciseEquipment.Visible = false;
        //exerciseVideo.Visible = false;
        //exerciseAttributes.Visible = false;
        //exerciseEnabled.Visible = false;
        exceriseNotFound.Visible = true;
          lblExerciseEquipment.Text = "";
          lblExerciseMuscleGroups.Text = "";
          lblExerciseVideo.Text = "";
          lblExerciseDescription.Text = "";
          ExerciseDDL.Items.Insert(0, new ListItem("No Exercises", "NONE"));
          ExerciseDDL.SelectedIndex = 0;
    }

    public string ddlValue
    {
        get { return ExerciseDDL.SelectedItem.Text; }
    }

    public int ddlSelectedValue
    {
        get { return Convert.ToInt32(ExerciseDDL.SelectedValue); }
    }

    public int ddlCount
    {
        get { return ExerciseDDL.Items.Count; }
    }


    protected void dllExercises_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void populateExiseList()
    {
   
        List<Exercise> foundExercises = manager.getExercisesByMuscleGroup(ddlMuscleGroups.SelectedValue.Trim());
        ExerciseDDL.Items.Clear();
        exerciseSearchBox.Text = "";
        if (foundExercises.Count != 0)
        {
            ExerciseDDL.DataSource = foundExercises;
            ExerciseDDL.DataBind();
            exceriseNotFound.Visible = false;
            populateExerciseInfo();
        }
        else
            exerciesNotFound();
        
    }

    protected void populateExerciseInfo()
    {
        lblExerciseEquipment.Text = "";
        lblExerciseMuscleGroups.Text = "";
        lblExerciseVideo.Text = "[Video]";
        //if (ExerciseDDL.SelectedValue != "NONE")
        //{
        Exercise exercise = exerciseManager.getExerciseById(Convert.ToInt32(ExerciseDDL.SelectedValue));
            lblExerciseEquipment.Text = exercise.equipment;

            if (exercise.description == null)
            {
                lblExerciseDescription.Text = "None";
            }
            else
            {
                lblExerciseDescription.Text = exercise.description;
            }



            lblExerciseVideo.NavigateUrl = exercise.videoLink;


            String[] muscles = exerciseManager.splitMuscleGroups(exercise.muscleGroups);


            foreach (var item in muscles)
            {
                if (item != "")
                    lblExerciseMuscleGroups.Text += "- " + item + "<br/>";
            }
        //}
        //else
        //{
         //   lblExerciseEquipment.Text = "";
         //   lblExerciseMuscleGroups.Text = "";
         //   lblExerciseVideo.Text = "";
         //   lblExerciseDescription.Text = "";
        //}
    }


}