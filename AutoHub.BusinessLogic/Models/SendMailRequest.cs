namespace AutoHub.BusinessLogic.Models;

public record SendMailRequest(string ToEmail, string Subject, string Body);
