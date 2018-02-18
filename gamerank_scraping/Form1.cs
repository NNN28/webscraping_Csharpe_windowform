using HtmlAgilityPack;
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

namespace gamerank_scraping
{
    public partial class Form1 : Form
    {
        DataTable table;
        public Form1()
        {
            InitializeComponent();
        
        }
      public void  inittable()
        {

            table = new DataTable("tablegame");
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Pourcentage", typeof(String));
            
            gamedataGridView.DataSource = table;
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            /* inittable();
             HtmlWeb web = new HtmlWeb();
             var doc = await Task.Factory.StartNew (()=> web.Load("http://www.gamerankings.com/browse.html"));

             var namenode = doc.DocumentNode.SelectNodes("//*[@id=\"main_col\"]//div//div//table//tr//td//a");
             var scorenode = doc.DocumentNode.SelectNodes("//*[@id=\"main_col\"]//div//div//table//tr//td//span//b");

     */
            inittable();
            var searchword = "iphone 8 plus";
            var url = "https://www.amazon.com.br/s/ref=nb_sb_noss/141-1509725-5841359?__mk_pt_BR=ÅMÅŽÕÑ&url=search-alias%3Daps&field-keywords=" + searchword;

            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse Response = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(Response.GetResponseStream());
            String read_text = sr.ReadToEnd();

            HtmlAgilityPack.HtmlDocument htmldocument = new HtmlAgilityPack.HtmlDocument();
            htmldocument.LoadHtml(read_text);
            var divs = htmldocument.DocumentNode.Descendants("div").
                 Where(node => node.GetAttributeValue("class", "").
             Equals("s-item-container")).ToList();
            int i = 0;
            foreach (var div in divs)
            {
                 var name = div.Descendants("h2").FirstOrDefault().InnerText;
                //     var price = div.ChildNodes.Descendants("span").Where(node => node.GetAttributeValue("class", "").

                //   Equals("a-size-base a-color-price a-text-bold")).FirstOrDefault().InnerText;
                string ch = "//*[@id=\"result_0\"]//div//div//div//a//span";
                string st = i.ToString();
                //var price = htmldocument.DocumentNode.SelectNodes("//*[@id=\"result_"+st+"\"]//div//div//div//a//span").FirstOrDefault().InnerText;
                i++;
               // var price = div.ChildNodes.Descendants("span").Where(node => node.GetAttributeValue("class", "").

                //Equals("a-size-base a-color-price a-text-bold")).First().InnerText;
                var price = htmldocument.DocumentNode.SelectNodes("//*[@id=\"result_"+st+"\"]//div//div//div//a//span").FirstOrDefault().InnerText;
                table.Rows.Add(name, price);
                ///html[1]/body[1]/div[1]/div[2]/div[1]/div[3]/div[2]/div[1]/div[4]/div[1]/div[1]/ul[1]/li[2]/div[1]/div[4]/div[3]/a[1]/span[1]
                /////*[@id="result_0"]/div/div[4]/div/a/span[1]
                //var autrepprice = htmldocument.DocumentNode.SelectNodes("//*[@id=\"result_0\"]//div//div//div//a//span").FirstOrDefault().InnerText;
                //*[@id="result_3"]/div/div[4]/div/a/span[1]
                //*[@id="result_1"]/div/div[4]/div[3]/a/span[1]
                //*[@id="result_0"]/div/div[4]/div/a/span[1]
                //*[@id="result_1"]/div/div[4]/div[1]/a/span[2]
                //*[@id="result_2"]/div/div[4]/div[1]/a/span[2]
                //*[@id="result_4"]/div/div[4]/div/a/span[1]
            }

        }
    }
}
