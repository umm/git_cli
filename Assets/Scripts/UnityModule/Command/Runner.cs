using System.Collections.Generic;
using System.Text;
using UniRx;

namespace UnityModule.Command {

    internal static class Runner {

        internal static IObservable<string> RunCommand(string command, string subCommand, List<string> argumentMap = null) {
            return Observable
                .Create<string>(
                    (observer) => {
                        System.Diagnostics.Process process = new System.Diagnostics.Process {
                            StartInfo = {
                                FileName = command,
                                Arguments = string.Format("{0}{1}", subCommand, CreateArgument(argumentMap)),
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                CreateNoWindow = true
                            },
                        };
                        process.Start();
                        process.WaitForExit();
                        if (process.ExitCode == 0) {
                            observer.OnNext(process.StandardOutput.ReadToEnd());
                            observer.OnCompleted();
                        } else {
                            observer.OnError(new System.InvalidOperationException(process.StandardError.ReadToEnd()));
                        }
                        process.Close();
                        return null;
                    }
                );
        }

        private static string CreateArgument(List<string> argumentList) {
            if (argumentList == default(List<string>) || argumentList.Count == 0) {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (string argument in argumentList) {
                sb.AppendFormat(" {0}", argument);
            }
            return sb.ToString();
        }

    }

    internal class SafeList<T> : List<T> {

        public SafeList(List<T> original) {
            if (original != default(List<T>)) {
                this.AddRange(original);
            }
        }

    }

    internal static class Extension {

        internal static string Combine(this IEnumerable<string> items, bool surroundDoubleQuatation = true) {
            StringBuilder sb = new StringBuilder();
            foreach (string item in items) {
                sb.AppendFormat(
                    "{1}{0}",
                    surroundDoubleQuatation ? item.Quot() : item,
                    sb.Length > 0 ? " " : string.Empty
                );
            }
            return sb.ToString();
        }

        internal static string Quot(this string original) {
            return string.Format("{1}{0}{1}", original, "\"");
        }

    }

}
