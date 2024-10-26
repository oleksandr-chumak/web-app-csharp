using System.ComponentModel.DataAnnotations;

namespace web_app_csharp.Models.Departments;

public class CreateDepartmentModel
{
    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(20, ErrorMessage = "Department name can't be longer than 20 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Department info is required.")]
    [StringLength(40, ErrorMessage = "Department info can't be longer than 40 characters.")]
    public string Info { get; set; } = "";

    public Dictionary<string, List<string>> FieldErrors { get; set; } = new();
}