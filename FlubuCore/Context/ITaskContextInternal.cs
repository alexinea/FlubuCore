﻿using System;
using FlubuCore.Context.FluentInterface;
using FlubuCore.Scripting;
using FlubuCore.Tasks;

namespace FlubuCore.Context
{
    public interface ITaskContextInternal : ITaskContext, IDisposable
    {
        CommandArguments Args { get; }

        bool IsInteractive { get; }

        void DecreaseDepth();

        void Fail(string message, int errorCode);

        void IncreaseDepth();

        void LogInfo(string message);

        void LogError(string message);
    }
}
