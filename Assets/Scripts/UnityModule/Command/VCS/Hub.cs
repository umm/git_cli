using System.Collections.Generic;
using UniRx;
using UnityModule.Settings;

namespace UnityModule.Command.VCS {

    public class HubAsync : Hub<IObservable<string>> {}

    public class Hub : Hub<string> {}

    public abstract class Hub<TResult> where TResult : class {

        private enum SubCommandType {
            PullRequest,
        }

        private static readonly Dictionary<SubCommandType, string> SUB_COMMAND_MAP = new Dictionary<SubCommandType, string>() {
            { SubCommandType.PullRequest, "pull-request" },
        };

        public static TResult PullRequest(string baseBranchName = "", string message = "", List<string> argumentList = null) {
            argumentList = new SafeList<string>(argumentList);
            if (!string.IsNullOrEmpty(baseBranchName)) {
                argumentList.Add(string.Format("-b {0}", baseBranchName));
            }
            if (!string.IsNullOrEmpty(message)) {
                argumentList.Add(string.Format("-m {0}", message.Quot()));
            }
            return Run(SubCommandType.PullRequest, argumentList);
        }

        private static TResult Run(SubCommandType subCommandType, List<string> argumentList = null) {
            return Runner<TResult>.Run(EnvironmentSetting.Instance.Path.CommandHub, SUB_COMMAND_MAP[subCommandType], argumentList);
        }

    }

}
