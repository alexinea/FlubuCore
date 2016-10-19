﻿using System.Collections.Generic;
using FlubuCore.Context;
using FlubuCore.Tasks.Process;

namespace FlubuCore.Tasks.NetCore
{
    public class ExecuteDotnetTask : TaskBase
    {
        private readonly List<string> _arguments = new List<string>();
        private string _workingFolder;
        private string _dotnetExecutable;

        public ExecuteDotnetTask(string command)
        {
            Command = command;
        }

        public ExecuteDotnetTask(StandardDotnetCommands command)
        {
            Command = command.ToString().ToLowerInvariant();
        }

        public string Command { get; private set; }

        public ExecuteDotnetTask WithArguments(string arg)
        {
            _arguments.Add(arg);
            return this;
        }

        public ExecuteDotnetTask WithArguments(params string[] args)
        {
            _arguments.AddRange(args);
            return this;
        }

        public ExecuteDotnetTask WorkingFolder(string folder)
        {
            _workingFolder = folder;
            return this;
        }

        public ExecuteDotnetTask DotnetExecutable(string fullPath)
        {
            _dotnetExecutable = fullPath;
            return this;
        }

        public ExecuteDotnetTask XmlOutput(string fullPath)
        {
            _arguments.Add("-xml");
            _arguments.Add(fullPath);
            return this;
        }

        protected override int DoExecute(ITaskContext context)
        {
            string program = _dotnetExecutable;

            if (string.IsNullOrEmpty(program))
            {
                program = context.GetDotnetExecutable();
            }

            if (string.IsNullOrEmpty(program))
            {
                context.Fail("Dotnet executable not set!", -1);
                return -1;
            }

            RunProgramTask task = context.Tasks().RunProgramTask(program);

            return task
                .WithArguments(Command)
                .WithArguments(_arguments.ToArray())
                .WorkingFolder(_workingFolder)
                .Execute(context);
        }
    }
}