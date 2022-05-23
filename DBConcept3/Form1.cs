using OfficeOpenXml;
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

namespace DBConcept3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using(asEntities1 db = new asEntities1())
                {
                dataGridView.DataSource = db.ficha.ToList();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Planilha|.xlsx" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    try 
                   {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        using (var package = new ExcelPackage(fileInfo))
                        {
                            ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add("ficha");
                            excelWorksheet.Cells.LoadFromCollection<ficha>(dataGridView.DataSource as List<ficha>, true);
                            package.Save();
                        }
                        MessageBox.Show("Tabela exportada com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex) 
                    {
                        MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
