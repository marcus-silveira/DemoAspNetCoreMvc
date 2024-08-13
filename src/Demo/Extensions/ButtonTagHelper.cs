using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.Extensions;

[HtmlTargetElement("*", Attributes = "type-button, route-id")]
public class ButtonTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator) : TagHelper
{
    [HtmlAttributeName("type-button")]
    public TypeButton TypeButton { get; set; }
    
    [HtmlAttributeName("route-id")]
    public int RouteId { get; set; }

    private string? _actionName = string.Empty;
    private string? _className = string.Empty;
    private string? _spanIcon = string.Empty;
    
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        switch (TypeButton)
        {
            case TypeButton.Details:
                _actionName = "Details";
                _className = "btn btn-info";
                _spanIcon = "fa fa-search";
                break;
            case TypeButton.Edit:
                _actionName = "Edit";
                _className = "btn btn-warning";
                _spanIcon = "fa fa-edit";
                break;
            case TypeButton.Delete:
                _actionName = "Delete";
                _className = "btn btn-danger";
                _spanIcon = "fa fa-trash";
                break;
            default: 
                throw new ArgumentOutOfRangeException();
        }
        
        var controller = contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

        var host = $"{contextAccessor.HttpContext.Request.Scheme}://" +
                   $"{contextAccessor.HttpContext.Request.Host.Value}";
        
        var indexPath = linkGenerator.GetPathByAction(contextAccessor.HttpContext, _actionName, controller,
            values: new { id = RouteId })!;
        
        output.TagName = "a";
        output.Attributes.SetAttribute("href", $"{host}{indexPath}");
        output.Attributes.SetAttribute("class", _className);
        
        var span = new TagBuilder("span");
        span.AddCssClass(_spanIcon);
        output.Content.AppendHtml(span);
    }
}

public enum TypeButton
{
    Details = 1,
    Edit,
    Delete
}