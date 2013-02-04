using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Intertek.Helpers;

namespace Intertek.WEB
{
    public partial class Pruebas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResultado_Click(object sender, EventArgs e)
        {
            string xx = Utils.RelativeWebRoot;
            //txtResultado.Text = Utils.Encriptar(txtOrigen.Text);
            cargarGrilla();
        }
        private void cargarGrilla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo");
            dt.Columns.Add("descripcion");

            DataRow dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "dennis";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "huallanca";
            dt.Rows.Add(dr);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}