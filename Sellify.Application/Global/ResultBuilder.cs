namespace Sellify.Application.Global
{
    #region Generic Version
    public sealed class Result<T>
    {
        private T? resultValue = default;
        private int resultStatusCode;
        private Dictionary<string, HashSet<string>>? resultErrorsKeyValues = null;
        private Dictionary<string, string>? resultErrorsKeyValue = null;
        private Result() { }
        public sealed class ResultBuilder<T>
        {
            private T? value = default;
            private int statusCode;
            private Dictionary<string, HashSet<string>>? errorsKeyValues = null;
            private Dictionary<string, string>? errorsKeyValue = null;
            private ResultBuilder() { }

            public ResultBuilder<T> SetValue(T value)
            {
                this.value = value;
                return this;
            }
            public ResultBuilder<T> SetStatusCode(int statusCode)
            {
                this.statusCode = statusCode;
                return this;
            }
            public ResultBuilder<T> HasManyErrorsPerKey()
            {
                errorsKeyValues = new Dictionary<string, HashSet<string>>();
                return this;
            }
            public ResultBuilder<T> HasOneErrorPerKey()
            {
                errorsKeyValue = new Dictionary<string, string>();
                return this;
            }
            public ResultBuilder<T> SetOneErrorPerKeyValue(string errorKey, string errorValue)
            {
                if (errorsKeyValue is null)
                    throw new InvalidOperationException();

                errorsKeyValue.Add(errorKey, errorValue);

                return this;
            }
            public ResultBuilder<T> SetErrorsPerKeyValue(string errorKey, HashSet<string> errorValue)
            {
                if (errorsKeyValues is null)
                    throw new InvalidOperationException();

                errorsKeyValues.Add(errorKey, errorValue);

                return this;
            }

            public Result<T> Build()
            {
                if (errorsKeyValue is not null)
                    return new Result<T>() { resultValue = value, resultErrorsKeyValue = errorsKeyValue, resultStatusCode = statusCode };

                return new Result<T>() { resultValue = value, resultErrorsKeyValues = errorsKeyValues, resultStatusCode = statusCode };
            }

        }
    }
    #endregion

    #region Non-Generic Version (No Value Here)
    public sealed class Result
    {
        private int resultStatusCode;
        private Dictionary<string, HashSet<string>>? resultErrorsKeyValues = null;
        private Dictionary<string, string>? resultErrorsKeyValue = null;
        private Result() { }
        public sealed class ResultBuilder
        {
            private int statusCode;
            private Dictionary<string, HashSet<string>>? errorsKeyValues = null;
            private Dictionary<string, string>? errorsKeyValue = null;
            private ResultBuilder() { }

            public ResultBuilder SetStatusCode(int statusCode)
            {
                this.statusCode = statusCode;
                return this;
            }
            public ResultBuilder HasManyErrorsPerKey()
            {
                errorsKeyValues = new Dictionary<string, HashSet<string>>();
                return this;
            }
            public ResultBuilder HasOneErrorPerKey()
            {
                errorsKeyValue = new Dictionary<string, string>();
                return this;
            }
            public ResultBuilder SetOneErrorPerKeyValue(string errorKey, string errorValue)
            {
                if (errorsKeyValue is null)
                    throw new InvalidOperationException();

                errorsKeyValue.Add(errorKey, errorValue);

                return this;
            }
            public ResultBuilder SetErrorsPerKeyValue(string errorKey, HashSet<string> errorValue)
            {
                if (errorsKeyValues is null)
                    throw new InvalidOperationException();

                errorsKeyValues.Add(errorKey, errorValue);

                return this;
            }

            public Result Build()
            {
                if (errorsKeyValue is not null)
                    return new Result() { resultErrorsKeyValue = errorsKeyValue, resultStatusCode = statusCode };

                return new Result() { resultErrorsKeyValues = errorsKeyValues, resultStatusCode = statusCode };
            }

        }
    } 
    #endregion
}
