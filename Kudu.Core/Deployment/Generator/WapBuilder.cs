﻿using Kudu.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kudu.Core.Deployment.Generator
{
    public class WapBuilder : GeneratorSiteBuilder
    {
        private readonly string _projectPath;
        private readonly string _solutionPath;

        public WapBuilder(IEnvironment environment, IDeploymentSettingsManager settings, IBuildPropertyProvider propertyProvider, string sourcePath, string projectPath, string solutionPath)
            : base(environment, settings, propertyProvider, sourcePath)
        {
            _projectPath = projectPath;
            _solutionPath = solutionPath;
        }

        protected override string ScriptGeneratorCommandArguments
        {
            get
            {
                StringBuilder commandArguments = new StringBuilder();
                commandArguments.AppendFormat("--aspWAP \"{0}\"", _projectPath);

                if (!String.IsNullOrEmpty(_solutionPath))
                {
                    commandArguments.AppendFormat(" --solutionFile \"{0}\"", _solutionPath);
                }

                return commandArguments.ToString();
            }
        }
    }
}
