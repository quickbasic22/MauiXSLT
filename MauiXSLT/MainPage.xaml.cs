using System.Xml;
using System.Xml.Xsl;
using System.Globalization;
using System.Text;
using MauiXSLT.Data;
using System.Diagnostics;

namespace MauiXSLT;

public partial class MainPage : ContentPage
{
    XslCompiledTransform xslt;
    XmlReader xslReader;
    StringReader xslReaderStringReader;
    XmlReader xmlReader;
    string xmlPath;
    StringWriter outputWriter;
    string htmlResult;
    Task<Stream> txtStream;
    FileStream inventoryStream;
    FileStream xsltStream;
    StringReader xmlStringReader;
    string xmlText;
    string xsltText;
    string inventoryPath;
    string xsltTextPath;

    public MainPage()
    {
        InitializeComponent();
        XsltEditor.TextColor = Colors.White;
        if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
        {
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
                xsltTextPath = MauiDirectory.WriteToFileSystem("XsltText.txt");
                xsltText = File.ReadAllText(xsltTextPath);
                bool xsltTextYes = File.Exists(xsltTextPath);
                Console.WriteLine(xsltTextPath);
                Console.WriteLine($"xsltText exists {xsltTextYes}");
            }
        }
        else if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            inventoryPath = "C:\\Users\\quick\\OneDrive\\Desktop\\XsltProgram\\inventory.xml";
            xsltTextPath = "C:\\Users\\quick\\OneDrive\\Desktop\\XsltProgram\\XsltText.txt";
            xmlText = File.ReadAllText(inventoryPath);
            xsltText = File.ReadAllText(xsltTextPath);
        }
        xmlStringReader = new StringReader(xmlText);
        XsltEditor.Text = xsltText;
    }
    protected void XsltTranslator_Clicked(object sender, EventArgs e)
    {
        LblError.Text = "";
        xsltText = XsltEditor.Text;
        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
        xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
        xmlReaderSettings.IgnoreComments = true;
        xslReaderStringReader = new StringReader(xsltText);
        try
        {
            xslt = new XslCompiledTransform();

            using (var xslReader = XmlReader.Create(xslReaderStringReader,xmlReaderSettings))
            {
                xslt.Load(xslReader);
            }
            using (var xmlReader = XmlReader.Create(xmlStringReader, xmlReaderSettings))
            {
                using (outputWriter = new StringWriter())
                {
                    xslt.Transform(xmlReader, null, outputWriter);
                    string htmlResult = outputWriter.ToString();
                    XsltWebView.Source = new HtmlWebViewSource { Html = htmlResult };
                }
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}

