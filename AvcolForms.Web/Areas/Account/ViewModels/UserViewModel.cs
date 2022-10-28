namespace AvcolForms.Web.Areas.Account.ViewModels;

// instead of using ApplicationUser to render user data to the pages we instead create a safe view model instead so that it doesn't contain any sensitive information
// if we were to just use ApplicationUser as the UI model for the page, and just include the fields needed in the table, the other data would still be passed through the websocket connection
// and could have stuff like the password hash and phone numbers stolen from the users and obviously we don't want that.

// This mainly prevents the edge case of the system adminstrator of this website from viewing user's password hash's as that is not ethical at all do.s

// This class on top of having the view model contains a static method to convert the database model to the view model that we can use

public class UserViewModel
{
#nullable disable

    /// <summary>
    /// The unique identifier used to identify the user in the database
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The email address of the user
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Whether the email has been confirmed through the verification link that has been sent to the user upon registration
    /// </summary>
    public bool EmailConfirmed { get; set; }
    
    /// <summary>
    /// If the account has the <see cref="Roles.Admin"/> role
    /// </summary>
    public bool IsElevated { get; set; }

    public static UserViewModel ConvertToUIModel(ApplicationUser user)
    {
        return new UserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            IsElevated = false // this field should be assigned by the caller but the default is they are not an elevated user
        };
    }
}
