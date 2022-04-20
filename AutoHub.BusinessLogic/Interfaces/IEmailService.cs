using AutoHub.BusinessLogic.Services;
using AutoHub.Domain.Entities;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IEmailService
{
    Task SendEmail(SendMailRequest mailRequest);
}
