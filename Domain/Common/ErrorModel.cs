namespace Domain.Entities;

public record class ErrorModel(int StatusCode, string ErrorMessage);