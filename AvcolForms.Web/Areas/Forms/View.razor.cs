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

    [Inject]
    public IDbContextFactory<ApplicationDbContext> Factory { get; set; }

    [CascadingParameter]
    [Inject]
    public AuthenticationStateProvider AuthState { get; set; }

    [Inject]
    public IEmailSender EmailSender { get; set; }


    
    [Parameter]
    public Guid Id { get; set; }

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
            .Where(f => f.Id == Id)
            .Where(f => f.Recipients.Any(u => u == user))
            .FirstOrDefaultAsync();

        if (form is null)
        {
            _navManager.NavigateTo("/");
            return;
        }

        AppForm = form;

        _firstLoading = false; // set loading flag
    }
}
