using System.ComponentModel.DataAnnotations;

namespace Felipe.CleanArchitecture.Application.Models.Requests;

public record RegisterTruckRequest
(
    [Required, StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
    string LicensePlate,

    [Required, StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres.")]
    string Model
);
