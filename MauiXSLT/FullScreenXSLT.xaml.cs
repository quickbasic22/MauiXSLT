using System.Xml.Xsl;
using System.Xml;

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

    public FullScreenXSLT()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        var txtStream = await FileSystem.OpenAppPackageFileAsync("XsltText.txt");
        xslContent = new StreamReader(txtStream).ReadToEnd();
    }

    private async void XsltTranslator_Clicked(object sender, EventArgs e)
    {
        var xmlStream = await FileSystem.OpenAppPackageFileAsync("inventory.xml");

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
            using (var xmlReader = XmlReader.Create(xmlStream))
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