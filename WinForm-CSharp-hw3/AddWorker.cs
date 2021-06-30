using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_CSharp_hw3
{
    public partial class AddWorker : Form
    {
        Form1 form1;
        bool added;
        public AddWorker(Form1 _form1)
        {
            InitializeComponent();
            added = false;
            form1 = _form1;
        }

        private void AddWorker_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (added)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != String.Empty && surnameTextBox.Text != String.Empty && fathernameTextBox.Text != String.Empty
                && birthdayTextBox.Text != String.Empty && placeTextBox.Text != String.Empty)
            {
                string[] date = birthdayTextBox.Text.Split('.');
                if (date.Length == 3)
                {
                    try
                    {
                        int day, month, year;
                        day = Int32.Parse(date[0]);
                        month = Int32.Parse(date[1]);
                        year = Int32.Parse(date[2]);
                        form1.workers.Add(new Worker(nameTextBox.Text, surnameTextBox.Text, fathernameTextBox.Text, new DateTime(year, month, day), placeTextBox.Text));
                        added = true;
                        this.Close();
                        return;
                    }
                    catch (Exception) { }
                }
                MessageBox.Show("Birthday entered incorrectly");
                return;
            }
            MessageBox.Show("You left an empty field");
        }
    }
}
