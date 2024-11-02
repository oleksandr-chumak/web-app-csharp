using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace web_app_csharp.Utils;

public class ModelErrorUtil
{
    public static Dictionary<string, List<string>> GetErrors(ModelStateDictionary modelState)
    {
        var fieldErrors = new Dictionary<string, List<string>>();

        foreach (var key in modelState.Keys)
        {
            var errors = modelState[key]?.Errors;
            if (errors == null || errors.Count == 0) continue;

            fieldErrors[key] = new List<string>();
            foreach (var error in errors)
            {
                fieldErrors[key].Add(error.ErrorMessage);
            }
        }

        return fieldErrors;
    }

    public static bool IsFieldInvalid(Dictionary<string, List<string>> errors, string fieldName)
    {
        return errors.ContainsKey(fieldName);
    }

    public static bool IsFieldValid(Dictionary<string, List<string>> errors, string fieldName)
    {
        return !errors.ContainsKey(fieldName);
    }

    public static string GetFieldClass(Dictionary<string, List<string>> errors, string fieldName, bool submitted)
    {
        if (!submitted) return string.Empty;
        return IsFieldValid(errors, fieldName) ? "is-valid" : "is-invalid";
    }
}