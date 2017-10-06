using System.Collections.Generic;
using UniRx;
using UnityModule.Settings;

namespace UnityModule.Command.VCS {

    public static class Hub {

        private enum SubCommandType {
            PullRequest,
        }

        private static readonly Dictionary<SubCommandType, string> SUB_COMMAND_MAP = new Dictionary<SubCommandType, string>() {
            { SubCommandType.PullRequest, "pull-request" },
        };

        public static IObservable<string> PullRequest(string baseBranchName = "", List<string> argumentList = null) {
            argumentList = new SafeList<string>(argumentList);
            if (!string.IsNullOrEmpty(baseBranchName)) {
                argumentList.Add(string.Format("-b {0}", baseBranchName));
            }
            return Run(SubCommandType.PullRequest, argumentList);
        }

        private static IObservable<string> Run(SubCommandType subCommandType, List<string> argumentList = null) {
            return Runner.RunCommand(EnvironmentSetting.Instance.Path.CommandHub, SUB_COMMAND_MAP[subCommandType], argumentList);
        }

    }

}