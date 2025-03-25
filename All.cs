using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NutritionalDietPlan
{
	public partial class All : Form
	{
		private MySqlConnection con;

		private MySqlDataAdapter dba;
		private DataTable dtble;

		Favorite fa = new Favorite();

		string product, prodes, lbl = "all";
		public string aluser, ill;
		public int idfav;
		string f;

		public All()
		{
			InitializeComponent();
		}

		private void Connect()
		{
			try
			{
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
					this.Text = "Connected";
				}
			}
			catch (Exception ex)
			{
				this.Text = "Disconnected";
				MessageBox.Show(ex.Message);
			}
		}

		private DataTable ExecuteSQL(string cmd)
		{
			try
			{
				if (con.State == ConnectionState.Closed) con.Open();
				dba = new MySqlDataAdapter(cmd, con);
				dtble = new DataTable();
				dba.Fill(dtble);
				dba.Dispose();
				con.Close();
				return dtble;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		private void All_Load(object sender, EventArgs e)
		{
			string constr = String.Format("server = localhost; port = 3306; uid = root; pwd = iamleo; database = oursystem");
			con = new MySqlConnection(constr);
			Connect();
			LoadProducts();

			if (ill == "No Illness")
			{
				rtxtdescprod.Text = "all";
			}
			if (ill == "diabetes")
			{
				rtxtdescprod.Text = "diabetes";
			}
			if (ill == "high blood")
			{
				rtxtdescprod.Text = "hblood";
			}
			if (ill == "low blood")
			{
				rtxtdescprod.Text = "hblood";
			}
			if (ill == "obesity")
			{
				rtxtdescprod.Text = "obesity";
			}
			if (ill == "pneumonia")
			{
				rtxtdescprod.Text = "pneumonia";
			}
			if (ill == "anemic")
			{
				rtxtdescprod.Text = "anemic";
			}
			if (ill == "UTI")
			{
				rtxtdescprod.Text = "uti";
			}
			if (ill == "uric acid")
			{
				rtxtdescprod.Text = "uric";
			}
			if (ill == "acid reflux")
			{
				rtxtdescprod.Text = "reflux";
			}

			txtsrch.Focus();
		}

		private void dbgridprod_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dbgridprod.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
			{
				dbgridprod.CurrentRow.Selected = true;
				product = dbgridprod.Rows[e.RowIndex].Cells["product"].FormattedValue.ToString();
				prodes = String.Format("SELECT prodesc FROM products WHERE product LIKE '{0}'", product);
				DataTable dt = ExecuteSQL(prodes);
				if (dt.Rows.Count > 0)
				{
					string prodes = dt.Rows[0]["prodesc"].ToString();
					rtxtdescprod.Text = prodes;
				}
			}

			string favor = String.Format("SELECT fvproduct FROM favorites WHERE username = '{0}'", aluser);
			DataTable dta = ExecuteSQL(favor);
			if (dta.Rows.Count > 0)
			{
				for (int i = 0; i < dta.Rows.Count; i++)
				{
					f = dta.Rows[i]["fvproduct"].ToString();
					if (product == f)
					{
						heart.BringToFront();
						break;
					}
					if (product != f)
					{
						halfheart.BringToFront();
					}
				}
			}
		}

		private void txtsrch_TextChanged(object sender, EventArgs e)
		{
			if(lbl == "all")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					LoadProducts();
				}
			}
			if(lbl == "diabetes")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%diabetes%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					diabetic_Click(sender, e);
				}

			}
			if (lbl == "hblood")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%high blood%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					hblood_Click(sender, e);
				}
			}
			if (lbl == "lblood")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%low blood%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					lblood_Click(sender, e);
				}
			}
			if (lbl == "obesity")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%obesity%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					obesity_Click(sender, e);
				}
			}
			if (lbl == "pneumonia")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%pneumonia%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					pneumonia_Click(sender, e);
				}
			}
			if (lbl == "anemic")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%anemic%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					anemic_Click(sender, e);
				}
			}
			if (lbl == "uti")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%uti%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					uti_Click(sender, e);
				}
			}
			if (lbl == "uric")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%uric acid%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					uric_Click(sender, e);
				}
			}
			if (lbl == "reflux")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%acid reflux%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					reflux_Click(sender, e);
				}
			}
			if (lbl == "malnou")
			{
				string srch = String.Format("SELECT product FROM products WHERE product LIKE '%{0}%' AND prodesc LIKE '%malnourished%'", txtsrch.Text);
				dbgridprod.DataSource = ExecuteSQL(srch);
				if (txtsrch.Text == "")
				{
					malnou_Click(sender, e);
				}
			}
		}

		private void diabetic_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 74);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Diabetic", myfont, mybrush, 0, 0);
		}

		private void hblood_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 84);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("HighBlood", myfont, mybrush, 0, 0);
		}

		private void lblood_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 81);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("LowBlood", myfont, mybrush, 0, 0);
		}

		private void obesity_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 74);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Obesity", myfont, mybrush, 0, 0);
		}

		private void pneumonia_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 86);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Pneumonia", myfont, mybrush, 0, 0);
		}

		private void diabetic_Click(object sender, EventArgs e)
		{
			lbl = "diabetes";
			string dia = String.Format("SELECT product FROM products WHERE prodesc LIKE '%diabetes%'");
			dbgridprod.DataSource = ExecuteSQL(dia);
		}

		private void hblood_Click(object sender, EventArgs e)
		{
			lbl = "hblood";
			string hblood = String.Format("SELECT product FROM products WHERE prodesc LIKE '%high blood%'");
			dbgridprod.DataSource = ExecuteSQL(hblood);
		}

		private void lblood_Click(object sender, EventArgs e)
		{
			lbl = "lblood";
			string lblood = String.Format("SELECT product FROM products WHERE prodesc LIKE '%low blood%'");
			dbgridprod.DataSource = ExecuteSQL(lblood);
		}

		private void obesity_Click(object sender, EventArgs e)
		{
			lbl = "obesity";
			string obes = String.Format("SELECT product FROM products WHERE prodesc LIKE '%obesity%'");
			dbgridprod.DataSource = ExecuteSQL(obes);
		}

		private void pneumonia_Click(object sender, EventArgs e)
		{
			lbl = "pneumonia";
			string pneu = String.Format("SELECT product FROM products WHERE prodesc LIKE '%pneumonia%'");
			dbgridprod.DataSource = ExecuteSQL(pneu);
		}

		private void all_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 14);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(8, 36);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("All", myfont, mybrush, 0, 0);
		}

		private void malnou_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 10);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 81);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Malnourish", myfont, mybrush, 0, 0);
		}

		private void anemic_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 75);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Anemic", myfont, mybrush, 0, 0);
		}

		private void uti_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 62);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("U.T.I.", myfont, mybrush, 0, 0);
		}

		private void uric_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 79);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Uric Acid", myfont, mybrush, 0, 0);
		}

		private void reflux_Paint(object sender, PaintEventArgs e)
		{
			Font myfont = new Font("Quantify", 12);
			Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Crimson);
			e.Graphics.TranslateTransform(1, 87);
			e.Graphics.RotateTransform(270);
			e.Graphics.DrawString("Acid Reflux", myfont, mybrush, 0, 0);
		}

		private void malnou_Click(object sender, EventArgs e)
		{
			lbl = "malnou";
			string malnou = String.Format("SELECT product FROM products WHERE prodesc LIKE '%malnourished%'");
			dbgridprod.DataSource = ExecuteSQL(malnou);
		}

		private void anemic_Click(object sender, EventArgs e)
		{
			lbl = "anemic";
			string anemic = String.Format("SELECT product FROM products WHERE prodesc LIKE '%anemic%'");
			dbgridprod.DataSource = ExecuteSQL(anemic);
		}

		private void uti_Click(object sender, EventArgs e)
		{
			lbl = "uti";
			string uti = String.Format("SELECT product FROM products WHERE prodesc LIKE '%uti%'");
			dbgridprod.DataSource = ExecuteSQL(uti);
		}

		private void uric_Click(object sender, EventArgs e)
		{
			lbl = "uric";
			string uric = String.Format("SELECT product FROM products WHERE prodesc LIKE '%uric acid%'");
			dbgridprod.DataSource = ExecuteSQL(uric);
		}

		private void reflux_Click(object sender, EventArgs e)
		{
			lbl = "reflux";
			string reflux = String.Format("SELECT product FROM products WHERE prodesc LIKE '%acid reflux%'");
			dbgridprod.DataSource = ExecuteSQL(reflux);
		}

		private void halfheart_Click(object sender, EventArgs e)
		{
			string yesfav = string.Format("INSERT INTO favorites VALUES('{0}', '{1}', '{2}')", aluser, product, rtxtdescprod.Text);
			DataTable dt = ExecuteSQL(yesfav);
			heart.BringToFront();
		}

		private void heart_Click(object sender, EventArgs e)
		{
			string nofav = string.Format("DELETE FROM favorites WHERE fvproduct = '{0}'", product);
			DataTable dt = ExecuteSQL(nofav);
			halfheart.BringToFront();
		}

		private void all_Click(object sender, EventArgs e)
		{
			lbl = "all";
			string all = String.Format("SELECT product FROM products UNION (SELECT csproduct FROM custom WHERE username = '{0}')", aluser);
			dbgridprod.DataSource = ExecuteSQL(all);
		}

		private void LoadProducts()
		{
			string cmd = string.Format("SELECT product FROM products UNION (SELECT csproduct FROM custom WHERE username = '{0}')", aluser);
			dbgridprod.DataSource = ExecuteSQL(cmd);
			dbgridprod.Refresh();
		}
	}
}
