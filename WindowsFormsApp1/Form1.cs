using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;
using FastReport.Export.Pdf;
using FastReport.Export.Image;
using FastReport.Export.Svg;
using FastReport.Export.Html;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        int id;

        class format:IConvertible
        {
            public int id { get; set; }
            public String name { get; set; }
            public String form { get; set; }

            public TypeCode GetTypeCode()
            {
                return id.GetTypeCode();
            }

            public bool ToBoolean(IFormatProvider provider)
            {
                return ((IConvertible)id).ToBoolean(provider);
            }

            public byte ToByte(IFormatProvider provider)
            {
                return ((IConvertible)id).ToByte(provider);
            }

            public char ToChar(IFormatProvider provider)
            {
                return ((IConvertible)id).ToChar(provider);
            }

            public DateTime ToDateTime(IFormatProvider provider)
            {
                return ((IConvertible)id).ToDateTime(provider);
            }

            public decimal ToDecimal(IFormatProvider provider)
            {
                return ((IConvertible)id).ToDecimal(provider);
            }

            public double ToDouble(IFormatProvider provider)
            {
                return ((IConvertible)id).ToDouble(provider);
            }

            public short ToInt16(IFormatProvider provider)
            {
                return ((IConvertible)id).ToInt16(provider);
            }

            public int ToInt32(IFormatProvider provider)
            {
                return ((IConvertible)id).ToInt32(provider);
            }

            public long ToInt64(IFormatProvider provider)
            {
                return ((IConvertible)id).ToInt64(provider);
            }

            public sbyte ToSByte(IFormatProvider provider)
            {
                return ((IConvertible)id).ToSByte(provider);
            }

            public float ToSingle(IFormatProvider provider)
            {
                return ((IConvertible)id).ToSingle(provider);
            }

            public string ToString(IFormatProvider provider)
            {
                return id.ToString(provider);
            }

            public object ToType(Type conversionType, IFormatProvider provider)
            {
                return ((IConvertible)id).ToType(conversionType, provider);
            }

            public ushort ToUInt16(IFormatProvider provider)
            {
                return ((IConvertible)id).ToUInt16(provider);
            }

            public uint ToUInt32(IFormatProvider provider)
            {
                return ((IConvertible)id).ToUInt32(provider);
            }

            public ulong ToUInt64(IFormatProvider provider)
            {
                return ((IConvertible)id).ToUInt64(provider);
            }
        }

        List<format> box = new List<format>
        {
            new format{ id=0, name="PDF", form=".pdf" },
            new format{ id=1, name="JPG", form=".jpg" },
            new format{ id=2, name="PNG", form=".png" },
            new format{ id=3, name="SVG", form=".svg" },
            new format{ id=4, name="HTML", form=".html" }
        };

        public Form1()
        {
            InitializeComponent();
            comboBoxFormat.DataSource = box;
            comboBoxFormat.DisplayMember = "form";
            comboBoxFormat.ValueMember = "id";
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null && openFileDialog1.FileName != "")
                textBox1.Text = openFileDialog1.FileName;
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Load("1.frx");
            RichObject rich = new RichObject();
            try
            {
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName, Encoding.Default))
                {
                    rich.Text = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine(rich.Text);

            report.SetParameterValue("Parameter.Parameter", rich.Text);

            report.Prepare();

            switch (id)
            {
                case 0:
                    PDFExport pdf = new PDFExport();

                    report.Export(pdf, "result" + comboBoxFormat.Text);

                    report.Dispose();
                    break;
                case 1:
                    ImageExport image1 = new ImageExport();

                    report.Export(image1, "result" + comboBoxFormat.Text);

                    report.Dispose();
                    break;
                case 2:
                    ImageExport image2 = new ImageExport();

                    report.Export(image2, "result" + comboBoxFormat.Text);

                    report.Dispose();
                    break;
                case 3:
                    SVGExport svg = new SVGExport();

                    report.Export(svg, "result" + comboBoxFormat.Text);

                    report.Dispose();
                    break;
                case 4:
                    HTMLExport html = new HTMLExport();

                    report.Export(html, "result" + comboBoxFormat.Text);

                    report.Dispose();
                    break;
            }
            Console.WriteLine("End");
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = Convert.ToInt32(comboBoxFormat.SelectedValue);
            Console.WriteLine(comboBoxFormat.Text);
        }
    }
}
