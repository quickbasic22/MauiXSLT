using System.Xml;
using System.Xml.Xsl;
using System.Globalization;
using System.Text;

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


    public MainPage()
    {
        InitializeComponent();
        XsltEditor.Text = File.ReadAllText(@"C:\Users\quick\OneDrive\Desktop\XsltProgram\XsltText.txt");
        xmlPath = "C:\\Users\\quick\\OneDrive\\Desktop\\XsltProgram\\inventory.xml";
    }

    private void XsltTranslator_Clicked(object sender, EventArgs e)
    {
        LblError.Text = "";
        string xslContent = XsltEditor.Text;
        xslReaderStringReader = new StringReader(xslContent);
        try
        {
            xslt = new XslCompiledTransform();

            using (var xslReader = XmlReader.Create(xslReaderStringReader))
            {
                xslt.Load(xslReader);
            }
            using (var xmlReader = XmlReader.Create(xmlPath))
            {
                using (outputWriter = new StringWriter())
                {
                    xslt.Transform(xmlReader, null, outputWriter);
                    string htmlResult = outputWriter.ToString();
                    XsltWebView.Source = new HtmlWebViewSource {  Html = htmlResult };
                }
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}

