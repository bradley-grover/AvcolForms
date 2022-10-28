namespace AvcolForms.Web.Areas.Forms.ViewModel;

public class UnansweredFormItem
{
#nullable disable
    public Guid Id { get; set; }
    public string Title { get; set; }

    public int ContentCount { get; set; }

    public DateTimeOffset? EndDate { get; set; }
}
