using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Person> People = new List<Person>();
       private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\Address Book - Chris"))
            Directory.CreateDirectory(path + "\\Address Book - Chris");
            if (!File.Exists(path + "\\Address Book - Chris\\settings.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter(path + "\\Address Book - Chris\\settings.xml", Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
        }

         private void button3_Click(object sender, EventArgs e)
        {
           
        Person p = new Person();
        p.Name = textBox1.Text;
        p.StreetAddress = textBox3.Text;
        p.Email = textBox2.Text;
        p.Birthday = dateTimePicker1.Value;
        p.AdditionalNotes = textBox4.Text;
        People.Add(p);
        listView1.Items.Add(p.Name);
        textBox1.Text = "";
        textBox2.Text = "";
        textBox3.Text = "";
        textBox4.Text = "";
        dateTimePicker1.Value = DateTime.Now;
    
        
    }

        class Person
        {
            public string Name
            {
                get;
                set;
            }
            public string Email
            {
                get;
                set;
            }
            public string StreetAddress
            {
                get;
                set;
            }
            public string AdditionalNotes
            {
                get;
                set;
            }
            public DateTime Birthday
            {
                get;
                set;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

      

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            textBox1.Text = People[listView1.SelectedItems[0].Index].Name;
            textBox2.Text = People[listView1.SelectedItems[0].Index].Email;
            textBox3.Text = People[listView1.SelectedItems[0].Index].StreetAddress;
            textBox4.Text = People[listView1.SelectedItems[0].Index].AdditionalNotes;
            dateTimePicker1.Value= People[listView1.SelectedItems[0].Index].Birthday;
        }

        void Remove()
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                People.RemoveAt(listView1.SelectedItems[0].Index);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Remove();
        }
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Remove();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            People[listView1.SelectedItems[0].Index].Name = textBox1.Text;
            People[listView1.SelectedItems[0].Index].Email = textBox2.Text;
            People[listView1.SelectedItems[0].Index].StreetAddress = textBox3.Text;
            People[listView1.SelectedItems[0].Index].AdditionalNotes = textBox4.Text;
            People[listView1.SelectedItems[0].Index].Birthday = dateTimePicker1.Value;
            listView1.SelectedItems[0].Text = textBox1.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xDoc.Load(path + "\\Address Book - Chris\\settings.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();
            foreach (Person p in People)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xAddress = xDoc.CreateElement("StreetAddress");
                XmlNode xNotes = xDoc.CreateElement("Additional Notes");
                XmlNode xBirthday = xDoc.CreateElement("Birthday");
                xName.InnerText = p.Name;
                xEmail.InnerText = p.Email;
                xAddress.InnerText = p.StreetAddress;
                xNotes.InnerText = p.AdditionalNotes;
                xBirthday.InnerText = p.Birthday.ToFileTime().ToString();
                xTop.AppendChild(xName);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xAddress);
                xTop.AppendChild(xNotes);
                xTop.AppendChild(xBirthday);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save(path + "\\Address Book - Chris\\settings.xml");
        }
    }
}
