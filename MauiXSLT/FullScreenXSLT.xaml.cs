using System.Xml.Xsl;
using System.Xml;
using MauiXSLT.Data;
using System.Net.Security;
using System.Diagnostics;
using System.Xml.Linq;

namespace MauiXSLT;

public partial class FullScreenXSLT : ContentPage
{
    XslCompiledTransform xslt;
    XmlReader xslReader;
    StringReader xslReaderStringReader;
    XmlReader xmlReader;
    string xmlPath;
    StringWriter outputWriter;
    string htmlResult;
    string xslContent;
    Task<Stream> txtStream;
    StreamReader xsltStream;
    StreamReader inventoryStream;
    string xmlText;
    string xsltText;
    StringReader xmlStringReader;
    string inventoryPath;
    string xsltTextPath;

    public FullScreenXSLT()
	{
		InitializeComponent();
        LoadTextAndXmlFiles();
    }
    private void XsltTranslator_Clicked(object sender, EventArgs e)
    { 
        LoadTextAndXmlFiles();


        if (XsltEditor.IsVisible)
        {
            XsltEditor.IsVisible = false;
            XsltWebView.IsVisible = true;
        }
        else
        {
            XsltEditor.IsVisible = true;
            XsltWebView.IsVisible = true;
        }
        xmlStringReader = new StringReader(xmlText);

        xslReaderStringReader = new StringReader(XsltEditor.Text);

        LblError.Text = "";
        xsltText = XsltEditor.Text;
        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
        xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
        xmlReaderSettings.IgnoreComments = true;
        xslReaderStringReader = new StringReader(xsltText);
        try
        {
            xslt = new XslCompiledTransform();

            using (var xslReader = XmlReader.Create(xslReaderStringReader, xmlReaderSettings))
            {
                xslt.Load(xslReader);
            }
            string css = "<head><title>XML Stylesheet Transformation</title><link rel='stylesheet' href='webView.css'></head>";
            using (var xmlReader = XmlReader.Create(xmlStringReader, xmlReaderSettings))
            {
                using (outputWriter = new StringWriter())
                {
                    xslt.Transform(xmlReader, null, outputWriter);
                    string htmlResult = outputWriter.ToString();
                    htmlResult = htmlResult.Insert(7, css);
                    Debug.WriteLine(htmlResult);
                    XsltWebView.Source = new HtmlWebViewSource { Html = htmlResult };
                }
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.ToString();
        }
    }

    void LoadTextAndXmlFiles()
    {
        if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
        {
            XsltEditor.TextColor = Colors.Black;
            XsltEditor.BackgroundColor = Colors.White;
            XsltWebView.BackgroundColor = Colors.White;
            XsltWebView.Source = new HtmlWebViewSource { Html = "Startup text" };
            if (!File.Exists(MauiDirectory.GetAppDataDirFileName("inventory.xml")))
            {
                inventoryPath = MauiDirectory.WriteToFileSystem("inventory.xml");
                xmlText = File.ReadAllText(inventoryPath);
                bool inventoryYes = File.Exists(inventoryPath);
                Console.WriteLine(inventoryPath);
                Console.WriteLine($"Inventory.xml exists {inventoryYes}");
            }
            else if (File.Exists(MauiDirectory.GetAppDataDirFileName("inventory.xml")))
            {
                inventoryPath = Path.Combine(MauiDirectory.GetAppDataDirFileName("inventory.xml"));
                xmlText = File.ReadAllText(inventoryPath);
                bool inventoryYes = File.Exists(inventoryPath);
                Console.WriteLine(inventoryPath);
                Console.WriteLine($"Inventory.xml exists {inventoryYes}");
            }

            if (!File.Exists(MauiDirectory.GetAppDataDirFileName("XsltText.txt")))
            {
                xsltTextPath = MauiDirectory.WriteToFileSystem("XsltText.txt");
                xsltText = File.ReadAllText(xsltTextPath);
                bool xsltTextYes = File.Exists(xsltTextPath);
                Console.WriteLine(xsltTextPath);
                Console.WriteLine($"xsltText exists {xsltTextYes}");
            }
            else if (File.Exists(MauiDirectory.WriteToFileSystem("XsltText.txt")))
            {
                xsltTextPath = Path.Combine(MauiDirectory.GetAppDataDirFileName("XsltText.txt"));
                xsltText = File.ReadAllText(xsltTextPath);
                bool xsltTextYes = File.Exists(xsltTextPath);
                Console.WriteLine(xsltTextPath);
                Console.WriteLine($"xsltText exists {xsltTextYes}");
            }
        }
        else if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            XsltEditor.TextColor = Colors.White;
            XsltEditor.BackgroundColor = Colors.Black;
            inventoryPath = "~\\WinUIFiles\\inventory.xml";
            xsltTextPath = "~\\WinUIFiles\\XsltText.txt";
            xmlText = File.ReadAllText(inventoryPath);
            xsltText = File.ReadAllText(xsltTextPath);
        }
        if (XsltEditor.Text == null)
        {
            xmlStringReader = new StringReader(xmlText);
            XsltEditor.Text = xsltText;
        }        
    }
}