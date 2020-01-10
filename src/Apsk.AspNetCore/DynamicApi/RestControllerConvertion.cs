// <copyright file="RestControllerConvertion.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.AspNetCore.DynamicApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Apsk.AspNetCore.Annotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class RestControllerConvertion
         : IApplicationModelConvention
    {
        /// <inheritdoc/>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                // 获取RestControllerAttribute
                var restController = controller.Attributes.FirstOrDefault(attr => attr.GetType() == typeof(RestControllerAttribute)) as RestControllerAttribute;
                if (restController == null)
                    continue;

                if (string.IsNullOrWhiteSpace(controller.ControllerName))
                    continue;

                var controllerName = controller.ControllerName;
                foreach (var controllerPostfix in restController.ControllerPostfixes)
                    controllerName = controllerName.Replace(controllerPostfix, string.Empty);
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
                        actionName = actionName.Replace(postfix, string.Empty);

                    action.ActionName = actionName;

                    if (action.ApiExplorer.IsVisible == null)
                        action.ApiExplorer.IsVisible = true;

                    routes.Add(restController.Scene);
                    routes.Add(controllerName.ToLowerInvariant());
                    routes.Add(actionName.ToLowerInvariant());

                    attributeRouteModel.Template = $"{string.Join(restController.Separator, routes)}/{restController.Version}";
                    if (!action.Selectors.Any())
                    {
                        selectorModel.AttributeRouteModel = attributeRouteModel;
                        selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "get" }));
                        action.Selectors.Add(selectorModel);
                    }
                    else
                    {
                        foreach (var selector in action.Selectors)
                        {
                            if (!selector.ActionConstraints.Any())
                                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "get" }));

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

        private bool CanUseFromBody(IList<SelectorModel> selectors)
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
