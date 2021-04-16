using CentralSecurityProject.Common;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// کلاس خاص برای ساخت کنترلر های
    /// اچ تی ام ال هلپر
    /// </summary>
    public static class MyHtmlHelper
    {
        public static MvcHtmlString CustomActionLink(this HtmlHelper helper, string linkCaption, string action, string controller, object routValues = null,
            string linkClass = "btn btn-secondary btn-sm", string iconClass = "", ActionLinkType actionLinkType = ActionLinkType.None)
        {
            MvcHtmlString actionTag = helper.ActionLink(linkCaption, action, controller, routValues, htmlAttributes: new { @class = linkClass });

            TagBuilder aTag = new TagBuilder("a");
            aTag.AddCssClass(linkClass);

            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string href = url.Action(action, controller, routValues);
            aTag.MergeAttribute("href", href);

            TagBuilder iTag = new TagBuilder("i");
            #region Set Action Link Type
            switch (actionLinkType)
            {
                case ActionLinkType.None:
                    iTag.AddCssClass(iconClass);
                    break;
                case ActionLinkType.Create:
                    iTag.AddCssClass("glyphicon glyphicon-plus");
                    break;
                case ActionLinkType.Edit:
                    iTag.AddCssClass("glyphicon glyphicon-edit");
                    break;
                case ActionLinkType.Delete:
                    iTag.AddCssClass("glyphicon glyphicon-trash");
                    break;
                case ActionLinkType.Details:
                    iTag.AddCssClass("glyphicon glyphicon-list");
                    break;
                case ActionLinkType.Selected:
                    iTag.AddCssClass("glyphicon glyphicon-check");
                    break;
                case ActionLinkType.Unselected:
                    iTag.AddCssClass("glyphicon glyphicon-unchecked");
                    break;
                case ActionLinkType.Approve:
                    iTag.AddCssClass("glyphicon glyphicon-ok");
                    break;
                case ActionLinkType.Cancel:
                    iTag.AddCssClass("glyphicon glyphicon-remove");
                    break;
                case ActionLinkType.Referral:
                    iTag.AddCssClass("glyphicon glyphicon-share-alt");
                    break;
                default:
                    iTag.AddCssClass(iconClass);
                    break;
            }
            #endregion
            aTag.InnerHtml = $"{iTag.ToString()} {linkCaption}";

            return MvcHtmlString.Create(aTag.ToString());
        }
    }
}