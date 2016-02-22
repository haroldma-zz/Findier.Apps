using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Findier.Core.Extensions
{
    public static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable DontMarshall(this Task task) => task.ConfigureAwait(false);

        public static ConfiguredTaskAwaitable<TResult> DontMarshall<TResult>(this Task<TResult> task) => task.ConfigureAwait(false);
    }
}