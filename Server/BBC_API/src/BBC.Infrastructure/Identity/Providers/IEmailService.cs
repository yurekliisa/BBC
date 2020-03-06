using BBC.Core.Dependency;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace BBC.Infrastructure.Identity.Providers
{
    public interface IEmailService: ITransientDI
    {
        Task SendAsync(string EmailDisplayName, string Subject, string Body, string From, string To);

        Task SendEmailConfirmationAsync(string Email, string CallbackUrl);

        Task SendPasswordResetAsync(string Email, string CallbackUrl);

        Task SendException(Exception ex);

        Task SendSqlException(SqlException ex);
    }
}
