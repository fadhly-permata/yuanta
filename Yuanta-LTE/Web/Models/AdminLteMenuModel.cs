namespace Web.Models;

public class AdminLteMenuModel
{
    public string? Icon { get; set; }
    public string? Text { get; set; }
    public string? Path { get; set; }

    public List<AdminLteMenuModel>? Childs { get; set; }
}
