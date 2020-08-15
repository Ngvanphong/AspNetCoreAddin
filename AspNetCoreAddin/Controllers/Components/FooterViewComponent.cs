using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreAddin.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View());
        }
    }
}