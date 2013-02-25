﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ui_uc_ucModifyDeleteRoutineLog : System.Web.UI.UserControl
{
    public int userID { get; set; }
    routineManager routManager;
    RadioButtonList rbl;
    SystemExerciseManager sysManager;
    int routineID;

    protected void Page_Load(object sender, EventArgs e)
    {
        sysManager = new SystemExerciseManager();
        routManager = new routineManager();
        rbl = (RadioButtonList)this.Parent.FindControl("rblRoutines");
        if (rbl != null && rbl.SelectedIndex > -1)
        {
            routineID = Convert.ToInt32(rbl.SelectedItem.Value);
            GridView1.DataSource = routManager.getLoggedExercises(userID, routineID);
            GridView1.DataBind();
        }
    }
    protected void okButton_Click(object sender, EventArgs e)
    {
        bool rc = routManager.deleteLoggedExercises(userID, routineID);
        if (rc)
        {
            GridView1.DataSource = routManager.getLoggedExercises(userID, routineID);
            GridView1.DataBind();
            // redirect page to itself (refresh)
            //Response.Redirect(Request.RawUrl);
        }
    }
}