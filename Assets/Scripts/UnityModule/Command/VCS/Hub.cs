using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;
using UnityModule.Settings;

namespace UnityModule.Command.VCS
{
    [PublicAPI]
    public class HubAsync : Hub<IObservable<string>>
    {
    }

    [PublicAPI]
    public class Hub : Hub<string>
    {
    }

    [PublicAPI]
    public abstract class Hub<TResult> where TResult : class
    {
        private enum SubCommandType
        {
            PullRequest,
        }

        private static readonly Dictionary<SubCommandType, string> SubCommandMap = new Dictionary<SubCommandType, string>()
        {
            {SubCommandType.PullRequest, "pull-request"},
        };

        public static TResult PullRequest(string baseBranchName = "", string message = "", List<string> argumentList = null)
        {
            argumentList = new SafeList<string>(argumentList);
            if (!string.IsNullOrEmpty(baseBranchName))
            {
                argumentList.Add($"-b {baseBranchName}");
            }

            if (!string.IsNullOrEmpty(message))
            {
                argumentList.Add($"-m {message.Quot()}");
            }

            return Run(SubCommandType.PullRequest, argumentList);
        }

        private static TResult Run(SubCommandType subCommandType, List<string> argumentList = null)
        {
            return Runner<TResult>.Run(GitSetting.GetOrDefault().CommandHub, SubCommandMap[subCommandType], argumentList);
        }
    }
}