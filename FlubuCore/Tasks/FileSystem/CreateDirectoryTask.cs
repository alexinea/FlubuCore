using System.IO;
using FlubuCore.Context;

namespace FlubuCore.Tasks.FileSystem
{
    public class CreateDirectoryTask : TaskBase<int>
    {
        private readonly bool _forceRecreate;

        public CreateDirectoryTask(string directoryPath, bool forceRecreate)
        {
            DirectoryPath = directoryPath;
            _forceRecreate = forceRecreate;
        }

        public string DirectoryPath { get; set; }

        public static void Execute(ITaskContextInternal context, string directoryPath, bool forceRecreate)
        {
            var task = new CreateDirectoryTask(directoryPath, forceRecreate);
            task.ExecuteVoid(context);
        }

        protected override int DoExecute(ITaskContextInternal context)
        {
            context.LogInfo($"Create directory '{DirectoryPath}'");

            if (Directory.Exists(DirectoryPath))
            {
                if (!_forceRecreate)
                    return 0;

                Directory.Delete(DirectoryPath, true);
            }

            Directory.CreateDirectory(DirectoryPath);

            return 0;
        }
    }
}