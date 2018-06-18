using System.Threading.Tasks;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class AsyncronousExtensions
    {
        public static TResult Await<TResult>(this Task<TResult> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}