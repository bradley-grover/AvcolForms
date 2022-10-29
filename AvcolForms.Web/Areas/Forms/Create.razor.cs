using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using AvcolForms.Core.Data.Models;

namespace AvcolForms.Web.Areas.Forms;

/// <summary>
/// Create page, for creating data forms for users to respond to.
/// </summary>
[Route(Routes.Forms.Create)]
public sealed partial class Create
{
    private Form CreateForm { get; } = new()
    {
        Id = Guid.NewGuid(),
        Recipients = new List<ApplicationUser>(),
        Content = new List<FormContent>(),
        Closes = DateTimeOffset.UtcNow,
        Created = default,
        Modified = default
    };

    private bool _processLock = false;

    [DataType(DataType.DateTime)]
    private DateTime? Time { get; set; }

    private readonly List<BreadcrumbItem> _items = new()
    {
        new BreadcrumbItem("Forms", null, disabled: true, Icons.Filled.Forum),
        new BreadcrumbItem("Creation", null, disabled: true, Icons.Filled.Create)
    };

    protected override async Task OnInitializedAsync()
    {
        CreateForm.CreatedBy = await _authProvider.GetIdentityUserAsync(_userManager);

        if (CreateForm.CreatedBy is null)
        {
            _navManager.NavigateTo(Routes.Accounts.Login, true);
        }
    }

    private static DateTimeOffset? ConvertToDtOffset(DateTime? time)
    {
        return time is null ? null : new DateTimeOffset(time.Value, DateTimeOffset.Now.Offset);
    }

    private async Task CreateAsync()
    {
        if (_processLock) // guard against multiple button presses
        {
            Debug.WriteLine("Reached create form lock");
            return;
        }

        Debug.WriteLine("Entering past lock");

        _processLock = true;

        CreateForm.Closes = ConvertToDtOffset(Time);
        CreateForm.Created = DateTimeOffset.UtcNow;
        CreateForm.Modified = CreateForm.Created;

        Debug.WriteLine("Set fields");

        using var context = await _factory.CreateDbContextAsync();

        CreateForm.Recipients.Add(new ApplicationUser());
        CreateForm.Content.Add(new() { Id = Guid.NewGuid(), ContentType = FormContentType.Multichoice, Created = DateTimeOffset.UtcNow, Data = "e", Modified = DateTimeOffset.UtcNow });

        context.Forms.Add(CreateForm);

        Debug.WriteLine("Attempted adding");

        await context.SaveChangesAsync();

        _navManager.NavigateTo(Routes.Forms.Dash, true);

        _processLock = false;
    }
}
