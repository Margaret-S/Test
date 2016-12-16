using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public string fileName;
        public string[] elements;
        public string[] test;
        public int iLines = 0;
        public Worker[] workers;

        public Form1()
        {
            InitializeComponent();
        }
        
        public void DoWorkers(int N)
        {
            SortWorkers();

            DGW1.DataSource = workers;
            DGW2.DataSource = Names(5).Select(x => new { Names = x }).ToList();
            DGW3.DataSource = LastNID(3).Select(x => new { IDs = x }).ToList();
        }

        public List<string> Names(int N)
        {
            List<string> names = new List<string>();
            var n = workers.Take(N);
            foreach (Worker aw in n)
            {
                names.Add(aw.Name);
            }
            return names;
        }

        public List<int> LastNID(int N)
        {
            List<int> ids = new List<int>();

            var n = workers.Skip(workers.Length - N);
            foreach (Worker aw in n)
            {
                ids.Add(aw.ID);
            }

            return ids;
        }

        public void SortWorkers()
        {
            for (int i = 0; i < workers.Count(); i++)
            {
                workers[i].Pay = workers[i].CalculationPay();
            }
            workers = (from w in workers
                       orderby w.Pay descending, w.Name
                       select w).ToArray();
        }


        public string[] ReadAllLines(int lenArr, String fileName)
        {
            string[] resultRead = new string[lenArr];
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(fileName, Encoding.Default);
                {
                    string line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        resultRead[i] = line;
                        i++;
                    }
                }
                return resultRead;
            }
            catch (Exception e)
            {
                MessageBox.Show("Файл не может быть прочитан" + e.Message);
                return resultRead;
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFd = new OpenFileDialog();
            oFd.Title = "Выберите файл";
            oFd.InitialDirectory = "C:";
            oFd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (oFd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = oFd.FileName;

                    System.IO.StreamReader reader = new System.IO.StreamReader(fileName, Encoding.Default);
                    {
                        while (reader.ReadLine() != null)
                        {
                            iLines = iLines + 1;
                        }
                        workers = new Worker[iLines];
                        test = ReadAllLines(iLines, fileName);
                        int a = 0;
                        int[] idPay = new int[test.Length];
                        int[] ID = new int[test.Length];
                        string[] fio = new string[test.Length];
                        double[] pay = new double[test.Length];
                        for (a = 0; a < test.Length; a++)
                        {
                            elements = test[a].Split(';');
                            idPay[a] = Convert.ToInt32(elements[0]);
                            ID[a] = Convert.ToInt32(elements[1]);
                            fio[a] = elements[2];
                            pay[a] = Convert.ToDouble(elements[3]);
                        }
                        int i;
                        for (i = 0; i < iLines; i++)
                        {
                            if (idPay[i] == 1) workers[i] = new HourWorker(ID[i], fio[i], pay[i]);
                            if (idPay[i] == 2) workers[i] = new FixWorker(ID[i], fio[i], pay[i]);
                        }
                        DoWorkers(iLines);
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось загрузить файл: " + ex.Message);
                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            System.IO.Stream myStream;

            SaveFileDialog sFd = new SaveFileDialog();
            sFd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sFd.FilterIndex = 2;

            if (sFd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = sFd.OpenFile()) != null)
                {
                    System.IO.StreamWriter myWriter = new System.IO.StreamWriter(myStream, Encoding.Default);
                    try
                    {

                        for (int i = 0; i < DGW1.RowCount - 1; i++)
                        {
                            for (int j = 0; j < DGW1.ColumnCount; j++)
                            {
                                myWriter.Write(DGW1.Rows[i].Cells[j].Value.ToString());
                                if ((DGW1.ColumnCount - j) != 1) myWriter.Write(";");
                            }

                            if (((DGW1.RowCount - 1) - i - 1) != 0) myWriter.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myWriter.Close();
                    }
                }
                myStream.Close();
            }
        }
    }
}