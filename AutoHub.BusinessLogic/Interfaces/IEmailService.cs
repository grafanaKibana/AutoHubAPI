using AutoHub.BusinessLogic.Services;
using AutoHub.Domain.Entities;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IEmailService
{
    Task SendEmail(SendMailRequest mailRequest);
}
