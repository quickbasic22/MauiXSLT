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
    string xslContent;
    Task<Stream> txtStream;
    FileStream inventoryStream;
    FileStream xsltStream;
    StringReader xmlStringReader;
    string xmlText;
    string xsltText;

    public MainPage()
    {
        InitializeComponent();
        var inventoryPath = MauiDirectory.WriteToFileSystem("inventory.xml");
        var xsltTextPath = MauiDirectory.WriteToFileSystem("XsltText.txt");
        Console.WriteLine(inventoryPath);
        Console.WriteLine(xsltTextPath);
         
        xmlText = File.ReadAllText(inventoryPath);
        xsltText = File.ReadAllText(xsltTextPath);

        xmlStringReader = new StringReader(xmlText);

        xslContent = xsltText.ToString();

       bool inventoryYes = File.Exists(inventoryPath);
       bool xsltTextYes = File.Exists(xsltTextPath);
        Console.WriteLine($"Inventory.xml exists {inventoryYes}");
        Console.WriteLine($"xsltText exists {xsltTextYes}");

    }
    protected async void XsltTranslator_Clicked(object sender, EventArgs e)
    {
        LblError.Text = "";      
        xslContent = XsltEditor.Text;
        xslReaderStringReader = new StringReader(xslContent);
        try
        {
            xslt = new XslCompiledTransform();

            using (var xslReader = XmlReader.Create(xslReaderStringReader))
            {
                xslt.Load(xslReader);
            }
            using (var xmlReader = XmlReader.Create(xmlStringReader))
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

