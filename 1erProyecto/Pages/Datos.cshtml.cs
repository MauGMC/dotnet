using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1erProyecto.Pages;

public class DatosModel : PageModel
{
    private readonly ILogger<DatosModel> _logger;

    public DatosModel(ILogger<DatosModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

