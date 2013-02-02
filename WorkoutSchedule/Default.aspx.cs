﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
public partial class WorkoutSchedule_Default4 : System.Web.UI.Page
{
    ScheduleManager scheduleManager = new ScheduleManager();
    ExerciseManager exerciseManager = new ExerciseManager();
    //routineManager routineManage = new routineManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            multiViewCalendar.ActiveViewIndex = 0;
            loadMonths();
            loadYears();
            loadCalendar();
        }
    }

    protected void loadMonths()
    {
        for (int i = 1; i <= 12; i++)
        {
            ListItem li = new ListItem();
            li.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
            li.Value = i.ToString();
            ddl_month.Items.Add(li);
            if (li.Value == DateTime.Now.Month.ToString())
            {
                li.Selected = true;

            }
        }
    }
    protected void loadYears()
    {
        int yearsBack = 3;
        int yearsForward = 3;
        for (int i = DateTime.Now.AddYears(-yearsBack).Year; i <= DateTime.Now.AddYears(yearsForward).Year; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddl_year.Items.Add(li);
            if (li.Value == DateTime.Now.Year.ToString())
                li.Selected = true;
        }
    }
    protected void rpt_calendar_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        //find the controls
        Panel pnl_calendarDay = (Panel)e.Item.FindControl("pnl_calendarDay");
        LinkButton lnk_dayLink = (LinkButton)e.Item.FindControl("lnk_dayLink");
        Literal ltl_dayEvents = (Literal)e.Item.FindControl("ltl_dayEvents");

        //set values
        DateTime dt = (DateTime)e.Item.DataItem;
        //Here we set the day value for each day entry within the calendar
        StringBuilder sb = new StringBuilder();
        sb.Append(dt.Day.ToString());
        sb.Append(" ");
        //sb.Append(dt.ToString("d")); //' gets the day name based on the users computer settings (their local day name rather than English default)
        lnk_dayLink.Text = sb.ToString();
        lnk_dayLink.CommandArgument = dt.ToString();
        //Check to see if we have any dates matching today
        Label lbl = (Label)e.Item.FindControl("Label1");

        List<ScheduledRoutine> routine;
        List<scheduledItem> items;

        items = scheduleManager.getScheduledItems();


        routine = scheduleManager.getRoutines();

        foreach (scheduledItem item in items)
        {
            if (item.startTime.ToString("MMMM dd, yyyy") == dt.ToString("MMMM dd, yyyy"))
            {
                lbl.Text = lbl.Text + "<b>" + item.itemName + "</b>" + " " + item.startTime.ToString("hh:mm tt") + "<br/>";

            }
        }



    }


    protected void rpt_emptyDates_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        //find the controls
        Panel pnl_calendarDay = (Panel)e.Item.FindControl("pnl_emptyDate");
        Label lblEmpty = (Label)e.Item.FindControl("lblEmpty");
        //Here we set the day value for each day entry within the calendar
        StringBuilder sb = new StringBuilder();
        sb.Append(" ");
        lblEmpty.Text = sb.ToString();
    }

    protected void lnk_loadCalendar_Click(object sender, EventArgs e)
    {
        loadCalendar();
    }
    protected void loadCalendar()
    {
        Int16 m = Convert.ToInt16(ddl_month.SelectedItem.Value);
        Int16 y = Convert.ToInt16(ddl_year.SelectedItem.Value);

        List<DateTime> dates = new List<DateTime>();
        List<String> empty = new List<String>();
        DateTime dateValue = new DateTime(Convert.ToInt32(ddl_year.SelectedValue), Convert.ToInt32(ddl_month.SelectedValue), 1);
        int sunday = Convert.ToInt32(DayOfWeek.Sunday) + 1;
        int currentDay = Convert.ToInt32(dateValue.DayOfWeek) + 1;
        int difference = currentDay - sunday;

        if (dateValue.DayOfWeek == DayOfWeek.Sunday)
        {
            for (int i = 1; i < System.DateTime.DaysInMonth(y, m) + 1; i++)
            {
                DateTime d = new DateTime(y, m, i);
                dates.Add(d);
            }
            rpt_calendar.DataSource = dates;
            rpt_calendar.DataBind();
        }

        if (dateValue.DayOfWeek != DayOfWeek.Sunday)
        {

            for (int i = 0; i < difference; i++)
            {
                String e = "";
                empty.Add(e);
            }

            rpt_emptyDates.DataSource = empty;
            rpt_emptyDates.DataBind();

            for (int i = 1; i < System.DateTime.DaysInMonth(y, m) + 1; i++)
            {
                DateTime d = new DateTime(y, m, i);
                dates.Add(d);
            }

            rpt_calendar.DataSource = dates;

            rpt_calendar.DataBind();

        }
 
    }

    protected void ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (((LinkButton)e.CommandSource).Text == "Add Item")
        {

            multiViewCalendar.ActiveViewIndex = 1;
            addItemView.ActiveViewIndex = 0;
        }
/*
 * Unfinished, when user clicks on any of the dates
        else
        {
            lblTest.Text = ((LinkButton)e.CommandSource).Text;
            multiViewCalendar.ActiveViewIndex = 1;
            addItemView.ActiveViewIndex = 0;
        }
  */
 }


    protected void addExercise_Click(object sender, EventArgs e)
    {
        addItemView.ActiveViewIndex = 1;
    }

    protected void addRoutine_Click(object sender, EventArgs e)
    {
        addItemView.ActiveViewIndex = 3;

    }

    protected void btnScheduleRoutine_Click(object sender, EventArgs e)
    {
        if (scheduleManager.scheduleNewRoutine(Convert.ToInt32(ddlRoutines.SelectedValue), Convert.ToDateTime(/*calDate.SelectedDate.ToString("d") + " " + ddlHours.Text + ":" + ddlMinutes.Text + ":00 " + ddlAmPm.Text*/ tbDate_routine.Text), Convert.ToInt32(1), false))
            lblResult_Exercise.Text = "Success!";
        else
            lblResult_Exercise.Text = "Failure!";
    
    }

    //Routine
    protected void calendar_selectionChanged_routine(object sender, EventArgs e)
    {

        tbDate_routine.Text = calDateExercise.SelectedDate.ToString("d");
        test.Text = calDateExercise.SelectedDate.ToString("d") + " " + ddlHours_routine.SelectedValue + ":" + ddlMinutes_routine.SelectedValue + ":00 " + ddlAmPm_routine.SelectedValue;
        tbDate_exercise.Text = calDateExercise.SelectedDate.ToString("d") + " " + ddlHours_routine.SelectedItem.Text + ":" + ddlMinutes_routine.SelectedItem.Text + ":00 " + ddlAmPm_routine.SelectedItem.Text;

    }


    //Exericse
    protected void calendar_selectionChanged_exercise(object sender, EventArgs e)
    {
        tbDate_routine.Text = calDateExercise.SelectedDate.ToString("d");
        test.Text = calDateExercise.SelectedDate.ToString("d") + " " + dllHours_exercise.SelectedItem.Text + ":" + ddlMinutes_exercise.SelectedValue + ":00 " + ddlAmPm_exericse.SelectedValue;

        tbDate_exercise.Text = calDateExercise.SelectedDate.ToString("d") + " " + dllHours_exercise.SelectedItem.Text + ":" + ddlMinutes_exercise.SelectedItem.Text + ":00 " + ddlAmPm_exericse.SelectedItem.Text;
     
    }



    protected void lnk_add_item_Click(object sender, EventArgs e)
    {
        multiViewCalendar.ActiveViewIndex = 1;
        addItemView.ActiveViewIndex = 0;
    }


    protected void btnScheduleExercise_Click(object sender, EventArgs e)
    {
        if (scheduleManager.scheduleNewExercise(Convert.ToInt32(dllExercises.SelectedValue), Convert.ToDateTime(/*calDate.SelectedDate.ToString("d") + " " + ddlHours.Text + ":" + ddlMinutes.Text + ":00 " + ddlAmPm.Text*/ tbDate_exercise.Text), Convert.ToInt32(1), false))
            lblResult_Exercise.Text = "Success!";
       else
            lblResult_Exercise.Text = "Failure!";
    
    }
}