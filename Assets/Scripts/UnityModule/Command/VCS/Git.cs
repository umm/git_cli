using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityModule.Settings;

namespace UnityModule.Command.VCS
{
    [PublicAPI]
    public class ObservableGit : Git<IObservable<string>>
    {
    }

    [PublicAPI]
    public class Git : Git<string>
    {
    }

    [PublicAPI]
    public abstract class Git<TResult> where TResult : class
    {
        private enum SubCommandType
        {
            Add,
            Branch,
            Checkout,
            Commit,
            Push,
            RevParse,
            Rm,
            Status,
        }

        private static readonly Dictionary<SubCommandType, string> SubCommandMap = new Dictionary<SubCommandType, string>()
        {
            {SubCommandType.Add, "add"},
            {SubCommandType.Branch, "branch"},
            {SubCommandType.Checkout, "checkout"},
            {SubCommandType.Commit, "commit"},
            {SubCommandType.Push, "push"},
            {SubCommandType.RevParse, "rev-parse"},
            {SubCommandType.Rm, "rm"},
            {SubCommandType.Status, "status"},
        };

        public static TResult Add(IEnumerable<string> files = null, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList)
            {
                files == null ? "." : $"-- {files.Combine()}",
            };

            return Run(SubCommandType.Add, argumentList, timeout);
        }

        public static TResult Branch(string branchName, bool force = false, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList);
            if (force)
            {
                argumentList.Add("-f");
            }

            argumentList.Add(branchName);
            return Run(SubCommandType.Branch, argumentList, timeout);
        }

        public static TResult Checkout(string branchName, bool create = false, bool force = false, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList);
            if (create)
            {
                argumentList.Add("-b");
            }

            argumentList.Add(branchName);
            return Run(SubCommandType.Checkout, argumentList, timeout);
        }

        public static TResult Commit(string message, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            // コマンド経由の場合何らかのメッセージを入れないとコミットできない
            argumentList = new SafeList<string>(argumentList)
            {
                $"-m {message.Quot()}",
            };
            return Run(SubCommandType.Commit, argumentList, timeout);
        }

        public static TResult Push(string branchName, string remoteName = "origin", List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList)
            {
                remoteName,
                branchName,
            };
            return Run(SubCommandType.Push, argumentList, timeout);
        }

        public static TResult RevParse(List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList);
            return Run(SubCommandType.RevParse, argumentList, timeout);
        }

        public static TResult Rm(IEnumerable<string> files, bool ignoreUnmatch = true, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList);
            if (ignoreUnmatch)
            {
                argumentList.Add("--ignore-unmatch");
            }

            argumentList.Add($"-- {files.Combine()}");
            return Run(SubCommandType.Rm, argumentList, timeout);
        }

        public static TResult Status(bool useShortFormat = true, List<string> argumentList = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            argumentList = new SafeList<string>(argumentList);
            if (useShortFormat)
            {
                argumentList.Add("--short");
            }
            return Run(SubCommandType.Status, argumentList, timeout);
        }

        public static TResult GetCurrentBranchName(double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            return RevParse(
                new List<string>()
                {
                    "--abbrev-ref",
                    "HEAD",
                },
                timeout
            );
        }

        public static TResult GetCurrentCommitHash(double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            return RevParse(
                new List<string>()
                {
                    "HEAD",
                },
                timeout
            );
        }

        private static TResult Run(SubCommandType subCommandType, List<string> argumentMap = null, double timeout = Runner<TResult>.DefaultTimeoutSeconds)
        {
            return Runner<TResult>.Run(GitSetting.GetOrDefault().CommandGit, SubCommandMap[subCommandType], argumentMap, timeout);
        }
    }
}
