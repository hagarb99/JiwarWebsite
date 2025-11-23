namespace Jiwar.Helpers
{
    public class ResultViewModel<T> 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResultViewModel<T> Ok(string msg, T Data) => new ResultViewModel<T> { Success = true, Message = msg, Data =Data};
        public static ResultViewModel<T> Fail(string msg) => new ResultViewModel<T> { Success = false, Message = msg ,Data = default };
    }
}
