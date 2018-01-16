using FluentValidation;
using System;
using System.Threading.Tasks;

namespace Validator
{
    public static class Validator<T, U> where T : AbstractValidator<U>, new()
    {
        public static async Task ValidateAndThrowAsync(U request)
        {
            var v = new T();
            await v.ValidateAndThrowAsync(request);
        }
    }
}
