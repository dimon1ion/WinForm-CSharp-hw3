using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinForm_CSharp_hw3
{
    public partial class Form1 : Form
    {
        AddWorker addWorker;
        public List<Worker> workers { get; set; }
        public Form1()
        {
            InitializeComponent();
            workers = new List<Worker>();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Worker tmpWorker = listBox1.SelectedItem as Worker;
            if (tmpWorker != null)
            {
                nameTextBox.Text = tmpWorker.Name;
                surnameTextBox.Text = tmpWorker.Surname;
                fathernameTextBox.Text = tmpWorker.FatherName;
                birthdayTextBox.Text = $"{tmpWorker.Birthday.Day}.{tmpWorker.Birthday.Month}.{tmpWorker.Birthday.Year}";
                placeTextBox.Text = tmpWorker.PlaceOfResidence;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt files(*.txt) |*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Worker>));
                try
                {
                    List<Worker> workers1 = new List<Worker>();
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        workers1 = (List<Worker>)xmlSerializer.Deserialize(fs);
                        workers = workers1;
                    }
                    for (int i = 0; i < workers.Count; i++)
                    {
                        listBox1.Items.Add(workers[i]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("The file cannot be opened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addWorker = new AddWorker(this);
            this.Visible = false;
            if (addWorker.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(workers[workers.Count - 1]);
            }
            this.Visible = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Txt file(*.txt) |*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Worker>));
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        xmlSerializer.Serialize(fs, workers);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("The file cannot be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Worker tmpWorker = listBox1.SelectedItem as Worker;
            if (tmpWorker != null)
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
                            tmpWorker.Name = nameTextBox.Text;
                            tmpWorker.Surname = surnameTextBox.Text;
                            tmpWorker.FatherName = fathernameTextBox.Text;
                            tmpWorker.Birthday = new DateTime(year, month, day);
                            tmpWorker.PlaceOfResidence = placeTextBox.Text;
                            listBox1.Items.Remove(listBox1.SelectedItem);
                            listBox1.Items.Add(tmpWorker);
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
                nameTextBox.Text = String.Empty;
                surnameTextBox.Text = String.Empty;
                fathernameTextBox.Text = String.Empty;
                birthdayTextBox.Text = String.Empty;
                placeTextBox.Text = String.Empty;
            }
        }

        private void youngOldButton_Click(object sender, EventArgs e)
        {
            if (workers.Count > 0)
            {
                Worker youngWorker = workers[0];
                Worker oldWorker = workers[0];
                int young = workers[0].Birthday.Year;
                int old = workers[0].Birthday.Year;
                for (int i = 0; i < workers.Count; i++)
                {
                    if (old > workers[i].Birthday.Year)
                    {
                        old = workers[i].Birthday.Year;
                        youngWorker = workers[i];
                    }
                    if (young < workers[i].Birthday.Year)
                    {
                        young = workers[i].Birthday.Year;
                        oldWorker = workers[i];
                    }
                }
                MessageBox.Show($"Youngest: {youngWorker.PlaceOfResidence}\nOldest: {oldWorker.PlaceOfResidence}");
            }
            else
            {
                MessageBox.Show("None");
            }
        }

        private void Task2Button_Click(object sender, EventArgs e)
        {
            if (workers.Count > 0)
            {
                int[] years = new int[workers.Count];
                for (int i = 0; i < workers.Count; i++)
                {
                    years[i] = workers[i].Birthday.Year;
                }
                Array.Sort(years);
                int max = 0, max1 = 0, num = years[0], nummax = years[0];
                for (int i = 0; i < years.Length; i++)
                {
                    if (num != years[i] || i == years.Length - 1)
                    {
                        if (i == years.Length - 1) { max1++; }
                        if (max < max1)
                        {
                            max = max1;
                            nummax = num;
                        }
                        max1 = 0;
                        num = years[i];
                    }
                    max1++;
                }
                string message = $"In {nummax}, {max} employees were born\n";
                for (int i = 0; i < workers.Count; i++)
                {
                    if (workers[i].Birthday.Year == nummax)
                    {
                        message += '\n' + workers[i].Surname;
                    }
                }
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("None");
            }
        }
    }
}
