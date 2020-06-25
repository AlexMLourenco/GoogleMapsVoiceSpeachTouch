using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using mmisharp;
using Newtonsoft.Json;
using DotNetBrowser;
using DotNetBrowser.WinForms;
using System.Windows.Forms;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Windows.Threading;
using Keys = OpenQA.Selenium.Keys;

namespace AppGui
{
    public partial class Form1 : Form {
        private MmiCommunication mmiC;
        private String startupPath = "C:\\Users\\manel\\Desktop\\IM\\assing4\\GoogleMapsVoiceSpeachTouch";//Environment.CurrentDirectory;
        IWebDriver googledriver;
        private String path = "";
        private browserCommands command;
        private string[] coords = new string[2];
        private string street = "";
        private CLocation coord = new CLocation();
        private string URL = "";
        private String transport = "DRIVING";
        private Boolean flag = false;
        dynamic json;
        
        public Form1() {
            InitializeComponent();
            this.coord.GetLocationEvent();
            
            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            command = new browserCommands();
            mmiC.Message += MmiC_Message;
            
            mmiC.Start();
        }

        private void Form1_Load(object sender, EventArgs e) {
            googledriver = new ChromeDriver(@"" + startupPath);
            path = startupPath + "/../../html/googleMaps.html";
            
            googledriver.Navigate().GoToUrl("https://www.google.pt/maps");


        }


        private void MmiC_Message(object sender, MmiEventArgs e)
        {

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                //

                Console.WriteLine(e.Message);
                var doc = XDocument.Parse(e.Message);
                var com = doc.Descendants("command").FirstOrDefault().Value;
                dynamic json = JsonConvert.DeserializeObject(com);
                Console.WriteLine((string)json.ToString());
                if (json.action != null)
                {
                    switch ((string)json.action.ToString())
                    {
                        case "SEARCH":
                            this.json = json;
                            command.searchLocation(googledriver, coord, convertToDict(json));
                            break;
                        case "DIRECTIONS":
                            command.getDirections(googledriver, coord, convertToDict(json), transport);
                            break;
                        case "MORE":
                            command.zoomIn(googledriver, convertToDict(json));
                            break;
                        case "LESS":
                            command.zoomOut(googledriver, convertToDict(json));
                            break;
                        case "CHANGE":
                            dynamic dict = convertToDict(json);
                            transport = (string)dict.transport.ToString();
                            break;
                    }
                }
            });
            
        }
        public dynamic convertToDict(dynamic json)
        {
            dynamic tojson2 = JsonConvert.DeserializeObject(json);
            string[] valuesAndKeys = tojson2.recognized.ToObject<string[]>();
            int i = valuesAndKeys.Length;
            string json3 = "{\n";
            for (int j = 0; j < valuesAndKeys.Length; j++)
            {
                json3 += "\"" + valuesAndKeys[j] + "\": " + "\"" + valuesAndKeys[j + 1] + "\",\n ";
                j++;
            }

            json3 = json3.Substring(0, json3.Length - 1);
            json3 += "\n}";


            dynamic tojson = JsonConvert.DeserializeObject(json3);
            return tojson;
        }
        
        public void setWebBrowser(dynamic json) {
            string tmpURL = "";
            if ((string)json.action.ToString() == "SEARCH") {
                tmpURL += "https://www.google.com/maps/embed/v1/search?key=AIzaSyCxJd14el9dRqIkvYqFwEx_zz8zwkTAlaU";
                if ((string)json.service != null) {
                    tmpURL += "&q=" + (string)json.service.ToString();
                    if ((string)json.location != null) {
                        tmpURL += "+in+"+ (string)json.location.ToString();
                    }
                } else if ((string)json.local != null) {
                    tmpURL += "&q=" + (string)json.local.ToString();
                    if ((string)json.location != null)
                        tmpURL += "+in+" + (string)json.location.ToString();
                }
                this.URL = tmpURL;    
            } else if ((string)json.action.ToString() == "DIRECTIONS") {
                tmpURL += "https://www.google.com/maps/embed/v1/directions?key=AIzaSyCxJd14el9dRqIkvYqFwEx_zz8zwkTAlaU&origin=40.637906,-8.637598&mode="+ (string)json.mode.ToString();
                if ((string)json.service != null) {
                    tmpURL += "&destination=" + (string)json.service.ToString();
                    if ((string)json.location != null)
                        tmpURL += "+in+" + (string)json.location.ToString();
                } else if ((string)json.local != null) {
                    tmpURL += "&destination=" + (string)json.local.ToString();
                    if ((string)json.location != null)
                        tmpURL += "+in+" + (string)json.location.ToString();
                }
                this.URL = tmpURL;
            }

        }  
    }
}
