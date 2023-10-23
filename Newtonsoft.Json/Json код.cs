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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string work = textBox2.Text;
            textBox2.Clear();
            listView1.Items.Add(work);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox4.Text;
            int age = int.Parse(textBox3.Text);

            string[] works = new string[listView1.Items.Count];
            for(int i = 0; i < works.Length; i++)
            {
                works[i] = listView1.Items[i].Text;
            }
            User user = new User(name, surname, age, works);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.ShowDialog();

            using (StreamWriter writer = new StreamWriter(dialog.FileName))
            {
                writer.Write(json);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string json = "";
            using (StreamReader reader =new StreamReader(dialog.FileName))
            {
                json = reader.ReadToEnd();
            }

            User s = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);

            textBox1.Text = s.Name;
            textBox4.Text = s.Surname;
            textBox3.Text = s.Age.ToString();

            for (int i = 0; i < s.Works.Length; i++)
            {
                listView1.Items.Add(s.Works[i]);
            }
        }
    }
}
