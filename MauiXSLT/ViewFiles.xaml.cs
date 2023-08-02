namespace MauiXSLT;

public partial class ViewFiles : ContentPage
{
	public ViewFiles()
	{
		InitializeComponent();
	}

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var XsltTextPath = Path.Combine(FileSystem.AppDataDirectory, "XsltText.txt");
            var inventoryXMLPath = Path.Combine(FileSystem.AppDataDirectory, "inventory.xml");
            var txtFile = File.CreateText(XsltTextPath);
            var xmlFile = File.CreateText(inventoryXMLPath);
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("xml", StringComparison.OrdinalIgnoreCase))
                {
                    if (result.FileName.EndsWith("xml"))
                    {
                        using var stream = await result.OpenReadAsync();
                        StreamWriter xmlSw = new StreamWriter(stream);
                        txtFile.Write(xmlSw.ToString());
                        xmlSw.Flush();
                        xmlSw.Close();
                        stream.Flush();
                        stream.Close();
                    }
                    else if (result.FileName.EndsWith("txt"))
                    {
                        using var stream = await result.OpenReadAsync();
                        StreamWriter txtSw = new StreamWriter(stream);
                        txtFile.Write(txtSw.ToString());
                        txtSw.Flush();
                        txtSw.Close();
                        stream.Flush();
                        stream.Close();
                    }
                    
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }
        return null;
    }

    private async void BtnFilePicker_Clicked(object sender, EventArgs e)
    {
        PickOptions pickOptions = new PickOptions();
        pickOptions.PickerTitle = "Text and XML File Picker";

        var fileResults = await PickAndShow(pickOptions);
        var FullPathToFile = fileResults.FullPath;
        await DisplayAlert(FullPathToFile, FullPathToFile, "Cancel");
    }
}