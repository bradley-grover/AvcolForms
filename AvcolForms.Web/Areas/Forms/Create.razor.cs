using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
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
        Recipients = new List<string>(),
        Content = new List<FormContent>(),
        Closes = DateTimeOffset.UtcNow,
        Created = default,
        Modified = default
    };

    private bool _processLock = false;
    private bool _errorOccured = false;

    [DataType(DataType.DateTime)]
    private DateTime? Time { get; set; }

    private TimeSpan? Span { get; set; }


    private readonly List<BreadcrumbItem> _items = new()
    {
        new BreadcrumbItem("Forms", null, disabled: true, Icons.Filled.Forum),
        new BreadcrumbItem("Creation", null, disabled: true, Icons.Filled.Create)
    };

    protected override async Task OnInitializedAsync()
    {
        var user = await _authProvider.GetIdentityUserAsync(_userManager);

        if (user is null)
        {
            _navManager.NavigateTo(Routes.Accounts.Login, true);
            return;
        }

        CreateForm.CreatedBy = user.Id;
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
        _errorOccured = false;

        try
        {
            if (Time.HasValue && Span.HasValue)
            {
                Time = Time.Value.Add(Span.Value);
            }

            CreateForm.Closes = ConvertToDtOffset(Time);
            CreateForm.Created = DateTimeOffset.UtcNow;
            CreateForm.Modified = CreateForm.Created;

            Debug.WriteLine("Set fields");

            using var context = await _factory.CreateDbContextAsync();

            CreateForm.Recipients.Add(CreateForm.CreatedBy);
            CreateForm.Content.Add(new() { Id = Guid.NewGuid(), ContentType = FormContentType.Multichoice, Created = DateTimeOffset.UtcNow, Data = "e", Modified = DateTimeOffset.UtcNow });

            context.Forms.Add(CreateForm); 

            Debug.WriteLine($"Form Id: {CreateForm.Id}");
            Debug.WriteLine($"User Id: {CreateForm.CreatedBy}");

            Debug.WriteLine("Attempted adding");

            await context.SaveChangesAsync();

            var list = new List<Task>();

            CreateForm.Recipients.ForEach(s => list.Add(SendEmailAsync(s)));

            _ = Task.WhenAll(list); // continue in the background, we don't really care when it completes

            _navManager.NavigateTo(Routes.Forms.Dash, true);
        }
        catch (Exception ex)
        {
            _logger.LogError("{ex}", ex);
            _errorOccured = true;
        }
        finally
        {
            _processLock = false;
        }
    }

    private async Task SendEmailAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        Uri uri = _navManager.ToAbsoluteUri($"{Routes.Forms.View}/{CreateForm.Id}");

        StringBuilder builder = new();

        builder.AppendLine("Hi there, a new form to be answered is now available.<br/><br/>");
        builder.AppendLine($"The title of the form is {CreateForm.Title}.<br/>");
        builder.Append($"Description is as: {CreateForm.Description}<br/>");
        builder.Append($"You can access the form <a href='{HtmlEncoder.Default.Encode(uri.ToString())}'>here</a><br/><br/>");
        builder.Append(CreateForm.Closes is null ? "<b>This form does not expire</b>" : $"<b>Get in before it expires at {CreateForm.Closes.ToLocalTimeOrNone()}</b>");

        await _emailSender.SendEmailAsync(user.Email, "New Form Available", builder.ToString());
    }
}
