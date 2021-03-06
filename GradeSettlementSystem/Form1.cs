﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GradeCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            //close the program.
            this.Close();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            //show about
            About box = new About();
            box.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //appear update button, and check list
            btnUpdate.Visible = true;
            CheckList.Visible = true;
            lblInstruction.Visible = true;

            //display status bar
            lblStatus.Text = "Choose category from list";
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                //display name of class
                lblName.Text = txtName.Text;
                lblName.Visible = true;
                txtName.Visible = false;
                btnNameEdit.Visible = true;
                btnName.Visible = false;
            }
            else
            {
                MessageBox.Show("Please Type the name of class (Ex. CptS 321 or \"Object-Oriented Software Princ\")");
            }
        }

        private void btnNameEdit_Click(object sender, EventArgs e)
        {
            //change name of class
            lblName.Visible = false;
            txtName.Visible = true;
            btnNameEdit.Visible = false;
            btnName.Visible = true;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            //when press enter button while type name of class
            if (e.KeyCode == Keys.Enter)
            {
                this.AcceptButton = btnName;
                /*
                lblName.Text = txtName.Text;
                lblName.Visible = true;
                txtName.Visible = false;
                btnNameEdit.Visible = true;
                btnName.Visible = false;
                */
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            //did not started yet
            //if (btnUpdate.Visible == true)

            SaveFileDialog save = new SaveFileDialog();

            save.Title = "Save the data";
            save.Filter = "Text files (.txt)|*.txt|All files|*.*";
            save.FilterIndex = 1;
            save.RestoreDirectory = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(save.FileName))
                {
                    //write class name
                    sw.WriteLine(lblName.Text);
                    sw.WriteLine("");

                    //write require percent
                    sw.WriteLine("Require Percent:");
                    sw.WriteLine("A=" + txtA.Text + ", B=" + txtB.Text + ", C=" + txtC.Text);
                    sw.WriteLine("");

                    //define category and write data
                    int i = 0;
                    string category = null, score = null;
                    if (lblAttedence.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("Attedence:");
                        sw.WriteLine("Require=" + txtAttedence.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }
                    if (lblInclass.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("In-Class Works:");
                        sw.WriteLine("Require=" + txtInclass.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }
                    if (lblAssignment.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("Assignment:");
                        sw.WriteLine("Require=" + txtAssignment.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }
                    if (lblMidterm.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("Midterm:");
                        sw.WriteLine("Require=" + txtMidterm.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }
                    if (lblFinal.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("Final:");
                        sw.WriteLine("Require=" + txtFinal.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }
                    if (lblOther.Visible == true)
                    {
                        category += "1";
                        sw.WriteLine("Others:");
                        sw.WriteLine("Require=" + txtOther.Text);

                        score = addScore(i);
                        i++;

                        sw.WriteLine("Score=" + score);
                        sw.WriteLine("");
                    }
                    else
                    {
                        category += "0";
                    }

                    sw.WriteLine("#" + category);
                }
            }
        }

        public string addScore(int i)
        {
            string score = null;

            for (int j = 0; j < DataSheet.Rows.Count; ++j)
            {
                if (DataSheet.Rows[j].Cells[i].Value != null)
                {
                    score += DataSheet.Rows[j].Cells[i].Value + ",";
                }
                else
                {
                    break;
                }
            }

            return score;
        }

        private void menuLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            
            open.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            open.FilterIndex = 1;
            open.Multiselect = true;
            
            
            if (open.ShowDialog() == DialogResult.OK)
            {
                Stream file = open.OpenFile();
                StreamReader sr = new StreamReader(file);
                string line = null;

                //if there have save file and data, then skip the first step, and fill up data that in the file.
                
                

                //define category
                //String last = File.ReadLines(@"D:\asdf.txt").Last();

                //txtMain.Clear();
                //txtMain.Text = sr.ReadToEnd();

                file.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //display warning message
            DialogResult result = MessageBox.Show("If you click yes, you will lost all data.", "Make sure go back.", MessageBoxButtons.YesNo);
            
            //remove all data and go back to checklist
            if (result == DialogResult.Yes)
            {
                DataSheet.Columns.Clear();

                CheckList.Visible = true;
                btnUpdate.Visible = true;
                menuData.Visible = false;
                layResult.Visible = false;
                lblAttedence.Visible = false;
                lblAttendence2.Visible = false;
                txtAttedence.Visible = false;
                resultAttendence.Visible = false;
                resultAttendence.Text = "";
                emptyAttendence.Visible = false;
                lblInclass.Visible = false;
                lblInclass2.Visible = false;
                txtInclass.Visible = false;
                resultInclass.Visible = false;
                resultInclass.Text = "";
                emptyInclass.Visible = false;
                lblAssignment.Visible = false;
                lblAssignment2.Visible = false;
                txtAssignment.Visible = false;
                resultAssignment.Visible = false;
                resultAssignment.Text = "";
                emptyAssignment.Visible = false;
                lblMidterm.Visible = false;
                lblMidterm2.Visible = false;
                txtMidterm.Visible = false;
                resultMidterm.Visible = false;
                resultMidterm.Text = "";
                emptyMidterm.Visible = false;
                lblFinal.Visible = false;
                lblFinal2.Visible = false;
                txtFinal.Visible = false;
                resultFinal.Visible = false;
                resultFinal.Text = "";
                emptyFinal.Visible = false;
                lblOther.Visible = false;
                lblOthers2.Visible = false;
                txtOther.Visible = false;
                resultOther.Visible = false;
                resultOther.Text = "";
                emptyOthers.Visible = false;
                lblInstruction.Visible = true;
                lblTotal.Visible = false;
                lblTotal.Text = "";
                resultTotal.Visible = false;
                resultTotal.Text = "";
                resultTotal2.Visible = false;
                resultTotal2.Text = "";
                resultTotal3.Visible = false;
                resultTotal3.Text = "";
                txtA.Text = "";
                txtB.Text = "";
                txtC.Text = "";
                txtAttedence.Text = "";
                txtInclass.Text = "";
                txtAssignment.Text = "";
                txtMidterm.Text = "";
                txtFinal.Text = "";
                txtOther.Text = "";
                lblStatus.Text = "Choose category from list";
                lblInstruction.Visible = true;

                foreach (int i in CheckList.CheckedIndices)
                {
                    CheckList.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check the list
            int count = 0;

            for (int i = 0; i < CheckList.Items.Count; i++)
            {
                if (CheckList.GetItemChecked(i))
                {
                    count++;
                }
            }

            //display datasheets, labels, and textbox depend on what user choose.
            if (count > 0)
            {
                CheckList.Visible = false;
                btnUpdate.Visible = false;
                menuData.Visible = true;
                layResult.Visible = true;
                lblInstruction.Visible = false;

                for (int i = 0; i < CheckList.Items.Count; i++)
                {
                    if (CheckList.GetItemChecked(i))
                    {
                        string item = (string)CheckList.Items[i];

                        DataSheet.Columns.Add(item, item);

                        switch (item)
                        {
                            case "Attendence":
                                lblAttedence.Visible = true;
                                txtAttedence.Visible = true;
                                lblAttendence2.Visible = true;
                                resultAttendence.Visible = true;
                                emptyAttendence.Visible = true;
                                break;
                            case "In-class works":
                                lblInclass.Visible = true;
                                txtInclass.Visible = true;
                                lblInclass2.Visible = true;
                                resultInclass.Visible = true;
                                emptyInclass.Visible = true;
                                break;
                            case "Assignments":
                                lblAssignment.Visible = true;
                                txtAssignment.Visible = true;
                                lblAssignment2.Visible = true;
                                resultAssignment.Visible = true;
                                emptyAssignment.Visible = true;
                                break;
                            case "Midterm":
                                lblMidterm.Visible = true;
                                txtMidterm.Visible = true;
                                lblMidterm2.Visible = true;
                                resultMidterm.Visible = true;
                                emptyMidterm.Visible = true;
                                break;
                            case "Final":
                                lblFinal.Visible = true;
                                txtFinal.Visible = true;
                                lblFinal2.Visible = true;
                                resultFinal.Visible = true;
                                emptyFinal.Visible = true;
                                break;
                            case "Others":
                                lblOther.Visible = true;
                                txtOther.Visible = true;
                                lblOthers2.Visible = true;
                                resultOther.Visible = true;
                                emptyOthers.Visible = true;
                                break;
                        }
                    }
                }

                for (int i = 0; i < 15; i++)
                {
                    int temp = i + 1;
                    DataSheet.Rows.Add();
                    DataSheet.Rows[i].HeaderCell.Value = temp.ToString();
                }

                lblStatus.Text = "You must type score out of 100 and fill up all of \"Require Percent\", and \"Percent of Category\" ";

                resultTotal.Visible = true;
                resultTotal2.Visible = true;
                resultTotal3.Visible = true;
                lblTotal.Visible = true;
            }
            else
            {
                MessageBox.Show("Please check at least one category.", "No checked category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please read and status bar at the bottom.\r\nScore Board must be type score out of 100.\r\nStatus: " + lblStatus.Text);
        }

        private void CheckList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AcceptButton = btnUpdate;
            }
        }

        private void btnFinalScore_Click(object sender, EventArgs e)
        {
            if (txtFinal.Visible == true)
            {
                if (txtA.Text != "" && txtB.Text != "" && txtC.Text != "")
                {
                    if (checkCetegoryAndScore())
                    {
                        if (categoryTotal() == 100)
                        { 
                            double req, sumPercent = calc(false);
                            string A, B, C;

                            if (sumPercent != -1)
                            {
                                if (sumPercent + Convert.ToDouble(txtFinal.Text) >= Convert.ToDouble(txtA.Text))
                                {
                                    req = (Convert.ToDouble(txtA.Text) - sumPercent) / Convert.ToDouble(txtFinal.Text) * 100;
                                    A = req + " / 100 need";
                                }
                                else
                                {
                                    A = "Not possible";
                                }

                                if (sumPercent + Convert.ToDouble(txtFinal.Text) >= Convert.ToDouble(txtB.Text))
                                {
                                    req = (Convert.ToDouble(txtB.Text) - sumPercent) / Convert.ToDouble(txtFinal.Text) * 100;
                                    B = req + " / 100 need";
                                }
                                else
                                {
                                    B = "Not possible";
                                }

                                if (sumPercent + Convert.ToDouble(txtFinal.Text) >= Convert.ToDouble(txtC.Text))
                                {
                                    req = (Convert.ToDouble(txtC.Text) - sumPercent) / Convert.ToDouble(txtFinal.Text) * 100;
                                    C = req + " / 100 need";
                                }
                                else
                                {
                                    C = "Not possible";
                                }
                                lblTotal.Text = "Require Final: ";
                                resultTotal.Text = "A: " + A;
                                resultTotal2.Text = "B: " + B;
                                resultTotal3.Text = "C: " + C;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sum of Category percent is not 100", "Not provided enough category percent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lblStatus.Text = "Sum of Category percent is not 100";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please full the \"Require Percent\".", "Not provided require percents", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Require Percent\".";
                }
            }
            else
            {
                MessageBox.Show("Final is not requred.", "No need to Final", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = "Final is not requred.";
            }
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            if (txtA.Text != "" && txtB.Text != "" && txtC.Text != "")
            {
                if (checkCetegoryAndScore())
                {
                    if (categoryTotal() == 100)
                    {
                        double sumPercent = calc(true);

                        if (sumPercent != -1)
                        {
                            string rank;

                            if (sumPercent >= Convert.ToDouble(txtA.Text))
                            {
                                rank = "A";
                            }
                            else if (sumPercent >= Convert.ToDouble(txtB.Text))
                            {
                                rank = "B";
                            }
                            else if (sumPercent >= Convert.ToDouble(txtC.Text))
                            {
                                rank = "C";
                            }
                            else
                            {
                                rank = "Under C";
                            }
                            lblTotal.Text = "Result: ";
                            resultTotal.Text = "You got \"" + rank + "\" (" + Convert.ToString(sumPercent) + "%)";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sum of Category percent is not 100", "Not provided enough category percent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblStatus.Text = "Sum of Category percent is not 100";
                    }
                }
            }
        }

        public double calc(bool final)
        {
            double count, all;
            double total = 0, ave, percent, sumPercent = 0; ;

            for (int i = 0; i < DataSheet.Columns.Count; i++)
            {
                count = 0;
                total = 0;
                ave = 0;
                percent = 0;

                if (DataSheet.Rows[1].Cells[i].Value == null && final)
                {
                    //-1 means score board is nor filled enough.
                    MessageBox.Show("Please full the \"Score Board\".", "Not provided Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Score Board\".";
                    return -1;
                }
                for (int j = 0; j < DataSheet.Rows.Count; ++j)
                {
                    if (DataSheet.Rows[j].Cells[i].Value != null)
                    {
                        total += Convert.ToDouble(DataSheet.Rows[j].Cells[i].Value);
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                ave = total / count;
                all = count * 100;

                switch (DataSheet.Columns[i].HeaderText)
                {
                    case "Attendence":
                        percent = ave / 100 * Convert.ToDouble(txtAttedence.Text);
                        sumPercent += percent;

                        resultAttendence.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";

                        break;

                    case "In-class works":
                        percent = ave / 100 * Convert.ToDouble(txtInclass.Text);
                        sumPercent += percent;

                        resultInclass.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";

                        break;

                    case "Assignments":
                        percent = ave / 100 * Convert.ToDouble(txtAssignment.Text);
                        sumPercent += percent;

                        resultAssignment.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";

                        break;

                    case "Midterm":
                        percent = ave / 100 * Convert.ToDouble(txtMidterm.Text);
                        sumPercent += percent;

                        resultMidterm.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";

                        break;

                    case "Final":
                        if (DataSheet.Rows[0].Cells[i].Value != null)
                        {
                            percent = ave / 100 * Convert.ToDouble(txtFinal.Text);
                            sumPercent += percent;

                            resultFinal.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";
                        }
                        else
                        {
                            resultFinal.Text = "No score";
                        }

                        break;

                    case "Others":
                        percent = ave / 100 * Convert.ToDouble(txtOther.Text);
                        sumPercent += percent;

                        resultOther.Text = "Total: " + total + " Ave: " + ave + " Percent: " + percent + "%";

                        break;
                }

            }
            return sumPercent;
        }

        public bool checkCetegoryAndScore()
        {   
            if (txtAttedence.Visible == true)
            {
                if (txtAttedence.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            if (txtInclass.Visible == true)
            {
                if (txtInclass.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            if (txtAssignment.Visible == true)
            {
                if (txtAssignment.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            if (txtMidterm.Visible == true)
            {
                if (txtMidterm.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            if (txtFinal.Visible == true)
            {
                if (txtFinal.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            if (txtOther.Visible == true)
            {
                if (txtOther.Text == "")
                {
                    MessageBox.Show("Please full the \"Percent of Category\".", "Not provided Percent of Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Please full the \"Percent of Category\"";
                    return false;
                }
            }

            return true;
        }

        public int categoryTotal()
        {
            int count = 0;
            if (txtAttedence.Visible == true)
            {
                count += Convert.ToInt32(txtAttedence.Text);
            }

            if (txtInclass.Visible == true)
            {
                count += Convert.ToInt32(txtInclass.Text);
            }

            if (txtAssignment.Visible == true)
            {
                count += Convert.ToInt32(txtAssignment.Text);
            }

            if (txtMidterm.Visible == true)
            {
                count += Convert.ToInt32(txtMidterm.Text);
            }

            if (txtFinal.Visible == true)
            {
                count += Convert.ToInt32(txtFinal.Text);
            }

            if (txtOther.Visible == true)
            {
                count += Convert.ToInt32(txtOther.Text);
            }

            return count;
        }

        //never used yet.
        //define what category column of score board.
        int difineScoreHeader(int i)
        {
            switch (DataSheet.Columns[i].HeaderText)
            {
                case "Attendence":
                    return 1;
                case "In-class works":
                    return 2;
                case "Assignments":
                    return 3;
                case "Midterm":
                    return 4;
                case "Final":
                    return 5;
                case "Others":
                    return 6;
            }
            return 0;
        }

        private void NumAndPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }
    }
}