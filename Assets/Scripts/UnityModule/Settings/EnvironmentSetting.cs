using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityModule.Settings {

    public partial class EnvironmentSetting {

        public partial class EnvironmentSetting_Path {

            private const string DEFAULT_GIT_COMMAND_PATH = "/usr/local/bin/git";

            /// <summary>
            /// git コマンドのパスの実体
            /// </summary>
            [SerializeField]
            private string commandGit = DEFAULT_GIT_COMMAND_PATH;

            /// <summary>
            /// git コマンドのパス
            /// </summary>
            public string CommandGit {
                get {
                    return this.commandGit;
                }
                set {
                    this.commandGit = value;
                }
            }
        }

    }

}