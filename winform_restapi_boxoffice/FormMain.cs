using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace winform_restapi_boxoffice
{
    public partial class FormMain : MetroFramework.Forms.MetroForm
    {
        //kobis.or.kr
        private const string apiKey = "";

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dtStart.Value = DateTime.Today;
            dtStart.Format = DateTimePickerFormat.Short;

            this.Text = "Box office ranking";
            btnSearch.Text = "Search";
            lblTitle.Text = "Top 10";


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //openapi

            Cursor.Current = Cursors.WaitCursor;

            if(dtStart.Value == null)
            {
                MessageBox.Show("No date selected");
                return;
            }

            try
            {

                string post = $"http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.xml?key={apiKey}&targetDt={dtStart.Value:yyyyMMdd}";

                WebRequest wr = WebRequest.Create(post);
                wr.Method = "GET";

                WebResponse wrs = wr.GetResponse();

                Stream s = wrs.GetResponseStream();
                StreamReader sr = new StreamReader(s);

                string response = sr.ReadToEnd();

                //Console.WriteLine(response);

                var movies = getMovies(response);

                metroGrid1.DataSource = movies;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;
        }

        private List<movie> getMovies(string response)
        {
            var ret = new List<movie>();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);

            XmlNode node = xml["boxOfficeResult"]["dailyBoxOfficeList"];

            for(int i=0; i< node.ChildNodes.Count; i++)
            {
                var mv = new movie
                {
                    Rank = Convert.ToInt32(node.ChildNodes[i]["rank"].InnerText),
                    RankInten = Convert.ToInt32(node.ChildNodes[i]["rankInten"].InnerText),
                    MovieName = node.ChildNodes[i]["movieNm"].InnerText,
                    OpenDate = node.ChildNodes[i]["openDt"].InnerText,
                    AudiAcc = Convert.ToInt32(node.ChildNodes[i]["audiAcc"].InnerText),
                    AudiCnt = Convert.ToInt32(node.ChildNodes[i]["audiCnt"].InnerText),
                };

                if (mv != null)
                {
                    ret.Add(mv);
                }
            }

            return ret;
        }

    }
}
