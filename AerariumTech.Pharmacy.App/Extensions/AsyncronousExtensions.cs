using System.Threading.Tasks;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class AsyncronousExtensions
    {
        /// <summary>
        /// It might be useful inside a syncronous method,
        /// but it'll block the thread.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TResult Await<TResult>(this Task<TResult> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}