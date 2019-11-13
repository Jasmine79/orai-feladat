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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_hozzaad_Click(object sender, EventArgs e)
        {
            string Sor = textBox_Nev.Text.Trim();
            //-- a textBox szöveg tartalma az elejéről és végéről leszedi a szóközt
            if (radioButton_N.Checked) 
                //-- a változó ki van -e jelölve egy logikai értéket ad vissza
            {
                Sor += " - " + radioButton_N.Tag;
            }
            else
            {
                Sor += " - " + radioButton_F.Tag;
            }

            listBox1.Items.Add(Sor);
            textBox_Nev.Text = ""; //--kiürítjük a textBox beviteli mezőt
        }

        private void button_Mentes_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Minden fájl (*.*)|*.*|Szövegfájl (*.txt)|*.txt";
            //-- a szűrésekhez használjuk: 1-1 szűrés 2 részből áll
            //-- 1. felh.nak szól, 2. op.rsz.-nek szól a kettőt az AltGr+W (|) választja el
            saveFileDialog1.FilterIndex = 2;
            //-- az =2 azt jelenti, hogy a második lehetőség lesz az alapért.
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) //--megtörtént-e a mentés
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    //--a saveFileDialog1.FileName- mel kiolvassuk a file helyét és nevét
                    {
                        foreach (string item in listBox1.Items)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
            //-- saveFileDialog1.ShowDialog(); 
            //--Mentés másként megnyitása, 
            //--filekiterjesztést nem tudunk választani
        }

        private void button_Betolt_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Minden fájl (*.*)|*.*|Szövegfájl (*.txt)|*.txt";
            //-- a Filter-t a szűrésekhez használjuk: 1-1 szűrés 2 részből áll
            //-- 1. felh.nak szól, 2. op.rsz.-nek szól a két részt és aszűrőket is az AltGr+W (|) választja el
            openFileDialog1.FilterIndex = 2;
            //-- az =2 azt jelenti, hogy a második lehetőség lesz az alapért.
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            //-- alapértelmezett alkvt. megadása
            openFileDialog1.RestoreDirectory = false;
            //--a megelőző könyvtár meghatározását megjegyezze-e
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
            try
            {
                using (StreamReader sr=new StreamReader(openFileDialog1.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        listBox1.Items.Add(sr.ReadLine()); 
                        //-- a listBox elemeihez adjuk hozzá soronként
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            //--teljes biztonsági másolatot hozunk létre, 
            //--hogy az eseménykezelésekhez is hozzáférjünk
            string[] sor = list.SelectedItem.ToString().Split('-');
            //-- annak a karaktersorozatnak az értéke melyre rákattintott a felh.
            textBox_Nev.Text = sor[0].Trim();
            if (radioButton_F.Tag.Equals(sor[1].Trim()))
            {
                radioButton_F.Checked = true;
                radioButton_N.Checked = false;
            }
            if (radioButton_N.Tag.Equals(sor[1].Trim()))
            {
                radioButton_F.Checked = false;
                radioButton_N.Checked = true;
            }
            

        }
    }
}
