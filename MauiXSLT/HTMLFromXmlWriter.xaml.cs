using System.Text;
using System.Xml;

namespace MauiXSLT;

public partial class HTMLFromXmlWriter : ContentPage
{
	StringBuilder HtmlStringBuilder = new StringBuilder();
	XmlWriter HTMLDocument;	
	public HTMLFromXmlWriter()
	{
		InitializeComponent();
		XmlWriterSettings writerSettings = new XmlWriterSettings();
		writerSettings.OmitXmlDeclaration = true;
		HtmlStringBuilder.AppendLine("<!DOCTYPE html>");
		HtmlStringBuilder.AppendLine("<html>");
		HtmlStringBuilder.AppendLine("<head>");
		HtmlStringBuilder.AppendLine("<title>My WebPage</title>");
		HtmlStringBuilder.AppendLine("<link type='text/css' rel='stylesheet' href='WebView.css'>");
		HtmlStringBuilder.AppendLine("</head>");
		HtmlStringBuilder.AppendLine("<body>");
		HtmlStringBuilder.AppendLine("<h1>My&nbsp;&#033;&#033;&#033;&nbsp;HTML Page with Stylesheet</h1>");
		HtmlStringBuilder.AppendLine("<p>This project is about XML StyleSheets</p>");
		HtmlStringBuilder.AppendLine("</body>");
		HtmlStringBuilder.AppendLine("</html>");
		HTMLDocument = XmlWriter.Create("C:\\Users\\quick\\source\\repos\\MauiXSLT\\MauiXSLT\\StringBuilderWebView.html", writerSettings);
		HTMLDocument.WriteRaw(HtmlStringBuilder.ToString());
		HTMLDocument.Flush();
		HTMLDocument.Close();
		HTMLDocument.Dispose();
		//ReadOnlyFile onlyFile = new ReadOnlyFile("C:\\Users\\quick\\source\\repos\\MauiXSLT\\MauiXSLT\\StringBuilderWebView.html");

		//OpenFileRequest openFile = new OpenFileRequest("my Page", onlyFile);
		//Launcher.OpenAsync(openFile);

		HtmlWebViewSource webViewSource = new HtmlWebViewSource();
		webViewSource.BaseUrl = "";
		webViewSource.BindingContext = this;
		
		webViewSource.Html = HtmlStringBuilder.ToString();
		webViewSource.Html = webViewSource.Html = webViewSource.Html.Replace("<base href=\"https://appdir/\">", "");

		bool containsBase = webViewSource.Html.Contains("<base href");
		this.Title = $"Contains base href {containsBase}";

		MyWebView.Source = webViewSource;


	}
}