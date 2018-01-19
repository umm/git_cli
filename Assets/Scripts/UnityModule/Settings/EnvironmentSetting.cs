using System;
using UnityEngine;

namespace UnityModule.Settings {

    public partial class EnvironmentSetting {

        public partial class EnvironmentSetting_Path {

            /// <summary>
            /// デフォルトの git コマンドパス
            /// </summary>
            private const string DEFAULT_COMMAND_PATH_GIT = "/usr/local/bin/git";

            /// <summary>
            /// デフォルトの hub コマンドパス
            /// </summary>
            private const string DEFAULT_COMMAND_PATH_HUB = "/usr/local/bin/hub";

            /// <summary>
            /// git コマンドへのパスを保存している環境変数のキー
            /// </summary>
            private const string ENVIRONMENT_KEY_COMMAND_GIT = "COMMAND_GIT";

            /// <summary>
            /// hub コマンドへのパスを保存している環境変数のキー
            /// </summary>
            private const string ENVIRONMENT_KEY_COMMAND_HUB = "COMMAND_HUB";

            /// <summary>
            /// git コマンドのパスの実体
            /// </summary>
            [SerializeField]
            private string commandGit;

            /// <summary>
            /// git コマンドのパス
            /// </summary>
            public string CommandGit {
                get {
                    if (string.IsNullOrEmpty(this.commandGit)) {
                        this.commandGit = Environment.GetEnvironmentVariable(ENVIRONMENT_KEY_COMMAND_GIT);
                    }
                    if (string.IsNullOrEmpty(this.commandGit)) {
                        this.commandGit = DEFAULT_COMMAND_PATH_GIT;
                    }
                    return this.commandGit;
                }
            }

            /// <summary>
            /// hub コマンドのパスの実体
            /// </summary>
            [SerializeField]
            private string commandHub;

            /// <summary>
            /// hub コマンドのパス
            /// </summary>
            public string CommandHub {
                get {
                    if (string.IsNullOrEmpty(this.commandHub)) {
                        this.commandHub = Environment.GetEnvironmentVariable(ENVIRONMENT_KEY_COMMAND_HUB);
                    }
                    if (string.IsNullOrEmpty(this.commandHub)) {
                        this.commandHub = DEFAULT_COMMAND_PATH_HUB;
                    }
                    return this.commandHub;
                }
            }

        }

    }

}