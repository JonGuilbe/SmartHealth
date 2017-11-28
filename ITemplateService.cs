using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SmartHealth.Data;
namespace SmartHealth{
    /// <summary>
    /// Renders email content based on razor templates
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders a template given the provided view model
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="filename">Filename of the template to render</param>
        /// <param name="viewModel">View model to use for rendering the template</param>
        /// <returns>Returns the rendered template content</returns>
        Task<string> RenderTemplateAsync<TViewModel>(string filename, TViewModel viewModel);
    }
}