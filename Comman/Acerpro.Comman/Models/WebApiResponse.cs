namespace Acerpro.Common.Model
{
    public class WebApiResponse<T>
    {
        public WebApiResponse()
        {

        }
        public void AddValidationError(string Key, string Value)
        {
            ValidationErrors.Add(new ValidationError { Key = Key, Value = Value });
        }

        public void SetMessage(string Message)
        {
            if (ValidationErrors.Any()) throw new Exception("Set message cant be use with validations errors");

            IsSuccess = false;
            this.ResultMessage = Message;
        }

        public void SetErrorMessage(string Message)
        {
            IsSuccess = false;
            this.ResultMessage = Message;
        }

        public WebApiResponse(bool isSuccess, string resultMessage)
        {
            IsSuccess = isSuccess;
            ResultMessage = resultMessage;
        }

        public WebApiResponse(bool isSuccess, string resultMessage, T resultData)
            : this(isSuccess, resultMessage)
        {
            ResultData = resultData;
        }
        public List<ValidationError> ValidationErrors { get; set; } = new();
        public bool IsSuccess { get; set; }
        public string ResultMessage { get; set; }
        public T ResultData { get; set; }
    }
}
