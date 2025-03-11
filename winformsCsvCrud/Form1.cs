using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace winformsCsvCrud
{
    public partial class Form1 : Form
    {
        private string filePath = "data.csv";

        public Form1()
        {
            InitializeComponent();
            LoadCsv();
        }

        private void LoadCsv()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "ID,Name,Age\n"); // ヘッダーを作成
            }

            var lines = File.ReadAllLines(filePath);
            var data = lines.Skip(1)
                            .Select(line => line.Split(','))
                            .Select(parts => new { ID = parts[0], Name = parts[1], Age = parts[2] })
                            .ToList();

            //dataGridView1.DataSource = data;
        }
        private void SaveCsv()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSVファイル (*.csv)|*.csv";
                sfd.Title = "CSVを保存";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // DataGridView の内容を CSV に書き出す
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                var values = row.Cells.Cast<DataGridViewCell>()
                                                      .Select(cell => cell.Value?.ToString() ?? "");
                                sw.WriteLine(string.Join(",", values));
                            }
                        }
                    }
                }
            }
        }
    }
}