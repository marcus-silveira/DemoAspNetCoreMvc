using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.Extensions;

[HtmlTargetElement("*", Attributes = "type-button, route-id")]
public class ButtonTagHelper(IHttpContextAccessor contextAccessor) : TagHelper
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

        
        output.TagName = "a";
        output.Attributes.SetAttribute("href", $"{controller}/{_actionName}/{RouteId}");
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