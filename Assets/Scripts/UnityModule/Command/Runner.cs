using System.Collections.Generic;
using System.Text;
using UniRx;

namespace UnityModule.Command {

    public abstract class Runner<TResult> where TResult : class {

        internal static TResult Run(string command, string subCommand, List<string> argumentMap = null) {
            if (typeof(TResult).IsGenericType && typeof(IObservable<>).IsAssignableFrom(typeof(TResult).GetGenericTypeDefinition())) {
                return RunCommandAsync(command, subCommand, argumentMap) as TResult;
            }
            return RunCommand(command, subCommand, argumentMap) as TResult;
        }

        private static IObservable<string> RunCommandAsync(string command, string subCommand, List<string> argumentMap = null) {
            return Observable
                .Create<string>(
                    (observer) => {
                        try {
                            observer.OnNext(RunCommand(command, subCommand, argumentMap));
                            observer.OnCompleted();
                        } catch (System.Exception e) {
                            observer.OnError(e);
                        }
                        return null;
                    }
                );
        }

        private static string RunCommand(string command, string subCommand, List<string> argumentMap = null) {
            string output;
            System.Diagnostics.Process process = CreateProcess(command, subCommand, argumentMap);
            process.Start();
            process.WaitForExit();
            if (process.ExitCode == 0) {
                output = process.StandardOutput.ReadToEnd();
                process.Close();
            } else {
                process.Close();
                throw new System.InvalidOperationException(process.StandardError.ReadToEnd());
            }
            return output;
        }

        private static System.Diagnostics.Process CreateProcess(string command, string subCommand, List<string> argumentMap = null) {
            return new System.Diagnostics.Process {
                StartInfo = {
                    FileName = command,
                    Arguments = string.Format("{0}{1}", subCommand, CreateArgument(argumentMap)),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
            };
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
