using Infrastructure.Web.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Web.DynamicApi
{
    public class RestControllerConvertion
         : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                //获取RestControllerAttribute
                var restController = controller.Attributes.FirstOrDefault(attr => attr.GetType() == typeof(RestControllerAttribute)) as RestControllerAttribute;
                if (restController == null)
                    continue;

                if (string.IsNullOrWhiteSpace(controller.ControllerName))
                    continue;

                var controllerName = controller.ControllerName;
                foreach (var controllerPostfix in restController.ControllerPostfixes)
                    controllerName = controllerName.Replace(controllerPostfix, "");
                controller.ControllerName = controllerName;

                if (string.IsNullOrWhiteSpace(controller.ApiExplorer.GroupName))
                    controller.ApiExplorer.GroupName = controllerName;

                if (controller.ApiExplorer.IsVisible == null)
                    controller.ApiExplorer.IsVisible = true;

                var actionName = string.Empty;
                var routes = new List<string>();
                var selectorModel = new SelectorModel();
                var attributeRouteModel = new AttributeRouteModel();
                foreach (var action in controller.Actions)
                {
                    if (string.IsNullOrWhiteSpace(action.ActionName))
                        continue;

                    actionName = action.ActionName;
                    foreach (var postfix in restController.ActionPostfixes)
                        actionName = actionName.Replace(postfix, "");

                    action.ActionName = actionName;

                    if (action.ApiExplorer.IsVisible == null)
                        action.ApiExplorer.IsVisible = true;

                    routes.Add(restController.Scene);
                    routes.Add(controllerName);
                    routes.Add(actionName);

                    attributeRouteModel.Template = string.Join(restController.Separator, routes);
                    if (!action.Selectors.Any())
                    {
                        selectorModel.AttributeRouteModel = attributeRouteModel;
                        selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "post" }));
                        action.Selectors.Add(selectorModel);
                    }
                    else
                    {
                        foreach (var selector in action.Selectors)
                        {
                            if (!selector.ActionConstraints.Any())
                                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "post" }));

                            selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(selector.AttributeRouteModel, attributeRouteModel);
                        }
                    }

                    foreach (var parameter in action.Parameters)
                    {
                        if (parameter.BindingInfo != null)
                            continue;

                        if (!typeof(ValueType).IsAssignableFrom(parameter.ParameterType) && CanUseFromBody(action.Selectors))
                            parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        else
                            parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() });
                    }
                    routes.Clear();
                }
            }
        }

        public bool CanUseFromBody(IList<SelectorModel> selectors)
        {
            var methods = new string[] { "GET", "HEAD", "DELETE" };
            foreach (var selector in selectors)
            {
                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpActionConstraint.HttpMethods.Any(method => methods.Contains(method)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
