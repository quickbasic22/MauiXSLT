namespace MauiXSLT;

public partial class WebViewCSS : ContentPage
{
	public WebViewCSS()
	{
		InitializeComponent();
		var htmlSource = new HtmlWebViewSource
		{
			Html =
			"<html><head><link rel='stylesheet' type='text/css' href='Resources/style.css'></head><body><h1>Hello, this is a WebView with CSS styling!</h1></body></html>"
		};
		htmlSource.BaseUrl = "\\";

        webView1.Source = htmlSource;
	}
}