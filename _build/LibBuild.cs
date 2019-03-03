using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class LibBuild : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<LibBuild>(x => x.Build);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] string MyGetSource;
    [Parameter] string MyGetApiKey;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Build => _ => _
        .DependsOn(Restore)
        .Before(Test)
        .Executes(() =>
        {
            //VersionInfo determineVersionInfo = DetermineVersionInfo(Configuration == Configuration.Release);

            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.GetNormalizedAssemblyVersion())
                .SetFileVersion(GitVersion.GetNormalizedFileVersion())
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });


    Target Test => _ => _
        .Before(Pack)
        .Executes(() =>
        {
            DotNetTest(s => s
               .SetProjectFile(Solution)
               .SetConfiguration(Configuration)
               .EnableNoBuild()
               .EnableNoRestore());
        });

    Target Pack => _ => _
        .Executes(() =>
        {
            DotNetPack(s => s
               .SetConfiguration(Configuration)
               .EnableNoBuild()
               .EnableNoRestore());
        });

    Target Push => _ => _
         .DependsOn(Pack)
         .Requires(() => MyGetSource)
         .Requires(() => MyGetApiKey)
         .Requires(() => Configuration.ToString().EqualsOrdinalIgnoreCase("Release"))
         .Executes(() =>
         {
             GlobFiles(ArtifactsDirectory, "*.nupkg").NotEmpty()
                .ForEach(x =>
                {
                    DotNetNuGetPush(s => s
                        .SetTargetPath(x)
                        .SetSource(MyGetSource)
                        .SetApiKey(MyGetApiKey));
                });
         });
    public static VersionInfo DetermineVersionInfo(bool ForProd)
    {
        string globalTool = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.dotnet\tools\dotnet-gitversion.exe");
        string globalToolUnix = Environment.ExpandEnvironmentVariables($"$HOME/.dotnet/tools");
        string dotnetTool = ToolPathResolver.GetPathExecutable("dotnet");

        IProcess res;
        if (File.Exists(globalTool))
        {
            // Try using the global tool
            Logger.Trace($"Attempting to use {globalTool}");

            res = ProcessTasks.StartProcess(globalTool, logOutput: false).AssertWaitForExit();
        }
        else if (File.Exists(globalToolUnix))
        {
            // Try using the global tool
            Logger.Trace($"Attempting to use {globalToolUnix}");

            res = ProcessTasks.StartProcess(globalToolUnix, logOutput: false).AssertWaitForExit();
        }
        else
        {
            // Try through dotnet
            res = ProcessTasks.StartProcess(dotnetTool, "gitversion", logOutput: false).AssertWaitForExit();
        }

        if (res.ExitCode != 0)
            throw new Exception("Unable to run 'dotnet gitversion', install using https://www.nuget.org/packages/GitVersion.Tool");

        string json = string.Join("", res.Output.Where(s => s.Type == OutputType.Std).Select(s => s.Text));

        JObject obj = JsonConvert.DeserializeObject<JObject>(json);

        string suffix = ForProd ? string.Empty : "-alpha";

        string nugetVersion = obj.Value<string>("AssemblySemFileVer") + suffix;
        string informationalVersion = $"{nugetVersion}+branch:{obj.Value<string>("BranchName")}+sha:{obj.Value<string>("Sha")}";

        return new VersionInfo
        {
            AssemblyVersion = obj.Value<string>("AssemblySemVer"),
            AssemblyFileVersion = obj.Value<string>("AssemblySemFileVer"),
            AssemblyInformationalVersion = informationalVersion,
            NugetVersion = nugetVersion
        };
    }


}

class VersionInfo
{
    public string AssemblyVersion { get; set; }
    public string AssemblyFileVersion { get; set; }
    public string AssemblyInformationalVersion { get; set; }
    public string NugetVersion { get; set; }
}
