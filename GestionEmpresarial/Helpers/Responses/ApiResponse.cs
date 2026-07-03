namespace GestionEmpresarial.Helpers.Responses
{
    public class ApiResponse
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public object? Data { get; init; }

        public static ApiResponse Ok(string message = "", object? data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse Fail(string message)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message
            };
        }

    }
}
