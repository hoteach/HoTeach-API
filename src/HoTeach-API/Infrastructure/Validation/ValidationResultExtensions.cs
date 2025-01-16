using HoTeach.API.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace HoTeach.API.Infrastructure.Validation
{
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Converts a FluentValidation ValidationResult into a Result object.
        /// </summary>
        /// <param name="validationResult">The ValidationResult instance.</param>
        /// <returns>A Result object representing the validation outcome.</returns>
        public static Result ToResult(this ValidationResult validationResult)
        {
            return validationResult.IsValid
                ? Result.Success
                : Result.Failure(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        /// <summary>
        /// Converts a FluentValidation ValidationResult into a Result<T> object.
        /// </summary>
        /// <typeparam name="T">The type of the data in the Result.</typeparam>
        /// <param name="validationResult">The ValidationResult instance.</param>
        /// <param name="data">The data to include in the Result if validation succeeds.</param>
        /// <returns>A Result<T> object representing the validation outcome.</returns>
        public static Result<T> ToResult<T>(this ValidationResult validationResult, T data)
        {
            return validationResult.IsValid
                ? Result<T>.SuccessWith(data)
                : Result<T>.Failure(validationResult.Errors.Select(error => error.ErrorMessage));
        }
    }
}