﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace AccessTeamStudio
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Access Team Studio"),
        ExportMetadata("Description", "Checks Access Team Records"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "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"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new AccessTeamStudio();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}