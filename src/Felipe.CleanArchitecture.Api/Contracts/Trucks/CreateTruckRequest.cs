using System.ComponentModel.DataAnnotations;

namespace Felipe.CleanArchitecture.Api.Contracts.Trucks;

public record CreateTruckRequest
(
    [Required, StringLength(50, ErrorMessage = "A placa deve ter no máximo 50 caracteres.")]
    string LicensePlate,

    [Required, StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres.")]
    string Model
);
