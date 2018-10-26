using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    internal interface IAsyncInitialization
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }
    }
}