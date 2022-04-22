using AutoHub.BusinessLogic.Models;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IEmailService
{
    Task SendEmail(SendMailRequest mailRequest);
}