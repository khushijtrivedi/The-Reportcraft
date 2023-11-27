﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtoType
{
    public partial class QuaterlyColum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call a method to populate the DropDownList
                PopulateMaintenanceDropDown();
                PopulateEquipClassDropDown();
                PopulateResponsibleDropDown();

                HttpContext.Current.Handler = this;

            }
        }

        private string scon = ConfigurationManager.ConnectionStrings["LTprojectdb"].ToString();
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataTable ds = new DataTable();
        DataSet dt = new DataSet();
        DataSet ds_r = new DataSet();
        DataSet ds_ec = new DataSet();
        public void openCon()
        {
            con.Close();
            con.ConnectionString = scon;
            con.Open();
        }

        public void closeCon()
        {
            con.Close();
        }

        public void PopulateMaintenanceDropDown()
        {
            try
            {
                openCon();

                string query = "SELECT distinct maint_code from maintenancetb";
                adp = new SqlDataAdapter(query, con);
                dt = new DataSet();
                adp.Fill(dt);
                ddlmaint.Items.Insert(0, new ListItem("SELECT MAINTENANCE", ""));
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ddlmaint.DataSource = dt.Tables[0];
                    ddlmaint.DataTextField = "maint_code";
                    ddlmaint.DataValueField = "maint_code";
                    ddlmaint.DataBind();
                }

                closeCon();

            }
            catch
            {
                Response.Write("<script>alert('connection issue');</script>");
            }
            finally
            {
                closeCon();
            }
        }
        public void PopulateResponsibleDropDown()
        {
            try
            {
                openCon();

                string query = "SELECT distinct dept_code from depttb";
                adp = new SqlDataAdapter(query, con);
                ds_r = new DataSet();
                adp.Fill(ds_r);
                ddlResponsible.Items.Insert(0, new ListItem("SELECT RESPONSIBLE", ""));
                if (ds_r.Tables[0].Rows.Count > 0)
                {
                    ddlResponsible.DataSource = ds_r.Tables[0];
                    ddlResponsible.DataTextField = "dept_code";
                    ddlResponsible.DataValueField = "dept_code";
                    ddlResponsible.DataBind();
                }

                closeCon();

            }
            catch
            {
                Response.Write("<script>alert('connection issue');</script>");
            }
            finally
            {
                closeCon();
            }
        }
        public void PopulateEquipClassDropDown()
        {
            try
            {
                openCon();

                string query = "SELECT distinct equip_class from equipmenttb";
                adp = new SqlDataAdapter(query, con);
                ds_ec = new DataSet();
                adp.Fill(ds_ec);
                ddlequipclass.Items.Insert(0, new ListItem("SELECT CLASS", ""));
                if (ds_ec.Tables[0].Rows.Count > 0)
                {
                    ddlequipclass.DataSource = ds_ec.Tables[0];
                    ddlequipclass.DataTextField = "equip_class";
                    ddlequipclass.DataValueField = "equip_class";
                    ddlequipclass.DataBind();
                }

                closeCon();

            }
            catch
            {
                Response.Write("<script>alert('connection issue');</script>");
            }
            finally
            {
                closeCon();
            }
        }

        public DataTable GetDepartmentDowntime()
        {
            try
            {
                openCon();

                string query = "SELECT m.function_desc, m.equip_desc, SUM(m.duration_min) AS dur \r\nFROM MAIN_DATA_R m, equipmenttb e\r\nwhere m.start_date >= DATEADD(MONTH, -4, GETDATE()) AND m.start_date <= GETDATE() AND m.equip_code = e.equip_code \r\n      AND e.equip_class = 'B'\r\nGROUP BY m.function_desc, m.equip_desc WITH ROLLUP \r\nHAVING (m.function_desc IS NOT NULL AND m.equip_desc IS NOT NULL) OR (m.function_desc IS NOT NULL AND m.equip_desc IS NULL)";
                cmd = new SqlCommand(query, con);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public DataTable GetDepartmentDowntime2(string maintenanceCode)
        {
            try
            {
                openCon();

                string query = "SELECT function_desc, equip_desc, SUM(duration_min) AS dur FROM MAIN_DATA_R WHERE maint_code = @maintenanceCode AND start_date >= DATEADD(MONTH,-4,GETDATE()) AND start_date <= GETDATE() GROUP BY function_desc, equip_desc WITH ROLLUP HAVING (function_desc IS NOT NULL AND equip_desc IS NOT NULL) OR (function_desc IS NOT NULL AND equip_desc IS NULL)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@maintenanceCode", maintenanceCode);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }

        public DataTable GetDepartmentDowntime3(string rdeptCode)
        {
            try
            {
                openCon();

                string query = "SELECT function_desc, equip_desc, SUM(duration_min) AS dur FROM MAIN_DATA_R WHERE dept_code = @rdeptCode AND start_date >= DATEADD(MONTH,-4,GETDATE()) AND start_date <= GETDATE() GROUP BY function_desc, equip_desc WITH ROLLUP HAVING (function_desc IS NOT NULL AND equip_desc IS NOT NULL) OR (function_desc IS NOT NULL AND equip_desc IS NULL)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rdeptCode", rdeptCode);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public DataTable GetDepartmentDowntime4(string eqclass)
        {
            try
            {
                openCon();

                string query = "SELECT m.function_desc, m.equip_desc, SUM(m.duration_min) AS dur FROM MAIN_DATA_R m, equipmenttb e where m.start_date >= DATEADD(MONTH,-4,GETDATE()) AND m.start_date <= GETDATE() AND m.equip_code = e.equip_code AND e.equip_class = @eqclass GROUP BY m.function_desc, m.equip_desc WITH ROLLUP HAVING (m.function_desc IS NOT NULL AND m.equip_desc IS NOT NULL) OR (m.function_desc IS NOT NULL AND m.equip_desc IS NULL)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@eqclass", eqclass);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public DataTable GetEquipmentDowntime(string department)
        {
            try
            {
                openCon();

                string query = "SELECT m.equip_desc, SUM(m.duration_min) AS dur FROM MAIN_DATA_R m WHERE m.function_desc = '" + department + "' AND m.equip_desc IS NOT NULL AND m.start_date >= DATEADD(MONTH,-4,GETDATE())  AND m.start_date <= GETDATE() GROUP BY m.equip_desc";

                adp = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public DataTable GetEquipmentDowntime2(string department, string maintenanceCode)
        {
            try
            {
                openCon();

                string query = "SELECT m.equip_desc, SUM(m.duration_min) AS dur FROM MAIN_DATA_R m WHERE m.function_desc = @department AND m.maint_code=@maintenanceCode AND m.equip_desc IS NOT NULL AND m.start_date >= DATEADD(MONTH,-4,GETDATE())  AND m.start_date <= GETDATE() GROUP BY m.equip_desc";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@maintenanceCode", maintenanceCode);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }

        public DataTable GetEquipmentDowntime3(string department, string rdeptCode)
        {
            try
            {
                openCon();

                string query = "SELECT m.equip_desc, SUM(m.duration_min) AS dur FROM MAIN_DATA_R m WHERE m.function_desc = @department AND m.dept_code = @rdeptCode AND m.equip_desc IS NOT NULL AND m.start_date >= DATEADD(MONTH,-4,GETDATE())  AND m.start_date <= GETDATE() GROUP BY m.equip_desc";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@rdeptCode", rdeptCode);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public DataTable GetEquipmentDowntime4(string department, string eqclass)
        {
            try
            {
                openCon();

                string query = "SELECT MAIN_DATA_R.equip_desc, SUM(MAIN_DATA_R.duration_min) AS dur FROM MAIN_DATA_R,equipmenttb WHERE MAIN_DATA_R.function_desc = @department AND MAIN_DATA_R.equip_code = equipmenttb.equip_code AND equipmenttb.equip_class = @eqclass AND MAIN_DATA_R.equip_desc IS NOT NULL AND MAIN_DATA_R.start_date >= DATEADD(MONTH,-4,GETDATE()) AND MAIN_DATA_R.start_date <= GETDATE() GROUP BY MAIN_DATA_R.equip_desc";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@eqclass", eqclass);
                adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                closeCon();

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                closeCon();
            }
        }
        public class GraphModel
        {
            public string equip { get; set; }
            public decimal dura { get; set; }


            public string mcode { get; set; }

            public string fdesc { get; set; }
            public int rid { get; set; }
            public List<DrilldownItem> drilldown { get; set; }
        }

        public class DrilldownItem
        {
            public string name { get; set; }
            public decimal y { get; set; }
        }

        [WebMethod]
        public static List<GraphModel> ShowDept()
        {
            List<GraphModel> _li = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetDepartmentDowntime();

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["equip_desc"].ToString()))
                {
                    GraphModel rd = new GraphModel();
                    rd.fdesc = dr["function_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    _li.Add(rd);
                }
            }

            return _li;
        }
        [WebMethod]
        public static List<GraphModel> ShowDept2(string maintenanceCode)
        {
            List<GraphModel> chartData = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetDepartmentDowntime2(maintenanceCode);

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["equip_desc"].ToString()))
                {
                    GraphModel rd = new GraphModel();
                    rd.fdesc = dr["function_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    chartData.Add(rd);
                }
            }

            return chartData;
        }

        [WebMethod]
        public static List<GraphModel> ShowDept3(string rdeptCode)
        {
            List<GraphModel> chartData = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetDepartmentDowntime3(rdeptCode);

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["equip_desc"].ToString()))
                {
                    GraphModel rd = new GraphModel();
                    rd.fdesc = dr["function_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    chartData.Add(rd);
                }
            }

            return chartData;
        }

        [WebMethod]
        public static List<GraphModel> ShowDept4(string eqclass)
        {
            List<GraphModel> chartData = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetDepartmentDowntime4(eqclass);

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["equip_desc"].ToString()))
                {
                    GraphModel rd = new GraphModel();
                    rd.fdesc = dr["function_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    chartData.Add(rd);
                }
            }

            return chartData;
        }


        [WebMethod]
        public static List<GraphModel> ShowEquipment(string department)
        {
            List<GraphModel> _li = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetEquipmentDowntime(department);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["equip_desc"] != DBNull.Value)
                {
                    GraphModel rd = new GraphModel();
                    rd.equip = dr["equip_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    _li.Add(rd);
                }
            }

            return _li;
        }
        [WebMethod]
        public static List<GraphModel> ShowEquipment2(string department, string maintenanceCode)
        {
            List<GraphModel> _li = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetEquipmentDowntime2(department, maintenanceCode);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["equip_desc"] != DBNull.Value)
                {
                    GraphModel rd = new GraphModel();
                    rd.equip = dr["equip_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    _li.Add(rd);
                }
            }

            return _li;
        }

        [WebMethod]
        public static List<GraphModel> ShowEquipment3(string department, string rdeptCode)
        {
            List<GraphModel> _li = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetEquipmentDowntime3(department, rdeptCode);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["equip_desc"] != DBNull.Value)
                {
                    GraphModel rd = new GraphModel();
                    rd.equip = dr["equip_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    _li.Add(rd);
                }
            }

            return _li;
        }

        [WebMethod]
        public static List<GraphModel> ShowEquipment4(string department, string eqclass)
        {
            List<GraphModel> _li = new List<GraphModel>();
            QuaterlyColum obj = (QuaterlyColum)HttpContext.Current.Handler;
            DataTable dt = obj.GetEquipmentDowntime4(department, eqclass);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["equip_desc"] != DBNull.Value)
                {
                    GraphModel rd = new GraphModel();
                    rd.equip = dr["equip_desc"].ToString();
                    rd.dura = Convert.ToDecimal(dr["dur"]);
                    _li.Add(rd);
                }
            }

            return _li;
        }
        protected void ddlmaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedValue"] = ddlmaint.SelectedValue;
        }

        protected void ddlequipclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedValueEC"] = ddlequipclass.SelectedValue;
        }

        protected void ddlResponsible_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["SelectedValueRd"] = ddlResponsible.SelectedValue;
        }

        protected void returnbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("StandardReport.aspx");
        }
    }
}