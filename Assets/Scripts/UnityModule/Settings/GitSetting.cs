using System;
using UnityEngine;

namespace UnityModule.Settings
{
    public class GitSetting : Setting<GitSetting>, IEnvironmentSetting
    {
        /// <summary>
        /// デフォルトの git コマンドパス
        /// </summary>
        private const string DefaultCommandPathGit = "/usr/local/bin/git";

        /// <summary>
        /// デフォルトの hub コマンドパス
        /// </summary>
        private const string DefaultCommandPathHub = "/usr/local/bin/hub";

        /// <summary>
        /// git コマンドへのパスを保存している環境変数のキー
        /// </summary>
        private const string EnvironmentKeyCommandGit = "COMMAND_GIT";

        /// <summary>
        /// hub コマンドへのパスを保存している環境変数のキー
        /// </summary>
        private const string EnvironmentKeyCommandHub = "COMMAND_HUB";

        /// <summary>
        /// git コマンドのパスの実体
        /// </summary>
        [SerializeField] private string commandGit = (
            !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentKeyCommandGit))
                ? Environment.GetEnvironmentVariable(EnvironmentKeyCommandGit)
                : DefaultCommandPathGit
        );

        /// <summary>
        /// git コマンドのパス
        /// </summary>
        public string CommandGit => commandGit;

        /// <summary>
        /// hub コマンドのパスの実体
        /// </summary>
        [SerializeField] private string commandHub = (
            !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentKeyCommandHub))
                ? Environment.GetEnvironmentVariable(EnvironmentKeyCommandHub)
                : DefaultCommandPathHub
        );

        /// <summary>
        /// hub コマンドのパス
        /// </summary>
        public string CommandHub => commandHub;

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Create/Settings/Git Setting")]
        public static void CreateSettingAsset()
        {
            CreateAsset();
        }
#endif
    }
}