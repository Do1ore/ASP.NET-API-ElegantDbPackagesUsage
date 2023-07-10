namespace Api.DTOs;

public record class ErrorModel(int StatusCode, string ErrorMessage);