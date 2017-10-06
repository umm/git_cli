using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityModule.Settings {

    public partial class EnvironmentSetting {

        public partial class EnvironmentSetting_Path {

            /// <summary>
            /// デフォルトの git コマンドパス
            /// </summary>
            private const string DEFAULT_GIT_COMMAND_PATH = "/usr/local/bin/git";

            /// <summary>
            /// デフォルトの hub コマンドパス
            /// </summary>
            private const string DEFAULT_HUB_COMMAND_PATH = "/usr/local/bin/hub";

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
            }

            /// <summary>
            /// hub コマンドのパスの実体
            /// </summary>
            [SerializeField]
            private string commanHub = DEFAULT_GIT_COMMAND_PATH;

            /// <summary>
            /// hub コマンドのパス
            /// </summary>
            public string CommandHub {
                get {
                    return this.commanHub;
                }
            }

        }

    }

}