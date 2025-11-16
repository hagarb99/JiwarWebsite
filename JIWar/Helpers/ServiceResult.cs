namespace Jiwar.Helpers
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ServiceResult Ok(string msg) => new ServiceResult { Success = true, Message = msg };
        public static ServiceResult Fail(string msg) => new ServiceResult { Success = false, Message = msg };
    }
}
