using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.ViewModels;

namespace MVCMiniAPpp.ViewComponents.Contact;

public sealed class ContactViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync() =>
        Task.FromResult<IViewComponentResult>(View(new ContactViewModel()));
}
