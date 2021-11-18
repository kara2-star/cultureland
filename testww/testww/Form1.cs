using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;

namespace testww
{
    public partial class Form1 : Form
    {
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;
            _options = new ChromeOptions();
            _options.AddArgument("disable-gpu");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("닉네임과 비밀번호를 정확히 입력해주십시오");
            }
            else
            {
                wjson();
                MessageBox.Show("계정 저장완료");
            }

        }
        private void wjson()
        {
            string path = @"C:\Users\songi\source\repos\testww\testww\bin\Debug\account.json";
            if (File.Exists(path))
            {
                Inputjson(path);
            }
        }
        private void Inputjson(string path)
        {

            JObject dpspec = new JObject(
                 new JProperty("ID", "" + textBox1.Text + ""),
                 new JProperty("PW", "" + textBox2.Text + "")
            );

            File.WriteAllText(path, dpspec.ToString());

        }
        public void readjson()
        {
            string jsonpath = @"C:\Users\songi\source\repos\testww\testww\bin\Debug\account.json";
            using (StreamReader file = File.OpenText(jsonpath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject json = (JObject)JToken.ReadFrom(reader);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (_driver == null)
            {
            }
            else
            {
                
                _driver.Close();
            }
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int leng1 = textBox3.Text.Length;
            int leng2 = textBox4.Text.Length;
            int leng3 = textBox5.Text.Length;
            int leng4 = textBox6.Text.Length;
            if (leng1 + leng2 + leng3 + leng4 != 18)
            {
                MessageBox.Show("올바른 핀번호를 적어주십시오.");
            }
            else
            {
                string jsonpath = @"C:\Users\songi\source\repos\testww\testww\bin\Debug\account.json";
                StreamReader file = File.OpenText(jsonpath);
                JsonTextReader reader = new JsonTextReader(file);
                JObject json = (JObject)JToken.ReadFrom(reader);
                string ID = (string)json["ID"].ToString();
                string PW = (string)json["PW"].ToString();
                string id = ID;
                string pw = PW;

                if (checkBox1.Checked == false)
                    _options.AddArgument("headless"); // 창을 숨기는 옵션입니다.

                _driver = new ChromeDriver(_driverService, _options);

                _driver.Navigate().GoToUrl("https://m.cultureland.co.kr/#"); // 웹 사이트에 접속합니다.

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                var em = _driver.FindElementByXPath("//*[@id='introAppShowMW_Btn']");//a[@aria-label="1"]
                em.Click();
                var v = _driver.FindElementByXPath("//*[@id='wrap']/header/div/a[2]");
                v.Click();
                var element = _driver.FindElementByXPath("//*[@id='loginText']");
                element.Click();
                Thread.Sleep(1500);
                element = _driver.FindElementByXPath("//*[@id='txtUserId']");
                element.SendKeys(id);
                var mss = _driver.FindElementByXPath("//*[@id='mtk_passwd_lower']");
                element = _driver.FindElementByXPath("//*[@id='passwd']");
                element.Click();
                var func_g = _driver.FindElementByXPath("//*[@id='mtk_passwd_lower']");
                foreach (var item in pw)
                {
                    func_g = _driver.FindElementByXPath("//img[@alt='" + item + "']");
                    func_g.Click();
                }
                mss = _driver.FindElementByXPath("//*[@id='mtk_done']/div/img");
                mss.Click();
                Thread.Sleep(1000);

                element = _driver.FindElementByXPath("//*[@id='btnLogin']");
                element.Click();





                var eelement = _driver.FindElementByXPath("//*[@id='wrap']/div[1]/div/a");
                eelement.Click();
                string pin = textBox6.Text;
                _driver.Navigate().GoToUrl("https://m.cultureland.co.kr/csh/cshGiftCard.do");
                var vj = _driver.FindElementByXPath("//*[@id='mtk_txtScr14_number']");

                var on = _driver.FindElementByXPath("//*[@id='txtScr11']");
                on.SendKeys(textBox3.Text);
                var onn = _driver.FindElementByXPath("//*[@id='txtScr12']");
                onn.SendKeys(textBox4.Text);
                var onnn = _driver.FindElementByXPath("//*[@id='txtScr13']");
                onnn.SendKeys(textBox5.Text);
                foreach (var ite in pin)
                {
                    vj = _driver.FindElementByXPath("//img[@alt='" + ite + "']");
                    vj.Click();
                }
                var c = _driver.FindElementByXPath("//*[@id='btnCshFrom']");
                c.Click();
                var b = _driver.FindElementByCssSelector(".charge_result dd");
                var n = _driver.FindElementByXPath("//*[@id='wrap']/div[3]/section/div/table/tbody/tr/td[3]/b");
                label6.Text = b.Text;
                label5.Text = n.Text;



            }



        }
    }
}

