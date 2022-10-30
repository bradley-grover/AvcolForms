using System.Text;
using AvcolForms.Core.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;

#nullable disable

namespace AvcolForms.Web.Areas.Forms;

[Route(Routes.Forms.ViewUrl)]
public partial class View
{
    internal Form AppForm { get; set; }
    internal bool _firstLoading = true;

    private readonly List<BreadcrumbItem> _items = new()
    {
        new BreadcrumbItem("Forms", Routes.Forms.Dash, icon: Icons.Filled.Forum)
    };

    [Inject]
    public IDbContextFactory<ApplicationDbContext> Factory { get; set; }

    [CascadingParameter]
    [Inject]
    public AuthenticationStateProvider AuthState { get; set; }

    [Inject]
    public IEmailSender EmailSender { get; set; }
    
    [Parameter]
    public Guid Id { get; set; }

    private FormResponse Response { get; } = new()
    {
        Id = Guid.NewGuid(),
        Created = default,
        Modified = default
    };

    private bool _buttonLock = false;

    private async Task CreateAsync()
    {
        if (_buttonLock)
        {
            return;
        }

        _buttonLock = true;

        try
        {
            using var context = await _factory.CreateDbContextAsync();

            AppForm.Responses.Add(Response);

            context.Forms.Update(AppForm);

            StringBuilder builder = new(100);

            builder.AppendLine($"{AppForm.Title}\n");

            await EmailSender.SendEmailAsync(AppForm.Receiver, $"Response by {Response.User}", $"");
        }
        finally
        {
            _buttonLock = false;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var state = await AuthState.GetAuthenticationStateAsync();

        var user = await _userManager.GetUserAsync(state.User);

        // edge case
        if (user == null)
        {
            _navManager.NavigateTo("/");
            return;
        }

        using var context = await Factory.CreateDbContextAsync(); // scoped context

        var form = await context.Forms
            .Include(f => f.Responses)
            .Where(f => (f.Closes == null || f.Closes.Value.Ticks >= DateTimeOffset.UtcNow.Ticks) && f.Recipients.Contains(user.Id))
            .Where(f => !f.Responses.Any(r => r.User == user))
            .Where(f => f.Id == Id)
            .FirstOrDefaultAsync();

        if (form is null)
        {
            _navManager.NavigateTo("/");
            return;
        }

        AppForm = form;

        _items.Add(new BreadcrumbItem(AppForm.Title, null, true, Icons.Filled.Forum));

        Response.User = user;
        Response.Form = AppForm;

        _firstLoading = false; // set loading flag
    }
}
